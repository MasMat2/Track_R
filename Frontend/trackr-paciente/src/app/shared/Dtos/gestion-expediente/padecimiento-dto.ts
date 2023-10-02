import { VariablesPadecimientoDTO } from "./variables-padecimiento-dto";


export class PadecimientoDTO
{
    iconoClase : string;
    idPadecimiento: number;
    nombrePadecimiento: string;
    variables : VariablesPadecimientoDTO[];
    idWidget : number;
    descripcionWidget : string;
}