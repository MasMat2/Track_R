import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { PickedFile } from '@capawesome/capacitor-file-picker';
import { AlertController, IonicModule } from '@ionic/angular';
import { ExpedienteEstudioService } from '@services/expediente-estudio.service';
import { CapacitorUtils } from '@utils/capacitor-utils';
import { ExpedienteEstudioFormularioCaptura } from 'src/app/shared/dtos/expediente-estudio-formulario-captura-dto';
import { validarCamposRequeridos } from 'src/app/shared/utils/utileria';
import { ModalController } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';

@Component({
  selector: 'app-mis-estudios-formulario',
  templateUrl: './mis-estudios-formulario.component.html',
  styleUrls: ['./mis-estudios-formulario.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    FormsModule,
    CommonModule,
    RouterModule,
  ],
  providers: [CapacitorUtils],
})

export class MisEstudiosFormularioPage implements OnInit {
  private files: PickedFile[] = [];
  protected isPictureTaken: boolean = false;
  protected esArchivoSeleccionado: boolean = false;
  protected fechastring: string = new Date().toISOString();
  private image_src: string = '';
  private mimeType: string = '';
  protected btnSubmit = false;

  public expedienteEstudio = new ExpedienteEstudioFormularioCaptura();
  constructor(
    private capacitorUtil: CapacitorUtils,
    private alertController: AlertController,
    private expedienteEstudioService: ExpedienteEstudioService,
    private router: Router,
    private modalController: ModalController,
  ) {
    addIcons({
    'eye': 'assets/img/svg/eye.svg',
    'user': 'assets/img/svg/user.svg',
    'calendar': 'assets/img/svg/calendar.svg',
    'camera': 'assets/img/svg/camera.svg',
    'file': 'assets/img/svg/file.svg',
    })
  }

  ngOnInit() {}

  protected async takePicture() {
    this.image_src = await this.capacitorUtil.takePicture();

    const [, data] = this.image_src.split(',');
    const mimeType = this.image_src.split(':')[1].split(';')[0];

    this.expedienteEstudio.archivo = data;
    this.expedienteEstudio.archivoTipoMime = mimeType;
    this.expedienteEstudio.archivoNombre = this.generateFileName();

    this.isPictureTaken = true;
  }

  protected enviarFormulario(formulario: NgForm) {
    this.btnSubmit = true;
    if(this.expedienteEstudio.fechaRealizacion == null)
      this.expedienteEstudio.fechaRealizacion = new Date();
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

    this.esArchivoSeleccionado = true;
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
    this.expedienteEstudioService
      .agregarExpediente(this.expedienteEstudio)
      .subscribe({
        next: () => {
        },
        error: () => {
          this.btnSubmit = false;
        },
        complete: () => {
          this.presentarAlertaSuccess();
        }
      });
  }

  protected cerrarModal(){
    this.modalController.dismiss();
  }

  protected eliminarAdjunto(){
    this.expedienteEstudio.archivo = null;
    this.expedienteEstudio.archivoNombre = '';
    this.expedienteEstudio.archivoTipoMime = '';

    this.isPictureTaken = false;
    this.esArchivoSeleccionado = false;
  }

  protected async presentarAlertaSuccess() {

    const alertSuccess = await this.alertController.create({
      header: 'Estudio registrado',
      subHeader: 'El estudio ha sido registrado correctamente',
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm',
        handler: ()=> {
          this.modalController.dismiss();
        }
      }],
      cssClass: 'custom-alert color-primary icon-check',
    });

    await alertSuccess.present();
  }
  
}
