export interface ChatDTO{
    idChat?: number;
    fecha: string;
    fechaYaFormateada?: boolean;
    habilitado: boolean;
    titulo?: string;
    ultimoMensaje?:string
    fechaUltimoMensaje?: string;
    idCreadorChat: number;
    imagenBase64?: string;
    tipoMime?: string;
    urlImagen? : any;
}