import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PadecimientoMuestraDTO } from '@dtos/gestion-expediente/padecimiento-muestra-dto';
import { SeccionCampoService } from '@http/gestion-expediente/seccion-campo.service';
import { IonicModule } from '@ionic/angular';
import { CampoExpedienteModule } from '@sharedComponents/campo-expediente/campo-expediente.module';
import { lastValueFrom } from 'rxjs/internal/lastValueFrom';

@Component({
  selector: 'app-muestras-formulario',
  templateUrl: './muestras-formulario.component.html',
  styleUrls: ['./muestras-formulario.component.scss'],
  standalone: true,
  imports: [
    FormsModule,
    IonicModule,
    CommonModule,
    CampoExpedienteModule],
    providers: [SeccionCampoService]
})
export class MuestrasFormularioComponent implements OnInit {
  protected recomendacion: any;
  protected dateToday: Date = new Date();
  protected arbolPadecimiento: PadecimientoMuestraDTO[] = [];
  constructor(
    private seccionCampoService: SeccionCampoService) { }

  async ngOnInit() {
    await this.consultarArbol();
  }

  protected async consultarArbol(){
    lastValueFrom(this.seccionCampoService.consultarPorSeccion())
    .then((arbolPadecimiento: PadecimientoMuestraDTO[]) => {
      console.log(arbolPadecimiento);
      this.arbolPadecimiento = arbolPadecimiento;
    });
  }

}
