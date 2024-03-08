export class PuntoVenta{
  public idPuntoVenta: number;
  public descripcion: string;
  public nombre: string;
  public clave: string;
  public idAlmacen: number;
  public idUbicacionVenta: number;
  public idTipoPuntoVenta: number;
  public idConcepto: number;

  //Extras
  public nombreUbicacionVenta: string;
  public nombreVendedor: string;
  public nombreConcepto: string;
  public idVendedor: number;
  public claveTipoPuntoVenta: string;
  constructor() {}
}
