import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AyudaSeccionService } from '@http/seguridad/ayuda-seccion.service';
import { AyudaSeccion } from '@models/seguridad/ayuda-seccion';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import * as Utileria from '@utils/utileria';

@Component({
  selector: 'app-ayuda-seccion-formulario',
  templateUrl: './ayuda-seccion-formulario.component.html',
  styleUrls: ['./ayuda-seccion-formulario.component.scss']
})
export class AyudaSeccionFormularioComponent implements OnInit {

  public seccion = new AyudaSeccion();
  public onClose: any;
  public btnSubmit = false;
  public accion: string;
  public mensajeAgregar = 'La seccion ayuda ha sido agregada.'
  public mensajeEditar = 'La seccion ayuda ha sido editada.'

  constructor(
    private ayudaSeccionService: AyudaSeccionService,
    private modalMensajeService: MensajeService) { }

  ngOnInit() {
  }

  public cancelar(): void {
    this.onClose(true);
  }

  public enviarFormulario(formulario: NgForm): void {
    this.btnSubmit = true;
    if (!formulario.valid) {
      Utileria.validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      return;
    }

    if (this.accion === GeneralConstant.MODAL_ACCION_AGREGAR) {
      this.agregar();
    } else if (this.accion === GeneralConstant.MODAL_ACCION_EDITAR) {
      this.editar();
    }
  }

  public agregar(): void{
    this.ayudaSeccionService.agregar(this.seccion).subscribe((data)=>{
      this.modalMensajeService.modalExito(this.mensajeAgregar);
      this.onClose(true);
        this.btnSubmit = false;
    },()=>{
      this.btnSubmit = false;
    })
  }

  public editar(): void{
    this.ayudaSeccionService.editar(this.seccion).subscribe((data)=>{
      this.modalMensajeService.modalExito(this.mensajeEditar);
      this.onClose(true);
        this.btnSubmit = false;
    },()=>{
      this.btnSubmit = false;
    })
  }

}
