import { Time } from "@angular/common";

export class ExamenDto{
    public idExamen: number;
    public tipoExamen: string;
    public fechaExamen: Date;
    public horaExamen: Time;
    public duracion: number;
    public totalPreguntas: number;
}