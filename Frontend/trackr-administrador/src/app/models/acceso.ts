export class Acceso {
    public idAcceso: number;
    public clave: string;
    public nombre: string;
    public ordenMenu: number;
    public url: string;
    public idIcono: number;
    public idTipoAcceso: number;
    public idAccesoPadre: number;
    public descripcion: string;
  
    // Extras
    public hijos: Acceso[];
    public claseIcono: string;

    constructor() {}
}