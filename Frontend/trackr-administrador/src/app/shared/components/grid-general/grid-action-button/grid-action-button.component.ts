import { Component } from '@angular/core';
import { GeneralConstant } from 'src/app/shared/utils/general-constant';

@Component({
  selector: 'app-grid-action',
  templateUrl: './grid-action-button.component.html',
  styleUrls: ['./grid-action-button.component.scss']
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
        case GeneralConstant.GRID_ACCION_EDITAR:
          this.title = 'Editar';
          break;
        case GeneralConstant.GRID_ACCION_ELIMINAR:
          this.title = 'Eliminar';
          break;
        case GeneralConstant.GRID_ACCION_COPIAR:
          this.title = 'Copiar';
          break;
        case GeneralConstant.GRID_ACCION_VER: //esta propiedad es la que se muestra cuando pasas el mouse sobre el boton
          this.title = 'Ver detalle';
          break;
        case GeneralConstant.GRID_ACCION_PAGAR:
          this.title = 'Pagar';
          break;
        case GeneralConstant.GRID_ACCION_NOTA_RESPONSIVA:
          this.title = 'Nota Responsiva';
          break;
        case GeneralConstant.GRID_ACCION_ESTUDIO:
          this.title = 'Estudio Laboratorio';
          break;
        case GeneralConstant.GRID_ACCION_IMPRIMIR:
          this.title = 'Imprimir';
          break;
        case GeneralConstant.GRID_ACCION_RECETASYORDENES:
          this.title = 'RecetasOrdenes';
          break;
        case GeneralConstant.GRID_ACCION_SOMATOMETRIA:
          this.title = 'Somatometria';
          break;
        case GeneralConstant.GRID_ACCION_VER_RECIBO:
          this.title = 'Ver Recibo';
          break;
        case GeneralConstant.GRID_ACCION_SELECCIONAR:
          this.title = 'Seleccionar';
          break;
        case GeneralConstant.GRID_ACCION_ORDEN_SALIDA:
          this.title = 'Orden de Salida';
          break;
        case GeneralConstant.GRID_ACCION_REGISTRAR_SALIDA_PERSONAL:
          this.title = 'Registrar Salida de Personal';
          break;
        case GeneralConstant.GRID_ACCION_DESGLOSAR:
          this.title = 'Desglose de Movimiento';
          break;
        case GeneralConstant.GRID_ACCION_VER_POLIZA:
          this.title = 'Ver Póliza';
          break;
        case GeneralConstant.GRID_ACCION_CONCILIAR:
          this.title = 'Conciliar Movimiento'
          break;
        case GeneralConstant.GRID_ACCION_COMPLEMENTO:
          this.title = 'Sellar'
          break;
        case GeneralConstant.GRID_ACCION_PDF_COMPLEMENTO:
          this.title = 'Ver PDF'
          break;
        case GeneralConstant.GRID_ACTION_REVISAR:
          this.title = 'Revisar'
          break;
        case GeneralConstant.GRID_ACCION_DESCARGAR_PDF:
          this.title = 'Descargar en PDF';
          break;
        case GeneralConstant.GRID_ACCION_DESCARGAR_EXCEL:
          this.title = 'Descargar en Excel';
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
