import { List } from "lodash";
import { ExpedienteMuestrasRegistroDTO } from "./expediente-muestras-registros-dto";

export class ExpedienteMuestrasGridDTO{
    idEntidadEstructura : number;
    idEntidadEstructuraTablaValor : number;
    fechaMuestra : Date;
    fueraDeRango : boolean;
    registro : ExpedienteMuestrasRegistroDTO[];

    constructor() {}
}