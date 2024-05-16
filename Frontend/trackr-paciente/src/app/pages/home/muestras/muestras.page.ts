import { SharedModule } from '@sharedComponents/shared.module';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EntidadEstructuraTablaValorService } from '@http/gestion-expediente/entidad-estructura-tabla-valor.service';
import { IonicModule } from '@ionic/angular';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { lastValueFrom } from 'rxjs';
import { HeaderComponent } from '../layout/header/header.component';
import { MuestrasFormularioComponent } from './muestras-formulario/muestras-formulario.component';
import { addIcons } from 'ionicons';
import { ValoresFueraRangoGridDTO } from '@dtos/gestion-expediente/valores-fuera-rango-grid-dto';
import { AlertController, ModalController } from '@ionic/angular/standalone';

@Component({
  selector: 'app-muestras',
  templateUrl: './muestras.page.html',
  styleUrls: ['./muestras.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    FormsModule,
    HeaderComponent,
    GridGeneralModule,
    MuestrasFormularioComponent,
    SharedModule
  ],
  providers: [
    EntidadEstructuraTablaValorService,
  ]
})
export class MuestrasPage implements OnInit {

  // Variables
  protected valoresFueraRango: ValoresFueraRangoGridDTO[];
  protected now = new Date();
  protected localOffset = this.now.getTimezoneOffset() * 60000;
  protected localISOTime = (new Date(this.now.getTime() - this.localOffset)).toISOString().slice(0,-1);
  protected dateToday: string = this.localISOTime;
  protected fechaSeleccionada: string = this.dateToday;

  constructor(
    private entidadEstructuraTablaValorService: EntidadEstructuraTablaValorService,
    private modalController: ModalController,
    private alertController: AlertController
  ) { addIcons({
      'plus': 'assets/img/svg/plus.svg',
      'calendar': 'assets/img/svg/calendar.svg',
      'clock-2': 'assets/img/svg/clock-2.svg'
    }) }

  ngOnInit() {
    this.consultarValoresFueraRango();
  }

  public consultarValoresFueraRango(): void {
    lastValueFrom(this.entidadEstructuraTablaValorService.consultarValoresFueraRangoUsuarioSesion())
      .then((valoresFueraRango: ValoresFueraRangoGridDTO[]) => {
        this.valoresFueraRango = valoresFueraRango.sort((a, b) => {
          return new Date(b.fechaHora).getTime() - new Date(a.fechaHora).getTime();
        });
      }
    );
  }

  protected listaValoresVacia(){
    return this.valoresFueraRango?.length <= 0;
  }

  protected onFechaChange(){
    console.log(this.dateToday);
    console.log(this.fechaSeleccionada);
  }

  protected async AgregarDatosClinicos(){
    const modal = await this.modalController.create({
      component: MuestrasFormularioComponent,
      componentProps: {fechaSeleccionada: this.fechaSeleccionada},
      breakpoints : [0, 1],
      initialBreakpoint: 1,
      cssClass: 'custom-sheet-modal'
      
    });
 
    await modal.present();

    const {data, role} = await modal.onWillDismiss();

    if(role == 'confirm'){
      this.presentarAlertSuccess();
      this.consultarValoresFueraRango();
    }
  }

  protected async presentarAlertSuccess() {
    const alertSuccess = await this.alertController.create({
      header: 'Datos registrados exitosamente.',
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm'
      }],
      cssClass: 'custom-alert-success',
    });

    await alertSuccess.present();
  }

}
