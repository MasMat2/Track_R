import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { GeneroSelectorDto } from '@dtos/catalogo/genero-selector-dto';
import { IonicModule, ModalController } from '@ionic/angular';
import { GenericoSelectorDto } from 'src/app/shared/Dtos/catalogo/generico-selector-dto';
@Component({
  selector: 'app-catalogo-formulario',
  templateUrl: './catalogo-formulario.component.html',
  styleUrls: ['./catalogo-formulario.component.scss'],
  standalone: true,
  imports: [ 
    IonicModule,
    CommonModule
  ]
})
export class CatalogoFormularioComponent implements OnInit {

  constructor( private modalController : ModalController) { }


  protected catalogoList: GenericoSelectorDto[] = [];

  ngOnInit() {
  }


  public seleccionar(genero : GenericoSelectorDto){
    this.modalController.dismiss({
      id : genero.id,
      descripcion: genero.descripcion
    });
  }

}
