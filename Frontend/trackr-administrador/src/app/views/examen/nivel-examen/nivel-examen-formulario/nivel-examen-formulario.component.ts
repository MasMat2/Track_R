import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NivelExamenService } from '@http/examen/nivel-examen.service';
import { NivelExamen } from '@models/examen/nivel-examen';
import { FORM_ACTION } from '@utils/constants/constants';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { FormularioService } from 'src/app/shared/services/formulario.service';

@Component({
  selector: 'app-nivel-examen-formulario',
  templateUrl: './nivel-examen-formulario.component.html',
})
export class NivelExamenFormularioComponent implements OnInit {

  protected nivelExamen = new NivelExamen();
  protected submitting = false;

  // Inputs
  public accion: string;
  public esEdicion = false;
  public idNivelExamen?: number;
  public onClose: (actualizar: boolean) => void;

  constructor(
    private formularioService: FormularioService,
    private mensajeService: MensajeService,
    private nivelExamenService: NivelExamenService,
  ) {}

  public ngOnInit(): void {
    if (this.accion === FORM_ACTION.Agregar) {
      this.nivelExamen.fechaAlta = new Date();
      this.nivelExamen.estatus = true;
    }
    else if (this.accion === FORM_ACTION.Editar) {
      this.consultarNivelExamen(this.idNivelExamen!)
    }
  }

  private consultarNivelExamen(idNivelExamen: number): void {
    this.nivelExamenService
      .consultar(idNivelExamen)
      .subscribe((nivelExamen: NivelExamen) => {
        nivelExamen.fechaAlta = new Date(nivelExamen.fechaAlta);
        this.nivelExamen = nivelExamen;
      });
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
    const MENSAJE_AGREGAR: string = 'El nivel de examen ha sido agregado';

    this.nivelExamenService
      .agregar(this.nivelExamen)
      .subscribe({
        next: (data) => {
          this.mensajeService.modalExito(MENSAJE_AGREGAR);
          this.onClose(true);
        },
        error: (error) => {
          this.submitting = false;
        }
      });
  }

  private editar(): void {
    const MENSAJE_EDITAR: string = 'El nivel de examen ha sido modificado';

    this.nivelExamenService
      .editar(this.nivelExamen)
      .subscribe({
        next: (data) => {
          this.mensajeService.modalExito(MENSAJE_EDITAR);
          this.onClose(true);
        },
        error: (error) => {
          this.submitting = false;
        }
      });
  }

  protected cancelar(): void {
    this.onClose(false);
  }

  protected esEditar(): boolean {
    return this.accion === FORM_ACTION.Editar;
  }
}
