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
    public fechaExamen: Date;
    public horaExamen: Time;
    public fechaAlta: Date;
    public estatus: boolean;
    public promedio: number;

    public participantes: number[];

    public porcentajeAvance: number;

    constructor() {}
}
