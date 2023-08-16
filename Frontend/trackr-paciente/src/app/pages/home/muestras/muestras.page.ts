import { SharedModule } from '@sharedComponents/shared.module';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ValoresFueraRangoGridDTO } from '@dtos/gestion-expediente/valores-fuera-rango-grid-dto';
import { EntidadEstructuraTablaValorService } from '@http/gestion-expediente/entidad-estructura-tabla-valor.service';
import { IonicModule } from '@ionic/angular';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { lastValueFrom } from 'rxjs';
import { HeaderComponent } from '../layout/header/header.component';
import { MuestrasFormularioComponent } from './muestras-formulario/muestras-formulario.component';

@Component({
  selector: 'app-muestras',
  templateUrl: './muestras.page.html',
  styleUrls: ['./muestras.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    FormsModule,
    HeaderComponent,
    GridGeneralModule,
    MuestrasFormularioComponent,
    SharedModule
  ],
  providers: [
    EntidadEstructuraTablaValorService,
  ]
})
export class MuestrasPage implements OnInit {
  // Variables
  protected valoresFueraRango: ValoresFueraRangoGridDTO[];
  protected HEADER_GRID = 'Valores Fuera de Rango';
  protected idPadecimiento: number;

  // Configuración Columnas Data Grid
  protected columns = [
    { headerName: 'Sección', field: 'variable', minWidth: 150 },
    { headerName: 'Campo', field: 'parametro', minWidth: 150 },
    { headerName: 'Fecha & Hora', field: 'fechaHora', minWidth: 150 },
    { headerName: 'Valor', field: 'valorRegistrado', minWidth: 150 },
  ];

  constructor(
    private entidadEstructuraTablaValorService: EntidadEstructuraTablaValorService,
    ) { }

  ngOnInit() {
    this.consultarValoresFueraRango();
  }

  private consultarValoresFueraRango(): void {
    lastValueFrom(this.entidadEstructuraTablaValorService.consultarValoresFueraRangoUsuarioSesion())
      .then((valoresFueraRango: ValoresFueraRangoGridDTO[]) => {
        console.log(valoresFueraRango);
        this.valoresFueraRango = valoresFueraRango;
      }
    );
  }

}
