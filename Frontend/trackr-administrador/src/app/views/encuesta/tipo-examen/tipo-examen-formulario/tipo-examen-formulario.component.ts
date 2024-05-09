import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { TipoExamenService } from '@http/examen/tipo-examen.service';
import { TipoExamen } from '@models/examen/tipo-examen';
import { FORM_ACTION } from '@utils/constants/constants';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { FormularioService } from 'src/app/shared/services/formulario.service';

@Component({
  selector: 'app-tipo-examen-formulario',
  templateUrl: './tipo-examen-formulario.component.html',
})
export class TipoExamenFormularioComponent implements OnInit {
  private readonly MENSAJE_AGREGAR: string = 'El Tipo de Encuesta ha sido agregado';
  private readonly MENSAJE_EDITAR: string = 'El Tipo de Encuesta ha sido modificado';

  public accion: string;
  public esEdicion = false;
  public idTipoExamen?: number;
  public onClose: (actualizar: boolean) => void;

  public submitting = false;

  public tipoExamen = new TipoExamen();

  constructor(
    private formularioService: FormularioService,
    private mensajeService: MensajeService,
    private tipoExamenService: TipoExamenService
  ) {}

  ngOnInit(): void {
    if (this.accion === FORM_ACTION.Agregar) {
      this.tipoExamen.fechaAlta = new Date();
      this.tipoExamen.estatus = true;
      this.tipoExamen.totalPreguntas = 0;
    }
    else if (this.accion === FORM_ACTION.Editar) {
      this.consultarTipoExamen(this.idTipoExamen!);
    }
  }

  private consultarTipoExamen(idTipoExamen: number): void {
    this.tipoExamenService
      .consultar(idTipoExamen)
      .subscribe((tipoExamen: TipoExamen) => {
        tipoExamen.fechaAlta = new Date(tipoExamen.fechaAlta);
        this.tipoExamen = tipoExamen;
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

  private agregar() {
    this.tipoExamenService
      .agregar(this.tipoExamen)
      .subscribe({
        next: () => {
          this.mensajeService.modalExito(this.MENSAJE_AGREGAR);
          this.onClose(true);
        },
        error: (error) => {
          this.submitting = false;
        }
      });
  }

  private editar() {
    this.tipoExamenService
      .editar(this.tipoExamen)
      .subscribe({
        next: () => {
          this.mensajeService.modalExito(this.MENSAJE_EDITAR);
          this.onClose(true);
        },
        error: (error) => {
          this.submitting = false;
        }
      });
  }

  protected cancelar(): void {
    this.onClose(true);
  }

  protected esEditar(): boolean {
    return this.accion === FORM_ACTION.Editar;
  }

  numberOnly(event: any): boolean {
    const charCode = event.which ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
}
