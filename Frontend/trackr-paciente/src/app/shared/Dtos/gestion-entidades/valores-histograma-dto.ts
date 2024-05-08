export class ValoresHistogramaDTO {
    fechaMuestra: string; // o Date, si estás enviando la fecha en un formato que JavaScript puede interpretar
    valor: number;
    fueraDeRango: boolean;
}