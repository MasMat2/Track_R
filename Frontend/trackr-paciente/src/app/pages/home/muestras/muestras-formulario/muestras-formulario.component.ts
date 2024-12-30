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
import { GeneralConstant } from '@utils/general-constant';
import { GetRecordsOptions, Record } from '@pages/home/dashboard/interfaces/healthconnect-interfaces';
import { RecordType } from 'capacitor-health-connect-local';
import { HealthConnectService } from 'src/app/services/dashboard/health-connect.service';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { LoadingSpinnerService } from 'src/app/services/dashboard/loading-spinner.service';
import { FechaService } from '@services/fecha.service';
import { BaumaReaderComponent } from './bauma-reader/bauma-reader.component';

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
    private router : Router,
    private dominioHospitalService:DominioHospitalService,
    private modalController: ModalController,
    private alertController: AlertController,
    private healthConnectservice: HealthConnectService,
    private loadingSpinner: LoadingSpinnerService,
    private fechaService: FechaService
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

  async syncronizeDataOmron() {
    const modal = await this.modalController.create({
      component: BaumaReaderComponent,
    });

    modal.onDidDismiss().then((dataReturned) => {
      if (dataReturned.data) {
        const readings = dataReturned.data.readings;
        this.updateVariablesWithReadings(readings);
      }
    });

    return await modal.present();
  }

  updateVariablesWithReadings(readings: any[]) {
    const reading = readings[0]; // Assuming you take the first reading
    if (this.seccionSeleccionada && this.seccionSeleccionada.seccionesCampo) {
      this.seccionSeleccionada.seccionesCampo.forEach((variable) => {
        if (variable.clave == "SE-001"){
          variable.valor = reading.systolic;
        } else if (variable.clave == "SE-002") {
          variable.valor = reading.diastolic;
        // } else if (variable.descripcion.toLowerCase().includes('pulse')) {
        //   variable.valor = reading.pulseRate;
        }
      });
    }
  }

  async syncronizeData(){
    this.seccionSeleccionada.seccionesCampo.forEach(async variable => {
      variable.valor = await this.callPlugin(variable.clave);
    });
  }

  async callPlugin(claveVariable : string){
    try {
      const current = new Date();
      const startTime6months = new Date(current);
      startTime6months.setMonth(startTime6months.getMonth() - 6);

      const options: GetRecordsOptions = {
          type: 'BloodPressure' as RecordType,
          timeRangeFilter: {
              type: 'between',
              startTime: startTime6months, 
              endTime: current
          }
      };

      const isComplete = await this.healthConnectservice.setupComplete$.toPromise();
      if (!isComplete) {
        throw new Error('HealthConnect is not setup.');
      }

      const res = await this.healthConnectservice.readRecords(options);
      console.log(JSON.stringify(res));

      if (res && res.records && res.records.length > 0) {
          const ultimoRegistro = res.records[res.records.length - 1] as Record;

          const systolic = ultimoRegistro.systolic!.value;
          const diastolic = ultimoRegistro.diastolic!.value;

          if (claveVariable === 'SE-001') {
            return systolic;
          } else if (claveVariable === 'SE-002') {
            return diastolic;
          } else {
              return undefined; 
          }

      } else {
        return undefined; 
      }
  } catch (error) {
      console.log('[HealthConnect util] Error reading blood pressure data:', error);
      throw error;
  }
  }
}
