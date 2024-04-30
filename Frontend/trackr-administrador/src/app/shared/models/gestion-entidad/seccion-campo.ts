import { Dominio } from "@models/catalogo/dominio";
import { DominioDetalle } from "@models/catalogo/dominio-detalle";
import { Seccion } from './seccion';

export class SeccionCampo {

    public idSeccionCampo: number;
    public clave: string;
    public descripcion: string;
    public idDominio: number;
    public idSeccion: number;
    public requerido: boolean;
    public orden: number;
    public tamanoColumna: number;
    public habilitado: boolean;
    public valor?: number[] | boolean | string | Date | number;
    public grupo: string;
    public fila: number;
    public idIcono: number;
    public mostrarDashboard : boolean;

    // Extras - Utilerias
    public idDominioNavigation: Dominio;
    public listaOpciones: DominioDetalle[];
    public idEntidadEstructuraValor: number;
    public idSeccionNavigation : Seccion;

    constructor() {}
}
