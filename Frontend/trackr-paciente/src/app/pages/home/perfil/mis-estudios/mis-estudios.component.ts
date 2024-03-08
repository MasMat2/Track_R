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

  constructor(
    private expedienteEstudioService: ExpedienteEstudioService,
    private modalController: ModalController,
    private alertController: AlertController
  ) {}

  ionViewWillEnter() {
    this.consultarGrid();
  }
  private consultarGrid(): void {
    this.expedientes$ = this.expedienteEstudioService.consultarParaGrid();
  }

  protected async onVer(estudio: ExpedienteEstudioGridDTO) {
    await lastValueFrom(
      this.expedienteEstudioService.consultar(estudio.idExpedienteEstudio)
    ).then((expedienteEstudio: ExpedienteEstudio) => {
      this.estudioSeleccionado = expedienteEstudio;
    });
    this.abrirModal(PdfVisorComponent, {
      archivo: this.estudioSeleccionado.archivo,
      archivoNombre: this.estudioSeleccionado.archivoNombre,
    });
  }

  protected async onVerImagen(estudio: ExpedienteEstudioGridDTO) {
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
    const MENSAJE_EXITO: string = `El estudio ha sido eliminado correctamente`;

    this.expedienteEstudioService
      .eliminar(expedienteEstudio.idExpedienteEstudio)
      .subscribe((data) => {
        this.presentAlert(MENSAJE_EXITO);
        this.consultarGrid();
      });
  }

  private async presentAlert(mensaje: string) {
    const alert = await this.alertController.create({
      header: 'Mis Estudios',
      message: mensaje,
      buttons: ['OK'],
    });

    await alert.present();
  }
  protected async abrirModal(component: any, componentProps: any) {
    const modal = await this.modalController.create({
      component: component,
      componentProps: componentProps,
    });

    await modal.present();
  }
}
