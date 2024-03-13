import { Component, OnInit, OnDestroy } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-modal-confirmacion',
  styleUrls: [],
  template: `
    <div class="d-flex">
      <div class="barra-lateral-modal"></div>
      <div class="modales modal-content">
        <div class="modal-header">
          <div>{{ titulo }}</div>
          <button *ngIf="botonCerrar" type="button" class="close pull-right" aria-label="Close" (click)="cancelar()">
            <i class="far fa-times-circle"></i>
          </button>
        </div>
        <div class="modal-body modal-confirmacion">
          <div class="form-row">
            <div class="col-md-12">
              <div class="mensaje" [innerHTML]="mensaje"></div>
            </div>
          </div>
          <div class="justify-content-center d-flex">
            <button (click)="confirmar()" type="button" class="btn-primario ml-3 mr-3">
              {{ tituloBtnAceptar }}
            </button>
          </div>
        </div>
      </div>
    </div>
  `
})
export class ModalConfirmacionComponent implements OnInit, OnDestroy {
  public readonly ID_BOTON_ACEPTAR = 1;
  public readonly ID_BOTON_CANCELAR = 2;
  public mensaje = "";
  public tituloBtnAceptar = 'Aceptar';
  public tituloBtnCancelar = 'Cancelar';
  public onClose: any;
  public titulo = '';
  public claseBarraLateral = '';
  public botonCerrar = true;

  constructor(public bsModalRef: BsModalRef) {}

  public confirmar(): void {
    this.bsModalRef.hide();
    this.onClose(this.ID_BOTON_ACEPTAR);
  }

  public cancelar(): void {
    this.bsModalRef.hide();
    this.onClose(this.ID_BOTON_CANCELAR);
  }

  public ngOnInit(): void {}

  public ngOnDestroy(): void {
    if (this.bsModalRef) {
      this.bsModalRef.hide();
    }
  }
}
