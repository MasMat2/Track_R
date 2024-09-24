import { SharedModule } from '@sharedComponents/shared.module';
import { EntidadEstructuraTablaValorService } from './../../../../shared/http/gestion-expediente/entidad-estructura-tabla-valor.service';
import { CommonModule } from '@angular/common';
import { Component, OnInit, } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { TablaValorMuestraDTO } from '@dtos/gestion-entidades/entidad-tabla-registro-dto';
import { SeccionCampoService } from '@http/gestion-entidad/seccion-campo.service';
import { IonicModule } from '@ionic/angular';
import { SeccionCampo } from '@models/gestion-expediente/seccion-campo';
import { CampoExpedienteModule } from '@sharedComponents/campo-expediente/campo-expediente.module';
import { lastValueFrom } from 'rxjs/internal/lastValueFrom';
import { SeccionMuestraDTO } from '@dtos/gestion-expediente/seccion-muestra-dto';
import { DominioHospitalService } from '@http/catalogo/dominio-hospital.service';
import { PadecimientoMuestraDTO } from '@dtos/gestion-expediente/padecimiento-muestra-dto';
import { addIcons } from 'ionicons';
import { validarCamposRequeridos } from '@utils/utileria';
import { AlertController, ModalController } from '@ionic/angular/standalone';
import { GeneralConstant } from '@utils/general-constant';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { LoadingSpinnerService } from 'src/app/services/dashboard/loading-spinner.service';
import { FechaService } from '@services/fecha.service';

@Component({
  selector: 'app-muestras-formulario',
  templateUrl: './muestras-formulario.component.html',
  styleUrls: ['./muestras-formulario.component.scss'],
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    IonicModule,
    SharedModule,
    CommonModule,
    CampoExpedienteModule,
    DirectiveModule,],
    providers: [SeccionCampoService, EntidadEstructuraTablaValorService,]
})
export class MuestrasFormularioComponent implements OnInit {

  protected arbolPadecimiento: PadecimientoMuestraDTO[] = [];
  protected fechaSeleccionada: string; //Viene desde muestrasPage
  protected padecimientoSeleccionado: PadecimientoMuestraDTO;
  protected seccionSeleccionada: SeccionMuestraDTO;
  protected seccionYaSeleccionada: boolean = false;
  protected submitting: boolean = false;
  protected variablesHealthKit = GeneralConstant.VARIABLES_HEALTHKIT;
  protected variablesExistenEnHealthKit: boolean = false;

  constructor(
    private seccionCampoService: SeccionCampoService,
    private entidadEstructuraTablaValorService: EntidadEstructuraTablaValorService,
    private dominioHospitalService:DominioHospitalService,
    private modalController: ModalController,
    private alertController: AlertController,
    private loadingSpinner: LoadingSpinnerService,
    private fechaService: FechaService
  ) { 

      addIcons({ 
        'chevron-up': 'assets/img/svg/chevron-up.svg',
        'chevron-down': 'assets/img/svg/chevron-down.svg',
        'swap-vertical-outline': 'assets/img/svg/swap-vertical-outline.svg',
      })
    }

  ngOnInit() {
    this.consultarArbol();
  }

  private consultarArbol(){
    lastValueFrom(this.seccionCampoService.consultarPorSeccion())
    .then((arbolPadecimiento: PadecimientoMuestraDTO[]) => {
      this.arbolPadecimiento = arbolPadecimiento;
    });
  }
  

  protected async enviarFormulario(formulario: NgForm) {
    this.submitting = true;

    if (!formulario.valid) {
      validarCamposRequeridos(formulario);
      return;
    }

    const camposAgregados: TablaValorMuestraDTO[] = [];
  
    for (const seccionCampo of this.seccionSeleccionada.seccionesCampo) {
      if (seccionCampo.valor) {
        camposAgregados.push({
          idSeccionVariable: seccionCampo.idSeccionCampo,
          valor: seccionCampo.valor.toString(),
          fueraDeRango: await this.estaFueraDeRango(seccionCampo),
          fechaMuestra: this.fechaService.fechaLocalAFechaUTC(this.fechaSeleccionada)
        });
      }
    }
    if (camposAgregados.length === 0) {
      this.submitting = false;
      return;
    }

    this.agregar(camposAgregados);
  }

  private agregar(campoAgregar: TablaValorMuestraDTO[]) {
    this.loadingSpinner.presentLoading();
    this.entidadEstructuraTablaValorService.agregarMuestra(campoAgregar).subscribe(
      {
        next: ()=> {
          this.submitting = false;
          this.padecimientoSeleccionado = new PadecimientoMuestraDTO();
          this.seccionSeleccionada = new SeccionMuestraDTO();
          this.seccionYaSeleccionada = false;
        },
        complete : () => {
          this.loadingSpinner.dismissLoading();
          this.presentarAlertSuccess();
        }
        ,
        error: () => {
          this.loadingSpinner.dismissLoading();
          this.submitting = false;
        }
      }
    );
  }

  //mover al backend
  private async estaFueraDeRango(campo: SeccionCampo) {
    const dominio = campo.idDominioNavigation;

    const number = Number.parseFloat(campo.valor?.toString() ?? '');

    if (Number.isNaN(number)) {
      return false;
    }

    //Verificacion de la tabla dominio hospital
    let domHos = await lastValueFrom(this.dominioHospitalService.obtenerDominioHospital(dominio.idDominio,0))
    if(domHos != null){
      if(domHos.valorMaximo != null){
        dominio.valorMaximo = Number(domHos.valorMaximo)
      }
      if(domHos.valorMinimo != null){
        dominio.valorMinimo = Number(domHos.valorMinimo)
      }
    }

    const min = dominio.valorMinimo;
    const max = dominio.valorMaximo;

    const valor = Number(campo.valor);

    const fueraDeRango = valor < min || valor > max;

    return fueraDeRango;
  }

  protected onChangePadecimiento(){
    this.seccionSeleccionada = new SeccionMuestraDTO();
    this.seccionYaSeleccionada = false;
  }

  protected onChangeSeccion(){
    this.seccionYaSeleccionada = true;
    this.variablesExistenEnHealthKit = this.seccionSeleccionada.seccionesCampo.every(seccionCampo => this.variablesHealthKit.includes(seccionCampo.clave));
  }

  protected valoresInputValidos(){
    for (const seccionCampo of this.seccionSeleccionada.seccionesCampo) {
      
      const number = Number.parseFloat(seccionCampo.valor?.toString() ?? '');
      if(Number.isNaN(number)){
        //this.presentarAlertaErrorInput();
        return false;
      }
      else{
        return true;
      }
    }

    return true;
  }

  async syncronizeData(){
    this.seccionSeleccionada.seccionesCampo.forEach(async variable => {
      await this.callPlugin();
    });
  }

  async callPlugin(){

  }

  protected async presentarAlertSuccess() {
    const alertSuccess = await this.alertController.create({
      header: 'Datos registrados exitosamente.',
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm',
        handler: () => {
          this.modalController.dismiss(null, 'confirm');
        }
      }],
      cssClass: 'custom-alert color-primary icon-check',
    });

    await alertSuccess.present();
  }
}
