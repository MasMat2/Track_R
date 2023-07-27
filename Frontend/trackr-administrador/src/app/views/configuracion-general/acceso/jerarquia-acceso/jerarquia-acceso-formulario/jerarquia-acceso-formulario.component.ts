import { Location } from "@angular/common";
import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ITreeOptions, TREE_ACTIONS } from "@circlon/angular-tree-component";
import { TipoCompaniaService } from '@http/catalogo/tipo-compania.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { JerarquiaAccesoEstructuraService } from "@http/seguridad/jerarquia-acceso-estructura.service";
import { JerarquiaAccesoService } from '@http/seguridad/jerarquiaAcceso.service';
import { TipoCompania } from '@models/catalogo/tipo-compania';
import { JerarquiaEstructuraArbol } from "@models/contabilidad/jerarquia-estructura-arbol";
import { Acceso } from '@models/seguridad/acceso';
import { JerarquiaAcceso } from '@models/seguridad/jerarquia-acceso';
import { JerarquiaAccesoEstructura } from "@models/seguridad/jerarquia-acceso-estructura";
import { CrudFormularioBase } from '@sharedComponents/crud/components/crud-formulario-base';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import { AngularTreeGridComponent } from "angular-tree-grid";

@Component({
  selector: 'app-jerarquia-acceso-formulario',
  templateUrl: './jerarquia-acceso-formulario.component.html',
  styleUrls: ['./jerarquia-acceso-formulario.component.scss']
})
export class JerarquiaAccesoFormularioComponent extends CrudFormularioBase<JerarquiaAcceso> implements OnInit {

  @ViewChild('angularGrid', {static: false}) angularGrid: AngularTreeGridComponent;

  // Constantes
  private TITULO_MODAL_ELIMINAR = `Eliminar Jerarquía Estructura`;
  private MENSAJE_EXITO_ELIMINAR: string = "La estructura de jerarquía ha sido eliminada";
  private MENSAJE_EXITO_AGREGAR_ESTRUCTURA: string = "Las estructuras de jerarquía han sido agregadas";

  // Variables de entrada
  public idJerarquiaAcceso: number;

  // Grid-Tree
  public elementosSeleccionados: Acceso[] = [];
  public accesos: Acceso[] = [];

  public gridAccesoColumns = [
    { name: 'nombre', header: 'Descripción' },
    { name: 'tipoAcceso', header: 'Tipo Acceso', css_class: 'tree-cell' }
	];

  public configTree: any ={
    id_field: 'idAcceso',
    parent_id_field: 'idAccesoPadre',
    parent_display_field: 'nombre',
    data_loading_text: 'Cargando...',
    multi_select: true,
    cascade_selection: true,
    css: {
      expand_class: 'fa fa-caret-right',
      collapse_class: 'fa fa-caret-down',
      row_selection_class: 'row-select',
      table_class: 'grid-table-tree'
    },
    columns: this.gridAccesoColumns
  }

  // Configuración de Arbol Jerarquía
  public jerarquiaArbol: JerarquiaEstructuraArbol[] = [];

  public options: ITreeOptions = {
    displayField: 'cuenta',
    isExpandedField: 'expanded',
    idField: 'idJerarquiaEstructura',
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
  public jerarquiaSelectorList: JerarquiaEstructuraArbol[] = [];
  public tipoCompaniaList: TipoCompania[] = [];

  public placeholderSelect: string = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeholderNoOptions: string = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

  public configJerarquiaPadre = Object.assign(
    { labelField: 'cuenta', valueField: 'idJerarquiaEstructura', searchField: ['cuenta'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  public configTipoCompania = Object.assign(
    {
      labelField: 'nombre',
      valueField: 'idTipoCompania',
      searchField: ['nombre'],
      dropdownParent: 'body',
      plugins: ['dropdown_direction', 'remove_button'],
      maxItems: null,
      dropdownDirection: 'down',
    }
  );

  // Configuración General
  public jerarquiaEstructura: JerarquiaAccesoEstructura = {} as JerarquiaAccesoEstructura;
  public disableAgregar: boolean = true;
  public loading: boolean = true;
  public deshabilitacionEventos: boolean = false;

  constructor(
    private jerarquiaAccesoService: JerarquiaAccesoService,
    private jerarquiaAccesoEstructuraService: JerarquiaAccesoEstructuraService,
    private tipoCompaniaService: TipoCompaniaService,
    private accesoService: AccesoService,
    mensajeService: MensajeService,
    router: Router,
    location: Location,
  ) {
    super(jerarquiaAccesoService, mensajeService, router, location);
  }

  async ngOnInit(): Promise<void> {
    await super.onInit();
    this.consultarSelectores();
    this.actualizarInformacion();
  }

  // ==== Consultas ==== //
  private async consultarArbol(): Promise<JerarquiaEstructuraArbol[]> {
    const jerarquiaEstructuras = await this.jerarquiaAccesoEstructuraService
      .consultarArbol(this.idEntidad)
      .toPromise();

    return jerarquiaEstructuras ?? [];
  }

  private async consultarSelector(): Promise<JerarquiaEstructuraArbol[]> {
    const jerarquiaEstructuras = await this.jerarquiaAccesoEstructuraService
      .consultarPorJerarquiaParaSelector(this.idEntidad)
      .toPromise();

    return jerarquiaEstructuras ?? [];
  }

  private async consultarGrid(): Promise<Acceso[]> {
    const accesos = await this.accesoService
      .consultarParaReporteArbol(0)
      .toPromise();

    return accesos ?? [];
  }

  private async consultarTiposCompania(): Promise<TipoCompania[]> {
    const tiposCompania = await this.tipoCompaniaService
      .consultarParaSelector()
      .toPromise();

    return tiposCompania ?? [];
  }

  private async consultarSelectores(): Promise<void> {
    this.tipoCompaniaList = await this.consultarTiposCompania().catch(() => []);
  }

  private async actualizarInformacion(): Promise<void> {

    if (!this.esEditar()) {
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
        this.jerarquiaSelectorList = data[1];
        this.accesos = data[2];
      })
      .catch((error) => { })
      .finally(() => {
        this.loading = false;
      });
  }

  // ==== Árbol de Jerarquía ==== //
  public seleccionarCuentaPadre(node: JerarquiaEstructuraArbol): void {
    if (node.tipoAcceso != 'Evento') {
      this.idJerarquiaPadre = node.idJerarquiaEstructura;
    }
  }

  private obtenerJerarquiasSeleccionadas(): JerarquiaAccesoEstructura[] {
    let jerarquiasSeleccionadas: JerarquiaAccesoEstructura[] = [];

    this.elementosSeleccionados.forEach((acceso: Acceso) => {
      let jerarquiaEstructura = {} as JerarquiaAccesoEstructura;

      jerarquiaEstructura.idJerarquiaAcceso = this.entidad.idJerarquiaAcceso;
      jerarquiaEstructura.idJerarquiaAccesoEstructuraPadre = this.idJerarquiaPadre;
      jerarquiaEstructura.idAcceso = acceso.idAcceso;
      jerarquiaEstructura.idAccesoPadre = acceso.idAccesoPadre;

      jerarquiasSeleccionadas.push(jerarquiaEstructura);
    });

    return jerarquiasSeleccionadas;
  }

  public onSelectionChange(event: any, seleccionar: boolean): void {
    let acceso = event.data as Acceso;
    let displayData = this.angularGrid.store.getDisplayData() as Acceso[];

    // Se obtiene la posicion inicial y final de los elementos a afectar.
    // En base a la posicion inicial del elemento seleccionado y la cantidad de descendientes.
    const indexInicial: number = displayData.findIndex(a => a.idAcceso === acceso.idAcceso);
    const indexFinal: number = indexInicial + acceso.cantidadDescendientes;

    // Se recorren los elementos
    // Si se seleccionó un elemento padre, se afectan sus hijos en cascada
    for (let index = indexInicial; index <= indexFinal; index++) {
      const accesoSeleccion = displayData[index];
      // Se aplica el estado al elemento
      this.angularGrid.store.display_data[index].row_selected = seleccionar;
      // Se agrega/elimina a la lista de elementos seleccionados
      if (seleccionar)
        this.elementosSeleccionados.push(displayData[index]);
      else
        this.elementosSeleccionados = this.elementosSeleccionados.filter(e => e.idAcceso != accesoSeleccion.idAcceso);
        if (accesoSeleccion.tipoAcceso === "Evento")
          this.angularGrid.store.display_data[index].selection_disabled = true;
    }
    this.actualizarBotonAgregar();
  }

  public onExpand(event: any): void {
    if (!this.deshabilitacionEventos) {
      this.deshabilitacionEventos = true;
      this.deshabilitarEventos();
    }
  }

  public onSelectAll(seleccionar: boolean): any {
    if (seleccionar)
      this.elementosSeleccionados = this.accesos;
    else
      this.elementosSeleccionados = [];

    this.actualizarBotonAgregar();
  }

  public deshabilitarEventos(): void {
    let displayData = this.angularGrid.store.getDisplayData() as Acceso[];
    const indexInicial: number = 0;
    const indexFinal: number = displayData.length - 1;

    // Se recorren los elementos
    for (let index = indexInicial; index <= indexFinal; index++) {
      const accesoSeleccion = displayData[index];

      // Se aplica el estado deshabilitado a los eventos
      if (accesoSeleccion.tipoAcceso === "Evento")
        this.angularGrid.store.display_data[index].selection_disabled = true;
    }
  }

  public actualizarBotonAgregar(): void {
    if (this.disableSubmit)
      return;

    this.disableAgregar = this.elementosSeleccionados.length <= 0;
  }

  public async onAgregarClick(): Promise<void> {
    this.disableAgregar = true;
    this.disableSubmit = true;

    await this.agregarNodosSeleccionados();

    this.disableSubmit = false;
    this.elementosSeleccionados = [];
    this.actualizarBotonAgregar();
  }

  private async agregarNodosSeleccionados(): Promise<void> {
    let jerarquiasSeleccionadas = this.obtenerJerarquiasSeleccionadas();
    await this.agregarJerarquiaEstructura(jerarquiasSeleccionadas);
  }

  private async agregarJerarquiaEstructura(jerarquias: JerarquiaAccesoEstructura[]) {
    return this.jerarquiaAccesoEstructuraService.agregar(jerarquias).toPromise()
      .then(async (data) => {
        await this.actualizarInformacion();
        this.mensajeService.modalExito(this.MENSAJE_EXITO_AGREGAR_ESTRUCTURA);
      })
      .catch((error) => { });
  }

  public async eliminarJerarquiaEstructura(jerarquia: JerarquiaEstructuraArbol): Promise<void> {
    if (this.disableSubmit)
      return;

    let cancel: boolean = false;

    await this.mensajeService.modalConfirmacion(
      `
        ¿Desea eliminar la estructura de jerarquía <strong>'${jerarquia.cuenta}'</strong>?
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

    this.jerarquiaAccesoEstructuraService.eliminar(jerarquia.idJerarquiaEstructura).toPromise()
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

  public esEditar(): boolean {
    return this.accion == GeneralConstant.MODAL_ACCION_EDITAR;
  }
}
