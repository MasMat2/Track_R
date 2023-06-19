import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AsignaturaService } from '@http/examen/asignatura.service';
import { Asignatura } from '@models/examen/asignatura';
import { FORM_ACTION } from '@utils/constants/constants';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { FormularioService } from 'src/app/shared/services/formulario.service';

@Component({
  selector: 'app-asignatura-formulario',
  templateUrl: './asignatura-formulario.component.html',
})
export class AsignaturaFormularioComponent implements OnInit {
  public MENSAJE_AGREGAR = 'La asignatura ha sido agregada';
  public MENSAJE_EDITAR = 'La asignatura ha sido modificada';

  // Inputs
  public accion: string;
  public onClose: any;
  public idAsignatura?: number;
  public esEdicion = false;

  public submiting = false;

  public asignatura = new Asignatura();

  constructor(
    private formularioService: FormularioService,
    private mensajeService: MensajeService,
    private asignaturaService: AsignaturaService,
    private bsModalRef: BsModalRef
  ) {}

  public ngOnInit(): void {
    if (this.accion === FORM_ACTION.Agregar) {
      this.asignatura.fechaAlta = new Date();
      this.asignatura.estatus = true;
    }
    else {
      if (this.idAsignatura === undefined) {
        throw new Error('No se especificó el id de la asignatura');
      }

      this.asignaturaService
        .consultar(this.idAsignatura)
        .subscribe((asignatura) => {
          asignatura.fechaAlta = new Date(asignatura.fechaAlta);
          this.asignatura = asignatura;
        });
    }
  }

  protected enviarFormulario(formulario: NgForm): void {
    this.submiting = true;

    if (!formulario.valid) {
      this.formularioService.validarCamposRequeridos(formulario);
      this.submiting = false;
      return;
    }

    if (this.accion === FORM_ACTION.Agregar) {
      this.agregar();
    } else if (this.accion === FORM_ACTION.Editar) {
      this.editar();
    }
  }

  private agregar(): void {
    this.asignaturaService.agregar(this.asignatura).subscribe({
      next: (idAsignatura) => {
        this.mensajeService.modalExito(this.MENSAJE_AGREGAR);
        this.asignatura.idAsignatura = idAsignatura;
        this.cerrar();
      },
      error: () => {
        this.submiting = false;
      }
    });
  }

  private editar(): void {
    this.asignaturaService.editar(this.asignatura).subscribe({
      next: () => {
        this.mensajeService.modalExito(this.MENSAJE_EDITAR);
        this.cerrar();
      },
      error: () => {
        this.submiting = false;
      },
    });
  }

  private cerrar(): void {
    this.onClose(true);
    this.bsModalRef.hide();
  }

  public cancelar(): void {
    this.onClose(false);
    this.bsModalRef.hide();
  }
}
