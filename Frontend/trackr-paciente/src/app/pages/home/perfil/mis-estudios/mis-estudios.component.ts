import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  AlertController,
  IonicModule,
  ModalController,
  ToastController,
} from '@ionic/angular';
import { ExpedienteEstudioService } from '@services/expediente-estudio.service';
import { ExpedienteEstudioGridDTO } from 'src/app/shared/dtos/expediente-estudio-grid-dto';
import { ExpedienteEstudio } from 'src/app/shared/dtos/expediente-estudio-dto';
import { TableModule } from 'primeng/table';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';

import { lastValueFrom } from 'rxjs';
import { PdfVisorComponent } from '@sharedComponents/pdf-visor/pdf-visor.component';
import { SafeResourceUrl } from '@angular/platform-browser';
import { ImagenVisorComponent } from '@sharedComponents/imagen-visor/imagen-visor.component';
import { ExpedienteEstudioFormularioCaptura } from 'src/app/shared/dtos/expediente-estudio-formulario-captura-dto';
import { MisEstudiosFormularioComponent } from './mis-estudios-formulario/mis-estudios-formulario.component';

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
export class MisEstudiosComponent implements OnInit {
  expedientes: ExpedienteEstudioGridDTO[] = [];
  estudios: ExpedienteEstudio[] = [];
  estudioSeleccionado: ExpedienteEstudio;
  imageSrc: SafeResourceUrl;
  component: ExpedienteEstudioFormularioCaptura;

  constructor(
    private expedienteEstudioService: ExpedienteEstudioService,
    private modalController: ModalController,
    private alertController: AlertController,
    private toastController: ToastController
  ) {}

  ngOnInit() {
    this.consultarGrid();
  }
  public consultarGrid() {
    this.expedienteEstudioService
      .consultarParaGrid()
      .subscribe((data: ExpedienteEstudioGridDTO[]) => {
        this.expedientes = data;
      });
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
  async eliminar(expedienteEstudio: ExpedienteEstudioGridDTO) {
    const MENSAJE_EXITO: string = `El estudio ha sido eliminado correctamente`;

    await this.expedienteEstudioService
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

  public async agregar() {
    const modal = await this.modalController.create({
      component: MisEstudiosFormularioComponent,
      componentProps: {},
    });

    modal.onDidDismiss().then((data) => {
      if (data.data && data.data.actualizarGrid) {
        this.consultarGrid();
      }
    });

    return await modal.present();
  }
}
