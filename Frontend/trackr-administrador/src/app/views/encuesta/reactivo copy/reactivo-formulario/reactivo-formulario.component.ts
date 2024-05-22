import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ArchivoDto } from '@dtos/archivos/archivo-dto';
import { AsignaturaService } from '@http/examen/asignatura.service';
import { NivelExamenService } from '@http/examen/nivel-examen.service';
import { ReactivoService } from '@http/examen/reactivo.service';
import { Asignatura } from '@models/examen/asignatura';
import { NivelExamen } from '@models/examen/nivel-examen';
import { Reactivo } from '@models/examen/reactivo';
import { ACCESO_ASIGNATURA } from '@utils/codigos-acceso/examen.acceso';
import { DROPDOWN_NO_OPTIONS, DROPDOWN_PLACEHOLDER, FORM_ACTION } from '@utils/constants/constants';
import { MODAL_CONFIG } from '@utils/constants/modal';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { FormularioService } from 'src/app/shared/services/formulario.service';
import { Respuesta1FormularioComponent } from './respuesta-formulario/respuesta-formulario.component';
import { Respuesta } from '@models/examen/respuesta';
import { RespuestaService } from '@http/examen/respuesta.service';
import { SimpleSelectorDto } from '@dtos/general/simple-selector-dto';
import { ClasificacionPreguntaService } from '@http/clasificacion-pregunta.service';

function circleCellRenderer(params: any) {
  const color = params.value ? '#ed7d31' : '#fff';
  return `<svg width="106" height="28" xmlns="http://www.w3.org/2000/svg">
    
    <rect x="5%" y="15%" width="18" height="18" fill="${color}" stroke="gray" stroke-width="2" />
    </svg>`;
};
@Component({
  selector: 'app-reactivo-formulario',
  templateUrl: './reactivo-formulario.component.html',
  styleUrls: ['./reactivo-formulario.component.scss'],
})
export class Reactivo1FormularioComponent implements OnInit {
  protected readonly URL_IMAGEN_DEFAULT: string = 'assets/img/svg/image-solid.png';
  protected readonly TOOLTIP_INSTRUCCIONES: string = "Para saltar línea necesita presionar 'Shift + Enter'";

  protected submitting: boolean = false;

  protected reactivo = new Reactivo();
  protected imagenBase64 : SafeResourceUrl; 

  // Selectores
  protected readonly placeHolderSelect: string = DROPDOWN_PLACEHOLDER;
  protected readonly placeHolderNoOptions: string = DROPDOWN_NO_OPTIONS;

  protected asignaturaList: Asignatura[] = [];
  protected nivelExamenList: NivelExamen[] = [];
  protected respuestasList: Respuesta[] = [];
  protected clasificacionPreguntaList: SimpleSelectorDto[] = []

  private readonly MENSAJE_AGREGAR: string = 'El reactivo ha sido agregado';
  private readonly MENSAJE_EDITAR: string = 'El reactivo ha sido modificado';

  
  // Inputs
  public accion: string ;
  public esEdicion = false;
  public idReactivo?: number;
  public onClose: (actualizar: boolean) => void;
  public soloRespuestas: boolean = true;

  public accesoEditarRespuesta = ACCESO_ASIGNATURA.Editar;
  public accesoEliminarRespuesta = ACCESO_ASIGNATURA.Eliminar;
  public noAcceso = ACCESO_ASIGNATURA.NoAcceso;

  public preguntasColumns = [
    { headerName: 'Identificador', field: 'clave', minWidth: 10, maxWidth:130 },
    { headerName: 'Respuesta', field: 'respuesta1', minWidth: 200},
    { headerName: 'Respuesta Correcta', field:'respuestaCorrecta', minWidth: 25, maxWidth:170, 
      cellRenderer: circleCellRenderer},
    { headerName: 'Valor', field: 'valor'}
  ];
  constructor(
    private formularioService: FormularioService,
    private mensajeService: MensajeService,
    private reactivoService: ReactivoService,
    private asignaturaService: AsignaturaService,
    private nivelExamenService: NivelExamenService,
    private sanitizer: DomSanitizer,
    private modalService: BsModalService,
    private respuestaService: RespuestaService,
    private clasificacionPreguntaService: ClasificacionPreguntaService
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

    this.reactivo.imagenNombre = '';
    this.consultarAsignaturas();
    this.consultarNivelesExamen();
    this.consultarClasificacionPregunta();
  }

  private consultarReactivo(idReactivo: number): void {
    this.reactivoService
      .consultar(idReactivo)
      .subscribe((reactivo: Reactivo) => {
        reactivo.fechaAlta = new Date(reactivo.fechaAlta);
        this.reactivo = reactivo;
        this.imagenBase64 = this.sanitizer.bypassSecurityTrustResourceUrl(`data:${reactivo.imagenTipoMime};base64,${reactivo.imagen}`);
        this.respuestasList = reactivo.respuestaList;
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

  protected recibirImagen(event: ArchivoDto): void {
    this.reactivo.imagen = event.archivo;
    this.reactivo.imagenTipoMime = event.tipoMime;
    this.reactivo.imagenNombre = event.nombreArchivo;
    this.imagenBase64 = this.sanitizer.bypassSecurityTrustResourceUrl(`data:${event.tipoMime};base64,${event.archivo}`);
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
          this.consultarGridRespuestas();
          this.submitting = false;
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
          //this.onClose(true);
          this.consultarGridRespuestas();
          this.submitting = false;
        },
        error: (error) => {
          this.submitting = false;
        }
      });
  }

  protected cancelar(): void {
    this.onClose(false);
  }

  public onGridClick(gridData: { accion: string; data: any }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editarRespuesta(gridData.data);
    } else if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.mensajeService
      .modalConfirmacion(
        '¿Desea eliminar la selección?',
        'Eliminar',
        GeneralConstant.ICONO_CRUZ
      )
      .then((aceptar: any) => {
        this.respuestaService.eliminar(gridData.data.idRespuesta).subscribe((data) => {
          this.mensajeService.modalExito('Eliminado con Exito');
          this.consultarGridRespuestas();
        });
      });
    }
  }

  public borrarImagen(){
    this.imagenBase64 = '';
    this.reactivo.imagen = '';
    this.reactivo.imagenTipoMime = '';
    this.reactivo.imagenNombre = '';
  }

  public downl_endpoint(event:number){
    return this.reactivoService.consultarArchivo(event);
  }

  public consultarGridRespuestas(){
    this.respuestaService.consultarTodosPorReactivo(this.idReactivo!).subscribe((data) =>{
      this.respuestasList = data;
      this.reactivo.respuestaList = this.respuestasList;
    },
    )
  }

  public abrirRespuesta(){
    const initialState = {
      accion: FORM_ACTION.Agregar,
      esEdicion: false,
      idReactivo: this.idReactivo,
      onClose: (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGridRespuestas();
        }

        bsModalRef.hide();
      }
    };

    const bsModalRef = this.modalService.show(
      Respuesta1FormularioComponent,
      {
        initialState: initialState,
        ...MODAL_CONFIG.Medium
      }
    );
  }

  public editarRespuesta(data: Respuesta){
    const initialState = {
      accion: FORM_ACTION.Editar,
      esEdicion: false,
      idReactivo: this.idReactivo,
      resp: data,
      onClose: (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGridRespuestas();
        }

        bsModalRef.hide();
      }
    };

    const bsModalRef = this.modalService.show(
      Respuesta1FormularioComponent,
      {
        initialState: initialState,
        ...MODAL_CONFIG.Medium
      }
    );
  }

  protected cambiosReglasNegocio(){
    if(this.reactivo.escalaLikert){
      this.reactivo.simple = false;
      this.reactivo.multiple = false;
      this.reactivo.abierta = false;
    }
    if(this.reactivo.abierta){
      this.reactivo.simple = false;
      this.reactivo.multiple = false;
      this.reactivo.escalaLikert = false;
      this.reactivo.necesitaRevision = true;
    }
    if(!this.reactivo.abierta){
      this.reactivo.necesitaRevision = false;
    }
  }

  protected consultarClasificacionPregunta(){
    this.clasificacionPreguntaService
      .consultarSelector()
      .subscribe((data: SimpleSelectorDto[]) => {
        this.clasificacionPreguntaList = data;
      });
  }
}
