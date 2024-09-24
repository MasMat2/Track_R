import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UnidadMedidaGridDto } from '@dtos/catalogo/unidad-medida-grid-dto';
import { UnidadMedidaService } from '@http/catalogo/unidad-medida.service';
import { CrudFormularioBase } from '@sharedComponents/crud/crud-base/crud-formulario-base';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import { map, Observable, Subject, takeUntil, tap } from 'rxjs';

@Component({
  selector: 'app-unidad-medida-formulario',
  templateUrl: './unidad-medida-formulario.component.html'
})
export class UnidadMedidaFormularioComponent extends CrudFormularioBase<UnidadMedidaGridDto> implements OnInit, OnDestroy {

  private destroy$: Subject<void> = new Subject<void>();

  // Selectores
  protected readonly placeHolderSelect: string = GeneralConstant.PLACEHOLDER_DROPDOWN;
  protected readonly placeHolderNoOptions: string = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

  protected idUnidadMedida?: number;
  protected unidadesMedida$: Observable<UnidadMedidaGridDto[]>;

  protected submiting: boolean = false;
  nombre: string;

  constructor(
    private unidadMedidaService : UnidadMedidaService,
    mensajeService: MensajeService,
  ) {
    super(mensajeService);
  }

  public ngOnInit(): void {
    super.onInit();

    this.consultarUnidadesMedida();
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
  }

  protected override consultar(idUnidadMedida: number): Observable<UnidadMedidaGridDto> {
    return this.unidadMedidaService
    .consultarParaFormulario(idUnidadMedida)
      .pipe(
        takeUntil(this.destroy$),
        tap((unidadMedida: UnidadMedidaGridDto) => {
          this.idUnidadMedida = unidadMedida.idUnidadMedida;
        }),
        map((unidadMedida: UnidadMedidaGridDto) => {
          const capturaDto = new UnidadMedidaGridDto();
          capturaDto.idUnidadMedida = unidadMedida.idUnidadMedida;
          capturaDto.nombre = unidadMedida.nombre;

          return capturaDto;
        })
      );
  }

  protected override agregar(unidadMedida: UnidadMedidaGridDto): Observable<void> {
    return this.unidadMedidaService.agregar(unidadMedida);
  }

  protected override editar(unidadMedida: UnidadMedidaGridDto): Observable<void> {
    return this.unidadMedidaService.editar(unidadMedida);
  }

  private consultarUnidadesMedida(): void {
    this.unidadesMedida$ = this.unidadMedidaService.consultarParaGrid();
  }

  protected override onSubmit(formulario: NgForm): void {
    this.entidad.idUnidadMedida = this.idUnidadMedida!;

    super.onSubmit(formulario);
  }

}
