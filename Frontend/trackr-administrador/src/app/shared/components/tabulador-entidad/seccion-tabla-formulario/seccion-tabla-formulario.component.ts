import { Component, EventEmitter, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { EntidadEstructuraTablaValorService } from '@http/gestion-entidad/entidad-estructura-tabla-valor.service';
import { SeccionCampo } from '@models/gestion-entidad/seccion-campo';
import { EntidadTablaRegistroDto, TablaValorDto } from '../../../dtos/gestion-entidades/entidad-tabla-registro-dto';
import { MensajeService } from '../../mensaje/mensaje.service';
import * as Utileria from '@utils/utileria';

@Component({
  selector: 'app-seccion-tabla-formulario',
  templateUrl: './seccion-tabla-formulario.component.html',
  styleUrls: ['./seccion-tabla-formulario.component.scss'],
})
export class SeccionTablaFormularioComponent implements OnInit {

  public accion: 'Agregar' | 'Editar' = 'Agregar';

  public idTabla: number;
  public numeroRegistro: number;
  public nombreSeccion: string;
  public idPestanaSeccion: number;
  public campos: SeccionCampo[] = [];

  public cerrar: EventEmitter<void> = new EventEmitter<void>();

  public submiting: boolean = false;

  constructor(
    private entidadEstructuraTablaValorService: EntidadEstructuraTablaValorService,
    private mensajeService: MensajeService
  ) {}

  public grupos: {
    nombre: string,
    fila: number;
    campos: SeccionCampo[]
  }[];

  ngOnInit() {
    if (this.accion === 'Agregar') {
      for (const campo of this.campos) {
        campo.valor = campo.idDominioNavigation.tipoDato === 'List'
          ? undefined
          : '';
      }
    }
    else {
      for (const campo of this.campos) {
        if (campo.idDominioNavigation.tipoDato === 'List') {
          campo.valor = Number(campo.valor);
        }
      }
    }


    const result: { [key: string]: SeccionCampo[] } = this.campos.reduce((r, a) => {
        r[a.grupo] = r[a.grupo] || [];
        r[a.grupo].push(a);
        return r;
    }, {} as { [key: string]: SeccionCampo[] });

    this.grupos = Object.keys(result)
      .map((groupKey) => {
        const campos = result[groupKey];
        const fila = campos[0].fila;

        return {
          nombre: groupKey,
          fila: fila,
          campos: campos,
        };
      })
      .sort((a, b) => b.fila - a.fila);
  }

  public enviarFormulario(formulario: NgForm): void {
    this.submiting = true;

    if (!formulario.valid) {
      this.submiting = false;
      Utileria.validarCamposRequeridos(formulario);
      return;
    }

    if (this.accion === 'Editar') {
      this.editar();
    } else {
      this.agregar();
    }
  }

  public editar(): void {
    const registro: EntidadTablaRegistroDto = {
      numero: this.numeroRegistro,
      idEntidadEstructura: this.idPestanaSeccion,
      idTabla: this.idTabla,
      valores: [],
    };

    const campos = this.campos.filter(
      (c) => c.idDominioNavigation.tipoCampo !== 'Select Múltiple' &&
        c.valor && c.valor !== ''
    );

    registro.valores = campos.map(
      (campo: SeccionCampo) => {
        const valor: TablaValorDto = {
          idEntidadEstructuraTablaValor: 0,
          claveCampo: campo.clave,
          valor: campo.valor?.toString() ?? '',
          fueraDeRango: this.estaFueraDeRango(campo)
        };

        return valor;
      }
    );

    const subscription = this.entidadEstructuraTablaValorService
      .editar(registro)
      .subscribe({
        next: () => {
          this.mensajeService.modalExito('Se modificó el registro correctamente');
          this.cerrar.emit();
          subscription.unsubscribe();
        }
      });
  }

  public agregar(): void {
    const registro: EntidadTablaRegistroDto = {
      numero: 0,
      idEntidadEstructura: this.idPestanaSeccion,
      idTabla: this.idTabla,
      valores: [],
    };

    const camposAgregar = this.campos.filter(
      (c) =>
        !(c.idEntidadEstructuraValor > 0) &&
        c.valor !== '' &&
        c.valor &&
        c.idDominioNavigation.tipoCampo !== 'Select Múltiple'
    );

    registro.valores = camposAgregar.map(
      (campo: SeccionCampo) => {
        const valor: TablaValorDto = {
          idEntidadEstructuraTablaValor: 0,
          claveCampo: campo.clave,
          valor: campo.valor?.toString() ?? '',
          fueraDeRango: this.estaFueraDeRango(campo)
        };

        return valor;
      }
    );

    const subscription = this.entidadEstructuraTablaValorService
      .agregar(registro)
      .subscribe({
        next: () => {
          this.mensajeService.modalExito('Se agregó el registro correctamente');
          this.cerrar.emit();
          subscription.unsubscribe();
        }
      });
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
