import { SharedModule } from '@sharedComponents/shared.module';
import { EntidadEstructuraTablaValorService } from './../../../../shared/http/gestion-expediente/entidad-estructura-tabla-valor.service';
import { CommonModule } from '@angular/common';
import { Component, EventEmitter, OnChanges, OnInit, Output } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { EntidadTablaRegistroDto, TablaValorDto, TablaValorMuestraDTO } from '@dtos/gestion-entidades/entidad-tabla-registro-dto';
import { PadecimientoMuestraDTO } from '@dtos/gestion-expediente/padecimiento-muestra-dto';
import { SeccionCampoService } from '@http/gestion-expediente/seccion-campo.service';
import { IonicModule } from '@ionic/angular';
import { SeccionCampo } from '@models/gestion-expediente/seccion-campo';
import { CampoExpedienteModule } from '@sharedComponents/campo-expediente/campo-expediente.module';
import { lastValueFrom } from 'rxjs/internal/lastValueFrom';
import * as Utileria from '@utils/utileria';
import { SeccionMuestraDTO } from '@dtos/gestion-expediente/seccion-muestra-dto';

@Component({
  selector: 'app-muestras-formulario',
  templateUrl: './muestras-formulario.component.html',
  styleUrls: ['./muestras-formulario.component.scss'],
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    IonicModule,
    SharedModule,
    CommonModule,
    CampoExpedienteModule],
    providers: [SeccionCampoService]
})
export class MuestrasFormularioComponent implements OnInit {

  @Output() consultarValoresFueraRango = new EventEmitter<void>();
  protected day = new Date().getDate();
  protected recomendacion: any;
  protected dateToday: string = new Date().toISOString();
  public arbolPadecimiento: PadecimientoMuestraDTO[] = [];
  protected submitting: boolean = false;
  protected fechaSeleccionada: string = this.dateToday;
  constructor(
    private seccionCampoService: SeccionCampoService,
    private entidadEstructuraTablaValorService: EntidadEstructuraTablaValorService,
    ) { }

  ngOnInit() {
    this.consultarArbol();
  }

  protected consultarArbol(){
    lastValueFrom(this.seccionCampoService.consultarPorSeccion())
    .then((arbolPadecimiento: PadecimientoMuestraDTO[]) => {
      this.arbolPadecimiento = arbolPadecimiento;
    });
  }

  public enviarFormulario(seccion: SeccionMuestraDTO, fechaSeleccionada: string): void {
    this.submitting = true;
    const camposAgregados: TablaValorMuestraDTO[] = [];
  
    for (const seccionCampo of seccion.seccionesCampo) {
      if (seccionCampo.valor) {
        camposAgregados.push({
          claveCampo: seccionCampo.clave,
          valor: seccionCampo.valor.toString(),
          fueraDeRango: this.estaFueraDeRango(seccionCampo),
          fechaMuestra: new Date(fechaSeleccionada)
        });
      }
    }
  
    if (camposAgregados.length === 0) {
      this.submitting = false;
      return;
    }

   this.agregar(camposAgregados);
  }

  public agregar(campoAgregar: TablaValorMuestraDTO[]): void {
    this.entidadEstructuraTablaValorService.agregarMuestra(campoAgregar).subscribe(
      {
        complete : () => {
          this.consultarValoresFueraRango.emit();
        }
      }
    );
  }

  private estaFueraDeRango(campo: SeccionCampo) {
    const dominio = campo.idDominioNavigation;

    const number = Number.parseFloat(campo.valor?.toString() ?? '');

    if (Number.isNaN(number)) {
      return false;
    }

    const min = dominio.valorMinimo;
    const max = dominio.valorMaximo;

    const valor = Number(campo.valor);

    const fueraDeRango = valor < min || valor > max;

    return fueraDeRango;
  }

}
