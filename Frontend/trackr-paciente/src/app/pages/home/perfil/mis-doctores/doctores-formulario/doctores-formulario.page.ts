import { NgFor } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MisDoctoresService } from '@http/seguridad/mis-doctores.service';
import { AlertController, IonicModule } from '@ionic/angular';
import { UsuarioDoctorDto } from 'src/app/shared/Dtos/usuario-doctor-dto';
import { UsuarioDoctoresSelectorDto } from 'src/app/shared/Dtos/usuario-doctores-selector-dto';

@Component({
  selector: 'app-doctores-formulario',
  templateUrl: './doctores-formulario.page.html',
  styleUrls: ['./doctores-formulario.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    NgFor
  ]
})
export class DoctoresFormularioPage {

  constructor(
    private doctoresService: MisDoctoresService,
    private alertController: AlertController
  ) {}

  ionViewWillEnter() {
    this.consultarSelector();
  }

  protected currentDoctor: UsuarioDoctorDto;
  protected doctoresSelector: UsuarioDoctoresSelectorDto[];

  seleccionDoctor(ev: any) {
    this.currentDoctor = ev.target.value;
  }

  protected agregar() {

    const MENSAJE_EXITO: string = `El doctor ha sido agregado correctamente`;
    const MENSAJE_REQUERIMIENTO: string = `Seleccione un doctor`;
    
    if(this.currentDoctor == undefined)
    {
      this.presentAlert(MENSAJE_REQUERIMIENTO);
      return;
    }
    const subscription = this.doctoresService.agregar(this.currentDoctor)
      .subscribe({
        next: () => {
          this.presentAlert(MENSAJE_EXITO);
          this.consultarSelector();
        },
        error: () => { },
        complete: () => {
          subscription.unsubscribe();
        }
      }
      );
  }

  private consultarSelector() {
    this.doctoresService.consultarSelector().subscribe((data) => {
      this.doctoresSelector = data;
    })
  }

  private async presentAlert(mensaje : string) {
    const alert = await this.alertController.create({
      header: 'Mis Doctores',
      message: mensaje,
      buttons: ['OK'],
    });

    await alert.present();
  }

}
