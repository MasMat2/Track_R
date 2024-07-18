import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { DominioDetalleService } from '@http/catalogo/dominio-detalle.service';
import { Dominio } from '@models/catalogo/dominio';
import { DominioDetalle } from '@models/catalogo/dominio-detalle';
import { DominioService } from '@http/catalogo/dominio.service';
import { FormularioService } from '@services/formulario.service';
import { SessionService } from '@services/session.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import * as Utileria from '@utils/utileria';
import * as moment from 'moment';
import { HospitalService } from '../../../../../shared/http/catalogo/hospital.service';
import { Hospital } from '@models/catalogo/hospital';
import { DominioHospitalService } from '../../../../../shared/http/catalogo/dominio-hospital.service';
import { DominioHospitalDto } from '@dtos/catalogo/dominio-hospital-dto';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { DominioHospitalFormularioComponent } from './dominio-hospital-formulario/dominio-hospital-formulario.component';

@Component({
  selector: 'app-dominio-formulario',
  templateUrl: './dominio-formulario.component.html',
  styleUrls: ['./dominio-formulario.component.scss'],
})
export class DominioFormularioComponent implements OnInit {

  @Input() public data: Dominio;
  @Input() public accion: string;
  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;
  public TITULO_MODAL_ELIMINAR = 'Eliminar Dominio Detalle';
  public onClose: any;
  public accionDetalle: string;
  public mensajeAgregar = 'El dominio ha sido agregado';
  public mensajeEditar = 'El dominio ha sido modificado';
  public mensajeError = 'Es necesario agregar un dominio';
  public mensajeAgregarDetalle = 'El dominio detalle ha sido agregado';
  public mensajeEditarDetalle = 'El dominio detalle ha sido modificado';
  public mensajeEliminarDetalle = 'El dominio detalle ha sido eliminado';
  public btnSubmit = false;
  public isDisable = false;
  public form = true;
  public dominio = new Dominio();
  public dominioDetalle = new DominioDetalle();
  public configDate = GeneralConstant.CONFIG_DATEPICKER;
  public minDate = Utileria.obtenerFechaActual();
  public minDateMaxima = Utileria.obtenerFechaActual();
  public dominioDetalleList: DominioDetalle[];

  public columns = [{ headerName: 'Dominio', field: 'valor', minWidth: 150 }];

  public tipoDatoList = [
    { label: '1', name: 'Decimal' },
    { label: '2', name: 'String' },
    { label: '3', name: 'List' },
    { label: '4', name: 'Boolean' },
    { label: '5', name: 'Date' }
  ];

  public tipoCampoList = [
    { label: '1', name: 'Text', display: 'Texto' },
    { label: '2', name: 'Number', display: 'Numérico' },
    { label: '3', name: 'Select', display: 'Selector' },
    { label: '3', name: 'Select Múltiple', display: 'Selector Múltiple' },
    { label: '4', name: 'Switch', display: 'Switch' },
    { label: '5', name: 'Date', display: 'Fecha' },
    { label: '6', name: 'CheckBox', display: 'Checkbox' },
    { label: '7', name: 'TextArea', display: 'Área de Texto' },
    { label: '8', name: 'Radio Button', display: 'Botón de Radio' },
    { label: '9', name: 'Numerico General', display: 'Númerico General' },
    { label: '10', name: 'Decimal General', display: 'Decimal General' },
    { label: '11', name: 'Time', display: 'Tiempo (Hr./Min.)' },
  ];

  public esUsuarioMaestroAtisc = false;

  protected hospitales:Hospital[];
  protected dominioHospital:DominioHospitalDto = {
    idDominio: 0,
    idHospital: 0
  };
  protected tituloEditar = '';

  constructor(
    private mensajeService: MensajeService,
    private formularioService: FormularioService,
    private dominioService: DominioService,
    private dominioDetalleService: DominioDetalleService,
    private sessionService: SessionService,
    private hospitalService:HospitalService,
    private dominioHospitalService:DominioHospitalService,
    private modalService:BsModalService
  ) {}

  public ngOnInit(): void {
    this.esUsuarioMaestroAtisc = this.sessionService.obtenerIdUsuarioSesion() === GeneralConstant.USUARIO_MAESTRO_ATISC;

    if (this.accion === GeneralConstant.COMPONENT_ACCION_EDITAR) {
      this.dominio = this.data;
      this.onDisable();
      this.consultarGrid();
    } else {
      this.dominioDetalle = new DominioDetalle();
    }
    this.accionDetalle = 'Agregar';

    this.obtenerHospitales();
  }

  obtenerHospitales(){
    this.hospitalService.consultarTodosParaSelector(this.dominio.idDominio).subscribe(res => {
      this.hospitales = res;
    })
  }

  // Funciones para validar fechas
  public onMinimaChange(value: any): void {
    if (this.accion !== 'Editar') {
      this.dominio.fechaMinima = value;

      if (this.dominio.fechaMinima && Utileria.isValidDate(value)) {
        this.minDateMaxima = moment(this.dominio.fechaMinima, 'DD-MM-YYYY').toDate();
      }
    }
  }

  public onMaximaChange(value: Date): void {
    if  (this.accion !== 'Editar') {
      this.dominio.fechaMaxima = value;
    }
  }

  // Función para desabilitar campos
  public onDisable(): void {
    if (this.dominio.tipoCampo === 'Date') {
      this.isDisable = true;
    } else {
      this.isDisable = false;
    }

    if (this.dominio.tipoCampo === 'Select'
        || this.dominio.tipoCampo === 'Checkbox'
        || this.dominio.tipoCampo === 'Select Múltiple'
        || this.dominio.tipoCampo === 'Radio Button') {
      this.form = false;
    } else {
      this.form = true;
    }
  }

  //Funcion para el boton guardar
  public enviarFormulario(formulario: NgForm): void {
    this.btnSubmit = true;
    if (!formulario.valid) {
      this.formularioService.validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      return;
    }

    // TODO: 2023-06-14 -> Tipo de Dato default
    this.dominio.tipoDato = 'default';

    if (this.accion === GeneralConstant.MODAL_ACCION_AGREGAR) {
      this.agregar(formulario);
    } else if (this.accion === GeneralConstant.MODAL_ACCION_EDITAR) {
      this.editar();
    }

    //Guardar en BD DominioHospital
    /* this.dominioHospitalService.obtenerDominioHospital(this.dominioHospital.idDominio,this.dominioHospital.idHospital).subscribe(res => {
      if(res != null){
        this.dominioHospitalService.editarDominioHospital(this.dominioHospital).subscribe(res => {})
      }
      else{
        if(this.dominioHospital.idDominio != 0 || this.dominioHospital.idHospital != 0){
          this.dominioHospitalService.guardarDominioHospital(this.dominioHospital).subscribe(res => {})
        } 
      }
    }) */
  }

  public enviarFormularioDetalle(formulario: NgForm): void {
    this.btnSubmit = true;
    if (!formulario.valid) {
      this.formularioService.validarCamposRequeridos(formulario);
      return;
    }

    if (this.accionDetalle === GeneralConstant.MODAL_ACCION_AGREGAR) {
      this.agregarDetalle(formulario);
    } else if (this.accionDetalle === GeneralConstant.MODAL_ACCION_EDITAR) {
      this.editarDetalleFormulario(formulario);
    }
  }

  public consultarGrid(): void {
    this.dominioDetalleService.consultarPorDominio(this.dominio.idDominio).subscribe((data) => {
        this.dominioDetalleList = data;
      });
  }

  public onGridClick(gridData: { accion: string; data: DominioDetalle }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editarDetalle(gridData.data.idDominioDetalle);
      return;
    }

    if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminarDetalle(gridData.data);
      return;
    }
  }

  public agregar(formulario: NgForm): void {
    this.dominioService.agregar(this.dominio).subscribe(
      (data) => {
        this.dominio.idDominio = data;
        this.mensajeService.modalExito(this.mensajeAgregar);
        this.btnSubmit = false;
        this.onClose(true);
      },
      (error) => {
        this.btnSubmit = false;
      }
    );
  }

  public agregarDetalle(formulario: NgForm): void {
    if (this.dominio.idDominio !== undefined) {
      this.dominioDetalle.idDominio = this.dominio.idDominio;
      this.dominioDetalleService.agregar(this.dominioDetalle).subscribe(
        (data) => {
          this.mensajeService.modalExito(this.mensajeAgregarDetalle);
          this.limpiarFormularioDetalle();
          this.consultarGrid();
          this.btnSubmit = false;
        },
        (error) => {
          this.btnSubmit = false;
        }
      );
    } else {
      this.mensajeService.modalError(this.mensajeError);
      this.btnSubmit = false;
    }
  }

  public editar(): void {
    this.dominioService.editar(this.dominio).subscribe(
      (data) => {
        this.mensajeService.modalExito(this.mensajeEditar);
        this.onClose(true);
      },
      (error) => {
        this.btnSubmit = false;
      }
    );
  }

  public editarDetalle(idDominioDetalle: number): void {
    this.dominioDetalleService.consultar(idDominioDetalle).subscribe(
      (data) => {
      this.dominioDetalle.idDominioDetalle = idDominioDetalle;
      this.dominioDetalle.idDominio =  data.idDominio;
      this.dominioDetalle.valor = data.valor;
      this.dominioDetalle.idCompania = data.idCompania;
    });

    this.accionDetalle = 'Editar';
  }

  public editarDetalleFormulario(formulario: NgForm): void {
    this.dominioDetalleService.editar(this.dominioDetalle).subscribe(
      (data) => {
        this.mensajeService.modalExito(this.mensajeEditarDetalle);
        formulario.reset();
        this.limpiarFormularioDetalle();
        this.consultarGrid();
        this.btnSubmit = false;
      },
      (error) => {
        this.btnSubmit = false;
      }
    );
  }

  public eliminarDetalle(dominioDetalle: DominioDetalle): void {
    this.mensajeService
      .modalConfirmacion(
        '¿Desea eliminar el dominio detalle <strong>' +
          dominioDetalle.valor +
          '</strong>?',
        this.TITULO_MODAL_ELIMINAR
      )
      .then((aceptar) => {
        this.dominioDetalleService.eliminar(dominioDetalle.idDominioDetalle).subscribe(
          (data) => {
            this.mensajeService.modalExito(this.mensajeEliminarDetalle);
            this.consultarGrid();
          });
      });
  }

  public limpiarFormularioDetalle(): void {
    this.dominioDetalle = new DominioDetalle();
    this.accionDetalle = GeneralConstant.MODAL_ACCION_AGREGAR;
  }

  public cancelar(): void {
    this.onClose(true);
  }

  protected onGridClickHospital(gridData: { accion: string; data: Hospital }){
    //this.tituloEditar = `Editando Dominio para ${gridData.data.nombre}`;
    if(gridData.accion === GeneralConstant.GRID_ACCION_EDITAR){
      this.dominioHospitalService.obtenerDominioHospital(this.dominio.idDominio,gridData.data.idHospital).subscribe(res => {
        let accion:string;
        if(res != null){
          this.dominioHospital = res
          accion = GeneralConstant.MODAL_ACCION_EDITAR;
        }
        else{
          this.dominioHospital = {
            idDominio: this.dominio.idDominio,
            idHospital: gridData.data.idHospital
          };
          accion = GeneralConstant.MODAL_ACCION_AGREGAR;
        }

        //Crear modal
        const initialState = {
          accion,
          dominioHospital:this.dominioHospital
        };
        let bsModalRef = this.modalService.show(
          DominioHospitalFormularioComponent,
          {initialState, ... GeneralConstant.CONFIG_MODAL_MEDIUM, id: 'modalDominioHospital'}
        );
        bsModalRef.onHide?.subscribe( () => {
          this.obtenerHospitales();
        })

      })
    }
    if(gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR){
      this.dominioHospitalService.eliminarDominioHospital(this.dominio.idDominio,gridData.data.idHospital).subscribe(res => {
        this.mensajeService.modalExito('Dominio de hospital eliminado exitosamente')
      })
    }
  }
}
