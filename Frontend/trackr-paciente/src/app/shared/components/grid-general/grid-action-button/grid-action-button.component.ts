import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { GridAction } from '@utils/grid-action';

@Component({
  selector: 'app-grid-action',
  templateUrl: './grid-action-button.component.html',
  styleUrls: ['./grid-action-button.component.scss'],
  standalone: true,
  imports:[CommonModule]
})
export class GridActionButtonComponent {
  public params: any;
  public action: string;
  private timeout = false;
  private disabled = false;
  public deshabilitar = '';
  public title = '';

  agInit(params: any): void {
    this.params = params;
    this.action =
      typeof params.colDef.action === 'function' ? params.colDef.action(params.data) : params.colDef.action;

    if (params.disabled !== undefined && params.disabled) {
      this.disabled = true;
      this.deshabilitar = 'deshabilitado';
    }

    if (params.colDef.title) {
      this.title = params.colDef.title;
    } else {
      switch (this.action) {
        case GridAction.GRID_ACCION_EDITAR:
          this.title = 'Editar';
          break;
        case GridAction.GRID_ACCION_ELIMINAR:
          this.title = 'Eliminar';
          break;
        case GridAction.GRID_ACCION_COPIAR:
          this.title = 'Copiar';
          break;
        case GridAction.GRID_ACCION_VER:
          this.title = 'Ver detalle';
          break;
        case GridAction.GRID_ACCION_PAGAR:
          this.title = 'Pagar';
          break;
        case GridAction.GRID_ACCION_NOTA_RESPONSIVA:
          this.title = 'Nota Responsiva';
          break;
        case GridAction.GRID_ACCION_ESTUDIO:
          this.title = 'Estudio Laboratorio';
          break;
        case GridAction.GRID_ACCION_IMPRIMIR:
          this.title = 'Imprimir';
          break;
        case GridAction.GRID_ACCION_RECETASYORDENES:
          this.title = 'RecetasOrdenes';
          break;
        case GridAction.GRID_ACCION_SOMATOMETRIA:
          this.title = 'Somatometria';
          break;
        case GridAction.GRID_ACCION_VER_RECIBO:
          this.title = 'Ver Recibo';
          break;
        case GridAction.GRID_ACCION_SELECCIONAR:
          this.title = 'Seleccionar';
          break;
        case GridAction.GRID_ACCION_ORDEN_SALIDA:
          this.title = 'Orden de Salida';
          break;
        case GridAction.GRID_ACCION_REGISTRAR_SALIDA_PERSONAL:
          this.title = 'Registrar Salida de Personal';
          break;
        case GridAction.GRID_ACCION_DESGLOSAR:
          this.title = 'Desglose de Movimiento';
          break;
        case GridAction.GRID_ACCION_VER_POLIZA:
          this.title = 'Ver Póliza';
          break;
        case GridAction.GRID_ACCION_CONCILIAR:
          this.title = 'Conciliar Movimiento'
          break;
        case GridAction.GRID_ACCION_COMPLEMENTO:
          this.title = 'Sellar'
          break;
        case GridAction.GRID_ACCION_PDF_COMPLEMENTO:
          this.title = 'Ver PDF'
          break;
        case GridAction.GRID_ACTION_PRESENTAR:
          this.title = 'Presentar'
          break;
      }
    }
  }

  public onClick() {
    if (!this.timeout) {
      if (this.action !== undefined && this.action !== '' && !this.disabled) {
        this.params.context.scope.actionButton({ action: this.action, value: this.params.data });
        this.timeout = !this.timeout;
        setTimeout(() => {
          this.timeout = !this.timeout;
        }, 1000);
      }
    }
  }

  refresh(): boolean {
    return false;
  }
}
