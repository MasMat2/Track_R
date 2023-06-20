import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AsignaturaService } from '@http/examen/asignatura.service';
import { NivelExamenService } from '@http/examen/nivel-examen.service';
import { ReactivoService } from '@http/examen/reactivo.service';
import { Asignatura } from '@models/examen/asignatura';
import { NivelExamen } from '@models/examen/nivel-examen';
import { Reactivo } from '@models/examen/reactivo';
import { DROPDOWN_NO_OPTIONS, DROPDOWN_PLACEHOLDER, FORM_ACTION } from '@utils/constants/constants';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { FormularioService } from 'src/app/shared/services/formulario.service';

@Component({
  selector: 'app-reactivo-formulario',
  templateUrl: './reactivo-formulario.component.html',
  styleUrls: ['./reactivo-formulario.component.scss'],
})
export class ReactivoFormularioComponent implements OnInit {
  protected readonly URL_IMAGEN_DEFAULT: string = 'assets/img/svg/image-solid.png';
  protected readonly TOOLTIP_INSTRUCCIONES: string = "Para saltar línea necesita presionar 'Shift + Enter'";

  protected submitting: boolean = false;

  protected reactivo = new Reactivo();

  // Selectores
  protected readonly placeHolderSelect: string = DROPDOWN_PLACEHOLDER;
  protected readonly placeHolderNoOptions: string = DROPDOWN_NO_OPTIONS;

  protected asignaturaList: Asignatura[] = [];
  protected nivelExamenList: NivelExamen[] = [];
  protected respuestas: { nombre: string }[] = [
    { nombre: 'a' },
    { nombre: 'b' },
    { nombre: 'c' },
    { nombre: 'd' },
    { nombre: 'e' },
  ];

  private readonly MENSAJE_AGREGAR: string = 'El reactivo ha sido agregado';
  private readonly MENSAJE_EDITAR: string = 'El reactivo ha sido modificado';

  // Inputs
  public accion: string;
  public esEdicion = false;
  public idReactivo?: number;
  public onClose: (actualizar: boolean) => void;

  constructor(
    private formularioService: FormularioService,
    private mensajeService: MensajeService,
    private reactivoService: ReactivoService,
    private asignaturaService: AsignaturaService,
    private nivelExamenService: NivelExamenService,
  ) {}

  public ngOnInit(): void {
    if (this.accion === FORM_ACTION.Agregar) {
      this.reactivo.fechaAlta = new Date();
      this.reactivo.estatus = true;
      this.reactivo.necesitaRevision = false;
    }
    else if (this.accion === FORM_ACTION.Editar) {
      this.consultarReactivo(this.idReactivo!);
    }

    this.consultarAsignaturas();
    this.consultarNivelesExamen();

    console.log(this.reactivo);
  }

  private consultarReactivo(idReactivo: number): void {
    this.reactivoService
      .consultar(idReactivo)
      .subscribe((reactivo) => {
        reactivo.fechaAlta = new Date(reactivo.fechaAlta);

        this.reactivo = reactivo;
      });
  }

  private consultarAsignaturas(): void {
    this.asignaturaService
      .consultarTodosParaSelector()
      .subscribe((data) => {
        this.asignaturaList = data;
      });
  }

  private consultarNivelesExamen(): void {
    this.nivelExamenService
      .consultarTodosParaSelector()
      .subscribe((data) => {
        this.nivelExamenList = data;
      });
  }

  protected fileChange(event: any): void {
    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]);

      reader.onload = () => {
        this.reactivo.imagenBase64 = reader.result?.toString()!;
        this.reactivo.imagen = reader.result!.toString().split(',')[1];
        this.reactivo.imagenTipoMime = event.target.files[0].type;
        this.reactivo.imagenNombre = event.target.files[0].name;
      };
    }
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
    this.reactivoService
      .agregar(this.reactivo)
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
    this.reactivoService
      .editar(this.reactivo)
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

  protected setRespuestaCorrecta(): void {
    this.reactivo.respuestaCorrecta = '';
  }

  protected cancelar(): void {
    this.onClose(false);
  }
}
