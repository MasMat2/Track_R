export class Almacen {
  public idAlmacen: number;
  public numero: number;
  public nombre: string;
  public descripcion: string;
  public fechaAlta: Date;
  public calle: string;
  public numeroExterior: string;
  public numeroInterior: string;
  public colonia: string;
  public localidad: string;
  public codigoPostal: string;
  public telefonoUno: string;
  public telefonoDos: string;
  public urlMapa: string;
  public direccion: string;
  public responsableNombre: string;

  public idEstatusAlmacen: number;
  public idUsuarioResponsable: number;
  public idEstado: number;
  public idCompania: number;

  public idCuentaContable: number;
  public nombreCuentaContable: string;

  constructor() {}
}
