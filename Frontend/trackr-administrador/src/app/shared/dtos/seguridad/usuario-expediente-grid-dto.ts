export class UsuarioExpedienteGridDTO {
    idExpedienteTrackr: number;
    idUsuario: number;
    doctorAsociado: string;
    nombreCompleto: string;
    imagenBase64?: string;
    tipoMime?: string;
    patologias: string[];
    dosisNoTomadas: number;
    variablesFueraRango: number;
    presionSistolica: number;
    presionAsistolica: number;
    edad: string;
}
