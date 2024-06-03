import { Component, Input, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { RespuestaService } from '@http/examen/respuesta.service';
import { Respuesta } from '@models/examen/respuesta';
import { FormularioService } from '@services/formulario.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { FORM_ACTION } from '@utils/constants/constants';

@Component({
  selector: 'app-respuesta-formulario',
  templateUrl: './respuesta-formulario.component.html',
  styleUrls: ['./respuesta-formulario.component.scss']
})
export class Respuesta1FormularioComponent implements OnInit {

  private readonly MENSAJE_AGREGAR: string = 'La respuesta ha sido agregado';
  private readonly MENSAJE_EDITAR: string = 'La respuesta ha sido modificado';

  public accion: string;
  public onClose: (actualizar: boolean) => void;
  protected submitting: boolean = false;
  public resp: Respuesta = new Respuesta();
  public idReactivo: number;

  constructor(private formularioService: FormularioService,
    private mensajeService: MensajeService,
    private respuestaService: RespuestaService){

  }

  public ngOnInit(){
    console.log(this.resp);
    this.resp.idReactivo = this.idReactivo;
    if(this.resp.respuestaCorrecta == null){
      this.resp.respuestaCorrecta = false;
    }
  }

  protected cancelar(): void {
    this.onClose(false);
  }

  protected enviarFormulario(formulario: NgForm): void {
    this.submitting = true;

    if (!formulario.valid) {
      this.formularioService.validarCamposRequeridos(formulario);
      this.submitting = false;
      return;
    }

    if (this.accion === FORM_ACTION.Agregar) {
      this.agregar();
    }
    else if (this.accion === FORM_ACTION.Editar) {
      this.editar();
    }
  }

  private agregar(): void {
    this.respuestaService
      .agregar(this.resp)
      .subscribe({
        next: () => {
          this.mensajeService.modalExito(this.MENSAJE_AGREGAR);
          this.onClose(true);
        },
        error: (error: any) => {
          this.submitting = false;
        }
      });
  }

  private editar(): void {
    this.respuestaService
      .editar(this.resp)
      .subscribe({
        next: () => {
          this.mensajeService.modalExito(this.MENSAJE_EDITAR);
          this.onClose(true);
        },
        error: (error: any) => {
          this.submitting = false;
        }
      });
  }
}
