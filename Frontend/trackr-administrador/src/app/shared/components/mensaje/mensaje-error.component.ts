import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  ViewChild,
  TemplateRef,
  OnDestroy
} from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-modal-error',
  styleUrls: [],
  template: `
    <div class="modales modal-content">
      <div class="modal-body modal-error">
        <div class="form-row">
          <div class="col-md-12">
            <div class="icon-error">
              <i class="fa fa-exclamation-triangle"></i>
            </div>
          </div>
          <div class="col-md-12 align-self-center">
            <div class="mensaje" [innerHTML]="mensaje"></div>
          </div>
        </div>
        <div class="form-row pb-2 pt-3">
          <div class="col-md-12 text-center">
            <button (click)="aceptar()" class="btn-primario">{{ tituloBotonAceptar }}</button>
          </div>
        </div>
      </div>
    </div>
  `
})
export class ModalErrorComponent implements OnInit, OnDestroy {
  private readonly ID_MODAL_EXITO = 1;
  private onClose: any;
  public mensaje: string = 'Ocurrió un error inesperado, favor de contactar al administrador del sistema';
  public tituloBotonAceptar = 'Aceptar';

  constructor(public bsModalRef: BsModalRef) {}

  aceptar() {
    this.bsModalRef.hide();
    this.onClose(this.ID_MODAL_EXITO);
  }

  ngOnInit() {}

  ngOnDestroy(): void {
    if (this.bsModalRef) {
      this.bsModalRef.hide();
    }
  }
}
