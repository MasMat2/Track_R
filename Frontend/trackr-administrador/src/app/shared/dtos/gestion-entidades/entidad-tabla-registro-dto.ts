export interface EntidadTablaRegistroDto {
    numero: number;
    idEntidadEstructura: number; // PestanaSección
    idTabla: number;
    valores: TablaValorDto[];
}

export interface TablaValorDto {
    idEntidadEstructuraTablaValor: number;
    idSeccionVariable: number;
    valor: string;
    fueraDeRango: boolean;
    fechaMuestra: Date;
}
