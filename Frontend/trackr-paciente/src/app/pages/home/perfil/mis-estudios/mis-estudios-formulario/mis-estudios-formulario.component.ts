import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { PickedFile } from '@capawesome/capacitor-file-picker';
import { AlertController, IonicModule } from '@ionic/angular';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { ExpedienteEstudioService } from '@services/expediente-estudio.service';
import { CapacitorUtils } from '@utils/capacitor-utils';
import { ExpedienteEstudioFormularioCaptura } from 'src/app/shared/dtos/expediente-estudio-formulario-captura-dto';
import { validarCamposRequeridos } from 'src/app/shared/utils/utileria';

@Component({
  selector: 'app-mis-estudios-formulario',
  templateUrl: './mis-estudios-formulario.component.html',
  styleUrls: ['./mis-estudios-formulario.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    FormsModule,
    HeaderComponent,
    CommonModule,
    RouterModule,
  ],
  providers: [CapacitorUtils],
})

export class MisEstudiosFormularioPage implements OnInit {
  private files: PickedFile[] = [];
  protected isPictureTaken: boolean = false;
  protected fecha = new Date();
  private image_src: string = '';
  private mimeType: string = '';
  protected btnSubmit = false;

  public expedienteEstudio = new ExpedienteEstudioFormularioCaptura();
  constructor(
    private capacitorUtil: CapacitorUtils,
    private alertController: AlertController,
    private expedienteEstudioService: ExpedienteEstudioService,
    private router: Router
  ) {}

  ngOnInit() {}

  protected async takePicture() {
    this.isPictureTaken = true;
    this.image_src = await this.capacitorUtil.takePicture();

    const [, data] = this.image_src.split(',');
    const mimeType = this.image_src.split(':')[1].split(';')[0];

    this.expedienteEstudio.archivo = data;
    this.expedienteEstudio.archivoTipoMime = mimeType;
    this.expedienteEstudio.archivoNombre = this.generateFileName();
  }

  protected enviarFormulario(formulario: NgForm) {
    this.btnSubmit = true;
    if (!formulario.valid) {
      validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      return;
    }
    this.agregarExpediente();
  }

  protected async agregarArchivo() {
    this.files = await this.capacitorUtil.pickFiles();
    this.expedienteEstudio.archivo = this.files[0].data;
    this.expedienteEstudio.archivoTipoMime = this.files[0].mimeType;
    this.expedienteEstudio.archivoNombre = this.files[0].name;
  }

  //temporal o de prueba del nombre de la fotografia cuando se toma o se selecciona
  private generateFileName(): string {
    const date = new Date();
    const formattedDate = date.toISOString().split('T')[0];
    const timestamp = date.getTime();
    const extension = this.mimeType === 'image/png' ? 'png' : 'jpg';

    return `foto_${formattedDate}_${timestamp}.${extension}`;
  }

  private agregarExpediente() {
    const MENSAJE_EXITO: string = `El estudio ha sido agregado correctamente`;
    const subscription = this.expedienteEstudioService
      .agregarExpediente(this.expedienteEstudio)
      .subscribe({
        next: () => {
          this.presentAlert(MENSAJE_EXITO);
          this.router.navigateByUrl('home/perfil/mis-estudios');
        },
        error: () => {
          this.btnSubmit = false;
        },
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
}
