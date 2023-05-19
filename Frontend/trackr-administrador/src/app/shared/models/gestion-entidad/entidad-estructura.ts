import { SeccionCampo } from "./seccion-campo";

export class EntidadEstructura {
    public idEntidadEstructura: number;
    public nombre: string;
    public clave: string;
    public tabulacion: boolean;
    public idEntidad: number;
    public idSeccion?: number;
    public idEntidadEstructuraPadre?: number;

    // Extras
    public hijos: EntidadEstructura[];
    public campos: SeccionCampo[];

    constructor() {}
}
