import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PadecimientoMuestraDTO } from '@dtos/gestion-expediente/padecimiento-muestra-dto';
import { SeccionCampoService } from '@http/gestion-expediente/seccion-campo.service';
import { IonicModule } from '@ionic/angular';
import { lastValueFrom } from 'rxjs/internal/lastValueFrom';

@Component({
  selector: 'app-muestras-formulario',
  templateUrl: './muestras-formulario.component.html',
  styleUrls: ['./muestras-formulario.component.scss'],
  standalone: true,
  imports: [
    FormsModule,
    IonicModule,
    CommonModule],
    providers: [SeccionCampoService]
})
export class MuestrasFormularioComponent implements OnInit {
  protected recomendacion: any;
  protected dateToday: Date = new Date();
  protected arbolPadecimiento: PadecimientoMuestraDTO[] = [];
  constructor(
    private seccionCampoService: SeccionCampoService) { }

  ngOnInit() {
    this.consultarArbol();
  }

  protected consultarArbol(){
    lastValueFrom(this.seccionCampoService.consultarPorSeccion())
    .then((arbolPadecimiento: PadecimientoMuestraDTO[]) => {
      console.log(arbolPadecimiento);
      this.arbolPadecimiento = arbolPadecimiento;
    });
  }

}
