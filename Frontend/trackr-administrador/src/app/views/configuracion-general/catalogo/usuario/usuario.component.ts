import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { CompaniaService } from '@http/catalogo/compania.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { PerfilService } from '@http/seguridad/perfil.service';
import { RolService } from '@http/seguridad/rol.service';
import { TipoUsuarioService } from '@http/seguridad/tipo-usuario.service';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { Compania } from '@models/catalogo/compania';
import { Perfil } from '@models/seguridad/perfil';
import { Rol } from '@models/seguridad/rol';
import { TipoUsuario } from '@models/seguridad/tipo-usuario';
import { Usuario } from '@models/seguridad/usuario';
import { EncryptionService } from '@services/encryption.service';
import { GridConfig } from '@sharedComponents/grid-filtro/grid-config';
import { GridFiltroComponent } from '@sharedComponents/grid-filtro/grid-filtro.component';
import { GridTab } from '@sharedComponents/grid-filtro/grid-tab';
import { GridGeneralComponent } from '@sharedComponents/grid-general/grid-general.component';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import * as Utileria from '@utils/utileria';
import { ColDef, GridOptions } from 'ag-grid-community';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UsuarioFormularioComponent } from './usuario-formulario/usuario-formulario.component';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.scss']
})
export class UsuarioComponent implements OnInit {
  @ViewChild('gridUsuario', { static: false }) public gridUsuario: GridGeneralComponent;
  @ViewChild('gridFiltro', { static: false }) public gridFiltro: GridFiltroComponent;
  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;
  public isCollapsed = true;
  public HEADER_GRID = 'Usuario';
  private MENSAJE_EXITO_ELIMINAR = 'Usuario eliminado exitosamente.';
  private TITULO_MODAL_ELIMINAR = 'Eliminar Usuario';
  public usuarioList: Usuario[];
  public gridOptions: GridOptions;
  public optionsFecha = GeneralConstant.CONFIG_DATEPICKER;
  public tieneAccesoAgregar = false;
  public EDITAR_USUARIO = CodigoAcceso.EDITAR_USUARIO;
  public ELIMINAR_USUARIO = CodigoAcceso.ELIMINAR_USUARIO;
  public buscando = false;
  public filtro: Usuario = new Usuario();
  private MENSAJE_EXITO_BUSCAR = 'Búsqueda realizada correctamente';
  public perfilList: Perfil[];
  public tipoUsuarioList: TipoUsuario[];
  public rolList: Rol[] = [];
  public companiaList: Compania[] = [];
  public esUsuarioCompaniaBase: boolean = false;

  public configTipoUsuario = Object.assign(
    { labelField: 'nombre', valueField: 'idTipoUsuario', searchField: ['nombre'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  public configPerfil = Object.assign(
    { labelField: 'nombre', valueField: 'idPerfil', searchField: ['nombre'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );
  public configRol = Object.assign(
    {...GeneralConstant.CONFIG_DROPDOWN_DEFAULT},
    { labelField: 'nombre', valueField: 'idRol', searchField: ['nombre'], dropdownParent: 'body',
      maxItems: null, plugins: ['dropdown_direction', 'remove_button'] },
  );
  public configCompania = Object.assign(
    {...GeneralConstant.CONFIG_DROPDOWN_DEFAULT},
    { labelField: 'nombre', valueField: 'idCompania', searchField: ['nombre'], dropdownParent: 'body',
      maxItems: null, plugins: ['dropdown_direction', 'remove_button'] },
  );

  public columnaEditar: ColDef = Object.assign(
    {
      action: GeneralConstant.GRID_ACCION_EDITAR,
      title: 'Editar',
      acceso: this.EDITAR_USUARIO,
      cellRendererSelector: (params: any) => {
        const component = { component: 'actionButton', params: { disabled: false } };
        return component;
      },
    },
    GeneralConstant.CONFIG_COLUMN_ACTION
  );

  public columnaEliminar: ColDef = Object.assign(
    {
      action: GeneralConstant.GRID_ACCION_ELIMINAR,
      title: 'Eliminar',
      acceso: this.ELIMINAR_USUARIO,
      cellRendererSelector: (params: any) => {
        const component = { component: 'actionButton',
        params: {
          disabled: (!params.data.habilitado)
        }
      };
      return component;
      }
    },
    GeneralConstant.CONFIG_COLUMN_ACTION
  );

  public columns: ColDef[] = [
    {
      headerName: 'Nombre',
      field: 'nombre',
      minWidth: 150,
      width: 200,
      valueGetter: (params: any) => Utileria.obtenerNombre(params.data)
    },
    {
      headerName: 'Tipo de Usuario',
      field: 'nombreTipoUsuario',
      minWidth: 100,
      width: 110
    },
    {
      headerName: 'Username',
      field: 'correo',
      minWidth: 70,
      width: 100
    },
    {
      headerName: 'Perfil',
      field: 'nombrePerfil',
      minWidth: 80,
      width: 100
    },
    {
      headerName: 'Rol',
      field: 'roles',
      minWidth: 80,
      width: 100
    },
    {
      headerName: 'Teléfono Móvil',
      field: 'telefonoMovil',
      minWidth: 100,
      width: 110
    },
    {
      headerName: 'Estatus',
      field: 'habilitado',
      minWidth: 70,
      width: 100,
      valueGetter: (params) => {
        return params.data.habilitado ? 'Activo' : 'Inactivo';
      }
    },
    {
      headerName: 'Compañía',
      field: 'nombreCompania',
      minWidth: 80,
      width: 100,
      hide:true
    },
    this.columnaEditar,
    this.columnaEliminar
  ];

  public configModal: any = {
    animated: true,
    keyboard: false,
    backdrop: 'static',
    ignoreBackdropClick: true,
    size: 'sm',
    class: 'modal-md modal-size-md modal-position-center'
  };

  public gridConfig: GridConfig = {
    disableDelete: false,
    disableEdit: false,
    headerName: "Usuarios",
    children: this.columns,
    accesoEditar: this.EDITAR_USUARIO,
    accesoEliminar: this.ELIMINAR_USUARIO,
  };

  public gridTabs: GridTab[] = [];

  constructor(
    private modalMensajeService: MensajeService,
    private router: Router,
    private usuarioService: UsuarioService,
    private companiaService: CompaniaService,
    private perfilService: PerfilService,
    private encryptionService: EncryptionService,
    private accesoService: AccesoService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef,
    private tipoUsuarioService: TipoUsuarioService,
    private rolService: RolService
  ) {}

  public ngOnInit(): void {
    this.accesoService.tieneAcceso(CodigoAcceso.AGREGAR_USUARIO).subscribe((data) => {
      this.tieneAccesoAgregar = data;
    });
    this.consultarGrid();
    this.consultarCompanias();
    this.consultarTiposDeUsuario();
    this.consultarPerfiles();
    this.consultarRoles();
  }

  /**
   * Consulta la clave de la compañía del usuario en sesión.
   *
   * Verifica si el usuario en sesión es de la compañía base con la clave,
   * encuentra el ID de la Compañía Base de acuerdo a la lista consultada
   * de las compañías y lo asigna al filtro para que se quede seleccionado
   *
   * Mostrar la columna de nombreCompañía si el usuario es de la compañía base
   */
  public consultarEsUsuarioCompaniaBase(): void {
    this.companiaService
      .consultarClaveCompaniaUsuarioSesion()
      .subscribe((claveCompania) => {
          this.esUsuarioCompaniaBase = claveCompania === GeneralConstant.CLAVE_COMPANIA_BASE;
          const idCompania= this.companiaList
            .find((compania) =>
            compania.clave == claveCompania)?.idCompania;
          this.filtro.idsCompania = idCompania !== undefined ? [idCompania] : [];
          this.gridFiltro.modificarVisibilidadColumna('nombreCompania', this.esUsuarioCompaniaBase);
      });
  }

  public consultarGrid(): void {
    this.usuarioService
      .consultarGeneral()
      .subscribe((data) => {
        this.usuarioList = data;
      });
  }

  public onGridClick(gridData: { accion: string; data: Usuario }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data.idUsuario);
    } else if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
    }
  }

  public editar(idUsuario: number): void {
    this.usuarioService.consultar(idUsuario).subscribe((data) => {

      const initialState = {
        accion: GeneralConstant.COMPONENT_ACCION_EDITAR,
        usuario: data
      };

      this.bsModalRef = this.modalService.show(
        UsuarioFormularioComponent,
        { initialState, ... GeneralConstant.CONFIG_MODAL_LARGE}
      );

      this.bsModalRef.content.onClose = (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }
        this.bsModalRef.hide();
      };
    });

  }

  public eliminar(usuario: Usuario): void {
    this.modalMensajeService
      .modalConfirmacion(
        '¿Desea eliminar el usuario <strong>' + usuario.nombreCompleto + '</strong>?',
        this.TITULO_MODAL_ELIMINAR,
        GeneralConstant.ICONO_CRUZ
      )
      .then((aceptar) => {
        this.usuarioService.eliminar(usuario.idUsuario).subscribe((data) => {
          this.modalMensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
          this.consultarGrid();
        });
      });
  }

  public agregar(): void {
    this.bsModalRef = this.modalService.show(
      UsuarioFormularioComponent,
      GeneralConstant.CONFIG_MODAL_LARGE
    );
    this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_AGREGAR;
    this.bsModalRef.content.onClose = (cerrar: boolean) => {
      if (cerrar) {
        this.consultarGrid();
      }
      this.bsModalRef.hide();
    };
  }

  public buscar(): void {
    this.buscando = true;
    if (!Boolean(this.filtro.idTipoUsuario)) {
      this.filtro.idTipoUsuario = 0;
    }
    this.usuarioService.consultarBusquedaGridFiltro(this.filtro).subscribe((data) => {
      this.buscando = false;
      this.usuarioList = data;
      this.modalMensajeService.modalExito(this.MENSAJE_EXITO_BUSCAR);
    });
  }

  public limpiar(): void  {
    this.filtro = new Usuario();
    this.consultarGrid();
  }

  public consultarPerfiles(): void {
    this.perfilService.consultarPorCompania().subscribe((data) => {
      this.perfilList = data;
    });
  }

  public consultarRoles() {
    this.rolService.consultarTodosParaSelector().subscribe((data) => {
      this.generarGridTabs(data);
      this.rolList = data;
    });
  }

  public consultarTiposDeUsuario(): any {
    this.tipoUsuarioService.consultarTiposUsuarioSelector().subscribe( (data) => {
      this.tipoUsuarioList = data;
    });
  }

  /**
   * Consulta las compañías para llenar el selector de compañías
   * Espera a que termine ese método para consultar si el usuario es de la compañía base
   */
  public async consultarCompanias(): Promise<void> {
    this.companiaService.consultarTodosParaSelector().subscribe((data) => {
      this.companiaList = data;
      this.consultarEsUsuarioCompaniaBase();
    });

  }

  private generarGridTabs(roles: Rol[]) {
    const temp: GridTab[] = [];

    const tabInterno: GridTab = {
      title: "Interno",
      filter: {
        field: "nombreTipoUsuario",
        criteria: (tipoUsuario: string) => tipoUsuario === "Interno"
      }
    };

    const tabExterno: GridTab = {
      title: "Externo",
      filter: {
        field: "nombreTipoUsuario",
        criteria: (tipoUsuario: string) => tipoUsuario === "Externo"
      }
    };

    temp.push(tabInterno);
    temp.push(tabExterno);

    roles.filter(rol => rol.filtrado)
      .forEach(rol => {
        const gridTab: GridTab = {
          title: rol.nombre,
          filter: {
            field: "roles",
            criteria: (roles: string) => roles.split(', ').includes(rol.nombre)
          }
        }

        temp.push(gridTab);
      });

    this.gridTabs = temp;
  }
}
