import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { ModalController } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { AgregarTratamientoPage } from '../agregar-tratamiento/agregar-tratamiento.page';
import { ImageOnlyModalComponent } from '@sharedComponents/image-only-modal/image-only-modal.component';
import { PerfilTratamientoService } from '@http/gestion-perfil/perfil-tratamiento.service';
import { map, Observable } from 'rxjs';
import { ExpedienteTratamientoDetalleDto } from 'src/app/shared/Dtos/gestion-perfil/expediente-tratamiento-detalle-dto';
import { FechaService } from '@services/fecha.service';


interface ObjetoConsumo {
  fecha: string;
  hora: string;
}

@Component({
  selector: 'app-detalle-tratamiento',
  templateUrl: './detalle-tratamiento.component.html',
  styleUrls: ['./detalle-tratamiento.component.scss'],
  standalone: true,
  imports: [
    IonicModule, 
    CommonModule, 
    FormsModule,
  ]
})
export class DetalleTratamientoComponent  implements OnInit {

  @Input() protected idExpedienteTratamiento: number;

  protected backgroundUrl: string = "";
  protected textoRecordatorios: string = "";
  protected tratamiento$: Observable<ExpedienteTratamientoDetalleDto>;
  protected tratamiento: ExpedienteTratamientoDetalleDto;

  protected isModalBitacoraOpen: boolean = false;
  protected tratamientoFueEditado: boolean = false;
  protected ordenFiltro: 'ascendente' |'descendente' = 'ascendente';
  protected listaConsumo: ObjetoConsumo[] = [];

  constructor(
    private modalController: ModalController,
    private perfilTratamientoService: PerfilTratamientoService,
    private fechaService: FechaService
  ) { addIcons({
    'chevron-left': 'assets/img/svg/chevron-left.svg',
    'chevron-right': 'assets/img/svg/chevron-right.svg',
    'external-link': 'assets/img/svg/external-link.svg',
    'pen-line': 'assets/img/svg/pen-line.svg',
    'arrow-up': 'assets/img/svg/arrow-up.svg',
    'arrow-down': 'assets/img/svg/arrow-down.svg',
  })}

  ngOnInit() {
    this.ordenFiltro = 'ascendente';
    this.consultarDetalleTratamiento();
  }

  protected consultarDetalleTratamiento(): void {
    this.perfilTratamientoService.consultarTratamientoDetalle(this.idExpedienteTratamiento).pipe(
      map((data) => {
        if(data.horas){
          data.horas = data.horas.map(hora => this.fechaService.horaUTCAHoraLocal(hora));
        }
        return data;
      })
    ).subscribe({
      next: (data)=> {
        this.tratamiento = data;
        this.backgroundUrl = `url(data:${this.tratamiento.tipoMime};base64,${this.tratamiento.imagenBase64})`;
        this.textoRecordatorios = this.formatearTextoRecordatorios(this.tratamiento.diaSemana, this.tratamiento.horas);
      }
    })
  }

  protected cerrarModal(rol: string){
    if(this.tratamientoFueEditado){
      this.modalController.dismiss(null, 'confirm');
    }
    else{
      this.modalController.dismiss(null, rol);
    }
  }

  protected setOpen(isOpen: boolean){
    this.isModalBitacoraOpen = isOpen;
  }

  protected async abrirModalEditarTratamiento(){
    const modal = await this.modalController.create({
      component: AgregarTratamientoPage,
      componentProps: { 
        accion: 'editar',
        perfilTratamientoDto: this.tratamiento
      }
    })

    await modal.present();

    await modal.onWillDismiss().then(({data, role}) => {
      if(role == 'confirm'){
        this.tratamientoFueEditado = true;
        this.consultarDetalleTratamiento();
      }
    })
  }

  protected async abrirModalImagen(){
    const modal = await this.modalController.create({
      component: ImageOnlyModalComponent,
      cssClass: 'image-only-modal',
       componentProps: {
         archivo : this.tratamiento.imagenBase64,
         archivoTipoMime: this.tratamiento.tipoMime}
    })
    if(this.tratamiento.imagenBase64 != ""){
      await modal.present();
    }
  }

  protected cambiarFiltro(){
    this.ordenFiltro === 'ascendente' ? (this.ordenFiltro = 'descendente') : (this.ordenFiltro = 'ascendente');
    this.ordenarlista(this.ordenFiltro);
  }

  protected listaConsumoVacia(){
    return this.tratamiento.bitacora?.length == 0;
  }

  protected formatearTextoRecordatorios(diaSemana: boolean[], horas: string[]): string {
    if(horas.length === 0){
      return 'Sin recordatorios'
    }

    const dias = ['Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado', 'Domingo'];
    const horasAmPm = horas.map(hora => {
        const [hour, minute] = hora.split(':').map(Number);
        const amPm = hour < 12 ? 'a.m.' : 'p.m.';
        const formattedHour = hour % 12 || 12; // Convertir la hora a formato de 12 horas
        return `${formattedHour}:${minute.toString().padStart(2, '0')} ${amPm}`;
    });

    let scheduleText = '';
    for (let i = 0; i < diaSemana.length; i++) {
      if (diaSemana[i]) {
          scheduleText += dias[i];
          if (i < dias.length - 1 && diaSemana.slice(i + 1).includes(true)) {
              scheduleText += ', ';
          } else if (i === dias.length - 2 && diaSemana[diaSemana.length - 1]) {
              scheduleText += ' y ';
          }
      }
    }
    scheduleText += ` a las ${horasAmPm.join(', ')}`;
    return scheduleText;
  }

  protected ordenarlista(opcion: 'ascendente' | 'descendente'){
    const dates: Date[] | undefined = this.tratamiento.bitacora?.map(dateString => new Date(dateString));

    if(this.tratamiento.bitacora?.length == 0){
      return
    }

    if(opcion == 'ascendente'){
      const ascendingDates = dates?.slice().sort((a, b) => a.getTime() - b.getTime());
      this.tratamiento.bitacora = ascendingDates;
    }

    if(opcion == 'descendente'){
      const descendingDates = dates?.slice().sort((a, b) => b.getTime() - a.getTime());
      this.tratamiento.bitacora = descendingDates;
    }
  }



  

}
