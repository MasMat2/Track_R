import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PickedFile } from '@capawesome/capacitor-file-picker';
import { AlertController, IonicModule, ModalController } from '@ionic/angular';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { ExpedienteEstudioService } from '@services/expediente-estudio.service';
import { CapacitorUtils } from '@utils/capacitor-utils';
import { ExpedienteEstudioFormularioCaptura } from 'src/app/shared/dtos/expediente-estudio-formulario-captura-dto';
import { validarCamposRequeridos } from 'src/app/shared/utils/utileria';

@Component({
  selector: 'app-mis-estudios-formulario',
  templateUrl: './mis-estudios-formulario.component.html',
  styleUrls: ['./mis-estudios-formulario.component.scss'],
  standalone:true,
  imports:[
    IonicModule,
    FormsModule,
    HeaderComponent,
    CommonModule,
    RouterModule,
  ],
  providers:[
    CapacitorUtils
  ]
})
export class MisEstudiosFormularioComponent  implements OnInit {

  public files: PickedFile[] = [];
  isPictureTaken: boolean = false;

  fecha = new Date();
  image_src: string='';
  mimeType: string = '';
  public btnSubmit = false;

  public expedienteEstudio = new ExpedienteEstudioFormularioCaptura();
  constructor(
    private capacitorUtil: CapacitorUtils,
    private alertController: AlertController,
    private expedienteEstudioService:ExpedienteEstudioService,
    private modalController:ModalController
  ) {}

  ngOnInit() {}

  async takePicture() {
    this.isPictureTaken = true;
    this.image_src = await this.capacitorUtil.takePicture();

    const [, data] = this.image_src.split(',');
    const mimeType = this.image_src.split(':')[1].split(';')[0];

    this.expedienteEstudio.Archivo = data;
    this.expedienteEstudio.ArchivoTipoMime = mimeType;
    this.expedienteEstudio.ArchivoNombre = this.generateFileName();

  }



  public enviarFormulario(formulario: NgForm): void {
   this.btnSubmit = true;
    if (!formulario.valid) {
      validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      return;
    }
    this.agregarExpediente();
    const modalData = { actualizarGrid: true };

    this.modalController.dismiss(modalData);
  }

  async agregarArchivo() {
    this.files = await this.capacitorUtil.pickFiles();
    this.expedienteEstudio.Archivo = this.files[0].data;
    this.expedienteEstudio.ArchivoTipoMime = this.files[0].mimeType;
    this.expedienteEstudio.ArchivoNombre = this.files[0].name;
  }

  //temporal o de prueba del nombre de la fotografia cuando se toma o se selecciona
  private generateFileName(): string {
    const date = new Date();
    const formattedDate = date.toISOString().split('T')[0];
    const timestamp = date.getTime();
    const extension = this.mimeType === 'image/png' ? 'png' : 'jpg';

    return `foto_${formattedDate}_${timestamp}.${extension}`;
  }

  public agregarExpediente(): void {

    const MENSAJE_EXITO: string = `El estudio ha sido agregado correctamente`;
     const subscription=this.expedienteEstudioService.agregarExpediente(this.expedienteEstudio).subscribe({
      next:()=>{
        this.presentAlert(MENSAJE_EXITO)
      },
      error:()=>{
        this.btnSubmit = false;
      },
      complete:()=>{
        subscription.unsubscribe();
      }
    },
      );

  }

  async cerrarModal() {
    await this.modalController.dismiss();
  }

  private async presentAlert(mensaje : string) {
    const alert = await this.alertController.create({
      header: 'Mis Estudios',
      message: mensaje,
      buttons: ['OK'],
    });

    await alert.present();
  }

}
