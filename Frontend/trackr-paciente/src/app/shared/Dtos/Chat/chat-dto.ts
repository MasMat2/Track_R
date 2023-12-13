export interface ChatDTO{
    idChat?: number;
    fecha: Date;
    habilitado: boolean;
    titulo?: string;
    ultimoMensaje?:string
    idCreadorChat?: number;
}