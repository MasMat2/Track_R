import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AlertController, IonicModule, ModalController } from '@ionic/angular';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { ExpedienteEstudioService } from '@services/expediente-estudio.service';
import { ImagenVisorComponent } from '@sharedComponents/imagen-visor/imagen-visor.component';
import { PdfVisorComponent } from '@sharedComponents/pdf-visor/pdf-visor.component';
import { NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';
import { TableModule } from 'primeng/table';
import { Observable, lastValueFrom } from 'rxjs';
import { ExpedienteEstudio } from 'src/app/shared/dtos/expediente-estudio-dto';
import { ExpedienteEstudioGridDTO } from 'src/app/shared/dtos/expediente-estudio-grid-dto';
import { MisEstudiosFormularioPage } from './mis-estudios-formulario/mis-estudios-formulario.component';
import { Constants } from '@utils/constants/constants';
import { add, chevronBack, trashOutline, chevronForward } from 'ionicons/icons';
import { addIcons } from 'ionicons';

@Component({
  selector: 'app-mis-estudios',
  templateUrl: './mis-estudios.component.html',
  styleUrls: ['./mis-estudios.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    TableModule,
    HeaderComponent,
    NgxExtendedPdfViewerModule,
    RouterModule,
  ],
})

export class MisEstudiosPage {
  protected expedientes$: Observable<ExpedienteEstudioGridDTO[]>;
  private estudioSeleccionado: ExpedienteEstudio;
  protected misEstudios: ExpedienteEstudioGridDTO[];
  protected filtrando: boolean = false;
  protected estudiosFiltradosPorBusqueda: ExpedienteEstudioGridDTO[];

  constructor(
    private expedienteEstudioService: ExpedienteEstudioService,
    private modalController: ModalController,
    private alertController: AlertController,
  ) { addIcons({add, chevronBack, trashOutline, chevronForward})}

  ionViewWillEnter() {
    this.consultarEstudios();
  }

  private consultarEstudios(): void {
    this.expedientes$ = this.expedienteEstudioService.consultarParaGrid();
    this.expedientes$.subscribe({
      next: (data)=> {
        this.misEstudios = data;
        this.estudiosFiltradosPorBusqueda = data;
      }
    })
  }

  protected async abrirEstudio(estudio: ExpedienteEstudioGridDTO){
    if(estudio.archivoTipoMime == 'application/pdf'){
      this.onVer(estudio);
    }
    if(estudio.archivoTipoMime == 'image/png' || estudio.archivoTipoMime == 'image/jpeg' || estudio.archivoTipoMime == 'image/gif'){
      this.onVerImagen(estudio);
    }
    else{
      return
    }
  }

  private async onVer(estudio: ExpedienteEstudioGridDTO) {
    await lastValueFrom(
      this.expedienteEstudioService.consultar(estudio.idExpedienteEstudio)
    ).then((expedienteEstudio: ExpedienteEstudio) => {
      this.estudioSeleccionado = expedienteEstudio;
    });
    this.abrirModal(PdfVisorComponent, {
      archivo: this.estudioSeleccionado.archivo,
      archivoNombre: this.estudioSeleccionado.archivoNombre,
      nombre: this.estudioSeleccionado.nombre,
    });
  }

  private async onVerImagen(estudio: ExpedienteEstudioGridDTO) {
    await lastValueFrom(
      this.expedienteEstudioService.consultar(estudio.idExpedienteEstudio)
    ).then((expedienteEstudio: ExpedienteEstudio) => {
      this.estudioSeleccionado = expedienteEstudio;
    });
    this.abrirModal(ImagenVisorComponent, {
      nombreEstudio: this.estudioSeleccionado.nombre,
      archivo: this.estudioSeleccionado.archivo,
      archivoTipoMime: this.estudioSeleccionado.archivoTipoMime,
    });
  }

  protected eliminar(expedienteEstudio: ExpedienteEstudioGridDTO) {
    this.expedienteEstudioService.eliminar(expedienteEstudio.idExpedienteEstudio).subscribe({
        next: ()=> {
          this.consultarEstudios();
        },
        error: () => {

        },
        complete : ()=> {
          this.presentarAlertaEliminadoExitosamente();
        }
      });
  }

  // private async presentAlert(mensaje: string) {
  //   const alert = await this.alertController.create({
  //     header: 'Mis Estudios',
  //     message: mensaje,
  //     buttons: ['OK'],
  //   });

  //   await alert.present();
  // }

  protected async abrirModal(component: any, componentProps: any) {
    const modal = await this.modalController.create({
      component: component,
      componentProps: componentProps,
    });

    await modal.present();
  }

  protected listaEstudiosVacia(){
    return this.misEstudios?.length <= 0;
  }

  protected async agregarEstudio(){
    const modal = await this.modalController.create({
      component: MisEstudiosFormularioPage,
    });
    //cuando se cierre el modal la lista de estudios ya estará actualizada
    modal.onWillDismiss().then(() => {
      this.consultarEstudios();
    })

    await modal.present();
  }

  protected async presentarAlertaEliminar(expediente: ExpedienteEstudioGridDTO) {
    const alert = await this.alertController.create({
      header: '¿Seguro que deseas eliminar este elemento?',
      subHeader: 'No podrás recuperarlo',
      cssClass: 'custom-alert color-error icon-trash two-buttons',
      buttons: [
        {
          text: 'No, regresar',
          role: 'cancel',
        },
        {
          text: 'Sí, eliminar',
          role: 'confirm',
          handler: ()=> {
            this.eliminar(expediente);
          }
          },
      ]
    });

    await alert.present();
  }

  protected async presentarAlertaEliminadoExitosamente() {

    const alertSuccess = await this.alertController.create({
      header: 'Elemento eliminado exitosamente',
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm'
      }],
      cssClass: 'custom-alert color-primary icon-check',
    });

    await alertSuccess.present();
  }

  protected buscarEstudio(event: any){
    const text = event.target.value;

    text == '' ? this.filtrando = false : this.filtrando = true; //el filtrado se activa cuando hay texto ingresado

    this.estudiosFiltradosPorBusqueda = this.misEstudios;
    if(text && text.trim() != ''){
      this.estudiosFiltradosPorBusqueda = this.estudiosFiltradosPorBusqueda.filter((estudio: ExpedienteEstudioGridDTO) =>{
        return (estudio.nombre.toLowerCase().indexOf(text.toLowerCase()) > -1 );
      })
    }
  }


}
