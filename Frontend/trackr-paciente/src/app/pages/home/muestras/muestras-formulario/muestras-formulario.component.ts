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

  constructor(
    private seccionCampoService: SeccionCampoService,
    private entidadEstructuraTablaValorService: EntidadEstructuraTablaValorService,
    private router : Router,
    private dominioHospitalService:DominioHospitalService,
    private modalController: ModalController,
    private alertController: AlertController
  ) { 

      addIcons({ 
        'chevron-up': 'assets/img/svg/chevron-up.svg',
        'chevron-down': 'assets/img/svg/chevron-down.svg'
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
      console.log(arbolPadecimiento);
      console.log(this.arbolPadecimiento);
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
    console.log(camposAgregados);
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
}
