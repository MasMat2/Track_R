export interface ChatMensajeDTO{
    idChatMensaje?: number;
    idChat: number;
    idPersona: number;
    mensaje: string;
    fecha: Date;
    nombrePersona?:string;
    idArchivo:number;
    nombre?:string
    fechaRealizacion ?: Date;
    archivo ?: any;
    archivoTipoMime ?: string;
    archivoNombre ?: string;
    esVideoChat ?: boolean;
}