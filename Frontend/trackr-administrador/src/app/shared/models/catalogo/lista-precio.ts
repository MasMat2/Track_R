export class ListaPrecio {
  public idListaPrecio: number;
  public clave: string;
  public fechaAlta: Date;
  public nombre: string;
  public observaciones: string;
  public fechaInicioVigencia: Date;
  public fechaFinVigencia: Date;
  public idMoneda: number;

  //Precio Detalle
  public precioBase: number;
  //Extras
  public esDefault: boolean;

  constructor() {}
}
