import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IonicModule, ModalController } from '@ionic/angular';
import { AlertController } from '@ionic/angular/standalone';
import { Constants } from '@utils/constants/constants';
import { addIcons } from 'ionicons';
import { chevronBack, add, trashOutline} from 'ionicons/icons';
import { AntecedenteFormularioComponent } from './antecedente-formulario/antecedente-formulario.component';
import { ExpedientePadecimientoDto } from '@dtos/seguridad/expediente-padecimiento-dto';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { ExpedientePadecimientoService } from '@http/gestion-expediente/expediente-padecimiento.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-info-antecedentes',
  templateUrl: './info-antecedentes.component.html',
  styleUrls: ['./info-antecedentes.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    RouterModule
  ]
})
export class InfoAntecedentesComponent  implements OnInit {

  protected misAntecedentes: ExpedientePadecimientoDto[];

  constructor(
    private alertController: AlertController,
    private modalController: ModalController,
    private usuarioService: UsuarioService,
    private expedientePadecimientoService: ExpedientePadecimientoService
  ) { addIcons({chevronBack, add, trashOutline}) }

  ngOnInit() {
    this.consultarAntecedentes();
  }

  private consultarAntecedentes(){
    this.usuarioService.consultarAntecedentesUsuario().subscribe({
      next:(value) => {
        this.misAntecedentes = value;
      },
    })
  }

  protected async presentarAlertaEliminarAntecedente(antecedente: ExpedientePadecimientoDto) {
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
            this.eliminarAntecedente(antecedente);
          }
          },
      ]
    });

    await alert.present();
  }

  protected async presentarAlertaEliminadoExitosamente() {

    const alertSuccess = await this.alertController.create({
      header: 'Elemento eliminado exitosamente',
      message: Constants.ALERT_SUCCESS,
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm',
      }],
      cssClass: 'custom-alert-success',
    });

    await alertSuccess.present();
  }

  protected async AgregarAntecedente(){
    const modal = await this.modalController.create({
      component: AntecedenteFormularioComponent,
      componentProps: {antecedentesUsuario: this.misAntecedentes},
      breakpoints : [0, 1],
      initialBreakpoint: 1,
      cssClass: 'custom-sheet-modal'
      
    });
    modal.onWillDismiss().then(() => {
      this.consultarAntecedentes();
    })

    await modal.present();
  }

  private eliminarAntecedente(antecedente: ExpedientePadecimientoDto){
    this.expedientePadecimientoService.eliminar(antecedente.idExpedientePadecimiento).subscribe({
      next: () => {
        this.consultarAntecedentes();
      },
      error: () => {

      },
      complete: () => {
        this.presentarAlertaEliminadoExitosamente();
      }
    });
  }

  protected listaAntecedentesVacia(){
    return this.misAntecedentes?.length <= 0
  }
  
}
