import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IonicModule, ModalController } from '@ionic/angular';
import { AlertController } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { AntecedenteFormularioComponent } from './antecedente-formulario/antecedente-formulario.component';
import { ExpedientePadecimientoDto } from '@dtos/seguridad/expediente-padecimiento-dto';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { ExpedientePadecimientoService } from '@http/gestion-expediente/expediente-padecimiento.service';
import { RouterModule } from '@angular/router';
import { LoadingSpinnerService } from 'src/app/services/dashboard/loading-spinner.service';

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
  protected eliminandoAntecedente: boolean = false;

  constructor(
    private alertController: AlertController,
    private modalController: ModalController,
    private usuarioService: UsuarioService,
    private expedientePadecimientoService: ExpedientePadecimientoService,
    private loadingSpinner: LoadingSpinnerService
  ) { 
    addIcons({
      'chevron-left': 'assets/img/svg/chevron-left.svg',
      'plus': 'assets/img/svg/plus.svg',
      'trash': 'assets/img/svg/trash-2.svg'
    }) 
  }

  ngOnInit() {
    this.consultarAntecedentes();
  }

  private consultarAntecedentes(){
    this.loadingSpinner.presentLoading();
    this.usuarioService.consultarAntecedentesUsuario().subscribe({
      next:(value) => {
        this.misAntecedentes = value;
      },
      error: () => {
        this.loadingSpinner.dismissLoading();
      },
      complete: () => {
        this.loadingSpinner.dismissLoading();
      }
    })
  }

  protected async presentarAlertaEliminarAntecedente(antecedente: ExpedientePadecimientoDto) {
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
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm',
      }],
      cssClass: 'custom-alert color-primary icon-check',
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

    await modal.present();

    const {data, role} = await modal.onWillDismiss();
    if(role == "confirm"){
      this.consultarAntecedentes();
    }
    else{
      return
    }
  }

  private eliminarAntecedente(antecedente: ExpedientePadecimientoDto){
    this.expedientePadecimientoService.eliminar(antecedente.idExpedientePadecimiento).subscribe({
      next: () => {
        this.eliminandoAntecedente = true;
      },
      error: () => {
        this.eliminandoAntecedente = false;
      },
      complete: () => {
        this.eliminandoAntecedente = false;
        this.consultarAntecedentes();
        this.presentarAlertaEliminadoExitosamente();
      }
    });
  }

  protected listaAntecedentesVacia(){
    return this.misAntecedentes?.length <= 0
  }
  
}
