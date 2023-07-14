import { ExpedientePadecimientoSidebarDTO } from "./expediente-padecimiento-sidebar-dto";

export class UsuarioExpedienteSidebarDTO{
    idUsuario:number;
    nombreCompleto:string;
    imagenBase64?:string;
    tipoMime?: string;
    genero:string;
    edad:string;
    colonia?:string;
    ciudad?:string;
    idestado:number;
    estado:string;
    direccion:string;
    padecimientos: ExpedientePadecimientoSidebarDTO[];
}