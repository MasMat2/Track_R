import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MisDoctoresService } from '@http/gestion-expediente/mis-doctores.service';
import { AlertController, IonicModule } from '@ionic/angular';
import { UsuarioDoctorDto } from 'src/app/shared/Dtos/usuario-doctor-dto';
import { UsuarioDoctoresSelectorDto } from 'src/app/shared/Dtos/usuario-doctores-selector-dto';
import { addIcons } from 'ionicons';
import { FormsModule } from '@angular/forms';
import { ModalController } from '@ionic/angular/standalone';
import { LoadingSpinnerService } from 'src/app/services/dashboard/loading-spinner.service';

@Component({
  selector: 'app-doctores-formulario',
  templateUrl: './doctores-formulario.page.html',
  styleUrls: ['./doctores-formulario.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    FormsModule,
    CommonModule,
  ]
})
export class DoctoresFormularioPage implements OnInit  {

  protected currentDoctor: UsuarioDoctorDto;
  protected doctoresSelector: UsuarioDoctoresSelectorDto[];

  constructor(
    private doctoresService: MisDoctoresService,
    private alertController: AlertController,
    private modarCtrl: ModalController,
    private loadingSpinnerService : LoadingSpinnerService
  ) {
    addIcons({
      'x': 'assets/img/svg/x.svg',
      'check': 'assets/img/svg/check.svg'
    })
  }

  ngOnInit(): void {
  }

  ionViewWillEnter() {
    this.consultarSelector();
  }

  protected seleccionDoctor(doctor: any) {
    this.doctorSeleccionado();
    this.currentDoctor = doctor;
    this.doctorSeleccionado();
  }

  protected agregar() {
    const subscription = this.doctoresService.agregar(this.currentDoctor)
      .subscribe({
        next: () => {
          this.consultarSelector();
        },
        error: () => {
        },
        complete: () => {
          this.presentAlertSuccess();
          subscription.unsubscribe();
        }
      }
      );
  }

  private consultarSelector() {
    this.loadingSpinnerService.presentLoading();
    this.doctoresService.consultarSelector().subscribe({
      next: (doctores) => {
        this.doctoresSelector = doctores;
      },
      error: () => {
        this.loadingSpinnerService.dismissLoading();
      },
      complete: ()=> {
        this.loadingSpinnerService.dismissLoading();
      }
    })
  }

  protected cerrarModal(data: any, role: string){
    this.modarCtrl.dismiss(data, role);
  }

  protected async presentAlertSuccess() {
    const alertSuccess = await this.alertController.create({
      header: 'Doctor Asignado',
      subHeader: 'El doctor ha sido asignado exitosamente.',
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm',
        handler: () => {
          this.cerrarModal(null, 'confirm');
        }
      }],
      cssClass: 'custom-alert color-primary icon-check',
    });

    await alertSuccess.present();
  }

  protected listaDoctoresVacia(){
    return (this.doctoresSelector?.length <= 0);
  }

  protected doctorSeleccionado(){
    return this.currentDoctor !== undefined;
  }

}
