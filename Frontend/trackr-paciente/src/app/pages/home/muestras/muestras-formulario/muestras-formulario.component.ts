import { SharedModule } from '@sharedComponents/shared.module';
import { EntidadEstructuraTablaValorService } from './../../../../shared/http/gestion-expediente/entidad-estructura-tabla-valor.service';
import { CommonModule } from '@angular/common';
import { Component, OnChanges, OnInit } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { EntidadTablaRegistroDto, TablaValorDto, TablaValorMuestraDTO } from '@dtos/gestion-entidades/entidad-tabla-registro-dto';
import { PadecimientoMuestraDTO } from '@dtos/gestion-expediente/padecimiento-muestra-dto';
import { SeccionCampoService } from '@http/gestion-expediente/seccion-campo.service';
import { IonicModule } from '@ionic/angular';
import { SeccionCampo } from '@models/gestion-expediente/seccion-campo';
import { CampoExpedienteModule } from '@sharedComponents/campo-expediente/campo-expediente.module';
import { lastValueFrom } from 'rxjs/internal/lastValueFrom';
import * as Utileria from '@utils/utileria';

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
  protected recomendacion: any;
  protected dateToday: Date = new Date();
  public arbolPadecimiento: PadecimientoMuestraDTO[] = [];
  protected submitting: boolean = false;
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
      // for (let padecimiento of arbolPadecimiento) {
      //   for (let seccion of padecimiento.seccionMuestraDTOs) {
      //     seccion.seccionesCampo = seccion.seccionesCampo.filter(x => x != null);
      //   }
      // }
      this.arbolPadecimiento = arbolPadecimiento;
    });
  }

  public enviarFormulario(formulario: NgForm, seccionCampo: SeccionCampo): void {
    this.submitting = true;
    console.log(formulario);

    if (!formulario.valid) {
      this.submitting = false;
      Utileria.validarCamposRequeridos(formulario);
      return;
    }
    console.log(formulario, seccionCampo);
    const campoAgregar: TablaValorMuestraDTO = {
      claveCampo: seccionCampo.clave,
      valor: seccionCampo.valor?.toString() ?? '',
      fueraDeRango: this.estaFueraDeRango(seccionCampo)
    };
    console.log(campoAgregar);
    this.agregar(campoAgregar);
  }

  public agregar(campoAgregar: TablaValorMuestraDTO): void {
    this.entidadEstructuraTablaValorService.agregarMuestra(campoAgregar).subscribe();
    // const registro: EntidadTablaRegistroDto = {
    //   numero: 0,
    //   idEntidadEstructura: this.idPestanaSeccion,
    //   valores: [],
    // };

    // const camposAgregar = this.campos.filter(
    //   (c) =>
    //     !(c.idEntidadEstructuraValor > 0) &&
    //     c.valor !== '' &&
    //     c.valor &&
    //     c.idDominioNavigation.tipoCampo !== 'Select Múltiple'
    // );

    // registro.valores = camposAgregar.map(
    //   (campo: SeccionCampo) => {
    //     const valor: TablaValorDto = {
    //       idEntidadEstructuraTablaValor: 0,
    //       claveCampo: campo.clave,
    //       valor: campo.valor?.toString() ?? '',
    //       fueraDeRango: this.estaFueraDeRango(campo)
    //     };

    //     return valor;
    //   }
    // );

    // const subscription = this.entidadEstructuraTablaValorService
    //   .agregar(registro)
    //   .subscribe({
    //     next: () => {
    //       // this.mensajeService.modalExito('Se agregó el registro correctamente');
    //       subscription.unsubscribe();
    //     }
    //   });
    console.log(this.arbolPadecimiento);
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
