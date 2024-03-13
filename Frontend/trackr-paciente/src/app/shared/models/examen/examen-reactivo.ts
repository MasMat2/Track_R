import { Time } from "@angular/common";

export class ExamenReactivo{

    public idExamenReactivo: number;
    public idExamen: number;
    public idReactivo: number;
    public resultado: boolean;
    public respuestaAlumno: string;
    public fechaAlta: Date;
    public estatus: boolean;

    public asignatura: string;
    public clave: string;
    public pregunta: string;
    public imagenBase64: string;
    public respuesta: string;
    public necesitaRevision: boolean;

    constructor() {}
}