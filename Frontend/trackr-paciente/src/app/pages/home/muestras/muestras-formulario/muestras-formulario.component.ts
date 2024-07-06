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
import { NavigationEnd, Router } from '@angular/router';
import { DominioHospitalService } from '@http/catalogo/dominio-hospital.service';
import { PadecimientoMuestraDTO } from '@dtos/gestion-expediente/padecimiento-muestra-dto';
import { addIcons } from 'ionicons';
import { validarCamposRequeridos } from '@utils/utileria';
import { AlertController, ModalController } from '@ionic/angular/standalone';
import { call, swapHorizontalOutline, swapVerticalOutline } from 'ionicons/icons';
import { HealthkitService } from '@services/healthkit.service';
import { GeneralConstant } from '@utils/general-constant';

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
    CampoExpedienteModule],
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
    private router : Router,
    private dominioHospitalService:DominioHospitalService,
    private modalController: ModalController,
    private alertController: AlertController,
    private healthKitService: HealthkitService
  ) { 

      addIcons({ 
        'chevron-up': 'assets/img/svg/chevron-up.svg',
        'chevron-down': 'assets/img/svg/chevron-down.svg',
        'swap-vertical-outline': 'assets/img/svg/swap-vertical-outline.svg',
      })
      // this.router.events
      // .pipe(filter((event): event is NavigationEnd => event instanceof NavigationEnd))
      // .subscribe(async (event: NavigationEnd) =>
      //  {
      //   const currentUrl = event.urlAfterRedirects;
      //   if ( currentUrl === '/home/clinicos') 
      //   {
      //     this.consultarArbol();
  
      //   }
      // });
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

    console.log(this.seccionSeleccionada);
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
          fechaMuestra: new Date(this.fechaSeleccionada)
        });
      }
    }
    if (camposAgregados.length === 0) {
      this.submitting = false;
      return;
    }

    this.agregar(camposAgregados);
  }

  private agregar(campoAgregar: TablaValorMuestraDTO[]): void {
    this.entidadEstructuraTablaValorService.agregarMuestra(campoAgregar).subscribe(
      {
        next: ()=> {
          this.submitting = false;
          this.padecimientoSeleccionado = new PadecimientoMuestraDTO();
          this.seccionSeleccionada = new SeccionMuestraDTO();
          this.seccionYaSeleccionada = false;
        },
        complete : () => {
          this.modalController.dismiss(null, 'confirm');
        }
        ,
        error: () => {
          this.submitting = false;
        }
      }
    );
  }

  private async estaFueraDeRango(campo: SeccionCampo) {
    const dominio = campo.idDominioNavigation;

    const number = Number.parseFloat(campo.valor?.toString() ?? '');

    if (Number.isNaN(number)) {
      return false;
    }

    //Verificacion de la tabla dominio hospital
    let domHos = await lastValueFrom(this.dominioHospitalService.obtenerDominioHospital(dominio.idDominio,0))
    console.log(domHos)
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
      variable.valor = await this.callPlugin(variable.clave);
    });
  }

  //En caso de que no se tengan datos sobre Salud de iOS el valor sera undefined
  async callPlugin(claveVariable : string){
    const bloodPressureSystolic = await this.healthKitService.getBloodPressureSystolic();
    const bloodPressureDiastolic = await this.healthKitService.getBloodPressureDiastolic();
    //ultimo uuid de presion arterial sistolica:
    const uuidSistolica = bloodPressureSystolic.resultData[0].uuid.toString();

    //ultimo valor de presion arterial sistolica
    const systolic = bloodPressureSystolic.resultData[0].value.toString();

    //ultimo uuid de presion arterial diastolica:
    const uuidDiastolica = bloodPressureDiastolic.resultData[0].uuid.toString();

    //ultimo valor de presion arterial diastolica
    const diastolic = bloodPressureDiastolic.resultData[0].value.toString();
    
    if (claveVariable === 'SE-001') {
      return systolic;
    } else if (claveVariable === 'SE-002') {
        return diastolic;
    } else {
        return undefined; 
    }
    
  }
}
