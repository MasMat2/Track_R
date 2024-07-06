export interface ChatDTO{
    idChat?: number;
    fecha: Date;
    habilitado: boolean;
    titulo?: string;
    ultimoMensaje?:string
    fechaUltimoMensaje?: Date;
    idCreadorChat: number;
    imagenBase64?: string;
    tipoMime?: string;
    urlImagen? : any;
}