import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { IonicModule, ModalController } from '@ionic/angular';
import { Observable } from 'rxjs';
import { PerfilTratamientoService } from '@http/gestion-perfil/perfil-tratamiento.service';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { addIcons } from 'ionicons';
import { AlertController } from '@ionic/angular/standalone';
import { AgregarTratamientoPage } from './agregar-tratamiento/agregar-tratamiento.page';
import { DetalleTratamientoComponent } from './detalle-tratamiento/detalle-tratamiento.component';
import { ExpedienteTratamientoPerfilDto } from 'src/app/shared/Dtos/gestion-perfil/expediente-tratamiento-perfil-dto';

@Component({
  selector: 'app-mis-tratamientos',
  templateUrl: './mis-tratamientos.page.html',
  styleUrls: ['./mis-tratamientos.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, RouterModule, HeaderComponent],
})
export class MisTratamientosPage implements OnInit {

  protected tratamientos$: Observable<ExpedienteTratamientoPerfilDto[]>;
  protected tratamientosFiltradosPorBusqueda: ExpedienteTratamientoPerfilDto[];
  protected tratamientos: ExpedienteTratamientoPerfilDto[];
  protected filtrando: boolean = false;

  constructor(
    private perfilTratamientoService: PerfilTratamientoService,
    private alertController: AlertController,
    private modalController: ModalController
  ) { 
    addIcons({
      'plus': 'assets/img/svg/plus.svg',
      'trash-2': 'assets/img/svg/trash-2.svg',
      'chevron-left': 'assets/img/svg/chevron-left.svg'
    }) 
  }

  public ngOnInit(): void {
    this.consultarTratamientos();
  }

  // public ionViewWillEnter() : void
  // {
  //   this.consultarTratamientos();
  // }

  protected consultarTratamientos(): void {
    this.tratamientos$ = this.perfilTratamientoService.consultarTratamientos();
    this.tratamientos$.subscribe({
      next: (data)=>{
        this.tratamientos = data;
      }
    })
  }

  protected listaTratamientosVacia(){
    return this.tratamientos?.length == 0;
  }

  protected async presentarAlertaEliminar(tratamiento: ExpedienteTratamientoPerfilDto) {
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
            this.eliminarTratamiento(tratamiento);
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

  private eliminarTratamiento(tratamiento: ExpedienteTratamientoPerfilDto){
    this.perfilTratamientoService.eliminarTratamiento(tratamiento.idExpedienteTratamiento).subscribe({
      next: ()=> {
        this.consultarTratamientos();
      },
      error: ()=> {
      },
      complete: ()=> {
        this.presentarAlertaEliminadoExitosamente();
      },
    })
  }

  protected async agregarTratamiento(){
    const modal = await this.modalController.create({
      component: AgregarTratamientoPage,
      componentProps: { accion: 'agregar' }
    })

    await modal.present();

    await modal.onWillDismiss().then(({data, role}) => {
      if(role == 'confirm'){
        this.consultarTratamientos();
      }
    })
  }

  protected async verDetalleTratamiento(_idExpedienteTratamiento: number){
    const modal = await this.modalController.create({
      component: DetalleTratamientoComponent,
      componentProps: {idExpedienteTratamiento: _idExpedienteTratamiento}
    })

    await modal.present();

    await modal.onWillDismiss().then(({data, role}) => {
      if(role == 'confirm'){
        this.consultarTratamientos();
      }
    })

  }

  protected buscarTratamiento(event: any){
    const text = event.target.value;

    text == '' ? this.filtrando = false : this.filtrando = true;
    this.tratamientosFiltradosPorBusqueda = this.tratamientos;
    if(text && text.trim() != ''){
      this.tratamientosFiltradosPorBusqueda = this.tratamientosFiltradosPorBusqueda.filter((tratamiento: ExpedienteTratamientoPerfilDto) =>{
        return (tratamiento.farmaco.toLowerCase().indexOf(text.toLowerCase()) > -1 );
      })
    }
  }

}
