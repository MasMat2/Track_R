export interface EntidadTablaRegistroDto {
    numero: number;
    idEntidadEstructura: number; // PestanaSección
    idTabla: number;
    valores: TablaValorDto[];
}

export interface TablaValorDto {
    idEntidadEstructuraTablaValor: number;
    claveCampo: string;
    valor: string;
    fueraDeRango: boolean;
}
