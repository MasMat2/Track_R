import { Component, EventEmitter, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { EntidadEstructuraTablaValorService } from '@http/gestion-entidad/entidad-estructura-tabla-valor.service';
import { SeccionCampo } from '@models/gestion-entidad/seccion-campo';
import { EntidadTablaRegistroDto, TablaValorDto } from '../../../dtos/gestion-entidades/entidad-tabla-registro-dto';
import { MensajeService } from '../../mensaje/mensaje.service';
import * as Utileria from '@utils/utileria';
import { DominioHospitalService } from '@http/catalogo/dominio-hospital.service';
import { lastValueFrom } from 'rxjs';

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
    private mensajeService: MensajeService,
    private dominioHospitalService:DominioHospitalService
  ) {}

  public grupos: {
    nombre: string,
    fila: number;
    campos: SeccionCampo[]
  }[];

  ngOnInit() {
    if (this.accion === 'Agregar') {
      for (const campo of this.campos) {
        campo.valor = campo.idDominioNavigation?.tipoDato === 'List'
          ? undefined
          : '';
      }
    }
    else {
      for (const campo of this.campos) {
        if (campo.idDominioNavigation?.tipoDato === 'List') {
          campo.valor = Number(campo.valor);
        }
      }
    }

    const result: { [key: string]: SeccionCampo[] } = this.campos
      .sort((a, b) => a.orden - b.orden)
      .reduce((r, a) => {
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
      });
  }

  public enviarFormulario(formulario: NgForm): void {
    this.submiting = true;


    if (this.accion === 'Editar') {
      this.editar();
    } else {
      this.agregar();
    }
  }

  public async editar() {
    const registro: EntidadTablaRegistroDto = {
      numero: this.numeroRegistro,
      idEntidadEstructura: this.idPestanaSeccion,
      idTabla: this.idTabla,
      valores: [],
    };

    const campos = this.campos.filter(
      (c) => c.idDominioNavigation?.tipoCampo !== 'Select Múltiple' &&
        c.valor && c.valor !== '' && c.idEntidadEstructuraValor > 0
    );

    registro.valores = await Promise.all(campos.map(
      async (campo: SeccionCampo) => {
        const valor: TablaValorDto = {
          idEntidadEstructuraTablaValor: campo.idEntidadEstructuraValor,
          idSeccionVariable: campo.idSeccionCampo,
          valor: campo.valor?.toString() ?? '',
          fueraDeRango: await this.estaFueraDeRango(campo),
          fechaMuestra: new Date()
        };

        return valor;
      }
    ));

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

  public async agregar() {
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
        c.idDominioNavigation?.tipoCampo !== 'Select Múltiple' &&
        c.clave !== 'ME-fecha' &&
        c.clave !== 'ME-hora'
    );
    var fechaSeleccionada = this.campos.find(c => c.clave === 'ME-fecha')?.valor?.toString();
    var horaSeleccionada = this.campos.find(c => c.clave === 'ME-hora')?.valor?.toString();
    if (fechaSeleccionada && horaSeleccionada) {

      let fecha = new Date(fechaSeleccionada);
      let [hora, minuto] = horaSeleccionada.split(':').map(str => parseInt(str, 10));
      let milisegundosActuales = new Date().getMilliseconds();
      fecha.setMilliseconds(milisegundosActuales);
      fecha.setHours(hora, minuto);

      let segundosActuales = new Date().getSeconds();
      fecha.setSeconds(segundosActuales);



      registro.valores = await Promise.all( camposAgregar.map(
        async (campo: SeccionCampo) => {
          const valor: TablaValorDto = {
            idEntidadEstructuraTablaValor: 0,
            idSeccionVariable: campo.idSeccionCampo,
            valor: campo.valor?.toString() ?? '',
            fueraDeRango: await this.estaFueraDeRango(campo),
            fechaMuestra: new Date(fecha!)
          };
  
          return valor;
        }
      ));
      
  
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

  }

  private async estaFueraDeRango(campo: SeccionCampo) {
    const dominio = campo.idDominioNavigation;

    const number = Number.parseFloat(campo.valor?.toString() ?? '');

    if (Number.isNaN(number)) {
      return false;
    }

    //Verificacion de la tabla dominio hospital
    let domHos = await lastValueFrom(this.dominioHospitalService.obtenerDominioHospital(dominio.idDominio,0))
    console.log(domHos)
    if(domHos != null){
      if(domHos.valorMaximo != null){
        dominio.valorMaximo = Number(domHos.valorMaximo)
      }
      if(domHos.valorMinimo != null){
        dominio.valorMinimo = Number(domHos.valorMinimo)
      }
    }


    const min = dominio.valorMinimo;
    const max = dominio.valorMaximo;

    const valor = Number(campo.valor);

    const fueraDeRango = valor < min || valor > max;

    return fueraDeRango;
  }
}
