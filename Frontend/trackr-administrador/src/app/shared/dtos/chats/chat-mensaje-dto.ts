export interface ChatMensajeDTO{
    idChatMensaje?: number;
    idChat: number;
    idPersona: number;
    mensaje: string;
    fecha: Date;
    nombrePersona?:string;
    nombre?:string
    fechaRealizacion ?: Date;
    archivo ?: any;
    archivoTipoMime ?: string;
    archivoNombre ?: string;
}