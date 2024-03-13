export class Reactivo{

    public idReactivo: number;
    public idAsignatura: number;
    public idNivelExamen: number;
    public clave: string;
    public pregunta: string;
    public imagen: any;
    public imagenTipoMime: string;
    public imagenNombre: string;
    public imagenBase64: string;
    public respuesta: String;
    public respuestaCorrecta: String;
    public necesitaRevision: boolean;
    public fechaAlta: Date;
    public estatus: boolean;

    public file: any;
    public asignatura: string;
    public nivelExamen: string;

    constructor() {}
}