import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { IonicModule, ModalController } from '@ionic/angular';
import { map, Observable } from 'rxjs';

import { PerfilTratamientoService } from '@http/gestion-perfil/perfil-tratamiento.service';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { addIcons } from 'ionicons';
import { PerfilTratamientoDto } from '@dtos/gestion-perfil/perfil-tratamiento-dto';
import { AlertController } from '@ionic/angular/standalone';
import { AgregarTratamientoPage } from './agregar-tratamiento/agregar-tratamiento.page';
import { ro } from 'date-fns/locale';

// interface Tratamiento extends PerfilTratamientoDto {
//   expandido: boolean;
// }

@Component({
  selector: 'app-mis-tratamientos',
  templateUrl: './mis-tratamientos.page.html',
  styleUrls: ['./mis-tratamientos.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, RouterModule, HeaderComponent],
})
export class MisTratamientosPage implements OnInit {

  protected tratamientos$: Observable<PerfilTratamientoDto[]>;
  //protected tratamientosFiltrados$: Observable<PerfilTratamientoDto[]>;

  protected tratamientosFiltradosPorBusqueda: PerfilTratamientoDto[];
  protected tratamientos: PerfilTratamientoDto[];

  //protected weekDays: string[] = ['Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá', 'Do'];
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
    //this.consultarTratamientos();
  }

  public ionViewWillEnter() : void
  {
    this.consultarTratamientos();
  }

  protected consultarTratamientos(): void {
    this.tratamientos$ = this.perfilTratamientoService.consultarTratamientos();

    this.tratamientos$.subscribe({
      next: (data)=>{
        this.tratamientos = data;
      }
    })

    // this.tratamientos$ = this.perfilTratamientoService.consultarTratamientos().pipe(
    //   map(tratamientos =>
    //     tratamientos.map(tratamiento => ({
    //       ...tratamiento,
    //       expandido: false
    //     }))
    //   )
    // );
  }

  // public toggleExpandido(tratamiento: Tratamiento) {
  //   tratamiento.expandido = !tratamiento.expandido;
  // }

  protected async presentarAlertaEliminar(tratamiento: PerfilTratamientoDto) {
    const alert = await this.alertController.create({
      header: '¿Seguro que deseas eliminar este elemento?',
      subHeader: 'No podrás recuperarlo',
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
        role: 'confirm'
      }],
      cssClass: 'custom-alert-success',
    });

    await alertSuccess.present();
  }

  private eliminarTratamiento(tratamiento: PerfilTratamientoDto){
    console.log('Eliminando tratamiento');
    console.error(tratamiento);
  }

  protected async agregarTratamiento(){
    const modal = await this.modalController.create({
      component: AgregarTratamientoPage,
    })

    await modal.present();

    await modal.onWillDismiss().then(({data, role}) => {
      console.log(role)

      if(role == 'confirm'){
        this.consultarTratamientos();
      }
    })

  }

  protected buscarTratamiento(event: any){
    console.log(this.filtrando);
    const text = event.target.value;

    text == '' ? this.filtrando = false : this.filtrando = true; //el filtrado se activa cuando hay texto ingresado
    this.tratamientosFiltradosPorBusqueda = this.tratamientos;
    if(text && text.trim() != ''){
      this.tratamientosFiltradosPorBusqueda = this.tratamientosFiltradosPorBusqueda.filter((tratamiento: PerfilTratamientoDto) =>{
        return (tratamiento.farmaco.toLowerCase().indexOf(text.toLowerCase()) > -1 );
      })
    }
  }

}
