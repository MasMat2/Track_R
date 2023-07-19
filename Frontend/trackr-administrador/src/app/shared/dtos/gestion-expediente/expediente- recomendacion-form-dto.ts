export class ExpedienteRecomendacionFormDTO
{
    public idExpedienteRecomendacion: number;
    public idExpediente: number;
    public fecha: Date ;
    public descripcion: string;
    public idDoctor: number;

    constructor() {
        this.fecha = new Date();
    }
}