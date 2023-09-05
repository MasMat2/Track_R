export class AccesoMenuDto {
  public idAcceso: number;
  public nombre: string;
  public clave: string;
  public url: string;
  public claseIcono: string;
  public urlImagen: string;
  public hijos: AccesoMenuDto[];
  public claveRolAcceso: string;
  public claveTipoAcceso: string;
}
