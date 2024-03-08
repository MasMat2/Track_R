import { ColDef } from 'ag-grid-community';

export enum GRID_ACTION {
  Editar = 'edit',
  Eliminar = 'delete',
  Ver = 'see', //esta propiedad es la que debe coincidir con el scss de grid-action-button
  Revisar = 'play',
  DescargarPdf = 'descargarPdf',
};

export const CONFIG_COLUMN_ACTION: ColDef = {
  headerName: '',
  field: '',
  cellClass: 'center-ag',
  minWidth: 44,
  maxWidth: 44,
  suppressMovable: true,
  resizable: false,
  filter: false,
  lockPinned: true,
  pinned: 'right',
  lockPosition: true
};

export * as GridConstants from './grid';
