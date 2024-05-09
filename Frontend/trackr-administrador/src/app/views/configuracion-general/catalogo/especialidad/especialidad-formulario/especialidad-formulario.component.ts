import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { EspecialidadFormularioCapturaDto } from '@dtos/catalogo/especialidad-formulario-captura-dto';
import { EspecialidadFormularioConsultaDto } from '@dtos/catalogo/especialidad-formulario-consulta-dto';
import { EspecialidadService } from '@http/catalogo/especialidad.service';
import { Especialidad } from '@models/catalogo/especialidad';
import { CrudFormularioBase } from '@sharedComponents/crud/crud-base/crud-formulario-base';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import { Observable, Subject, map, takeUntil, tap } from 'rxjs';

@Component({
  selector: 'app-especialidad-formulario',
  templateUrl: './especialidad-formulario.component.html',
})
export class EspecialidadFormularioComponent extends CrudFormularioBase<EspecialidadFormularioCapturaDto> implements OnInit, OnDestroy {
  private destroy$: Subject<void> = new Subject<void>();

  // Selectores
  protected readonly placeHolderSelect: string = GeneralConstant.PLACEHOLDER_DROPDOWN;
  protected readonly placeHolderNoOptions: string = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

  protected idEspecialidad?: number;
  protected especialidades$: Observable<Especialidad[]>;
  
  protected submiting: boolean = false;
  nombre: string;

  constructor(
    private especialidadService: EspecialidadService,
    mensajeService: MensajeService,
  ) {
    super(mensajeService);
  }

  public ngOnInit(): void {
    super.onInit();

    this.consultarEspecialidades();
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
  }

  protected override consultar(idEspecialidad: number): Observable<EspecialidadFormularioCapturaDto> {
    return this.especialidadService
      .consultarParaFormulario(idEspecialidad)
      .pipe(
        takeUntil(this.destroy$),
        tap((especialidad: EspecialidadFormularioConsultaDto) => {
          this.idEspecialidad = especialidad.idEspecialidad;
        }),
        map((especialidad: EspecialidadFormularioConsultaDto) => {
          const capturaDto = new EspecialidadFormularioCapturaDto();
          capturaDto.idEspecialidad = especialidad.idEspecialidad;
          capturaDto.nombre = especialidad.nombre;

          return capturaDto;
        })
      );

  }
  

  protected override agregar(especialidad: EspecialidadFormularioCapturaDto): Observable<void> {
    console.log(especialidad);
    console.log(this.especialidadService.agregar(especialidad));
    return this.especialidadService.agregar(especialidad);
  }

  protected override editar(especialidad: EspecialidadFormularioCapturaDto): Observable<void> {
    return this.especialidadService.editar(especialidad);
  }

  protected override onSubmit(formulario: NgForm): void {
    // Verifica si this.idEspecialidad es undefined
    if (typeof this.idEspecialidad !== 'undefined') {
      this.entidad.nombre = this.nombre;
    }
  
    super.onSubmit(formulario);

  
  }
  private consultarEspecialidades(): void {
    this.especialidades$ = this.especialidadService.consultarParaGrid();
  }
}