export class ExpedientePadecimientoDto {
    idExpedientePadecimiento: number;
    idPadecimiento: number;
    idUsuarioDoctor: number;
    nombreDoctor: string;
    apellidosDoctor: string;
    tituloDoctor: string;
    nombrePadecimiento: string;
    fechaDiagnostico: Date;
    esAntecedente: boolean;
}
