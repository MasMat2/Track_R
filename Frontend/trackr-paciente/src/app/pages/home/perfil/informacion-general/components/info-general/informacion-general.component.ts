import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormsModule, NgForm, ReactiveFormsModule, ValidatorFn } from '@angular/forms';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import { ColoniaService } from '@http/catalogo/colonia.service';
import { EstadoService } from '@http/catalogo/estado.service';
import { LocalidadService } from '@http/catalogo/localidad.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { EntidadEstructuraService } from '@http/gestion-entidad/entidad-estructura.service';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { AlertController, IonicModule, ModalController } from '@ionic/angular';
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
import { HeaderComponent } from '../../../../layout/header/header.component';
import { MisDoctoresService } from '@http/seguridad/mis-doctores.service';
import { UsuarioDoctoresSelectorDto } from 'src/app/shared/Dtos/usuario-doctores-selector-dto';
import { UsuarioDoctoresDto } from 'src/app/shared/Dtos/usuario-doctores-dto';
import { ConfirmacionCorreoService } from '@http/seguridad/confirmacion-correo.service';
import { ConfirmarCorreoDto } from '../../../../../../shared/Dtos/seguridad/confirmar-correo-dto';
import {GeneroService} from '@http/catalogo/genero.service'
import { addIcons } from 'ionicons';
import { addCircleOutline, closeCircleOutline, chevronBack, arrowDown, chevronDown, chevronUp } from 'ionicons/icons'
import { OnExit } from 'src/app/shared/guards/exit.guard';
import { RouterModule } from '@angular/router';
import { IonIcon } from '@ionic/angular/standalone';
import { GeneroSelectorDto } from '@dtos/catalogo/genero-selector-dto';
import { CatalogoFormularioComponent } from './catalogo-formulario/catalogo-formulario.component';

@Component({
  selector: 'app-informacion-general',
  templateUrl: './informacion-general.component.html',
  styleUrls: ['./informacion-general.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    IonicModule,
    HeaderComponent
  ]
})
export class InformacionGeneralComponent implements OnInit , OnExit {

  protected informacionUsuario$: Observable<InformacionGeneralDto>;
  protected infoUsuario: InformacionGeneralDto;
  protected misDoctores: UsuarioDoctoresDto[];
  protected edadUsuario: string;
  protected submiting = false;
  protected emailsubmiting = false;
  protected esPaisExtranjero: boolean = false;
  private idPaisMexico: 1;
  protected nuevoPadecimiento: ExpedientePadecimientoDto = new ExpedientePadecimientoDto();
  protected nuevoAntecedente: ExpedientePadecimientoDto = new ExpedientePadecimientoDto();
  protected nuevoDiagnostico: ExpedientePadecimientoDto = new ExpedientePadecimientoDto();
  protected nuevoAntecedenteInvalido = false;
  protected nuevoDiagnosticoInvalido = false;
  protected nombreGenero : string;
  protected modalGeneroAbierto : boolean = false;

  protected paisList: PaisSelectorDto[] = [];
  protected estadoList: EstadoSelectorDto[] = [];
  protected municipioList: municipioSelectorDto[] = [];
  protected localidadList: LocalidadSelectorDto[] = [];
  protected coloniaList: ColoniaSelectorDto[] = [];

  protected antecedenteList: ExpedientePadecimientoSelectorDTO[] = [];
  protected diagnosticoList: ExpedientePadecimientoSelectorDTO[] = [];
  protected antecedenteFiltradoList: ExpedientePadecimientoSelectorDTO[] = [];
  protected diagnosticoFiltradoList: ExpedientePadecimientoSelectorDTO[] = [];

  customAlertOptions = {
    cssClass: 'custom-select-alert',
    // header: '',
    // subHeader: '',
    // message: '',
    // translucent: true,
  };

  @ViewChild('formulario') formulario: NgForm;
  generoList: GeneroSelectorDto[];
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
    private alertController: AlertController,
    private confirmacionCorreoService: ConfirmacionCorreoService,
    private generoService: GeneroService,
    private modalController: ModalController
  ) {
    addIcons({addCircleOutline, closeCircleOutline, chevronBack, chevronDown, chevronUp, 'informacion': 'assets/img/svg/info.svg'})
    }

  async ngOnInit(){
   
    await this.consultarGeneros();
    this.obtenerUsuario();
    this.consultarPaises();
  }

  ionViewWillEnter(){
    this.consultarDoctores();
  }


  async openGeneroModal() {
    const modal = await this.modalController.create({
      component: CatalogoFormularioComponent,
      breakpoints : [0, 1],
      initialBreakpoint: 1,
      cssClass: 'custom-sheet-modal',
      componentProps: {
        generoList: this.generoList
      }
    });

    await modal.present();
    this.modalGeneroAbierto = true;
    const { data } = await modal.onWillDismiss();
    if (data) {
      this.infoUsuario.idGenero = data.id;
      this.nombreGenero = data.descripcion;
    }
    
    this.modalGeneroAbierto = false;
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

  private async consultarAntecedentes(){
    return lastValueFrom(this.entidadEstructuraService.consultarAntecedentesParaSelector())
    .then((antecedentes: ExpedientePadecimientoSelectorDTO[]) => {
     this.antecedenteList = antecedentes;
     this.antecedenteFiltradoList = antecedentes.filter(ante => !this.infoUsuario.padecimientos.some(pad => pad.idPadecimiento === ante.idPadecimiento));
    })
  }

  private async consultarDiagnosticos(){
    return lastValueFrom(this.entidadEstructuraService.consultarDiagnosticosParaSelector())
    .then((diagnosticos: ExpedientePadecimientoSelectorDTO[]) => {
      this.diagnosticoList = diagnosticos;
      this.diagnosticoFiltradoList = diagnosticos.filter(diag => !this.infoUsuario.padecimientos.some(pad => pad.idPadecimiento === diag.idPadecimiento));

    })
  }

  consultarDoctores() {
    this.doctoresService.consultarExpediente().subscribe((data => {
      this.misDoctores = data;
    }));
  }

  private async consultarGeneros() {
    this.generoService.consultarGeneros().subscribe(
      generos => {
        this.generoList = generos;
      }
    );
  }

  private obtenerUsuario(){
    this.informacionUsuario$ = this.usuarioService.consultarInformacionGeneral().pipe(
      tap(
        (data) => {
          this.infoUsuario = data;
          const genero = this.generoList.find(genero => genero.idGenero === this.infoUsuario.idGenero);
          this.nombreGenero = genero ? genero.descripcion : '';
          this.consultarEstados(this.infoUsuario.idPais);
          this.consultarMunicipios(this.infoUsuario.idEstado);
          this.consultarLocalidades(this.infoUsuario.idEstado);
          this.consultarColonias(this.infoUsuario.codigoPostal);
          this.consultarAntecedentes();
          this.consultarDiagnosticos();
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
    let edadString = edadObject.years + ' años ';
    this.edadUsuario = edadString;
  }

  protected async enviarFormulario(formulario: NgForm){
    this.submiting = true;

    if(formulario.invalid){
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

  protected agregarAntecedente(){
    if(this.nuevoAntecedente.idPadecimiento == null || this.nuevoAntecedente.idUsuarioDoctor == null){
      this.nuevoAntecedenteInvalido = true;
      return;
    }
    const padecimiento = new ExpedientePadecimientoDto();
    padecimiento.idPadecimiento = this.nuevoAntecedente.idPadecimiento;
    padecimiento.idUsuarioDoctor = this.nuevoAntecedente.idUsuarioDoctor;
    padecimiento.fechaDiagnostico = this.nuevoAntecedente.fechaDiagnostico;
    padecimiento.esAntecedente = true;
    this.infoUsuario.padecimientos = [...this.infoUsuario.padecimientos, padecimiento ];
    this.nuevoAntecedenteInvalido = false;
    this.nuevoAntecedente = new ExpedientePadecimientoDto();
    this.consultarAntecedentes();
  }

  protected agregarDiagnostico(){
    if(this.nuevoDiagnostico.idPadecimiento == null || this.nuevoDiagnostico.idUsuarioDoctor == null){
      this.nuevoDiagnosticoInvalido = true;
      return;
    }
    const padecimiento = new ExpedientePadecimientoDto();
    padecimiento.idPadecimiento = this.nuevoDiagnostico.idPadecimiento;
    padecimiento.idUsuarioDoctor = this.nuevoDiagnostico.idUsuarioDoctor;
    padecimiento.fechaDiagnostico = this.nuevoDiagnostico.fechaDiagnostico;
    padecimiento.esAntecedente = false;
    this.infoUsuario.padecimientos = [...this.infoUsuario.padecimientos, padecimiento ];
    this.nuevoDiagnosticoInvalido = false;
    this.nuevoDiagnostico = new ExpedientePadecimientoDto();
    this.consultarDiagnosticos();
  }

  protected reenviarConfirmacionCorreo(correoUsuario: string){
    this.emailsubmiting = true;
    const confirmarCorreo: ConfirmarCorreoDto = { correo: correoUsuario, token: ""};
    this.confirmacionCorreoService.enviarCorreoConfirmacion(confirmarCorreo).subscribe({
      next: () => {
      }
    });
  }
  private async presentAlertSalir(): Promise<boolean> {
    const alert = await this.alertController.create({
      header: '¿Está seguro que desea salir?',
      message: 'Si sale, perderá los cambios que no haya guardado',
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
          cssClass: 'secondary',
          handler: () => {
            return false;
          }
        },
        {
          text: 'Salir',
          handler: () => {
            return true;
          }
        }
      ],
      cssClass: 'custom-alert'
    });
    alert.present();
    const data = await alert.onDidDismiss();
    return data.role === 'cancel' ? false : true;
  }


  private async presentAlert() {
    const alert = await this.alertController.create({
      header: 'Información actualizada',
      message: 'La información se actualizó correctamente',
      buttons: ['OK'],
      cssClass: 'custom-alert'
    });

    await alert.present();
  }

  private async presentAlertError() {
    const alert = await this.alertController.create({
      header: 'Campos requeridos',
      message: 'Llene todos los campos requeridos',
      buttons: ['OK'],
      cssClass: 'custom-alert'
    });

    await alert.present();
  }


  onExit(){
    if(this.formulario.dirty){
      const rta  = this.presentAlertSalir();
      return rta;
    }

    return true;
  };


}
