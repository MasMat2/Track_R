import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ClasificacionPreguntaService } from '@http/clasificacion-pregunta.service';
import { CrudFormularioBase } from '@sharedComponents/crud/crud-base/crud-formulario-base';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { FORM_ACTION } from '@utils/constants/constants';
import { GeneralConstant } from '@utils/general-constant';
import { Observable, Subject } from 'rxjs';
import { ClasificacionPreguntaFormularioDto } from 'src/app/shared/examen/clasificacion-pregunta-formulario-dto';

@Component({
  selector: 'app-clasificacion-pregunta-formulario',
  templateUrl: './clasificacion-pregunta-formulario.component.html',
  styleUrls: ['./clasificacion-pregunta-formulario.component.scss']
})
export class ClasificacionPreguntaFormularioComponent extends CrudFormularioBase<ClasificacionPreguntaFormularioDto> implements OnInit, OnDestroy{
  @ViewChild('formulario', { static: false }) public formulario: NgForm;
  
  public deshabilitarAgregar: boolean = false;
  private destroy$: Subject<void> = new Subject<void>();
  public seleccionado: string | undefined;
  public esAgregar: boolean = false;
  modoEditar: boolean = false;
  public esEdicion = false;
  protected readonly placeHolderSelect: string = GeneralConstant.PLACEHOLDER_DROPDOWN;
  protected readonly placeHolderNoOptions: string = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

  protected submiting: boolean = false;

  constructor(
    mensajeService: MensajeService, 
    private clasificacionPreguntaService: ClasificacionPreguntaService,
   
  ) {
    super(mensajeService);
  }
  
  public async ngOnInit(): Promise<void>{
    super.onInit();
    this.esEditar();

    if (this.accion === FORM_ACTION.Agregar && !this.idEntidad) {
      this.esAgregar = true;
      
    }
  }


  public esEditar() {
    if (this.accion == FORM_ACTION.Editar) {
      this.modoEditar = true;
    }
  }
  public ngOnDestroy(): void {
    this.destroy$.next();
  }
  protected override consultar(idClasificacionPregunta: number): Observable<ClasificacionPreguntaFormularioDto> {
    return this.clasificacionPreguntaService.consultar(idClasificacionPregunta);
  }
  protected override agregar(entidad: ClasificacionPreguntaFormularioDto): Observable<void> {
    return this.clasificacionPreguntaService.agregar(entidad);
  }
  protected override editar(entidad: ClasificacionPreguntaFormularioDto): Observable<void> {
    return this.clasificacionPreguntaService.editar(entidad);
  }

}
