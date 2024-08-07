import { SharedModule } from '@sharedComponents/shared.module';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EntidadEstructuraTablaValorService } from '@http/gestion-expediente/entidad-estructura-tabla-valor.service';
import { IonicModule } from '@ionic/angular';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { MuestrasFormularioComponent } from './muestras-formulario/muestras-formulario.component';
import { addIcons } from 'ionicons';
import { ValoresFueraRangoGridDTO } from '@dtos/gestion-expediente/valores-fuera-rango-grid-dto';
import { ModalController } from '@ionic/angular/standalone';
import { LoadingSpinnerService } from '../../../services/dashboard/loading-spinner.service';
import { FechaService } from '@services/fecha.service';

@Component({
  selector: 'app-muestras',
  templateUrl: './muestras.page.html',
  styleUrls: ['./muestras.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    FormsModule,
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
    private loadingSpinner: LoadingSpinnerService,
    private fechaService: FechaService,
  ) { addIcons({
      'plus': 'assets/img/svg/plus.svg',
      'calendar': 'assets/img/svg/calendar.svg',
      'clock-2': 'assets/img/svg/clock-2.svg'
    }) }

  ngOnInit() {
    this.consultarValoresFueraRango();
  }

  public consultarValoresFueraRango(): void {
    this.loadingSpinner.presentLoading();

    this.entidadEstructuraTablaValorService.consultarValoresFueraRangoUsuarioSesion().subscribe({
      next: (valoresFueraRango: ValoresFueraRangoGridDTO[]) => {
        valoresFueraRango.map((data) => {
          data.fechaHora = this.fechaService.fechaUTCAFechaLocal(data.fechaHora);
          return data;
        })
        this.valoresFueraRango = valoresFueraRango.sort((a, b) => {
          return new Date(b.fechaHora).getTime() - new Date(a.fechaHora).getTime();
        });
      },
      error: () => {
        this.loadingSpinner.dismissLoading();
      },
      complete: ()=> {
        this.loadingSpinner.dismissLoading();
      }
    })
  }

  protected listaValoresVacia(){
    return this.valoresFueraRango?.length <= 0;
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
      this.consultarValoresFueraRango();
    }
  }

}
