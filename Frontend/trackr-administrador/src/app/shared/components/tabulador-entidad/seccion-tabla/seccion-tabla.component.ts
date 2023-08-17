import { Component, Input, OnInit } from '@angular/core';
import { EntidadTablaRegistroDto } from '@dtos/gestion-entidades/entidad-tabla-registro-dto';
import { EntidadEstructuraTablaValorService } from '@http/gestion-entidad/entidad-estructura-tabla-valor.service';
import { EntidadEstructura } from '@models/gestion-entidad/entidad-estructura';
import { EntidadEstructuraTablaValor } from '@models/gestion-entidad/entidad-estructura-tabla-valor';
import { RegistroTabla } from '@models/gestion-entidad/registro-tabla';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalService } from 'ngx-bootstrap/modal';
import { SeccionTablaFormularioComponent } from '../seccion-tabla-formulario/seccion-tabla-formulario.component';
import * as moment from 'moment';

@Component({
  selector: 'app-seccion-tabla',
  templateUrl: './seccion-tabla.component.html',
  styleUrls: ['./seccion-tabla.component.scss']
})
export class SeccionTablaComponent implements OnInit {
  @Input() public entidadEstructuraSeccion: EntidadEstructura;
  @Input() public idTabla: number;

  public seleccionado?: RegistroTabla;

  constructor(
    private modalService: BsModalService,
    private mensajeService: MensajeService,
    private entidadEstructuraTablaValorService: EntidadEstructuraTablaValorService,
  ) { }

  ngOnInit() {
    this.entidadEstructuraSeccion.campos.sort((a, b) => a.orden - b.orden);
  }

  public seleccionar(registro: RegistroTabla): void {
    this.seleccionado = registro;
  }

  private actualizarGrid(): void {
    const subscription = this.entidadEstructuraTablaValorService
      .consultarPorPestanaSeccion(this.entidadEstructuraSeccion.idEntidadEstructura, this.idTabla)
      .subscribe({
        next: (registros: RegistroTabla[]) => {
          subscription.unsubscribe();
          this.entidadEstructuraSeccion.registrosTabla = registros;
        }
      });
  }

  public obtenerValor(claveColumna: string, valores: EntidadEstructuraTablaValor[]): string {
    const valor = valores.find(v => v.claveCampo === claveColumna);
    const campo = this.entidadEstructuraSeccion.campos.find(c => c.clave === claveColumna);

    if (!valor) {
      return '';
    }

    if(campo?.idDominioNavigation.tipoDato == 'Date') {
      return moment(valor.valor).format('DD/MM/YYYY');
    }

    return valor.valor;
  }

  public agregar(): void {
    const initialState = {
      accion: 'Agregar',
      idTabla: this.idTabla,
      idPestanaSeccion: this.entidadEstructuraSeccion.idEntidadEstructura,
      nombreSeccion: this.entidadEstructuraSeccion.nombre,
      campos: this.entidadEstructuraSeccion.campos,
    };

    const modalRef = this.modalService.show(
      SeccionTablaFormularioComponent,
      {
        initialState,
        ...GeneralConstant.CONFIG_MODAL_DEFAULT
      }
    );

    modalRef.content?.cerrar.subscribe(() => {
      modalRef.hide();
      this.actualizarGrid();
    });
  }

  public editar(): void {
    if (!this.seleccionado) {
      return;
    }

    for (const campo of this.entidadEstructuraSeccion.campos) {
      const valor = this.seleccionado.valores.find(v => v.claveCampo === campo.clave);
      campo.valor = valor ? valor.valor : '';
    }

    const initialState = {
      accion: 'Editar',
      numeroRegistro: this.seleccionado.idRegistroTabla,
      idTabla: this.idTabla,
      idPestanaSeccion: this.entidadEstructuraSeccion.idEntidadEstructura,
      nombreSeccion: this.entidadEstructuraSeccion.nombre,
      campos: this.entidadEstructuraSeccion.campos,
    };

    const modalRef = this.modalService.show(
      SeccionTablaFormularioComponent,
      {
        initialState,
        ...GeneralConstant.CONFIG_MODAL_DEFAULT
      }
    );

    modalRef.content?.cerrar.subscribe(() => {
      modalRef.hide();
      this.actualizarGrid();
    });
  }

  public async eliminar(): Promise<void> {
    if (!this.seleccionado) {
      return;
    }

    const confirmacion = await this.mensajeService
      .modalConfirmacion(
        '¿Desea eliminar el registro <strong>' + this.seleccionado.idRegistroTabla + '</strong>?',
        'Eliminar Registro',
        GeneralConstant.ICONO_CRUZ
      )
      .then(_ => true)
      .catch(_ => false);

    if (!confirmacion) {
      return;
    }

    const registro: EntidadTablaRegistroDto = {
      numero: this.seleccionado.idRegistroTabla,
      idEntidadEstructura: this.seleccionado.idEntidadEstructura,
      idTabla: this.idTabla,
      valores: [],
    };

    const subscription = this.entidadEstructuraTablaValorService
      .eliminar(registro)
      .subscribe({
        next: () => {
          this.mensajeService.modalExito('Se eliminó el registro correctamente');
          this.actualizarGrid();
          subscription.unsubscribe();
        }
      });
  }

}
