import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { NgForm } from '@angular/forms';
import { EntidadEstructuraTablaValorService } from '@http/gestion-entidad/entidad-estructura-tabla-valor.service';
import { EntidadEstructuraValorService } from '@http/gestion-entidad/entidad-estructura-valor.service';
import { EntidadEstructuraService } from '@http/gestion-entidad/entidad-estructura.service';
import { EntidadService } from '@http/gestion-entidad/entidad.service';
import { DominioDetalle } from '@models/expedientes/dominio-detalle';
import { EntidadEstructura } from '@models/gestion-entidad/entidad-estructura';
import { EntidadEstructuraValor } from '@models/gestion-entidad/entidad-estructura-valor';
import { RegistroTabla } from '@models/gestion-entidad/registro-tabla';
import { SeccionCampo } from '@models/gestion-entidad/seccion-campo';
import * as Utileria from '@utils/utileria';
import * as moment from 'moment';
import { MensajeService } from '../mensaje/mensaje.service';
import { ExternalTemplate } from './external-template';

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

  constructor(
    private entidadEstructuraService : EntidadEstructuraService,
    private entidadEstructuraValorService: EntidadEstructuraValorService,
    private entidadEstructuraTablaValorService: EntidadEstructuraTablaValorService,
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
    const tabulacion = this.entidadEstructuras[index];
    const seccionesTabla : EntidadEstructura[] = tabulacion.hijos.filter(s => s.esTabla);
    const valores : EntidadEstructuraValor[] = await this.consultarEntidadEstructuraValor(tabulacion.idEntidadEstructura, this.idTabla);

    // Se identifica si la tabulación cuenta con secciones de tipo tabla
    if (seccionesTabla.length > 0) {
      // Se consultan los valores de las secciones de tipo tabla.
      const registros : RegistroTabla[] = await this.consultarRegistroTabla(tabulacion.idEntidadEstructura, this.idTabla);
      this.mapearRegistrosTabla(registros, seccionesTabla);
    }

    // Se procesan los valores estándar
    this.mapearValorInicial(valores, tabulacion.idEntidadEstructura);
  }

  private async consultarIdEntidad(clave: string) : Promise<number> {
    return this.entidadService.consultarPorClave(clave)
      .toPromise()
      .then((entidad) => { return entidad?.idEntidad ?? 0 })
      .catch(() => { return 0 })
  }

  private async consultarEntidadEstructura(idEntidad: number): Promise<void> {
    return this.entidadEstructuraService.consultarParaTabulador(idEntidad)
      .toPromise()
      .then((estructuras) => {
        this.entidadEstructuras = estructuras ?? [];
        this.loading = false;
      })
      .catch(() => {
        this.loading = false;
      });
  }

  private async consultarEntidadEstructuraValor(idEntidadEstructura: number, idTabla: number) : Promise<EntidadEstructuraValor[]> {
    const estructuras = await this.entidadEstructuraValorService
      .consultarPorTabulacion(idEntidadEstructura, idTabla)
      .toPromise()
      .catch(() => [])

    return estructuras ?? [];
  }

  private async consultarRegistroTabla(idEntidadEstructura: number, idTabla: number) : Promise<RegistroTabla[]> {
    const registros = await this.entidadEstructuraTablaValorService
      .consultarRegistroTablaPorTabulacion(idEntidadEstructura, idTabla)
      .toPromise()
      .catch(() => []);

    return registros ?? [];
  }

  private async obtenerValoresFinales(idEntidadEstructura: number) : Promise<EntidadEstructuraValor[]> {
    const tabulacion = this.entidadEstructuras.find(e => e.idEntidadEstructura == idEntidadEstructura)!;
    const secciones = tabulacion.hijos;
    let camposIniciales: SeccionCampo[] = [];

    for await (const seccion of secciones) {
      camposIniciales = camposIniciales.concat(seccion.campos);
    }

    return this.mapearCampoValorFinal(camposIniciales, idEntidadEstructura);
  }

  private async mapearValorInicial(valoresIniciales: EntidadEstructuraValor[], idEntidadEstructura: number) : Promise<void> {
    const tabulacion = this.entidadEstructuras.find(e => e.idEntidadEstructura == idEntidadEstructura)!;
    const secciones = tabulacion.hijos;
    let camposIniciales: SeccionCampo[] = [];

    for await (const seccion of secciones) {
      camposIniciales = camposIniciales.concat(seccion.campos);
    }

    for (const valor of valoresIniciales) {
      const campo = camposIniciales.find((c) => c.clave === valor.claveCampo);

      if (!campo) {
        continue;
      }

      campo.idEntidadEstructuraValor = valor.idEntidadEstructuraValor;

      if (campo.idDominioNavigation.tipoCampo === 'Date')
        campo.valor = new Date(moment(valor.valor).format('MM-DD-YYYY'));
      else
        campo.valor = valor.valor;
    }
  }

  private async mapearRegistrosTabla(registros: RegistroTabla[], seccionesTabla: EntidadEstructura[]) : Promise<void> {
    seccionesTabla.forEach(seccion => {
      seccion.registrosTabla = registros.filter(r => r.idEntidadEstructura === seccion.idEntidadEstructura);
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
