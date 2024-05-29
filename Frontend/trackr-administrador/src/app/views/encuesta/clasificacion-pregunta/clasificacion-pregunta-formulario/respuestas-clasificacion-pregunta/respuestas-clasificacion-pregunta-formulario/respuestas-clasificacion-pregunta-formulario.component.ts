import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { RespuestasClasificacionPreguntaService } from '@http/examen/respuestas-clasificacion-pregunta.service';
import { CrudFormularioBase } from '@sharedComponents/crud/crud-base/crud-formulario-base';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { DROPDOWN_PLACEHOLDER, DROPDOWN_NO_OPTIONS, FORM_ACTION } from '@utils/constants/constants';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Observable, Subject } from 'rxjs';
import { RespuestasClasificacionPreguntaFormularioDto } from 'src/app/shared/examen/respuestas-clasificacion-pregunta-formulario-dto';

@Component({
  selector: 'app-respuestas-clasificacion-pregunta-formulario',
  templateUrl: './respuestas-clasificacion-pregunta-formulario.component.html',
  styleUrls: ['./respuestas-clasificacion-pregunta-formulario.component.scss']
})
export class RespuestasClasificacionPreguntaFormularioComponent extends CrudFormularioBase<RespuestasClasificacionPreguntaFormularioDto> implements OnInit, OnDestroy{
  @ViewChild('formulario', { static: false }) public formulario: NgForm;
  @Input() idClasificacionPregunta: number;
  modoEditar: boolean = false;
  public esEdicion = false;
  public DROPDOWN_PLACEHOLDER: string = DROPDOWN_PLACEHOLDER;
  private destroy$: Subject<void> = new Subject<void>();
  public DROPDOWN_NO_OPTIONS: string = DROPDOWN_NO_OPTIONS;
  
  constructor(
    private respuestasClasificacionPreguntaService: RespuestasClasificacionPreguntaService,
    mensajeService: MensajeService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef,
    protected router: Router
  ) {
    super(mensajeService);
  }
  public ngOnDestroy(): void {
    this.destroy$.next();
  }
  ngOnInit(){
    this.entidad.idClasificacionPregunta = this.idClasificacionPregunta;
    super.onInit();
    this.esEditar();
  }
  public esEditar() {
    if (this.accion == FORM_ACTION.Editar) {
      this.modoEditar=true;
    }
  }
  
  protected override consultar(idEntidad: number): Observable<RespuestasClasificacionPreguntaFormularioDto> {
    return this.respuestasClasificacionPreguntaService.consultarParaFormulario(idEntidad);
  }
  protected override agregar(entidad: RespuestasClasificacionPreguntaFormularioDto): Observable<void> {
    return this.respuestasClasificacionPreguntaService.agregar(entidad);
  }
  protected override editar(entidad: RespuestasClasificacionPreguntaFormularioDto): Observable<void> {
    return this.respuestasClasificacionPreguntaService.editar(entidad);
  }
  
  protected override onSubmit(): void {
    let formulario = this.formulario;
    if (formulario) {
      super.onSubmit(formulario);
    }
  }

  protected cerrar(formulario: NgForm): void {
    // super.onSubmit(formulario);
  }

}
