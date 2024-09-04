import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AsignaturaService } from '@http/examen/asignatura.service';
import { ContenidoExamenService } from '@http/examen/contenido-examen.service';
import { ExamenService } from '@http/examen/examen.service';
import { NivelExamenService } from '@http/examen/nivel-examen.service';
import { Asignatura } from '@models/examen/asignatura';
import { ContenidoExamen } from '@models/examen/contenido-examen';
import { NivelExamen } from '@models/examen/nivel-examen';
import { TipoExamen } from '@models/examen/tipo-examen';
import { FormularioService } from '@services/formulario.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { DROPDOWN_NO_OPTIONS, DROPDOWN_PLACEHOLDER, FORM_ACTION } from '@utils/constants/constants';

@Component({
  selector: 'app-contenido-examen-formulario',
  templateUrl: './contenido-examen-formulario.component.html',
})
export class ContenidoExamenFormularioComponent implements OnInit {
  private readonly MENSAJE_AGREGAR: string = 'El contenido del Cuestionario ha sido agregado';
  private readonly MENSAJE_EDITAR: string = 'El contenido del Cuestionario ha sido modificado';

  public accion: string;
  public esEdicion = false;
  public onClose: (actualizar: boolean) => void;
  public idContenidoExamen?: number;
  public tipoExamen = new TipoExamen();

  public submitting = false;
  public contenidoExamen = new ContenidoExamen();

  // Selectores
  protected readonly placeHolderSelect: string = DROPDOWN_PLACEHOLDER;
  protected readonly placeHolderNoOptions: string = DROPDOWN_NO_OPTIONS;

  protected asignaturaList: Asignatura[] = [];
  protected nivelExamenList: NivelExamen[] = [];

  constructor(
    private formularioService: FormularioService,
    private mensajeService: MensajeService,
    private contenidoExamenService: ContenidoExamenService,
    private asignaturaService: AsignaturaService,
    private nivelExamenService: NivelExamenService,
    private examenService : ExamenService,
  ) {}

  public ngOnInit(): void {
    if (this.accion === FORM_ACTION.Agregar) {
      this.contenidoExamen.idTipoExamen = this.tipoExamen.idTipoExamen;
      this.contenidoExamen.fechaAlta = new Date();
      this.contenidoExamen.estatus = true;
    }
    else if (this.accion === FORM_ACTION.Editar) {
      this.consultarContenidoExamen(this.idContenidoExamen!);
    }

    this.consultarAsignaturas();
    this.consultarNivelesExamen();
  }

  onSelectChange(): void {
    if(this.contenidoExamen.idAsignatura && this.contenidoExamen.idNivelExamen) {
      this.examenService.consultarCantidadReactivos(this.contenidoExamen.idAsignatura, this.contenidoExamen.idNivelExamen).subscribe((cantidadReactivos: number) => {
        this.contenidoExamen.totalPreguntas = cantidadReactivos;
      });
    }
  }

  private consultarContenidoExamen(idContenidoExamen: number): void {
    this.contenidoExamenService
      .consultar(idContenidoExamen)
      .subscribe((contenidoExamen: ContenidoExamen) => {
        contenidoExamen.fechaAlta = new Date(contenidoExamen.fechaAlta);
        contenidoExamen.idTipoExamen = this.tipoExamen.idTipoExamen;

        this.contenidoExamen = contenidoExamen;
      });
  }

  private consultarAsignaturas(): void {
    this.asignaturaService
      .consultarTodosParaSelector()
      .subscribe((asignaturas: Asignatura[]) => {
        this.asignaturaList = asignaturas;
      });
  }

  private consultarNivelesExamen(): void {
    this.nivelExamenService
      .consultarTodosParaSelector()
      .subscribe((nivelesExamen: NivelExamen[]) => {
        this.nivelExamenList = nivelesExamen;
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
    this.contenidoExamenService
      .agregar(this.contenidoExamen)
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

  private editar(): void {
    this.contenidoExamenService
      .editar(this.contenidoExamen)
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

  numberOnly(event: any): boolean {
    // TODO: 2023-06-15 -> Revisar tipo de dato
    const charCode = event.which ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
}
