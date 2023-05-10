import { Component, OnInit } from '@angular/core';
import { AccesoService } from '@http/seguridad/acceso.service';
import { RolService } from '@http/seguridad/rol.service';
import { Rol } from '@models/seguridad/rol';
import { SessionService } from '@services/session.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { RolFormularioComponent } from './rol-formulario/rol-formulario.component';

@Component({
  selector: 'app-rol',
  templateUrl: './rol.component.html',
  styleUrls: ['./rol.component.scss']
})
export class RolComponent implements OnInit {

  public tieneAccesoAgregar = false;
  public accesoEditar = CodigoAcceso.EDITAR_ROL;
  public accesoEliminar = CodigoAcceso.ELIMINAR_ROL;

  public HEADER_GRID = 'Rol';
  public MENSAJE_EXITO_ELIMINAR = 'El rol ha sido eliminado';
  public TITULO_MODAL_ELIMINAR = 'Eliminar Rol';
  public RolList: Rol[];
  public idCompaniaUsuarioSesion: number;

  public columnaEditar = Object.assign(
    {
      action: GeneralConstant.GRID_ACCION_EDITAR,
      title: 'Editar',
      acceso: this.accesoEditar,
      cellRendererSelector: (params: any) => {
        if (params.data.idCompania !== this.idCompaniaUsuarioSesion) {
          return null;
        }

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
        if (params.data.idCompania !== this.idCompaniaUsuarioSesion) {
          return null;
        }

        const component = { component: 'actionButton', params: { disabled: false } };
        return component;
      },
    },
    GeneralConstant.CONFIG_COLUMN_ACTION
  );

  public columns = [

    { headerName: 'Clave', field: 'clave', minWidth: 150 },
    { headerName: 'Rol', field: 'nombre', minWidth: 150 },
    { headerName: 'Filtrado', field: 'filtrado', minWidth: 150,
      valueGetter: (params: any) => params.data.filtrado ? "Sí" : "No"
    },
    this.columnaEditar,
    this.columnaEliminar
  ];

  constructor(
    private mensajeService: MensajeService,
    private rolService: RolService,
    private accesoService: AccesoService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef,
    private sessionService: SessionService
  ) { }

  public ngOnInit(): void {

    this.accesoService.tieneAcceso(CodigoAcceso.AGREGAR_ROL).subscribe({
      next: (data) => {
        this.tieneAccesoAgregar = data;
      },
    });
    this.idCompaniaUsuarioSesion = this.sessionService.obtenerIdCompaniaUsuarioSesion() ?? 0;
    this.consultarGrid();
  }

  public consultarGrid(): void {
    this.rolService.consultarTodosParaGrid().subscribe((data) => {
      this.RolList = data;
    });
  }

  public onGridClick(gridData: { accion: string; data: Rol }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data.idRol);
      return;
    }

    if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
      return;
    }
  }

  public agregar(): void {
    this.bsModalRef = this.modalService.show(
      RolFormularioComponent,
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
  public editar(idRol: number): void {
    this.rolService.consultar(idRol).subscribe((data) => {
      this.bsModalRef = this.modalService.show(
        RolFormularioComponent,
        GeneralConstant.CONFIG_MODAL_DEFAULT
      );
      this.bsModalRef.content.rol = data;
      this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_EDITAR;
      this.bsModalRef.content.onClose = (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }
        this.bsModalRef.hide();
      };
    });
  }

  public eliminar(rol: Rol): void {
    this.mensajeService
      .modalConfirmacion(
        '¿Desea eliminar el rol <strong>' + rol.nombre + '</strong>?',
        this.TITULO_MODAL_ELIMINAR

      )
      .then((aceptar) => {
        this.rolService.eliminar(rol.idRol).subscribe((data) => {
          this.mensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
          this.consultarGrid();
        });
      });
  }

}
