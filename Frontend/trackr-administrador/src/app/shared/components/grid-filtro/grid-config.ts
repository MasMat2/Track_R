import { ColDef } from "ag-grid-community";

export class GridConfig {
  public headerName: string;
  public children: ColDef[];
  public disableEdit: boolean;
  public disableDelete: boolean;

  public headerButtons?: string[];
  public accesoEliminar?: string;
  public accesoEditar?: string;
  public disableXLS?: boolean;
  public gridOptions?: any;
}