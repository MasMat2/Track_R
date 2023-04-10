import { Component, OnInit, OnDestroy } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component( {
  selector: 'app-modal-exito',
  styleUrls: [   ],
  template: `<div class="modales modal-content">
              <div class="modal-body modal-exito">
                <div class="form-row">
                  <div class="col-md-12">
                    <div class="icon-success">
                      <i class="fa fa-check"></i>
                    </div>
                  </div>
                </div>
                <div class="form-row">
                  <div class="col-md-12">
                    <div
                      class="mensaje"
                      [innerHTML]="mensaje"></div>
                  </div>
                </div>
                <div class="form-row pb-2 pt-3">
                  <div class="col-md-12 text-center">
                    <button
                      (click)="aceptar()"
                      class="btn-primario">{{ tituloBotonAceptar }}</button>
                  </div>
                </div>
              </div>
            </div>`
} )

export class ModalExitoComponent implements OnInit, OnDestroy {

  private readonly ID_MODAL_EXITO = 1;
  private onClose: any;
  public mensaje: string = 'El registro se ha agregado exitosamente';
  public tituloBotonAceptar = 'Aceptar';

  constructor( public bsModalRef: BsModalRef ) { }

  aceptar() {
    this.bsModalRef.hide();
    this.onClose( this.ID_MODAL_EXITO );
  }

  ngOnInit() { }

  ngOnDestroy(): void {
    if ( this.bsModalRef ) {
      this.bsModalRef.hide();
    }
  }

}
