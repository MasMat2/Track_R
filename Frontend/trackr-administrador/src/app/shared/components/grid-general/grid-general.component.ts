import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges
} from '@angular/core';
import { AccesoService } from '@http/seguridad/acceso.service';
import { IsRowSelectable } from 'ag-grid-community';
import { get } from 'lodash';
import * as Utileria from '@utils/utileria';
import { GridActionButtonComponent } from './grid-action-button/grid-action-button.component';
import { GridGeneralService } from './grid-general.service';
import { CustomLoadingCellRenderer } from './loading-cell-renderer/loading-cell-renderer.component';

@Component({
  selector: 'app-grid-general',
  templateUrl: './grid-general.component.html'
})
export class GridGeneralComponent implements OnInit, AfterViewInit, OnChanges {
  public vmGrid: any = this;
  public headerConfig = false;

  public lstGridPageSize: any = [];

  @Input() accesoEliminar?: string;
  @Input() accesoEditar?: string;
  @Input() mostrarTodo = false;

  public lstGridPageSizeConfig = {
    highlight: false,
    labelField: 'cant',
    valueField: 'id',
    dropdownDirection: 'down',
    maxItems: 1,
    dropdownParent: 'body'
  };

  public editButton: any = {
    headerName: '',
    field: '',
    cellRenderer: 'actionButton',
    minWidth: 44,
    maxWidth: 44,
    resizable: false,
    sortable: false,
    filter: false,
    suppressMovable: true,
    lockPosition: true,
    action: 'edit',
    pinned: 'right'
  };
  public deleteButton: any = {
    headerName: '',
    field: '',
    cellRenderer: 'actionButton',
    floatingFilterComponent: 'closeFilter',
    floatingFilterComponentParams: {
      scope: this.vmGrid
    },
    minWidth: 44,
    maxWidth: 44,
    resizable: false,
    sortable: false,
    filter: false,
    suppressMovable: true,
    lockPosition: true,
    action: 'delete',
    pinned: 'right'
  };

  @Input() gridOptions: any;
  @Input() headerName: any;
  @Input() headerButtons: string[];
  @Input() children: any;

  @Input() data: any;
  @Input() disableEdit: boolean;
  @Input() disableDelete: boolean;
  @Input() disableXLS = false;
  @Input() descargarFA = false;
  @Input() downloadAction = false;
  @Input() downloadActionTitle: string;
  @Input() downloadParam: any;
  @Input() pagination = true;
  @Input() disableHeader = false;
  @Input() keepHidenColumnsDownload = false;
  @Input() dinamicHeightKey: string;
  @Input() paginationPageSizeSection = true;
  @Input() pageSize = 1;
  @Input() manualButton = false;
  @Input() isRowSelectable: IsRowSelectable;
  @Input() biggerRows = false;

  private _cargandoGrid = false;

  @Input('cargandoGrid')
  set cargandoGrid(cargandoGrid: boolean) {
    this._cargandoGrid = cargandoGrid;
    setTimeout(() => {
      if (this.gridOptions && cargandoGrid) {
        this.gridOptions.api.showLoadingOverlay();
      }
    }, 500);
  }

  get cargandoGrid(): boolean { return this._cargandoGrid; }

  public param: any;
  public defaultParam: any = {
    skipHeader: false,
    columnGroups: false,
    skipFooters: true,
    skipGroups: true,
    skipPinnedTop: true,
    skipPinnedBottom: true,
    allColumns: false,
    onlySelected: false,
    suppressQuotes: true,
  };

  // TODO: 2023-05-03 -> Agregar Spinner
  // public mensajeCargando = `<span class="ag-overlay-loading-center"><img class='mr-2' height='30px' src='assets/img/spinner-grid.gif' />Cargando...</span>`;
  public mensajeCargando = 'Cargando';

  public defaultGridOptions: any = {
    suppressPropertyNamesCheck: true,
    animateRows: true,
    suppressMenuHide: true,
    context: { scope: this },
    domLayout: 'autoHeight',
    defaultColDef: {
      resizable: true,
      filter: true,
      sortable: true
    },
    floatingFilter: false,
    floatingFiltersHeight: 36,
    frameworkComponents: {
      actionButton: GridActionButtonComponent,
      customLoadingCellRenderer: CustomLoadingCellRenderer,
    },
    loadingCellRenderer: 'customLoadingCellRenderer',
    loadingCellRendererParams: { loadingMessage: 'One moment please...' },
    overlayLoadingTemplate: this.mensajeCargando,
    headerHeight: 36,
    localeText: {
      page: 'P\u00e1g.',
      more: 'M\u00e1s',
      to: 'a',
      of: 'de',
      next: 'Siguiente',
      last: 'Ultimo',
      first: 'Primero',
      previous: 'Anterior',
      loadingOoo: 'Cargando...',
      noRowsToShow: 'No se han encontrado registros',
      contains: 'Contiene',
      notContains: 'No contiene',
      startsWith: 'Empieza con',
      endsWith: 'Termina con',
      equals: 'Igual a',
      notEqual: 'Diferente de',
      filterOoo: 'Filtro...',
      andCondition: 'Y',
      orCondition: 'Ó'
    },
    pagination: this.pagination,
    paginationPageSize: 10,
    rowDragManaged: false,
    rowSelection: 'multiple',
    singleClickEdit: true,
    suppressCellSelection: true,
    suppressDragLeaveHidesColumns: true,
    suppressRowClickSelection: true
  };

  @Output() event = new EventEmitter<{ accion: string; data: any }>();
  @Output() eventDownload = new EventEmitter<any>();
  @Output() selectionChanged = new EventEmitter<any>();
  @Output() rowSelected = new EventEmitter<any>();
  @Output() filterChanged = new EventEmitter<{ data: any }>();

  constructor(private gridGeneralService: GridGeneralService,
    private accesoService: AccesoService) {

  }

  async ngOnInit() {
    this.editButton.acceso = this.accesoEditar;
    this.deleteButton.acceso = this.accesoEliminar;
    this.setGridOptions();

    if (!this.mostrarTodo) {
      this.lstGridPageSize = this.gridGeneralService.cargarListaPaginado();
    } else {
      this.gridOptions.paginationPageSize = -1;
      this.gridOptions.pagination = false;
    }
    this.setXlsDownloadParams();

    if (this.biggerRows){
      this.gridOptions.rowHeight = 150;
    }
  }

  onColumnResized() {
    this.gridOptions.api.resetRowHeights();
  }

  ngAfterViewInit() {
    this.bindGridData(this.data);
  }

  setXlsDownloadParams() {
    this.param = this.defaultParam;
    if (this.downloadParam) {
      const props = Object.keys(this.downloadParam);
      for (const p of Object.keys(props)) {
        // FIXME: 2023-05-02 -> Arreglar indexado
        // this.param[props[p]] = this.downloadParam[props[p]];
      }
    } else {
      this.param.fileName = this.headerName;
      this.param.columnKeys = this.children.filter((x: any) => x.headerName !== '').map((x: any) => x.field);
    }
  }

  setGridOptions() {
    this.gridOptions = Object.assign(this.defaultGridOptions, this.gridOptions);
    this.gridOptions.columnDefs = this.children;
  }

  bindGridData(data: any) {
    this.gridOptions.rowData = data;
    if (this.gridOptions.api) {
      this.gridOptions.api.setRowData(this.gridOptions.rowData);
      this.gridOptions.api.setColumnDefs(this.gridOptions.columnDefs);
      this.gridOptions.api.sizeColumnsToFit();
    }
  }

  getDinamicRowHeight(data: any) {
    const array = data[this.dinamicHeightKey].split('height:', 2);
    if (array[1] !== undefined) {
      const array2 = array[1].split('p', 2);
      return array2[0] > 200 ? 200 : array2[0];
    } else {
      return data[this.dinamicHeightKey].length < 27
        ? 27
        : (5 * data[this.dinamicHeightKey].length) / 280 + 27;
    }
  }

  cambiarPaginado(pageSize: string) {
    if (pageSize !== '' && this.mostrarTodo === false) {
      this.pageSize = Number(pageSize);
      this.gridOptions.api.paginationSetPageSize(this.lstGridPageSize[Number(pageSize)].cant);
    }
  }

  actionButton(action: any) {
    this.event.emit({
      accion: action.action,
      data: action.value
    });
  }

  headerButton(action: string) {
    this.event.emit({
      accion: action,
      data: null
    });
  }

  toggleFilter() {
    if (!this.gridOptions.rowData || this.gridOptions.rowData.length === 0) {
      return;
    }
    if (this.headerConfig) {
      this.toggleConfig();
    }
    this.gridOptions.floatingFilter = !this.gridOptions.floatingFilter;
    this.gridOptions.api.refreshHeader();
    if (!this.gridOptions.floatingFilter && this.gridOptions.api.isAdvancedFilterPresent()) {
      this.gridOptions.api.setFilterModel(null);
    }
  }

  toggleConfig() {
    if (!this.gridOptions.rowData || this.gridOptions.rowData.length === 0) {
      return;
    }
    if (this.gridOptions.floatingFilter) {
      this.toggleFilter();
    }
    this.headerConfig = !this.headerConfig;
  }

  filterModified(event: any) {
    const rowsData = this.printPageDisplayedRows();
    this.filterChanged.emit({
      data: rowsData
    });
  }

  printPageDisplayedRows() {
    const rowCount = this.gridOptions.api.getDisplayedRowCount();
    const lastGridIndex = rowCount - 1;
    const currentPage = this.gridOptions.api.paginationGetCurrentPage();
    const pageSize = this.gridOptions.api.paginationGetPageSize();
    const startPageIndex = currentPage * pageSize;
    let endPageIndex = (currentPage + 1) * pageSize - 1;
    let array: any[] = [];

    if (endPageIndex > lastGridIndex) {
      endPageIndex = lastGridIndex;
    }
    for (let i = startPageIndex; i <= endPageIndex; i++) {
      array.push(this.gridOptions.api.getDisplayedRowAtIndex(i));
    }
    return array;
  }

  descargarFormatoArbol() {
    const rowsData = this.gridOptions.api.getModel()['rowsToDisplay'];
    const listaIds = [];
    rowsData.forEach((row: any) => {
      listaIds.push(row.data.idConfiguracionEstructura);
    });
  }

  downloadXls() {
    if (!this.gridOptions.rowData || this.gridOptions.rowData.length === 0) {
      return;
    }
    let headers: any;
    let formats: any;
    const rowsData = [...this.gridOptions.api.getModel()['rowsToDisplay']];
    // const rowsData = cloneDeep(this.gridOptions.api.getModel()['rowsToDisplay']);

    // agrega los rows de totales
    if (this.gridOptions.api.getPinnedBottomRow(0) !== undefined) {
      rowsData.push(this.gridOptions.api.getPinnedBottomRow(0));
    }

    // Aplica el formato de moneda a las columnas que tengan marcada esa opcion
    rowsData.forEach((r: any) => {
      for (const p of Object.keys(r.data)) {
        const columns: any = this.gridOptions.columnDefs;
        const columnMoneda = columns.find((c: any) => c.field === p && c.formatoMoneda);
        if (columnMoneda !== undefined) {
          r.data[p] = Utileria.formatoMonto(r.data[p]);
        }
      }
    });

    // obtiene los headers que se pondran en el excel
    if (this.keepHidenColumnsDownload) {
      headers = this.gridGeneralService.getHeadersOfChildrenWithHidden(this.children);
      formats = this.gridGeneralService.getFormatsWithHidden(this.children);
    } else {
      headers = this.gridGeneralService.getHeadersOfChildren(this.children);
      formats = this.gridGeneralService.getFormats(this.children);
    }

    // obtiene los campos de las columnas
    const headerSizes = this.gridGeneralService.getHeaderSizes(headers);

    // obtiene el contenido/datos del excel
    const content = this.gridGeneralService.getContentFileFromData(formats, rowsData);

    // const content = this.gridOptions.rowData;
    this.gridGeneralService.download(this.param, content, 'xlsx', headers, headerSizes);
  }

  downloadSelection() {
    if (!this.downloadAction) {
      this.downloadXls();
    } else {
      this.eventDownload.emit();
    }
  }

  onGridSizeChanged(event: any) {
    this.gridOptions.api.sizeColumnsToFit();
  }

  onSelectionChanged(event: any) {
    this.selectionChanged.emit(event.api.getSelectedNodes());
  }

  onRowSelected(event: any) {
    this.rowSelected.emit(event);
  }

  containsButtons(colDef: any) {
    const i = colDef.length;
    let edit = false;
    let del = false;
    const editCondition = get(colDef, [(i - 2).toString(), 'action'], undefined);
    const deleteCondition = get(colDef, [(i - 1).toString(), 'action'], undefined);

    if (i > 0) {
      edit =
        (this.disableDelete ? deleteCondition !== undefined : editCondition !== undefined) &&
        (this.disableDelete ? deleteCondition === 'edit' : editCondition === 'edit');
      del = deleteCondition !== undefined && deleteCondition === 'delete';
    }
    return edit || del;
  }

  actualizarEstadoBotones() {
    if (this.containsButtons(this.children)) {
      if (this.disableEdit !== undefined && this.disableEdit) {
        this.children.pop(this.editButton);
      }
      if (this.disableDelete !== undefined && this.disableDelete) {
        this.children.pop(this.deleteButton);
      }
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['children'] !== undefined && changes['children'].firstChange) {
      if (!this.containsButtons(this.children)) {
        if (!(this.disableEdit !== undefined && this.disableEdit)) {
          this.children.push(this.editButton);
        }
        if (!(this.disableDelete !== undefined && this.disableDelete)) {
          this.children.push(this.deleteButton);
        }
      }
    }
    let array: any = [];
    for (let i = 0; i < this.children.length; i++) {
      if (this.children[i].acceso !== undefined) {
        array.push(this.children[i].acceso);
      }
    }

    if (array && array.length > 0) {
      this.accesoService.tieneAccesoLista(array).subscribe((data) => {
        for (let i = 0; i < data.length; i++) {
          for (let j = 0; j < this.children.length; j++) {
            if (data[i].clave === this.children[j].acceso) {
              // FIXME: 2023-05-02 -> Propiedad 'tieneAcceso'
              if (data[i].tieneAcceso === true) {
              } else {
                this.children.splice(j, 1);
              }
            }
          }
        }

        if (changes['data'] !== undefined && !changes['data'].firstChange) {
          this.bindGridData(changes['data'].currentValue);
        }
        if (changes['pagination'] !== undefined && changes['pagination'].currentValue !== undefined) {
          this.gridOptions.pagination = changes['pagination'].currentValue;
        }
      });
    } else {
      if (changes['data'] !== undefined && !changes['data'].firstChange) {
        this.bindGridData(changes['data'].currentValue);
      }
      if (changes['pagination'] !== undefined && changes['pagination'].currentValue !== undefined) {
        this.gridOptions.pagination = changes['pagination'].currentValue;
      }
    }

    this.cargandoGrid = false;
  }
}
