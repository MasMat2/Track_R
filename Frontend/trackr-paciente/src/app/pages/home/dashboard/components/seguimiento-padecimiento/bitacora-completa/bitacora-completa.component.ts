import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ValoresFueraRangoGridDTO } from '@dtos/gestion-expediente/valores-fuera-rango-grid-dto';
import { IonicModule } from '@ionic/angular';
import { ModalController } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { ValoresClaveCampoGridDto } from '../../../../../../shared/Dtos/gestion-entidades/valores-clave-campo-grid-dto';

@Component({
  selector: 'app-bitacora-completa',
  templateUrl: './bitacora-completa.component.html',
  styleUrls: ['./bitacora-completa.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    FormsModule
  ]
})
export class BitacoraCompletaComponent  implements OnInit {

  protected valoresCampo: ValoresClaveCampoGridDto;
  protected valoresCampoFiltrados = new ValoresClaveCampoGridDto;
  protected padecimiento: string;
  protected variable: string ;
  protected periodo: string;

  protected filtradoPorFecha: boolean = false;
  protected fechaFiltro: Date;
  protected ordenFiltro: 'ascendente' |'descendente' = 'ascendente';


  constructor(
    private modalController: ModalController
  ) {
    addIcons({
      'calendar': 'assets/img/svg/calendar.svg',
      'arrow-down': 'assets/img/svg/arrow-down.svg',
      'arrow-up': 'assets/img/svg/arrow-up.svg'
    })
   }

  ngOnInit() {
  }

  protected regresarBtn(){
    this.filtradoPorFecha = false;
    this.valoresCampoFiltrados = new ValoresClaveCampoGridDto;
    this.modalController.dismiss();
  }

  protected cambiarFiltroOrden(){
    this.ordenFiltro === 'ascendente' ? (this.ordenFiltro = 'descendente') : (this.ordenFiltro = 'ascendente');
    this.ordenarlista(this.ordenFiltro);
  }

  
  protected ordenarlista(opcion: 'ascendente' | 'descendente'){
    this.filtradoPorFecha = false;
    this.valoresCampo.valores.sort((a, b) => {
      const fechaA = new Date(a.fechaMuestra).getTime();
      const fechaB = new Date(b.fechaMuestra).getTime();

      if (opcion === 'ascendente') {
        return fechaA - fechaB;
      }
      if(opcion === 'descendente') {
        return fechaB - fechaA;
      }

      return 0;
    });

  }

  protected filtrarPorFecha(){
    this.filtradoPorFecha = true;
    const targetDate = new Date(this.fechaFiltro).toISOString().split('T')[0]; // Solo toma la parte de la fecha, ignorando la hora
    const coincidencias = this.valoresCampo?.valores.filter(obj => obj.fechaMuestra.split('T')[0] === targetDate);
    this.valoresCampoFiltrados.valores = coincidencias;
  }

  protected listaVacia(){
    if(this.filtradoPorFecha){
      return (this.valoresCampoFiltrados.valores.length == 0)
    }
    else{
      return (this.valoresCampo.valores.length == 0)
    }
  }

}
