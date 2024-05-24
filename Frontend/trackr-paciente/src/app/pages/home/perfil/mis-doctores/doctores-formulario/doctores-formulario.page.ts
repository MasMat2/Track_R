import { CommonModule } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MisDoctoresService } from '@http/gestion-expediente/mis-doctores.service';
import { AlertController, IonicModule } from '@ionic/angular';
import { UsuarioDoctorDto } from 'src/app/shared/Dtos/usuario-doctor-dto';
import { UsuarioDoctoresSelectorDto } from 'src/app/shared/Dtos/usuario-doctores-selector-dto';
import { addIcons } from 'ionicons';
import { close, checkmark } from 'ionicons/icons';
import { FormsModule } from '@angular/forms';
import { ModalController } from '@ionic/angular/standalone';
import { Constants } from '@utils/constants/constants';
import { BehaviorSubject } from 'rxjs';

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

  private cargandoSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private cargando$ = this.cargandoSubject.asObservable();
  private loading: any;

  constructor(
    private doctoresService: MisDoctoresService,
    private alertController: AlertController,
    private modarCtrl: ModalController,
  ) {addIcons({close, checkmark})}

  ngOnInit(): void {
    this.cargando$.subscribe(cargando => {
      if (cargando) {
        this.presentLoading();
      } else {
        this.dismissLoading();
      }
    });
  }

  ionViewWillEnter() {
    this.consultarSelector();
  }

  async presentLoading() {
    this.loading = await this.alertController.create({
      cssClass: "custom-alert-loading"
    })
    return await this.loading.present();
  }

  async dismissLoading() {
    if (this.loading) {
      await this.loading.dismiss();
      this.loading = null;
    }
  }

  protected seleccionDoctor(doctor: any) {
    this.currentDoctor = doctor;
  }

  protected agregar() {
    this.cargandoSubject.next(true);

    const MENSAJE_REQUERIMIENTO: string = `Seleccione un doctor`;
    
    if(this.currentDoctor == undefined)
    {
      this.presentAlert(MENSAJE_REQUERIMIENTO);
      return;
    }
    const subscription = this.doctoresService.agregar(this.currentDoctor)
      .subscribe({
        next: () => {
          this.consultarSelector();
        },
        error: () => {
          this.cargandoSubject.next(false);
          
         },
        complete: () => {
          this.cargandoSubject.next(false);
          this.presentAlertSuccess();
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
      subHeader: mensaje,
      buttons: ['OK'],
      cssClass: 'custom-alert color-error icon-info'
    });

    await alert.present();
  }

  protected cerrarModal(){
    this.modarCtrl.dismiss();
  }

  protected async presentAlertSuccess() {

    const alertSuccess = await this.alertController.create({
      header: 'Doctor Asignado',
      subHeader: 'El doctor ha sido asignado exitosamente.',
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm',
        handler: () => {
          this.cerrarModal();
        }
      }],
      cssClass: 'custom-alert color-primary icon-check',
    });

    await alertSuccess.present();
  }

}
