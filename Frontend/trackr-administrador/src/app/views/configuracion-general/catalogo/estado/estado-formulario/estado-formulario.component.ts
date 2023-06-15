import { Component, EventEmitter, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { EstadoFormularioCapturaDto } from '@dtos/catalogo/estado-formulario-captura-dto';
import { EstadoFormularioConsultaDto } from '@dtos/catalogo/estado-formulario-consulta-dto';
import { EstadoService } from '@http/catalogo/estado.service';
import { PaisService } from '@http/catalogo/pais.service';
import { Pais } from '@models/catalogo/pais';
import { FormularioService } from '@services/formulario.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import { Observable, Subject, map, takeUntil } from 'rxjs';

@Component({
  selector: 'app-estado-formulario',
  templateUrl: './estado-formulario.component.html',
})
export class EstadoFormularioComponent implements OnInit, OnDestroy {
  private readonly MENSAJE_AGREGAR: string = 'El estado ha sido agregado';
  private readonly MENSAJE_EDITAR: string = 'El estado ha sido modificado';

  protected estado = new EstadoFormularioCapturaDto();

  // Inputs
  public accion: string;
  public idEstado?: number;
  public closed = new EventEmitter<EstadoFormularioCapturaDto | undefined>;

  // Selectores
  protected readonly placeHolderSelect: string =
    GeneralConstant.PLACEHOLDER_DROPDOWN;
  protected readonly placeHolderNoOptions: string =
    GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

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
    // TODO: 2023-06-14 -> Error si cualquiera de las peticiones falla
    if (this.accion === GeneralConstant.MODAL_ACCION_EDITAR && this.idEstado) {
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

    // TODO: 2023-06-14 -> Agregar el id al agregar

    const [observable, mensajeExito]: [Observable<void>, string] =
      this.accion === GeneralConstant.MODAL_ACCION_AGREGAR
        ? [this.agregar(), this.MENSAJE_AGREGAR]
        : [this.editar(), this.MENSAJE_EDITAR];

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
