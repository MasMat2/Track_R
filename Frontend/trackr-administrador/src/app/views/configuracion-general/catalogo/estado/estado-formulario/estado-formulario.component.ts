import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { EstadoFormularioCapturaDto } from '@dtos/catalogo/estado-formulario-captura-dto';
import { EstadoFormularioConsultaDto } from '@dtos/catalogo/estado-formulario-consulta-dto';
import { EstadoService } from '@http/catalogo/estado.service';
import { PaisService } from '@http/catalogo/pais.service';
import { Pais } from '@models/catalogo/pais';
import { CrudFormularioBase } from '@sharedComponents/crud/crud-base/crud-formulario-base';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import { Observable, Subject, map, takeUntil, tap } from 'rxjs';

@Component({
  selector: 'app-estado-formulario',
  templateUrl: './estado-formulario.component.html',
})
export class EstadoFormularioComponent extends CrudFormularioBase<EstadoFormularioCapturaDto> implements OnInit, OnDestroy {
  private destroy$: Subject<void> = new Subject<void>();

  // Selectores
  protected readonly placeHolderSelect: string = GeneralConstant.PLACEHOLDER_DROPDOWN;
  protected readonly placeHolderNoOptions: string = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

  protected idPais?: number;
  protected paises$: Observable<Pais[]>;

  protected submiting: boolean = false;

  constructor(
    private estadoService: EstadoService,
    private paisService: PaisService,
    mensajeService: MensajeService,
  ) {
    super(mensajeService);
  }

  public ngOnInit(): void {
    super.onInit();

    this.consultarPaises();
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
  }

  protected override consultar(idEstado: number): Observable<EstadoFormularioCapturaDto> {
    return this.estadoService
      .consultarParaFormulario(idEstado)
      .pipe(
        takeUntil(this.destroy$),
        tap((estado: EstadoFormularioConsultaDto) => {
          this.idPais = estado.idPais;
        }),
        map((estado: EstadoFormularioConsultaDto) => {
          const capturaDto = new EstadoFormularioCapturaDto();
          capturaDto.idEstado = estado.idEstado;
          capturaDto.clave = estado.clave;
          capturaDto.nombre = estado.nombre;
          capturaDto.idPais = estado.idPais;

          return capturaDto;
        })
      );
  }

  protected override agregar(estado: EstadoFormularioCapturaDto): Observable<void> {
    return this.estadoService.agregar(estado);
  }

  protected override editar(estado: EstadoFormularioCapturaDto): Observable<void> {
    return this.estadoService.editar(estado);
  }

  protected override onSubmit(formulario: NgForm): void {
    this.entidad.idPais = this.idPais!;

    super.onSubmit(formulario);
  }

  private consultarPaises(): void {
    this.paises$ = this.paisService.consultarGeneral();
  }
}
