import { EntidadEstructuraTablaValor } from "./entidad-estructura-tabla-valor";

export class RegistroTabla {
    public idRegistroTabla: number;
    public idEntidadEstructura: number;
    public valores: EntidadEstructuraTablaValor[];

    constructor() {}
}