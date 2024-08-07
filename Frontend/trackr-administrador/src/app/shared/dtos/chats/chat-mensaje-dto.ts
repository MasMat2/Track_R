export interface ChatMensajeDTO{
    idChatMensaje?: number;
    idChat: number;
    idPersona: number;
    mensaje: string;
    fecha: string;
    nombrePersona?:string;
    idArchivo:number;
    nombre?:string
    fechaRealizacion ?: string;
    archivo ?: any;
    archivoTipoMime ?: string;
    archivoNombre ?: string;
    fechaYaFormateada?: boolean;
}