import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { HeaderComponent } from '../../layout/header/header.component';
import { InformacionGeneralDto } from 'src/app/shared/dtos/perfil/informacion-general-dto';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { Observable, lastValueFrom, of, tap } from 'rxjs';
import { PaisSelectorDto } from 'src/app/shared/dtos/catalogo/pais-selector-dto';
import { EstadoSelectorDto } from 'src/app/shared/dtos/catalogo/estado-selector-dto';
import { municipioSelectorDto } from 'src/app/shared/dtos/catalogo/municipio-selector-dto';
import { LocalidadSelectorDto } from 'src/app/shared/dtos/catalogo/localidad-selector-dto';
import { ColoniaSelectorDto } from 'src/app/shared/dtos/catalogo/colonia-selector-dto';
import { PaisService } from '@http/catalogo/pais.service';
import { EstadoService } from '@http/catalogo/estado.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { LocalidadService } from '@http/catalogo/localidad.service';
import { ColoniaService } from '@http/catalogo/colonia.service';
import { ExpedientePadecimientoDto} from 'src/app/shared/dtos/seguridad/expediente-padecimiento-dto';
import {EntidadEstructuraService} from '@http/gestion-entidad/entidad-estructura.service';
import { GeneroSelectorDto } from 'src/app/shared/dtos/catalogo/genero-selector-dto';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import * as Utileria from '@utils/utileria';
import { ExpedientePadecimientoSelectorDTO } from 'src/app/shared/dtos/seguridad/expediente-padecimiento-selector-dto';


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
  protected edadUsuario: string;
  public btnSubmit = true;
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
    private entidadEstructuraService: EntidadEstructuraService
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
          this.calcularEdad();
        }
      )
    );

  }

  private actualizarInformacionUsuario(informacion: InformacionGeneralDto){
    this.usuarioService.actualizarInformacionGeneral(informacion).subscribe({
      next: ()=> {console.log('información actualizada')},
    });
  }
  
  protected calcularEdad(){
    let fechaNacimiento = new Date(this.infoUsuario.fechaNacimiento);
    let edadObject = Utileria.diferenciaFechas(fechaNacimiento, new Date());
    let edadString = edadObject.years + ' años, ' + edadObject.months + ' meses, ' + edadObject.days + ' días';
    this.edadUsuario = edadString;
  }

  protected async enviarFormulario(formulario: NgForm){
    this.btnSubmit = true;

    if(formulario.invalid){
      Utileria.validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      return;
    }
    this.actualizarInformacionUsuario(this.infoUsuario);
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
    console.log(padecimiento)
    this.infoUsuario.padecimientos = [...this.infoUsuario.padecimientos, padecimiento ];

  }


}
