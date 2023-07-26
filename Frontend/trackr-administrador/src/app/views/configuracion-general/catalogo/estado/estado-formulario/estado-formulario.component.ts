import { Component, EventEmitter, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { EstadoFormularioCapturaDto } from '@dtos/catalogo/estado-formulario-captura-dto';
import { EstadoFormularioConsultaDto } from '@dtos/catalogo/estado-formulario-consulta-dto';
import { EstadoService } from '@http/catalogo/estado.service';
import { PaisService } from '@http/catalogo/pais.service';
import { Pais } from '@models/catalogo/pais';
import { FormularioService } from '@services/formulario.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { DROPDOWN_NO_OPTIONS, DROPDOWN_PLACEHOLDER, FORM_ACTION } from '@utils/constants/constants';
import { Observable, Subject, map, takeUntil } from 'rxjs';

@Component({
  selector: 'app-estado-formulario',
  templateUrl: './estado-formulario.component.html',
})
export class EstadoFormularioComponent implements OnInit, OnDestroy {
  protected estado = new EstadoFormularioCapturaDto();

  // Inputs
  public accion: string;
  public idEstado?: number;
  public closed = new EventEmitter<EstadoFormularioCapturaDto | undefined>;

  // Selectores
  protected readonly DROPDOWN_PLACEHOLDER: string = DROPDOWN_PLACEHOLDER;
  protected readonly DROPDOWN_NO_OPTIONS: string = DROPDOWN_NO_OPTIONS;

  protected idPais?: number;
  protected paises$: Observable<Pais[]>;

  protected submiting: boolean = false;

  constructor(
    private mensajeService: MensajeService,
    private formularioService: FormularioService,
    private estadoService: EstadoService,
    private paisService: PaisService
  ) {}

  private destroy$: Subject<void> = new Subject<void>();

  public ngOnInit(): void {
    if (this.accion === FORM_ACTION.Editar && this.idEstado) {
      this.consultarEstado(this.idEstado);
    }

    this.consultarPaises();
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
  }

  private consultarEstado(idEstado: number): void {
    const subscription = this.estadoService
      .consultarParaFormulario(idEstado)
      .pipe(
        takeUntil(this.destroy$),
        map((estado: EstadoFormularioConsultaDto) => {
          const capturaDto = new EstadoFormularioCapturaDto();
          capturaDto.idEstado = estado.idEstado;
          capturaDto.clave = estado.clave;
          capturaDto.nombre = estado.nombre;
          capturaDto.idPais = estado.idPais;

          return capturaDto;
        })
      )
      .subscribe({
        next: (estado: EstadoFormularioCapturaDto) => {
          this.estado = estado;
          this.idPais = estado.idPais;
        },
        error: () => {},
        complete: () => {
          subscription.unsubscribe();
        },
      });
  }

  private consultarPaises(): void {
    this.paises$ = this.paisService.consultarGeneral();
  }

  protected async enviarFormulario(formulario: NgForm): Promise<void> {
    this.submiting = true;

    if (!formulario.valid) {
      this.formularioService.validarCamposRequeridos(formulario);
      this.submiting = false;
      return;
    }

    this.estado.idPais = this.idPais!;

    const MENSAJE_AGREGAR: string = `El estado ha sido agregado`;
    const MENSAJE_EDITAR: string = `El estado ha sido modificado`;


    const [observable, mensajeExito]: [Observable<void>, string] =
      this.accion === FORM_ACTION.Agregar
        ? [this.agregar(), MENSAJE_AGREGAR]
        : [this.editar(), MENSAJE_EDITAR];

    const subscription = observable.subscribe({
      next: () => {
        this.mensajeService.modalExito(mensajeExito);
        this.closed.emit(this.estado);
      },
      error: () => {},
      complete: () => {
        subscription.unsubscribe();
      },
    });
  }

  private agregar() {
    return this.estadoService.agregar(this.estado);
  }

  private editar() {
    return this.estadoService.editar(this.estado);
  }

  protected cancelar(): void {
    this.closed.emit(undefined);
  }
}
