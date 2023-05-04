import { ColDef } from "ag-grid-community";

export interface ICatalogoConfig {
    titulo?: string;
    children: ColDef[];
    gridOptions?: any;
}