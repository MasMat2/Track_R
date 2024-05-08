import { VariablesPadecimientoDTO } from "./variables-padecimiento-dto";


export class PadecimientoDTO
{
    idPadecimiento: number;
    nombrePadecimiento: string;
    variables : VariablesPadecimientoDTO[];
    idWidget : number;
    descripcionWidget : string;
    tomasTomadas : number;
    tomasTotales : number;
}