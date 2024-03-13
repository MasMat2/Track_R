export class ExpedienteRecomendacionGeneralFormDTO{
    public idExpedienteRecomendacionGeneral ?: number;
    public tipo: number;
    public descripcion : string;
    public idDoctor ?: number;
    public fecha : Date;
    public idPadecimiento ?: number;
    public paciente ?: number[];
}