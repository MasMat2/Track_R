import { Dominio } from "@models/catalogo/dominio";
import { DominioDetalle } from "@models/catalogo/dominio-detalle";

export class SeccionCampo {

    public idSeccionCampo: number;
    public clave: string;
    public descripcion: string;
    public idDominio: number;
    public idSeccion: number;
    public requerido: boolean;
    public orden: number;
    public tamanoColumna: number;
    public deshabilitado: boolean;
    public valor: number[] | boolean | string | Date | number;
    public grupo: string;
    public fila: number;

    // Extras - Utilerias
    public idDominioNavigation: Dominio;
    public listaOpciones: DominioDetalle[];
    public idEntidadEstructuraValor: number;

    constructor() {}
}
