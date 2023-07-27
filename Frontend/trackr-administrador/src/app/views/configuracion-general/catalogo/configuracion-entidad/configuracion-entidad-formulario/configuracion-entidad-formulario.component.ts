import { Location } from "@angular/common";
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ITreeOptions, TREE_ACTIONS } from '@circlon/angular-tree-component';
import { EntidadEstructuraService } from '@http/gestion-entidad/entidad-estructura.service';
import { EntidadService } from '@http/gestion-entidad/entidad.service';
import { SeccionService } from '@http/gestion-entidad/seccion.service';
import { Entidad } from '@models/gestion-entidad/entidad';
import { EntidadEstructura } from '@models/gestion-entidad/entidad-estructura';
import { Seccion } from '@models/gestion-entidad/seccion';
import { CrudFormularioBase } from '@sharedComponents/crud/components/crud-formulario-base';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { SeccionCampoModalComponent } from './seccion-campo-modal/seccion-campo-modal.component';

@Component({
  selector: 'app-configuracion-entidad-formulario',
  templateUrl: './configuracion-entidad-formulario.component.html',
  styleUrls: ['./configuracion-entidad-formulario.component.scss']
})
export class ConfiguracionEntidadFormularioComponent extends CrudFormularioBase<Entidad> implements OnInit {
  @ViewChild('claveEntidadEstructura', { static: false }) claveEstructuraRef: NgControl;
  @ViewChild('nombreEntidadEstructura', { static: false }) nombreEstructuraRef: NgControl;

  private TITULO_MODAL_ELIMINAR: string  = `Eliminar Jerarquía Estructura`;
  private MENSAJE_EXITO_ELIMINAR: string = "La estructura de jerarquía ha sido eliminada";
  public MENSAJE_EXITO_AGREGAR_ESTRUCTURA: string = "La sección ha sido agregada";

  // Grid Secciones
  public elementosSeleccionados: Seccion[] = [];
  public HEADER_GRID_SECCIONES = 'Secciones';
  public seccionesList: Seccion[] = [];

  public columnaVer = Object.assign(
    {
      title: 'Ver',
      action: GeneralConstant.GRID_ACCION_VER,
      cellRendererSelector: (params: any) => {
        const component = { component: 'actionButton', params: { disabled: false } };
        return component;
      }
    },
    GeneralConstant.CONFIG_COLUMN_ACTION
  );

  public columnaCheckbox = {
    headerName: '',
    field: '',
    width: 30,
    minWidth: 40,
    cellClass: 'center-ag',
    headerCheckboxSelection: true,
    headerCheckboxSelectionFilteredOnly: true,
    checkboxSelection: true,
    resizable: false,
    sortable: false,
    filter: false,
    suppressMovable: true,
    lockPosition: true,
    pinned: 'right'
  };

  public columnsSeccion = [
    { headerName: 'Clave', field: 'clave', minWidth: 50 },
    { headerName: 'Sección', field: 'nombre', minWidth: 150 },
    this.columnaCheckbox,
    this.columnaVer
  ];

  // Arbol de Jerarquía
  public jerarquiaArbol: EntidadEstructura[] = [];
  public options: ITreeOptions = {
    displayField: 'nombre',
    isExpandedField: 'expanded',
    idField: 'idEntidadEstructura',
    childrenField: 'hijos',
    useCheckbox: false,
    useTriState: false,
    actionMapping: {
      mouse: {
        dblClick: TREE_ACTIONS.TOGGLE_EXPANDED
      }
    }
  };

  // Selector Jerarquía Padre
  public idJerarquiaPadre?: number;
  public entidadList: EntidadEstructura[] = [];

  public placeholderSelect: string = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeholderNoOptions: string = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

  public configJerarquiaPadre = Object.assign(
    { labelField: 'nombre', valueField: 'idEntidadEstructura', searchField: ['nombre'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  // Configuración General
  public entidadEstructura: EntidadEstructura = {} as EntidadEstructura;
  public disableAgregar: boolean = true;
  public loading: boolean = true;
  public esNuevaPestana: boolean = false;

  constructor(
    private entidadService: EntidadService,
    private entidadEstructuraService: EntidadEstructuraService,
    private seccionService: SeccionService,
    private bsModalRef: BsModalRef,
		private bsModalService: BsModalService,
    mensajeService: MensajeService,
    router: Router,
    location: Location
  ) {
    super(entidadService, mensajeService, router, location);
  }

  async ngOnInit(): Promise<void> {
    await super.onInit();
    this.actualizarInformacion();
  }

  // #region Consultas
  private async consultarArbol(): Promise<EntidadEstructura[]> {
    const arbol = await this.entidadEstructuraService
      .consultarArbol(this.idEntidad)
      .toPromise();

    return arbol ?? [];
  }

  private async consultarGrid(): Promise<Seccion[]> {
    const grid = await this.seccionService
      .consultarGeneral()
      .toPromise();

    return grid ?? [];
  }

  private async consultarSelector(): Promise<EntidadEstructura[]> {
    const estructuras = await this.entidadEstructuraService
      .consultarPorEntidadParaSelector(this.idEntidad)
      .toPromise();

    return estructuras ?? [];
  }
  // #endregion

  private async actualizarInformacion(): Promise<void> {

    if (!(this.accion == GeneralConstant.MODAL_ACCION_EDITAR)) {
      this.loading = false;
      return;
    }

    let p1 = this.consultarArbol();
    let p2 = this.consultarSelector();
    let p3 = this.consultarGrid();

    return Promise.all([p1, p2, p3])
      .then((data) => {
        this.idJerarquiaPadre = undefined;
        this.elementosSeleccionados = [];

        this.jerarquiaArbol = data[0];
        this.entidadList = data[1];
        this.seccionesList = data[2];
      })
      .catch((error) => { })
      .finally(() => {
        this.loading = false;
      });
  }

  // #region Árbol de Jerarquía
  public onSelectionChange(event: any): void {
    this.elementosSeleccionados = [];

    event.forEach((elemento: any) => {
      this.elementosSeleccionados.push(elemento.data);
    });

    this.actualizarBotonAgregar();
  }

  public onGridClick(gridData: { accion: string; data: Seccion }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_VER) {
      this.verCamposSeccion(gridData.data.idSeccion);
    }
  }

  public seleccionarCuentaPadre(node: EntidadEstructura): void {
    if (node.idEntidadEstructuraPadre === undefined) {
      this.idJerarquiaPadre = node.idEntidadEstructura;
    }
  }

  private verCamposSeccion(idSeccion: number): void{
    const initialState = {
			idSeccion: idSeccion,
		};

		this.bsModalRef = this.bsModalService.show(
			SeccionCampoModalComponent,
			{
        initialState,
        ...GeneralConstant.CONFIG_MODAL_MEDIUM
      }
		);

    this.bsModalRef.content.onClose = () => {
      this.bsModalRef.hide();
    }
  }

  private obtenerJerarquiasSeleccionadas(): EntidadEstructura[] {
    let estructurasSeleccionadas: EntidadEstructura[] = [];

    this.elementosSeleccionados.forEach((seccion: Seccion) => {
      let entidadEstructura = {} as EntidadEstructura;

      entidadEstructura.idEntidad = this.entidad.idEntidad;
      entidadEstructura.idEntidadEstructuraPadre = this.idJerarquiaPadre;
      entidadEstructura.idSeccion = seccion.idSeccion;

      estructurasSeleccionadas.push(entidadEstructura);
    });

    return estructurasSeleccionadas;
  }

  public async onAgregarClick(): Promise<void> {
    // Validar que se cuente con una pestaña seleccionada, para agregar secciones
    if (!this.esNuevaPestana && this.idJerarquiaPadre === undefined) {
      this.mensajeService.modalError("Es necesario seleccionar una pestaña, para asignar sus secciones");
      return;
    }

    this.disableAgregar = true;
    this.disableSubmit = true;

    if (this.esNuevaPestana)
      await this.agregarNuevoNodo();
    else
      await this.agregarNodosSeleccionados();

    this.disableSubmit = false;
    this.elementosSeleccionados = [];
    this.actualizarBotonAgregar();
  }

  private limpiarCamposNuevoNodo() {
    this.entidadEstructura.clave = "";
    this.entidadEstructura.nombre = "";

    if (this.claveEstructuraRef != undefined) {
      this.claveEstructuraRef.control?.markAsPristine();
      this.claveEstructuraRef.control?.markAsUntouched();
    }

    if (this.nombreEstructuraRef != undefined) {
      this.nombreEstructuraRef.control?.markAsPristine();
      this.nombreEstructuraRef.control?.markAsUntouched();
    }
  }

  private async agregarNuevoNodo(): Promise<void> {
    // Validar campos
    if (!this.claveEstructuraRef.valid || !this.nombreEstructuraRef.valid) {
      this.claveEstructuraRef.control?.markAsDirty();
      this.claveEstructuraRef.control?.markAsTouched();

      this.nombreEstructuraRef.control?.markAsDirty();
      this.nombreEstructuraRef.control?.markAsTouched();

      return;
    }

    // Agregar registro
    this.entidadEstructura.idEntidad = this.entidad.idEntidad;
    this.entidadEstructura.idSeccion = undefined;
    this.entidadEstructura.tabulacion = true;

    await this.agregarEntidadEstructura([this.entidadEstructura]);
    this.limpiarCamposNuevoNodo();
  }

  private async agregarNodosSeleccionados(): Promise<void> {
    let jerarquiasSeleccionadas = this.obtenerJerarquiasSeleccionadas();
    await this.agregarEntidadEstructura(jerarquiasSeleccionadas);
  }

  private async agregarEntidadEstructura(estructuras: EntidadEstructura[]) {
    return this.entidadEstructuraService.agregar(estructuras).toPromise()
      .then(async (data) => {
        await this.actualizarInformacion();
        this.mensajeService.modalExito(this.MENSAJE_EXITO_AGREGAR_ESTRUCTURA);
      })
      .catch((error) => { });
  }

  public async eliminarEntidadEstructura(jerarquia: EntidadEstructura): Promise<void> {
    if (this.disableSubmit)
      return;

    let cancel: boolean = false;

    await this.mensajeService.modalConfirmacion(
      `
        ¿Desea eliminar la estructura de jerarquía <strong>'${jerarquia.nombre}'</strong>?
        También se eliminarán las jerarquías hijas.
      `,
      this.TITULO_MODAL_ELIMINAR,
      GeneralConstant.ICONO_CRUZ
    )
    .catch((error) => { cancel = true; });

    if (cancel)
      return;

    this.disableAgregar = true;
    this.disableSubmit = true;

    this.entidadEstructuraService.eliminar(jerarquia.idEntidadEstructura).toPromise()
      .then(async (data) => {
        await this.actualizarInformacion();
        this.mensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
      })
      .catch((error) => { })
      .finally(() => {
        this.disableAgregar = false;
        this.disableSubmit = false;
      });
  }
  // #endregion

  public actualizarBotonAgregar(): void {
    if (this.disableSubmit)
      return;

    if (this.esNuevaPestana)
      this.disableAgregar = false;
    else
      this.disableAgregar = this.elementosSeleccionados.length <= 0;
  }
}
