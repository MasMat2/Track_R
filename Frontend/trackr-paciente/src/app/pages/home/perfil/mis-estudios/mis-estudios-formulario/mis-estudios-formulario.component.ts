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
import { ModalController } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { eyeOutline, personOutline, calendarOutline, cameraOutline, documentOutline, trashOutline} from 'ionicons/icons';
import { Constants } from '@utils/constants/constants';

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
  protected esArchivoSeleccionado: boolean = false;
  protected fecha = new Date();
  protected fechastring: string = "2023-11-02T01:22:00";
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
  ) {addIcons({eyeOutline, personOutline, calendarOutline, cameraOutline, documentOutline, trashOutline})}

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

  protected imprimirFecha(){
    console.log(this.fechastring);
  }

  protected eliminarAdjunto(){
    this.expedienteEstudio.archivo = '';
    this.expedienteEstudio.archivoNombre = '';
    this.expedienteEstudio.archivoTipoMime = '';

    this.isPictureTaken = false;
    this.esArchivoSeleccionado = false;
  }

  protected async presentarAlertaSuccess() {

    const alertSuccess = await this.alertController.create({
      header: 'Estudio registrado',
      subHeader: 'El estudio ha sido registrado correctamente',
      message: Constants.ALERT_SUCCESS,
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm',
        handler: ()=> {
          this.modalController.dismiss();
        }
      }],
      cssClass: 'custom-alert-success',
    });

    await alertSuccess.present();
  }
}
