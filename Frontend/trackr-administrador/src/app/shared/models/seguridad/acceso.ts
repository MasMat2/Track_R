export class Acceso {

  public idAcceso: number;
  public clave: string;
  public nombre: string;
  public ordenMenu?: number;
  public url: string;
  public idIcono?: number;
  public claseIcono: string;
  public idRolAcceso: number;
  public idTipoAcceso: number;
  public idAccesoPadre: number;
  public descripcion: string;

  public imagen: string;
  public urlImagen: string;
  public imagenBase64: string;
  public nombreImagen: string;
  public imagenTipoMime: string;
  public urlVideoAyuda: string;

  // Extras
  public hijos: Acceso[];
  public tipoAcceso: string;
  public claveRolAcceso: string;
  public claveTipoAcceso: string;
  public tieneAcceso: boolean;
  public cantidadDescendientes: number;

  constructor() {}
}

