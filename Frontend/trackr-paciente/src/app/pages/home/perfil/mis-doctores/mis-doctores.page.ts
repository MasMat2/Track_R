import { Component, OnInit } from '@angular/core';
import { AlertController, IonicModule, ModalController } from '@ionic/angular';
import { MisDoctoresService } from '@http/seguridad/mis-doctores.service';
import { UsuarioDoctoresDto } from '../../../../shared/Dtos/usuario-doctores-dto';
import { CommonModule, NgFor } from '@angular/common';
import { UsuarioDoctoresSelectorDto } from 'src/app/shared/Dtos/usuario-doctores-selector-dto';
import { UsuarioDoctorDto } from 'src/app/shared/Dtos/usuario-doctor-dto';
import { DoctoresFormularioPage } from './doctores-formulario/doctores-formulario.page';
import { RouterModule } from '@angular/router';
import { url } from 'inspector';
import { DomSanitizer } from '@angular/platform-browser';
import { ArchivoService } from '@services/archivo.service';
import { addIcons } from 'ionicons';
import { chevronBack, add, trashOutline } from 'ionicons/icons';
import { FormsModule } from '@angular/forms';
import { Constants } from '@utils/constants/constants';


@Component({
  selector: 'app-mis-doctores',
  templateUrl: './mis-doctores.page.html',
  styleUrls: ['./mis-doctores.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    DoctoresFormularioPage,
    RouterModule
  ]
})
export class MisDoctoresPage   {
  
  protected misDoctores: UsuarioDoctoresDto[];
  protected doctoresSelector: UsuarioDoctoresSelectorDto[];
  protected currentDoctor: UsuarioDoctorDto;

  constructor(
    private doctoresService: MisDoctoresService,
    private alertController: AlertController,
    private archivoService : ArchivoService,
    private sanitizer : DomSanitizer,
    private modalCtrl : ModalController
  ) { addIcons({chevronBack, add, trashOutline})}



  ionViewWillEnter() {
    this.consultarDoctores();
  }

  consultarDoctores() {
    this.doctoresService.consultarExpediente().subscribe((doctores => {
      doctores.forEach((doctor) => { 
        this.archivoService.obtenerUsuarioImagen(doctor.idUsuarioDoctor).subscribe((imgaen) => {
          let objectURL = URL.createObjectURL(imgaen);
          let urlImagen = objectURL;
          let url = this.sanitizer.bypassSecurityTrustUrl(urlImagen);
          doctor.urlImagen = url;
        });
      }
      )
      this.misDoctores = doctores;
    }));
  }

  private eliminarDoctor(doctor: UsuarioDoctorDto) {
    const subscription = this.doctoresService.eliminar(doctor)
      .subscribe({
        next: () => {
          this.consultarDoctores();
        },
        error: () => {
        },
        complete: () => {
          subscription.unsubscribe();
        }
      });
  }

  protected async presentarAlertaEliminar(doctor: UsuarioDoctorDto) {
    const alert = await this.alertController.create({
      header: '¿Seguro que deseas eliminar este elemento?',
      subHeader: 'No podrás recuperarlo',
      message: Constants.ALERT_DELETE,
      cssClass: 'custom-alert-delete',
      buttons: [
        {
          text: 'No, regresar',
          role: 'cancel',
        },
        {
          text: 'Sí, eliminar',
          role: 'confirm',
          handler: ()=> {
            this.eliminarDoctor(doctor);
          }
          },
      ]
    });

    await alert.present();
  }

  protected async AgregarDoctor(){
    const modal = await this.modalCtrl.create({
      component: DoctoresFormularioPage,
    });
    //cuando se cierre el modal la lista de doctores ya estará actualizada
    modal.onWillDismiss().then(() => {
      this.consultarDoctores();
    })

    await modal.present();
  }

  protected listaDoctoresVacia(){
    return this.misDoctores?.length <= 0
  }

}
