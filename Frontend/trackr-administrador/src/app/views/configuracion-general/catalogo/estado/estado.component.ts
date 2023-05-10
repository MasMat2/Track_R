import { Component, OnInit } from '@angular/core';
import { EstadoService } from '@http/catalogo/estado.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Estado } from '@models/catalogo/estado';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EstadoFormularioComponent } from './estado-formulario/estado-formulario.component';

@Component({
  templateUrl: 'estado.component.html',
})
export class EstadoComponent implements OnInit {
  public tieneAccesoAgregar = false;
  public accesoEditar = CodigoAcceso.EDITAR_ESTADO;
  public accesoEliminar = CodigoAcceso.ELIMINAR_ESTADO;
  public HEADER_GRID = 'Estados';
  public MENSAJE_EXITO_ELIMINAR = 'El estado ha sido eliminado';
  public TITULO_MODAL_ELIMINAR = 'Eliminar Estado';
  public estadoList: Estado[];

  public columns = [
    { headerName: 'Clave', field: 'clave', minWidth: 150, width: 70 },
    { headerName: 'Nombre', field: 'nombre', minWidth: 150 },
    { headerName: 'País', field: 'nombrePais', minWidth: 150 },
  ];

  constructor(
    private mensajeService: MensajeService,
    private estadoService: EstadoService,
    private accesoService: AccesoService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef
  ) {}

  public ngOnInit(): void {
    this.accesoService.tieneAcceso(CodigoAcceso.AGREGAR_ESTADO).subscribe({
      next: (data) => {
        this.tieneAccesoAgregar = data;
      },
    });

    this.consultarGrid();
  }

  public consultarGrid(): void {
    this.estadoService.consultarTodosParaGrid().subscribe((data) => {
      this.estadoList = data;
    });
  }

  public onGridClick(gridData: { accion: string; data: Estado }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data.idEstado);
      return;
    }

    if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
      return;
    }
  }

  public agregar(): void {
    this.bsModalRef = this.modalService.show(
      EstadoFormularioComponent,
      GeneralConstant.CONFIG_MODAL_DEFAULT
    );
    this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_AGREGAR;
    this.bsModalRef.content.onClose = (cerrar: boolean) => {
      if (cerrar) {
        this.consultarGrid();
      }
      this.bsModalRef.hide();
    };
  }

  public editar(idEstado: number): void {
    this.estadoService.consultar(idEstado).subscribe((data) => {
      this.bsModalRef = this.modalService.show(
        EstadoFormularioComponent,
        GeneralConstant.CONFIG_MODAL_DEFAULT
      );
      this.bsModalRef.content.estado = data;
      this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_EDITAR;
      this.bsModalRef.content.onClose = (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }
        this.bsModalRef.hide();
      };
    });
  }

  public eliminar(estado: Estado): void {
    this.mensajeService
      .modalConfirmacion(
        '¿Desea eliminar el estado <strong>' + estado.nombre + '</strong>?',
        this.TITULO_MODAL_ELIMINAR
      )
      .then((aceptar) => {
        this.estadoService.eliminar(estado.idEstado).subscribe((data) => {
          this.mensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
          this.consultarGrid();
        });
      });
  }
}
