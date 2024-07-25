import { Component } from '@angular/core';
import { AlertController, IonicModule, ModalController } from '@ionic/angular';
import { MisDoctoresService } from '@http/gestion-expediente/mis-doctores.service';
import { UsuarioDoctoresDto } from '../../../../shared/Dtos/usuario-doctores-dto';
import { CommonModule } from '@angular/common';
import { UsuarioDoctoresSelectorDto } from 'src/app/shared/Dtos/usuario-doctores-selector-dto';
import { UsuarioDoctorDto } from 'src/app/shared/Dtos/usuario-doctor-dto';
import { DoctoresFormularioPage } from './doctores-formulario/doctores-formulario.page';
import { RouterModule } from '@angular/router';
import { addIcons } from 'ionicons';
import { FormsModule } from '@angular/forms';
import { LoadingSpinnerService } from 'src/app/services/dashboard/loading-spinner.service';

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
    RouterModule,
  ],
})
export class MisDoctoresPage {
  protected misDoctores: UsuarioDoctoresDto[];
  protected doctoresSelector: UsuarioDoctoresSelectorDto[];
  protected currentDoctor: UsuarioDoctorDto;
  protected eliminandoDoctor: boolean = false;

  constructor(
    private doctoresService: MisDoctoresService,
    private alertController: AlertController,
    private modalCtrl: ModalController,
    private loadingSpinner: LoadingSpinnerService
  ) {
    addIcons({
     'chevron-left': 'assets/img/svg/chevron-left.svg',
      'plus': 'assets/img/svg/plus.svg',
      'trash': 'assets/img/svg/trash-2.svg'
    });
  }

  ionViewWillEnter() {
    this.consultarDoctores();
  }

  consultarDoctores() {
    this.loadingSpinner.presentLoading();

    this.doctoresService.consultarExpediente().subscribe({
      next: (doctores) => {
        this.misDoctores = doctores;
      },
      error: () => {
        this.loadingSpinner.dismissLoading();
      },
      complete: () => {
        this.loadingSpinner.dismissLoading();
      }
    })
  }

  private eliminarDoctor(doctor: UsuarioDoctorDto) {
    this.doctoresService.eliminar(doctor).subscribe({
      next: () => {
        this.eliminandoDoctor = true;
      },
      error: () => {
        this.eliminandoDoctor = false;
      },
      complete: () => {
        this.eliminandoDoctor = false;
        this.consultarDoctores();
        this.presentarAlertaEliminadoExitosamente();
      },
    });
  }

  protected async presentarAlertaEliminar(doctor: UsuarioDoctorDto) {
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
          handler: () => {
            this.eliminarDoctor(doctor);
          },
        },
      ],
    });

    await alert.present();
  }

  protected async presentarAlertaEliminadoExitosamente() {
    const alertSuccess = await this.alertController.create({
      header: 'Elemento eliminado exitosamente',
      buttons: [
        {
          text: 'De acuerdo',
          role: 'confirm',
        },
      ],
      cssClass: 'custom-alert color-primary icon-check',
    });

    await alertSuccess.present();
  }

  protected async AgregarDoctor() {
    const modal = await this.modalCtrl.create({
      component: DoctoresFormularioPage,
    });

    await modal.present();

    const {data, role} = await modal.onWillDismiss();

    if(role == 'confirm'){
      this.consultarDoctores();
    }
  }

  protected listaDoctoresVacia() {
    return this.misDoctores?.length <= 0;
  }
}
