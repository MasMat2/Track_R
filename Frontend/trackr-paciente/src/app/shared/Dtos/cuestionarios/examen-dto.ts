import { Time } from "@angular/common";

export class ExamenDto{
    public idExamen: number;
    public tipoExamen: string;
    public fechaExamen: string;
    public horaExamen: string;
    public duracion: number;
    public totalPreguntas: number;
}