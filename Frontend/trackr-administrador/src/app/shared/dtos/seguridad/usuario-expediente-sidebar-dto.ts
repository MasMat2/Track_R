import { ExpedientePadecimientoSidebarDTO } from "./expediente-padecimiento-sidebar-dto";

export class UsuarioExpedienteSidebarDTO{
    idUsuario:number;
    nombreCompleto:string;
    imagenBase64?:string;
    tipoMime?: string;
    idGenero:number;
    edad:string;
    colonia?:string;
    ciudad?:string;
    estado?:string;
    padecimientos?: ExpedientePadecimientoSidebarDTO[];
}