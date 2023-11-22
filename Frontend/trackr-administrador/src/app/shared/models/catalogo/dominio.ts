import { DominioDetalle } from "@models/catalogo/dominio-detalle";

export class Dominio {
    public idDominio: number;
    public clave: string;
    public nombre: string;
    public descripcion: string;
    public tipoDato: string;
    public tipoCampo: string;
    public longitudMinima: number;
    public longitudMaxima: number;
    public valorMinimo: number;
    public valorMaximo: number;
    public fechaMinima: Date;
    public fechaMaxima: Date;
    public permiteFueraDeRango: boolean;
    public unidadMedida: string;

    public dominioDetalle: DominioDetalle[];

    constructor() {}
}
