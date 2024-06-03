import { Time } from "@angular/common";
import { Respuesta } from "./respuesta";
export class ExamenReactivo{

    public idExamenReactivo: number;
    public idExamen: number;
    public idReactivo: number;
    public resultado: boolean;
    public respuestaAlumno: string;
    public respuestaValor : number;
    public fechaAlta: Date;
    public estatus: boolean;

    public asignatura: string;
    public clave: string;
    public pregunta: string;
    public imagenBase64: string;
    public respuestas: Respuesta[];
    public necesitaRevision: boolean;

    constructor() {}
}