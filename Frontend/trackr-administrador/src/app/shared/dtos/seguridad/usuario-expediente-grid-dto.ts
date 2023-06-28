export class UsuarioExpedienteGridDTO {
    idExpedienteTrackr: number;
    idUsuario: number;
    nombreCompleto: string;
    imagenBase64?: string;
    tipoMime?: string;
    patologias: string[];
    glucosa: number;
    presionSistolica: number;
    presionAsistolica: number;
    edad: string;
}
