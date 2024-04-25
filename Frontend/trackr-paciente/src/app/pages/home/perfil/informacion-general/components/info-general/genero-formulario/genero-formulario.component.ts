import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { GeneroSelectorDto } from '@dtos/catalogo/genero-selector-dto';
import { GeneroService } from '@http/catalogo/genero.service';
import { IonicModule, ModalController } from '@ionic/angular';
@Component({
  selector: 'app-genero-formulario',
  templateUrl: './genero-formulario.component.html',
  styleUrls: ['./genero-formulario.component.scss'],
  standalone: true,
  imports: [ 
    IonicModule,
    CommonModule
  ]
})
export class GeneroFormularioComponent implements OnInit {

  constructor(private generoService : GeneroService , private modalController : ModalController) { }


  protected generoList: GeneroSelectorDto[] = [];

  ngOnInit() {
    this.consultarGeneros();
  }

  private consultarGeneros() {
    this.generoService.consultarGeneros().subscribe(
      generos => {
        this.generoList = generos;
      }
    );
  }

  public seleccionar(genero : GeneroSelectorDto){
    this.modalController.dismiss({
      idGenero : genero.idGenero,
      nombreGenero : genero.descripcion
    });
  }

}
