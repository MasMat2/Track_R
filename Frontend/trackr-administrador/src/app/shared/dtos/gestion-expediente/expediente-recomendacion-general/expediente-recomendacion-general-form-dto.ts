export class ExpedienteRecomendacionGeneralDTO{
    public descripcion : string;
    public idDoctor : number;
    public fecha : Date;
    public idPadecimiento ?: number;
    public paciente ?: number[];
}