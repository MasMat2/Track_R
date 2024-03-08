export class ExpedienteTratamiento{
    idExpedienteTratamiento : number;
    idExpediente : number;
    farmaco : string;
    fechaRegistro : Date;
    cantidad : number;
    unidad : string;
    indicaciones : string;
    idPadecimiento : number;
    idUsuarioDoctor : number;
    imagen : Blob;
    imagenTipoMime : string
}