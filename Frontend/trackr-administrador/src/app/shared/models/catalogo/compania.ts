import { CompaniaContacto } from "./compania-contacto";
import { Estado } from "./estado";

export class Compania {
  public idCompania: number;
  public nombre: string;
  public clave: string;
  public correo: string;
  public regimenFiscal: string;
  public portalWeb: string;
  public calle: string;
  public numeroExterior: string;
  public numeroInterior: string;
  public colonia: string;
  public codigoPostal: string;
  public telefono: string;
  public ciudad: string;
  public idEstado: number;
  public idPais: number;
  public estado: Estado;
  public rfc: string;
  public idLada: number;
  public idRegimenFiscal: number;
  public idAgrupadorCuentaContable: number;
  public idTipoCompania: number;
  public idMoneda: number;
  public afectacionContable: boolean;
  public idGiroComercial: number;
  public timbrado: boolean;
  public idMunicipio: number;
  public usoAlmacen: boolean;

  public contrasenaUsuario: string;

  // Formulario Login
  public companiaContacto: CompaniaContacto;

  constructor() {}
}
