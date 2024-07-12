import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ExpedientePadecimientoDto } from '@dtos/seguridad/expediente-padecimiento-dto';
import { ExpedientePadecimientoService } from '@http/gestion-expediente/expediente-padecimiento.service';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { IonicModule } from '@ionic/angular';
import { AlertController, ModalController } from '@ionic/angular/standalone';
import { Constants } from '@utils/constants/constants';
import { addIcons } from 'ionicons';
import { DiagnosticoFormularioComponent } from './diagnostico-formulario/diagnostico-formulario.component';



@Component({
  selector: 'app-info-diagnosticos',
  templateUrl: './info-diagnosticos.component.html',
  styleUrls: ['./info-diagnosticos.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    RouterModule
  ]
})
export class InfoDiagnosticosComponent  implements OnInit {

  protected misDiagnosticos: ExpedientePadecimientoDto[];
  
  constructor(
    private alertController: AlertController,
    private modalController: ModalController,
    private usuarioService: UsuarioService,
    private expedientePadecimientoService: ExpedientePadecimientoService
  ) { 
    addIcons({
      'chevron-left': 'assets/img/svg/chevron-left.svg',
      'plus': 'assets/img/svg/plus.svg',
      'trash': 'assets/img/svg/trash-2.svg',
    }) }

  ngOnInit() {
    this.consultarDiagnosticos();
  }



  private consultarDiagnosticos(){
    this.usuarioService.consultarDiagnosticosUsuario().subscribe({
      next:(value) => {
        this.misDiagnosticos = value;
      },
    })
  }

  protected async presentarAlertaEliminarDiagnostico(diagnostico: ExpedientePadecimientoDto) {
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
          handler: ()=> {
            this.eliminarAntecedente(diagnostico);
          }
          },
      ]
    });

    await alert.present();
  }

  protected async presentarAlertaEliminadoExitosamente() {

    const alertSuccess = await this.alertController.create({
      header: 'Elemento eliminado exitosamente',
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm',
      }],
      cssClass: 'custom-alert color-primary icon-check',
    });

    await alertSuccess.present();
  }

  protected async AgregarDiagnostico(){
    const modal = await this.modalController.create({
      component: DiagnosticoFormularioComponent,
      componentProps: { diagnosticosUsuario: this.misDiagnosticos },
      breakpoints : [0, 1],
      initialBreakpoint: 1,
      cssClass: 'custom-sheet-modal'
    });
    modal.onWillDismiss().then(() => {
      this.consultarDiagnosticos();
    })

    await modal.present();
  }

  private eliminarAntecedente(antecedente: ExpedientePadecimientoDto){
    this.expedientePadecimientoService.eliminar(antecedente.idExpedientePadecimiento).subscribe({
      next: () => {
        this.consultarDiagnosticos();
      },
      error: () => {

      },
      complete: () => {
        this.presentarAlertaEliminadoExitosamente();
      }
    });
  }

  protected listaDiagnosticosVacia(){
    return this.misDiagnosticos?.length <= 0
  }
  
}
