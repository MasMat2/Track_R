import { Component, OnInit } from '@angular/core';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Dominio } from '@models/catalogo/dominio';
import { DominioService } from '@http/catalogo/dominio.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import * as moment from 'moment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { DominioFormularioComponent } from './dominio-formulario/dominio-formulario.component';

@Component({
  templateUrl: './dominio.component.html',
})
export class DominioComponent implements OnInit {
  public tieneAccesoAgregar = false;
  public accesoEditar = CodigoAcceso.EDITAR_DOMINIO;
  public accesoEliminar = CodigoAcceso.ELIMINAR_DOMINIO;
  public HEADER_GRID = 'Dominios';
  public MENSAJE_EXITO_ELIMINAR = 'El dominio ha sido eliminado';
  public TITULO_MODAL_ELIMINAR = 'Eliminar Dominio';
  public dominioList: Dominio[];
  public accion: string;

  public columnaEditar = Object.assign(
    {
      action: GeneralConstant.GRID_ACCION_EDITAR,
      title: 'Editar',
      acceso: this.accesoEliminar,
      cellRendererSelector: (params: any) => {
        const component = { component: 'actionButton', params: { disabled: false } };
        return component;
      },
    },
    GeneralConstant.CONFIG_COLUMN_ACTION
  );

  public columnaEliminar = Object.assign(
    {
      action: GeneralConstant.GRID_ACCION_ELIMINAR,
      title: 'Eliminar',
      acceso: this.accesoEliminar,
      cellRendererSelector: (params: any) => {
        const component = { component: 'actionButton', params: { disabled: false } };
        return component;
      },
    },
    GeneralConstant.CONFIG_COLUMN_ACTION
  );

  public columns = [
    { headerName: 'Dominio', field: 'nombre', minWidth: 150 },
    { headerName: 'Descripción', field: 'descripcion', minWidth: 150 },
    this.columnaEditar,
    this.columnaEliminar
  ];

  constructor(
    private mensajeService: MensajeService,
    private dominioService: DominioService,
    private accesoService: AccesoService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef
  ) { }

  public ngOnInit(): void {

    this.accesoService.tieneAcceso(CodigoAcceso.AGREGAR_DOMINIO).subscribe({
      next: (data) => {
        this.tieneAccesoAgregar = data;
      },
    });

    this.consultarGrid();
  }

  public consultarGrid(): void {
    this.dominioService.consultarTodosParaGrid().subscribe((data) => {

      this.dominioList = data;
    });
  }

  public onGridClick(gridData: { accion: string; data: Dominio }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data.idDominio);
      return;
    }

    if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
      return;
    }
  }

  public agregar(): void {
    const initialState = {
      accion: GeneralConstant.COMPONENT_ACCION_AGREGAR,
    };

    this.bsModalRef = this.modalService.show(
      DominioFormularioComponent,
      { initialState, ... GeneralConstant.CONFIG_MODAL_LARGE}
    );
    this.bsModalRef.content.onClose = (cerrar: boolean) => {
      if (cerrar) {
        this.consultarGrid();
      }
      this.bsModalRef.hide();
    };
  }

  public editar(idDominio: number): void {
    this.dominioService.consultar(idDominio).subscribe((dataModal) => {

      const initialState = {
        accion: GeneralConstant.COMPONENT_ACCION_EDITAR,
        data: dataModal
      };

      this.bsModalRef = this.modalService.show(
        DominioFormularioComponent,
        { initialState, ... GeneralConstant.CONFIG_MODAL_LARGE, id:'modalDominioFormulario'}
      );

      this.bsModalRef.content.dominio = dataModal;
      if (dataModal.fechaMinima) {
        this.bsModalRef.content.dominio.fechaMinima = new Date(moment(dataModal.fechaMinima).format('MM/DD/YYYY'));
      }
      if (dataModal.fechaMaxima) {
        this.bsModalRef.content.dominio.fechaMaxima = new Date(moment(dataModal.fechaMaxima).format('MM/DD/YYYY'));
      }
      this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_EDITAR;
      this.bsModalRef.content.onClose = (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }
        this.bsModalRef.hide();
      };
    });
  }

  public eliminar(dominio: Dominio): void {
    this.mensajeService
      .modalConfirmacion(
        '¿Desea eliminar el dominio <strong>' + dominio.nombre + '</strong>?',
        this.TITULO_MODAL_ELIMINAR
      )
      .then((aceptar) => {
        this.dominioService.eliminar(dominio.idDominio).subscribe((data) => {
          this.mensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
          this.consultarGrid();
        });
      });
  }
}
