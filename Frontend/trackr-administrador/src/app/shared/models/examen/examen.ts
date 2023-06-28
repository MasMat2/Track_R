import { Time } from "@angular/common";

export class Examen{

    public idExamen: number;
    public idProgramacionExamen: number;
    public idUsuarioParticipante: number;
    public idEstatusExamen: number;
    public resultado: number;
    public preguntasCorrectas: number;
    public fechaAlta: Date;
    public estatus: boolean;

    public tipoExamen: string;
    public fechaExamen: Date;
    public horaExamen: Time;
    public duracion: number;
    public totalPreguntas: number;

    public nombreUsuario: string;
    public clave: string;

    constructor() {}
}