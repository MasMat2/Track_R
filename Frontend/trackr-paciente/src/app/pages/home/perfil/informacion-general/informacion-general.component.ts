import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormsModule, NgForm, ReactiveFormsModule, ValidatorFn } from '@angular/forms';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import { ColoniaService } from '@http/catalogo/colonia.service';
import { EstadoService } from '@http/catalogo/estado.service';
import { LocalidadService } from '@http/catalogo/localidad.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { EntidadEstructuraService } from '@http/gestion-entidad/entidad-estructura.service';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { AlertController, IonicModule } from '@ionic/angular';
import * as Utileria from '@utils/utileria';
import { Observable, lastValueFrom, of, tap } from 'rxjs';
import { ColoniaSelectorDto } from 'src/app/shared/dtos/catalogo/colonia-selector-dto';
import { EstadoSelectorDto } from 'src/app/shared/dtos/catalogo/estado-selector-dto';
import { LocalidadSelectorDto } from 'src/app/shared/dtos/catalogo/localidad-selector-dto';
import { municipioSelectorDto } from 'src/app/shared/dtos/catalogo/municipio-selector-dto';
import { PaisSelectorDto } from 'src/app/shared/dtos/catalogo/pais-selector-dto';
import { InformacionGeneralDto } from 'src/app/shared/dtos/perfil/informacion-general-dto';
import { ExpedientePadecimientoDto } from 'src/app/shared/dtos/seguridad/expediente-padecimiento-dto';
import { ExpedientePadecimientoSelectorDTO } from 'src/app/shared/dtos/seguridad/expediente-padecimiento-selector-dto';
import { HeaderComponent } from '../../layout/header/header.component';
import { MisDoctoresService } from '@http/seguridad/mis-doctores.service';
import { UsuarioDoctoresSelectorDto } from 'src/app/shared/Dtos/usuario-doctores-selector-dto';
import { UsuarioDoctoresDto } from 'src/app/shared/Dtos/usuario-doctores-dto';
import { GeneroSelectorDto } from 'src/app/shared/Dtos/catalogo/genero-selector-dto';

@Component({
  selector: 'app-informacion-general',
  templateUrl: './informacion-general.component.html',
  styleUrls: ['./informacion-general.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    IonicModule,
    HeaderComponent,
  ]
})
export class InformacionGeneralComponent implements OnInit {

  protected informacionUsuario$: Observable<InformacionGeneralDto>;
  protected infoUsuario: InformacionGeneralDto;
  protected misDoctores: UsuarioDoctoresDto[];
  protected edadUsuario: string;
  public submiting = false;
  public esPaisExtranjero: boolean = false;
  public idPaisMexico: 1;
  protected nuevoPadecimiento: ExpedientePadecimientoDto = new ExpedientePadecimientoDto();
  

  public paisList: PaisSelectorDto[] = [];
  public estadoList: EstadoSelectorDto[] = [];
  public municipioList: municipioSelectorDto[] = [];
  public localidadList: LocalidadSelectorDto[] = [];
  public coloniaList: ColoniaSelectorDto[] = [];
  public generoList: GeneroSelectorDto[] = [];
  public padecimientoList: ExpedientePadecimientoSelectorDTO[] = [];

  constructor(
    private usuarioService: UsuarioService,
    private paisService: PaisService,
    private estadoService: EstadoService,
    private municipioService: MunicipioService,
    private localidadService: LocalidadService,
    private coloniaService: ColoniaService,
    private codigoPostalService: CodigoPostalService,
    private entidadEstructuraService: EntidadEstructuraService,
    private doctoresService: MisDoctoresService,
    private alertController: AlertController
  ) {  }

  ngOnInit(){
    this.consultarGeneros();
    this.consultarPadecimientos();
    this.obtenerUsuario();
    this.consultarPaises();
  }

  private async consultarPaises(): Promise<void> {
    const paises = await this.paisService
      .consultarTodosParaSelector()
      .toPromise();

    this.paisList = paises ?? [];
  }

  private async consultarEstados(idPais: number): Promise<void> {
    const estados = idPais > 0
      ? await this.estadoService.consultarPorPaisParaSelector(idPais).toPromise()
      : [];

    this.estadoList = estados ?? [];
  }

  private async consultarMunicipios(idEstado: number): Promise<void> {
    const municipios = idEstado > 0
      ? await this.municipioService.consultarPorEstadoParaSelector(idEstado).toPromise()
      : [];

    this.municipioList = municipios ?? [];
  }

  private async consultarLocalidades(idEstado: number): Promise<void> {
    const localidades = idEstado > 0
      ? await this.localidadService.consultarPorEstado(idEstado).toPromise()
      : [];

    this.localidadList = localidades ?? [];
  }

  private async consultarColonias(codigoPostal: string): Promise<void> {
    const colonias = codigoPostal && codigoPostal.length === 5
      ? await this.coloniaService.consultarPorCodigoParaSelector(codigoPostal).toPromise()
      : [];

    this.coloniaList = colonias ?? [];
  }

  protected async onChangePais() {
    this.esPaisExtranjero = this.infoUsuario.idPais !== this.idPaisMexico;
    this.infoUsuario.idEstado = 0;
    await this.consultarEstados(this.infoUsuario.idPais);

    this.onChangeEstado();
  }

  protected async onChangeEstado() {
    this.infoUsuario.idMunicipio = 0;
    this.infoUsuario.idLocalidad = 0;

    await Promise.all([
      this.consultarMunicipios(this.infoUsuario.idEstado),
      this.consultarLocalidades(this.infoUsuario.idEstado),

    ]);
  }

  protected async onChangeCodigoPostal() {
    this.infoUsuario.idColonia = 0;

    const codigoPostalValue: string = this.infoUsuario.codigoPostal;

    if (codigoPostalValue.length !== 5) {
      return;
    }

    await this.asignarValoresDeCodigoPostal(codigoPostalValue);

  }


  private async asignarValoresDeCodigoPostal(codigoPostalValue: string): Promise<void> {
    const codigoPostal = await this.codigoPostalService
      .consultarPorCodigoPostal(codigoPostalValue)
      .toPromise()
      .then((codigosPostales) => {
        return codigosPostales && codigosPostales.length > 0
          ? codigosPostales[0]
          : null;
      });

    if (!codigoPostal) {
      return;
    }

    this.infoUsuario.idEstado = codigoPostal.idEstado;

    await Promise.all([
      this.consultarMunicipios(codigoPostal.idEstado),
      this.consultarLocalidades(codigoPostal.idEstado)
    ]);
    this.infoUsuario.idMunicipio = codigoPostal.idMunicipio;
    this.infoUsuario.idLocalidad = 0

    await this.consultarColonias(codigoPostalValue);
    const colonia = this.coloniaList
      .find((colonia) => Utileria.equalsNormalized(colonia.nombre, codigoPostal.colonia));

    if (colonia) {
      this.infoUsuario.idColonia = colonia.idColonia;
    }
  }

  private async consultarPadecimientos(){
    return lastValueFrom(this.entidadEstructuraService.consultarPadecimientosParaSelector())
    .then((padecimientos: ExpedientePadecimientoSelectorDTO[]) => {
      this.padecimientoList = padecimientos;
    })
  }

  consultarDoctores() {
    this.doctoresService.consultarExpediente().subscribe((data => {
      this.misDoctores = data;
    }));
  }



  private consultarGeneros() {
    this.generoList = [
      {
        idGenero: 1,
        descripcion: "Hombre"
      },
      {
        idGenero: 2,
        descripcion: "Mujer"
      },
      {
        idGenero: 3,
        descripcion: "Otro"
      },
    ];

    return lastValueFrom(of(this.generoList));
  }

  private obtenerUsuario(){

    this.informacionUsuario$ = this.usuarioService.consultarInformacionGeneral().pipe(
      tap(
        (data) => {
          this.infoUsuario = data;

          this.consultarEstados(this.infoUsuario.idPais);
          this.consultarMunicipios(this.infoUsuario.idEstado);
          this.consultarLocalidades(this.infoUsuario.idEstado);
          this.consultarColonias(this.infoUsuario.codigoPostal);
          this.consultarDoctores();
          this.calcularEdad();
        }
      )
    );

  }

  private actualizarInformacionUsuario(informacion: InformacionGeneralDto){
    this.usuarioService.actualizarInformacionGeneral(informacion).subscribe({
      next: ()=> {}
    });
  }

  protected calcularEdad(){
    let fechaNacimiento = new Date(this.infoUsuario.fechaNacimiento);
    let edadObject = Utileria.diferenciaFechas(fechaNacimiento, new Date());
    let edadString = edadObject.years + ' años, ' + edadObject.months + ' meses, ' + edadObject.days + ' días';
    this.edadUsuario = edadString;
  }

  protected async enviarFormulario(formulario: NgForm){
    this.submiting = true;

    if(formulario.invalid){
      console.log('Formulario inválido');
      Utileria.validarCamposRequeridos(formulario);
      this.presentAlertError();
      this.submiting = false;
      return;
    }
    this.presentAlert();
    this.actualizarInformacionUsuario(this.infoUsuario);
    this.submiting = false;
  }

  protected eliminarPadecimiento(index: number){
    if(index < 0 || index >= this.infoUsuario.padecimientos.length){
      return;
    }
    this.infoUsuario.padecimientos.splice(index, 1);

    if(this.infoUsuario.padecimientos.length === 0){
      this.agregarPadecimiento();
    }

  }

  protected agregarPadecimiento(){
    const padecimiento = new ExpedientePadecimientoDto();
    padecimiento.idPadecimiento = 0;
    this.infoUsuario.padecimientos = [...this.infoUsuario.padecimientos, padecimiento ];

  }

  private async presentAlert() {
    const alert = await this.alertController.create({
      header: 'Información actualizada',
      message: 'La información se actualizó correctamente',
      buttons: ['OK'],
    });

    await alert.present();
  }

  private async presentAlertError() {
    const alert = await this.alertController.create({
      header: 'Campdos requeridos',
      message: 'Llene todos los campos requeridos',
      buttons: ['OK'],
    });

    await alert.present();
  }


}
