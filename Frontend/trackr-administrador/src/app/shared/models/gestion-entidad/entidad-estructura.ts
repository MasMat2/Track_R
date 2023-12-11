import { RegistroTabla } from "./registro-tabla";
import { SeccionCampo } from "./seccion-campo";

export class EntidadEstructura {
    public idEntidadEstructura: number;
    public nombre: string;
    public clave: string;
    public tabulacion: boolean;
    public idEntidad: number;
    public idSeccion?: number;
    public idEntidadEstructuraPadre?: number;
    public idIcono? : number;
    public idTipoWidget? : number;
    public esAntecedente? : boolean;

    // Extras
    // Estructuras - Pestañas
    public esTabla: boolean;
    public hijos: EntidadEstructura[];

    // Estructuras - Secciones
    public campos: SeccionCampo[];
    public registrosTabla: RegistroTabla[];

    constructor() {}
}
