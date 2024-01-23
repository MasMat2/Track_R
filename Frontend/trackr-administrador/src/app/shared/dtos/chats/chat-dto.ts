export interface ChatDTO{
    idChat?: number;
    fecha: Date;
    doctorAsociado?: string;
    habilitado: boolean;
    titulo?: string;
    ultimoMensaje?:string;
    idCreadorChat: number;
    imagenBase64?: string;
    tipoMime?: string;
}