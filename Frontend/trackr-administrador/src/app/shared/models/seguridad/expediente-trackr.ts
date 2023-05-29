export class ExpedienteTrackR {
    idExpediente: number;
    numero: number;
    idUsuario: number;
    fechaNacimiento: Date;
    
    peso: number;
    cintura: number;
    estatura: number;

    // TODO: Los siguientes campos no existen en la BD:

    edad: string;
    fechaAlta: Date;
    genero: string;

    constructor() {
        this.idExpediente = 0;
        this.numero = 0;
        this.idUsuario = 0;
        this.fechaNacimiento = new Date();
        this.peso = 0;
        this.cintura = 0;
        this.estatura = 0;
        this.edad = '';
        this.fechaAlta = new Date();
        this.genero = '';
    }
}