import { Time } from "@angular/common";

export class ProgramacionExamen{

    public idProgramacionExamen: number;
    public idTipoExamen: number;
    public idProyectoElementoTecnica: number;
    public tipoExamen: string;
    public idUsuarioResponsable: number;
    public usuarioResponsable: string;
    public clave: string;
    public duracion: number;
    public cantidadParticipantes: number;
    public fechaExamen: string;
    public horaExamen: string;
    public fechaAlta: Date;
    public estatus: boolean;
    public promedio: number;
    public idsPadecimiento : number[];

    public participantes: number[];

    public porcentajeAvance: number;

    constructor() {}
}
