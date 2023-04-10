import { Injectable } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { ModalConfirmacionComponent } from './mensaje-confirmacion.component';
import { AlertifyService } from '../../services/alertify.service';
import { MensajeComponent } from './mensaje';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MensajeService {
  private bsModalRef: BsModalRef;

  private readonly ID_BOTON_ACEPTAR = 1;
  private readonly ID_BOTON_CANCELAR = 2;

  private configuracionesDefault: ModalOptions = {
    animated: true,
    keyboard: false,
    backdrop: 'static',
    ignoreBackdropClick: true,
    class: 'modal-xsm modal-position-center',
    initialState: {}
  };

  constructor(
    private modalService: BsModalService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  modalExito(opciones: string) {
    this.alertify.success(opciones);
  }

  modalError(opciones: string) {
    this.alertify.error(opciones);
  }

  /**
   * Crea un modal de tipo Confirmación. Solamente se necesita enviar o el mensaje en
   * tipo string o un Objeto de tipo ModalOptions con todas las configuraciones
   * que se cambiaran al momento de crear el modal.
   *
   * @param opciones - Ya sea en formato string o un objeto de tipo ModalOptions
   *                  *Si se envia un objeto de tipo ModalOptions, el mensaje
   *                  debera estar en una variable de llamada 'mensaje' dentro de
   *                  initialState{}
   * @return promise - retorna una promesa cuando el botón aceptar es presionado o
   *                  un reject cuando el botón cancelar es presionado
   */
  modalConfirmacion(
    opciones: string | ModalOptions,
    titulo?: string,
    claseBarraLateral?: string,
    textoConfirmar?: string,
    botonCerrar?: boolean
  ): Promise<any> {
    const promise = new Promise((resolve, reject) => {
      this.configuracionesDefault.class = 'modal-md-aviso modal-position-center';
      if (typeof opciones === 'string') {
        this.configuracionesDefault.initialState = { mensaje: opciones };
      } else {
        this.configuracionesDefault = Object.assign(this.configuracionesDefault, opciones);
      }

      this.bsModalRef = this.modalService.show(ModalConfirmacionComponent, this.configuracionesDefault);
      this.bsModalRef.content.titulo = titulo;
      this.bsModalRef.content.claseBarraLateral = claseBarraLateral;

      if (botonCerrar === false) {
        this.bsModalRef.content.botonCerrar = false;
      }

      if (textoConfirmar) {
        this.bsModalRef.content.tituloBtnAceptar = textoConfirmar;
      }

      this.bsModalRef.content.onClose = (idBoton: number) => {
        if (idBoton === this.ID_BOTON_ACEPTAR) {
          resolve(true);
        }

        if (idBoton === this.ID_BOTON_CANCELAR) {
          reject(false);
        }

        const body = document.getElementsByTagName('body')[0];
        body.classList.remove('modal-open');
      };
    });
    return promise;
  }

  mensaje(opciones: {
    titulo: string;
    mensaje: string;
    clsIcono?: string;
    mostrarBotonCerrar?: boolean;
    btnTitulo?: string;
  }) {
    return new Promise((resolve, reject) => {
      this.configuracionesDefault.class = 'modal-md-aviso modal-position-center';
      this.configuracionesDefault = Object.assign(this.configuracionesDefault);
      this.bsModalRef = this.modalService.show(MensajeComponent, this.configuracionesDefault);
      this.bsModalRef.content.opciones = Object.assign(this.bsModalRef.content.opciones, opciones);
      this.bsModalRef.content.onClose = () => {
        resolve(null);
        const body = document.getElementsByTagName('body')[0];
        body.classList.remove('modal-open');
      };
    });
  }

  /**
   * Cierra todos los modales en caso que haya alguno abierto. Se usa solamente cuando
   * se destruye el componente desde el cual fue creado el componente.
   */
  destruirModal() {
    for (let i = 1; i <= this.modalService.getModalsCount(); i++) {
      this.modalService.hide(i);
    }
  }
}
