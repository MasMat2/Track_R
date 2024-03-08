import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { PaisService } from '@http/catalogo/pais.service';
import { Pais } from '@models/catalogo/pais';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef } from 'ngx-bootstrap/modal';
import * as Utileria from '@utils/utileria';

@Component({
  selector: 'app-pais-formulario',
  templateUrl: './pais-formulario.component.html',
})
export class PaisFormularioComponent implements OnInit {

  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public onClose: any;
  public accion: string;
  public mensajeAgregar = 'El país ha sido agregado';
  public mensajeEditar = 'El país ha sido modificado';
  public btnSubmit = false;
  public pais = new Pais();

  constructor(
    public bsModalRef: BsModalRef,
    private modalMensajeService: MensajeService,
    private paisService: PaisService,
  ) {}

  ngOnInit() {}

  limpiarFormulario() {
    this.pais = new Pais();
    this.accion = GeneralConstant.MODAL_ACCION_AGREGAR;
  }

  agregar(formulario: NgForm) {
    this.paisService.agregar(this.pais).subscribe(
      data => {
        this.modalMensajeService.modalExito(this.mensajeAgregar);
        formulario.reset();
        this.limpiarFormulario();
        this.onClose(true);
        this.btnSubmit = false;
      },
      error => {
        this.btnSubmit = false;
      }
    );
  }

  editar() {
    this.paisService.editar(this.pais).subscribe(
      data => {
        this.modalMensajeService.modalExito(this.mensajeEditar);
        this.onClose(true);
      },
      error => {
        this.btnSubmit = false;
      }
    );
  }

  cancelar() {
    this.onClose(true);
  }

  enviarFormulario(formulario: NgForm) {
    this.btnSubmit = true;
    if (!formulario.valid) {
      Utileria.validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      return;
    }

    if (this.accion === GeneralConstant.MODAL_ACCION_AGREGAR) {
      this.agregar(formulario);
    } else if (this.accion === GeneralConstant.MODAL_ACCION_EDITAR) {
      this.editar();
    }
  }

}
