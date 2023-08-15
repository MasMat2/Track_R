import { Component, OnInit } from '@angular/core';
import { AlertController, IonicModule } from '@ionic/angular';
import { MisDoctoresService } from '@http/seguridad/mis-doctores.service';
import { UsuarioDoctoresDto } from '../../../../shared/Dtos/usuario-doctores-dto';
import { CommonModule, NgFor } from '@angular/common';
import { UsuarioDoctoresSelectorDto } from 'src/app/shared/Dtos/usuario-doctores-selector-dto';
import { UsuarioDoctorDto } from 'src/app/shared/Dtos/usuario-doctor-dto';
import { DoctoresFormularioPage } from './doctores-formulario/doctores-formulario.page';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-mis-doctores',
  templateUrl: './mis-doctores.page.html',
  styleUrls: ['./mis-doctores.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    NgFor,
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
    private alertController: AlertController
  ) { }



  ionViewWillEnter() {
    this.consultarDoctores();
  }

  consultarDoctores() {
    this.doctoresService.consultarExpediente().subscribe((data => {
      this.misDoctores = data;
    }));
  }

  protected eliminar(doctor: UsuarioDoctorDto) {
    const subscription = this.doctoresService.eliminar(doctor)
      .subscribe({
        next: () => {
          this.consultarDoctores();
          this.presentAlert();
        },
        error: () => {
        },
        complete: () => {
          subscription.unsubscribe();
        }
      });
  }

  private async presentAlert() {
    const alert = await this.alertController.create({
      header: 'Doctor eliminado',
      message: 'El doctor se eliminó correctamente',
      buttons: ['OK'],
    });

    await alert.present();
  }

}
