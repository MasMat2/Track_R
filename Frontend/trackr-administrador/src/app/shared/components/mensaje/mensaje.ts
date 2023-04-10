import { Component, OnInit, OnDestroy } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-mensaje',
  styleUrls: [],
  template: `
    <div class="d-flex mensaje-aviso">
      <div class="barra-lateral-modal"><div class="{{ opciones.clsIcono }}"></div></div>
      <div class="modales modal-content p-2">
        <div [style.visibility]="opciones.mostrarBotonCerrar ? 'visible' : 'hidden'" class="modal-header">
          <button type="button" class="close pull-right" aria-label="Close" (click)="aceptar()">
            <i class="far fa-times-circle"></i>
          </button>
        </div>
        <div class="modal-body modal-confirmacion pt-0 pl-3 pr-5 mr-5 ml-4 mb-2">
          <div class="row">
            <div class="col-md-12 titulo mb-2">
              {{ opciones.titulo }}
            </div>
          </div>
          <div class="row">
            <div class="col-md-12 mensaje text-left" [innerHTML]="opciones.mensaje"></div>
          </div>
          <div class="row">
            <div class="col-md-12 accion">
              <button (click)="aceptar()" type="button" class="btn-primario">
                {{ opciones.btnTitulo }}
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class MensajeComponent implements OnInit, OnDestroy {
  public onClose: any;
  public opciones = {
    titulo: 'Notificación',
    mensaje: 'Este es un mensaje de notificación.',
    clsIcono: 'envio-mail', // TODO: CAMBIAR ICONO DEFAULT
    mostrarBotonCerrar: true,
    btnTitulo: 'Aceptar'
  };

  constructor(public bsModalRef: BsModalRef) {}

  aceptar() {
    this.bsModalRef.hide();
    this.onClose();
  }

  ngOnInit() {}

  ngOnDestroy(): void {
    if (this.bsModalRef) {
      this.bsModalRef.hide();
    }
  }
}
