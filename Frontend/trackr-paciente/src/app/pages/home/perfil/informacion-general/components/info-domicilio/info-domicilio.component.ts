import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ColoniaSelectorDto } from '@dtos/catalogo/colonia-selector-dto';
import { EstadoSelectorDto } from '@dtos/catalogo/estado-selector-dto';
import { GeneroSelectorDto } from '@dtos/catalogo/genero-selector-dto';
import { LocalidadSelectorDto } from '@dtos/catalogo/localidad-selector-dto';
import { municipioSelectorDto } from '@dtos/catalogo/municipio-selector-dto';
import { PaisSelectorDto } from '@dtos/catalogo/pais-selector-dto';
import { InformacionGeneralDto } from '@dtos/perfil/informacion-general-dto';
import { ExpedientePadecimientoDto } from '@dtos/seguridad/expediente-padecimiento-dto';
import { ExpedientePadecimientoSelectorDTO } from '@dtos/seguridad/expediente-padecimiento-selector-dto';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import { ColoniaService } from '@http/catalogo/colonia.service';
import { EstadoService } from '@http/catalogo/estado.service';
import { GeneroService } from '@http/catalogo/genero.service';
import { LocalidadService } from '@http/catalogo/localidad.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { EntidadEstructuraService } from '@http/gestion-entidad/entidad-estructura.service';
import { ConfirmacionCorreoService } from '@http/seguridad/confirmacion-correo.service';
import { MisDoctoresService } from '@http/gestion-expediente/mis-doctores.service';
import { AlertController, IonicModule, ModalController } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { addCircleOutline, closeCircleOutline, chevronBack, chevronDown, chevronUp } from 'ionicons/icons';
import { Observable, forkJoin, switchMap, tap, lastValueFrom } from 'rxjs';
import { UsuarioDoctoresDto } from 'src/app/shared/Dtos/usuario-doctores-dto';
import * as Utileria from '@utils/utileria';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { RouterModule } from '@angular/router';
import { CatalogoFormularioComponent } from '../info-general/catalogo-formulario/catalogo-formulario.component';

@Component({
  selector: 'app-info-domicilio',
  templateUrl: './info-domicilio.component.html',
  styleUrls: ['./info-domicilio.component.scss'],
  standalone: true,
  imports : [
    CommonModule,
    IonicModule,
    FormsModule,
    RouterModule
  ]
})
export class InfoDomicilioComponent  implements OnInit {

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
  protected nombrePais : string;
  protected nombreEstado : string ;
  protected nombreMunicipio : string;
  protected nombreLocalidad : string;
  protected nombreColonia : string;
  protected modalPaisAbierto : boolean = false;
  protected modalEstadoAbierto : boolean = false;
  protected modalMunicipioAbierto : boolean = false;
  protected modalLocalidadAbierto : boolean = false;
  protected modalColoniaAbierto : boolean = false;

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
    private alertController: AlertController,
    private modalController: ModalController
  ) {
    addIcons({addCircleOutline, closeCircleOutline, chevronBack, chevronDown, chevronUp, 'informacion': 'assets/img/svg/info.svg'})
    }

  ngOnInit() {
    this.consultarPaises();
    this.obtenerUsuario();
  }

  protected async onChangePais() {
    this.esPaisExtranjero = this.infoUsuario.idPais !== this.idPaisMexico;
    this.infoUsuario.idEstado = 0;
    
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
    console.log(this.infoUsuario);
    this.actualizarInformacionUsuario(this.infoUsuario);
    this.submiting = false;
  }

private async obtenerUsuario(){
  this.informacionUsuario$ = this.usuarioService.consultarInformacionGeneral().pipe(
    tap(
      async (infoUsuario) => {

        this.infoUsuario = infoUsuario;
        await Promise.all([
          this.consultarEstados(infoUsuario.idPais),
          this.consultarMunicipios(infoUsuario.idEstado),
          this.consultarLocalidades(infoUsuario.idEstado),
          this.consultarColonias(infoUsuario.codigoPostal)
        ]);
     
        this.setNombrePais();
        this.setNombreEstado();
        this.setNombreMunicipio();
        this.setNombreLocalidad();
        this.setNombreColonia();
        
      }));


}

private setNombrePais() {
  const pais = this.paisList.find(pais => pais.idPais === this.infoUsuario.idPais);
  this.nombrePais = pais ? pais.nombre : '';
}

private setNombreEstado() {
  const estado = this.estadoList.find(estado => estado.idEstado === this.infoUsuario.idEstado);
  this.nombreEstado = estado ? estado.nombre : '';
}

private setNombreMunicipio() {
  const municipio = this.municipioList.find(municipio => municipio.idMunicipio === this.infoUsuario.idMunicipio);
  this.nombreMunicipio = municipio ? municipio.nombre : '';
}

private setNombreLocalidad() {
  const localidad = this.localidadList.find(localidad => localidad.idLocalidad === this.infoUsuario.idLocalidad);
  this.nombreLocalidad = localidad ? localidad.nombre : '';
}

private setNombreColonia() {
  const colonia = this.coloniaList.find(colonia => colonia.idColonia === this.infoUsuario.idColonia);
  this.nombreColonia = colonia ? colonia.nombre : '';
}
  private async consultarPaises(): Promise<void> {
    const paises = await this.paisService
      .consultarTodosParaSelector()
      .toPromise();

    this.paisList = paises ?? [];
  }

  protected async onChangeEstado() {
    this.infoUsuario.idMunicipio = 0;
    this.infoUsuario.idLocalidad = 0;

    await Promise.all([
      this.consultarMunicipios(this.infoUsuario.idEstado),
      this.consultarLocalidades(this.infoUsuario.idEstado),

    ]);
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

  private async consultarEstados(idPais: number): Promise<void> {
    const estados = idPais > 0
      ? await this.estadoService.consultarPorPaisParaSelector(idPais).toPromise()
      : [];

    this.estadoList = estados ?? [];
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
      this.consultarLocalidades(codigoPostal.idEstado),
      this.consultarEstados(codigoPostal.idPais)
    ]);
    this.infoUsuario.idMunicipio = codigoPostal.idMunicipio;

      const estado = this.estadoList.find(estado => estado.idEstado === codigoPostal.idEstado);
    if (estado) {
      this.nombreEstado = estado.nombre;
    }

    const municipio = this.municipioList.find(municipio => municipio.idMunicipio === codigoPostal.idMunicipio);
    if (municipio) {
      this.nombreMunicipio = municipio.nombre;
    }
    this.nombreLocalidad = '';

    await this.consultarColonias(codigoPostalValue);
    const colonia = this.coloniaList
      .find((colonia) => Utileria.equalsNormalized(colonia.nombre, codigoPostal.colonia)) || this.coloniaList[0];


    if (colonia) {
      this.infoUsuario.idColonia = colonia.idColonia;
      this.nombreColonia = colonia.nombre;
    }
  }


  private async consultarColonias(codigoPostal: string): Promise<void> {
    const colonias = codigoPostal && codigoPostal.length === 5
      ? await this.coloniaService.consultarPorCodigoParaSelector(codigoPostal).toPromise()
      : [];

    this.coloniaList = colonias ?? [];
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

  private async presentAlert() {
    const alert = await this.alertController.create({
      header: 'Información actualizada',
      message: 'La información se actualizó correctamente',
      buttons: ['OK'],
      cssClass: 'custom-alert'
    });

    await alert.present();
  }

  private actualizarInformacionUsuario(informacion: InformacionGeneralDto){
    this.usuarioService.actualizarInformacionGeneral(informacion).subscribe({
      next: ()=> {}
    });
  }

  async openEstadoModal() {
    if(this.modalEstadoAbierto)
       return;
    
    this.modalEstadoAbierto = true;
    const modal = await this.modalController.create({
      component: CatalogoFormularioComponent,
      breakpoints : [0, 0.25, 0.5, 0.75],
      initialBreakpoint: 0.25,
      cssClass: 'custom-sheet-modal',
      componentProps: {
        catalogoList: this.estadoList.map((estado) => ({ id: estado.idEstado, descripcion: estado.nombre })),
      }
    });

    await modal.present();
    const { data } = await modal.onWillDismiss();
    if (data) {
      this.infoUsuario.idEstado = data.id;
      this.nombreEstado = data.descripcion;

      this.onChangeEstado();
    
    }
    
    this.modalEstadoAbierto = false;
  }
  async openMunicipioModal() {
    if(this.modalMunicipioAbierto)
       return;
      
    this.modalMunicipioAbierto = true;
    const modal = await this.modalController.create({
      component: CatalogoFormularioComponent,
      breakpoints : [0, 0.25, 0.5, 0.75, 1],
      initialBreakpoint: 0.25,
      cssClass: 'custom-sheet-modal',
      componentProps: {
        catalogoList: this.municipioList.map((municipio) => ({ id: municipio.idMunicipio, descripcion: municipio.nombre })),
      }
    });

    await modal.present();
    const { data } = await modal.onWillDismiss();
    if (data) {
      this.infoUsuario.idMunicipio = data.id;
      this.nombreMunicipio = data.descripcion;
    }
    
    this.modalMunicipioAbierto = false;
  }
  async openLocalidadModal() {
    if(this.modalLocalidadAbierto)
       return;
      
    this.modalLocalidadAbierto = true;
    const modal = await this.modalController.create({
      component: CatalogoFormularioComponent,
      breakpoints : [0, 0.25, 0.5, 0.75, 1],
      initialBreakpoint: 0.25,
      cssClass: 'custom-sheet-modal',
      componentProps: {
        catalogoList: this.localidadList.map((localidad) => ({ id: localidad.idLocalidad, descripcion: localidad.nombre })),
      }
    });

    await modal.present();
    const { data } = await modal.onWillDismiss();
    if (data) {
      this.infoUsuario.idLocalidad = data.id;
      this.nombreLocalidad = data.descripcion;
    }
    
    this.modalLocalidadAbierto = false;
  }
  async openColoniaModal() {
    if(this.modalColoniaAbierto)
       return;
      
    this.modalColoniaAbierto = true;
    const modal = await this.modalController.create({
      component: CatalogoFormularioComponent,
      breakpoints : [0, 0.25, 0.5, 0.75, 1],
      initialBreakpoint: 0.25,
      cssClass: 'custom-sheet-modal',
      componentProps: {
        catalogoList: this.coloniaList.map((colonia) => ({ id: colonia.idColonia, descripcion: colonia.nombre })),
      }
    });

    await modal.present();
    const { data } = await modal.onWillDismiss();
    if (data) {
      this.infoUsuario.idLocalidad = data.id;
      this.nombreLocalidad = data.descripcion;
    }
    
    this.modalColoniaAbierto = false;
  }
  async openPaisModal() {
    if(this.modalPaisAbierto)
       return;
      
    this.modalPaisAbierto = true;
    const modal = await this.modalController.create({
      component: CatalogoFormularioComponent,
      breakpoints : [0, 0.25, 0.5, 0.75, 1],
      initialBreakpoint: 1,
      cssClass: 'custom-sheet-modal',
      componentProps: {
        catalogoList: this.paisList.map((pais) => ({ id: pais.idPais, descripcion: pais.nombre })),
      }
    });

    await modal.present();
    const { data } = await modal.onWillDismiss();
    if (data) {
      this.infoUsuario.idPais  = data.id;
      this.nombrePais= data.descripcion;
    }
    await this.consultarEstados(this.infoUsuario.idPais);

    this.modalPaisAbierto = false;
  }

}
