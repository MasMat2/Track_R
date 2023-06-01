import { Component, Input, OnInit, Output, SimpleChanges, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import { DominioDetalle } from '@models/expedientes/dominio-detalle';
import { EntidadEstructura } from '@models/gestion-entidad/entidad-estructura';
import { EntidadEstructuraValor } from '@models/gestion-entidad/entidad-estructura-valor';
import { SeccionCampo } from '@models/gestion-entidad/seccion-campo';
import { EntidadEstructuraValorService } from '@http/gestion-entidad/entidad-estructura-valor.service';
import { EntidadEstructuraService } from '@http/gestion-entidad/entidad-estructura.service';
import { EntidadService } from '@http/gestion-entidad/entidad.service';

import * as Utileria from '@utils/utileria';
import { MensajeService } from '../mensaje/mensaje.service';
import { ExternalTemplate } from './external-template';
import { lastValueFrom } from 'rxjs';
/**
 * Componente que por medio de la entidad que recibe, renderiza las tabulaciónes y sus contenidos en base a su estructura.
 */
@Component({
  selector: 'app-tabulador-entidad',
  templateUrl: './tabulador-entidad.component.html',
  styleUrls: ['./tabulador-entidad.component.scss']
})
export class TabuladorEntidadComponent implements OnInit {

  /** 
  * Variables de entrada de configuración
  **/ 
  @Input() public claveEntidad: string = "";
  @Input() public idTabla: number;

  /** 
  * Configuración para los tabs externos
  **/ 
  @Input() public externalTemplates: ExternalTemplate[] = [];
  @Output() public enviarFormularioExterno = new EventEmitter<boolean>();

  public MENSAJE_ERRORRES_CAMPO = 'Favor de revisar que los campos estén correctamente llenados';

  public loading: boolean = true;
  public btnSubmit: boolean = false;

  public entidadEstructuras: EntidadEstructura[] = [];
  public entidadEstructuraValores: EntidadEstructuraValor[] = [];

  constructor(
    private entidadEstructuraService : EntidadEstructuraService,
    private entidadEstructuraValorService: EntidadEstructuraValorService,
    private entidadService: EntidadService,
    private mensajeService: MensajeService
  ) { }

  public async ngOnInit() : Promise<void> {
    const idEntidad = await this.consultarIdEntidad(this.claveEntidad);
    await this.consultarEntidadEstructura(idEntidad);
    this.onSelectTabChange(0);
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if (changes['externalTemplates']) {
      this.externalTemplates = changes['externalTemplates'].currentValue;
    }

    if (changes['idTabla']) {
      this.idTabla = changes['idTabla'].currentValue;
    }
  }

  public async enviarFormulario(formulario: NgForm, idEntidadEstructura: number) : Promise<void> {
    this.btnSubmit = true;

    if (!formulario.valid) {
      Utileria.validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      this.mensajeService.modalError(this.MENSAJE_ERRORRES_CAMPO);
      return;
    }

    const valores = await this.obtenerValoresFinales(idEntidadEstructura);

    this.entidadEstructuraValorService.guardar(valores).subscribe((success) => {
      this.mensajeService.modalExito('Información guardada exitosamente');
      this.btnSubmit = false;
    }, (error) => { 
      this.btnSubmit = false 
    });
  }

  public onEnviarFormularioExterno(): void {
    this.enviarFormularioExterno.emit(true);
  }

  public async onSelectTabChange(index: number): Promise<void> {

    const externalTemplatesLength = this.externalTemplates.length;

    /**
     * Identificar si el tab pertenece a un componente externo
     * @externalTemplatesLength me indica si existen templates de componentes externos.
     * El orden en el que se presentan los tabs: tabs externos -> tabs Internos (Pestañas).
    */

    if (externalTemplatesLength > 0 && index < externalTemplatesLength) {
      return;
    }

    // Se ajusta el index si existen tabs externos, para ser utilizado en el arreglo de EntidadEstructura
    index -= externalTemplatesLength;

    // Se consultan los valores relacionados a la tabulación seleccionada
    const idEntidadEstructura = this.entidadEstructuras[index].idEntidadEstructura;
    const valores : EntidadEstructuraValor[] = await this.consultarEntidadEstructuraValor(idEntidadEstructura, this.idTabla);
    // Se procesan los valores
    this.mapearValorInicial(valores, idEntidadEstructura);
  }

  private async consultarIdEntidad(clave: string) : Promise<number> {
    return lastValueFrom(this.entidadService.consultarPorClave(clave))
      .then((entidad) => { 
        return entidad?.idEntidad ?? 0;
      })
      .catch(() => { return 0 })
  }

  private async consultarEntidadEstructura(idEntidad: number): Promise<void> {
    return lastValueFrom(this.entidadEstructuraService.consultarArbol(idEntidad))
      .then((estructuras) => {
        this.entidadEstructuras = estructuras;
        this.loading = false;
      })
      .catch(() => {
        this.loading = false;
      });
  }

  private async consultarEntidadEstructuraValor(idEntidadEstructura: number, idTabla: number) : Promise<EntidadEstructuraValor[]> {
    return lastValueFrom(this.entidadEstructuraValorService.consultarPorTabulacion(idEntidadEstructura, idTabla))
      .catch(() => []);
  }

  private async obtenerValoresFinales(idEntidadEstructura: number) : Promise<EntidadEstructuraValor[]> {
    const tabulacion = this.entidadEstructuras.find(e => e.idEntidadEstructura == idEntidadEstructura);
    const secciones = tabulacion?.hijos ?? [];
    let camposIniciales: SeccionCampo[] = [];

    for await (const seccion of secciones) {
      camposIniciales = camposIniciales.concat(seccion.campos);
    }

    return this.mapearCampoValorFinal(camposIniciales, idEntidadEstructura);
  }

  private async mapearValorInicial(valoresIniciales: EntidadEstructuraValor[], idEntidadEstructura: number) : Promise<void> {
    const tabulacion = this.entidadEstructuras.find(e => e.idEntidadEstructura == idEntidadEstructura);
    const secciones = tabulacion?.hijos ?? [];
    let camposIniciales: SeccionCampo[] = [];

    for await (const seccion of secciones) {
      camposIniciales = camposIniciales.concat(seccion.campos);
    }

    valoresIniciales.forEach((valor: EntidadEstructuraValor) => {
      const campo = camposIniciales.find((c) => c.clave === valor.claveCampo);
      if(!campo) return;
      campo.valor = valor.valor;
      campo.idEntidadEstructuraValor = valor.idEntidadEstructuraValor;
    });
  }

  private mapearCampoValorFinal(camposIniciales: SeccionCampo[], idEntidadEstructura: number) : EntidadEstructuraValor[] {
    let valoresFinales: EntidadEstructuraValor[] = [];

    const camposEditar = camposIniciales.filter((c) => c.idEntidadEstructuraValor > 0 && c.idDominioNavigation.tipoCampo !== 'Select Múltiple');
    const camposAgregar = camposIniciales.filter((c) => !(c.idEntidadEstructuraValor > 0) && c.valor !== '' && c.valor && c.idDominioNavigation.tipoCampo !== 'Select Múltiple');
    const camposListaMultiple = camposIniciales.filter((c) => c.idDominioNavigation.tipoCampo === 'Select Múltiple');

    const valoresEditar: EntidadEstructuraValor[] = camposEditar
      .map((campo: SeccionCampo) => {

        let valor : EntidadEstructuraValor = {
          idEntidadEstructuraValor : campo.idEntidadEstructuraValor,
          claveCampo : campo.clave,
          valor : campo.valor.toString(),
          idEntidadEstructura : idEntidadEstructura,
          idTabla : this.idTabla
        };

      return valor;
    });

    const valoresAgregar: EntidadEstructuraValor[] = camposAgregar
    .map((campo: SeccionCampo) => {
      
      let valor : EntidadEstructuraValor = {
        idEntidadEstructuraValor : campo.idEntidadEstructuraValor,
        claveCampo : campo.clave,
        valor : campo.valor.toString(),
        idEntidadEstructura : idEntidadEstructura,
        idTabla : this.idTabla
      };

    return valor;
  });

    // Campos selectores
    camposListaMultiple.forEach((campo : SeccionCampo) => {
      campo.listaOpciones.forEach((opcion : DominioDetalle) => {

        if (opcion.seleccionada) {

          let nuevoValor: EntidadEstructuraValor = {
            idEntidadEstructuraValor : opcion.idExpedienteCampoValor,
            claveCampo : campo.clave,
            valor : opcion.idDominioDetalle.toString(),
            idEntidadEstructura : idEntidadEstructura,
            idTabla : this.idTabla
          };

          valoresFinales.push(nuevoValor);

        }

      });
    });

    valoresFinales = valoresFinales.concat(valoresAgregar);
    valoresFinales = valoresFinales.concat(valoresEditar);

    return valoresFinales;
  }

}
