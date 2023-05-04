import { Estado } from './estado';
import { Compania } from './compania';
import { Usuario } from '../seguridad/usuario';

export class Hospital {
  public idHospital: number;
  public nombre: string;
  public nombreComercial: string;
  public calle: string;
  public numeroExterior: string;
  public numeroInterior: string;
  public colonia: string;
  public codigoPostal: string;
  public telefono: string;
  public correo: string;
  public fechaContableActual: Date;
  public idUsuarioGerente: number;
  public usuario: Usuario;
  public idEstado: number;
  public estado: Estado;
  public ciudad: string;
  public idBanco: number;
  public titularCuenta: string;
  public cuenta: string;
  public clabe: string;
  public portalWeb: string;
  public regimenFiscal: string;
  public rfc: string;
  public IdCompania: number;
  public compania: Compania;
  public idPais: number;
  public idMunicipio: number;
  public idRegimenFiscal: number;
  public idLada: number;
  public horaInicialEntrada: Date;
  public horaFinalEntrada: Date;
  public horaInicialSalida: Date;
  public horaFinalSalida: Date;
  public recomendacionCheckin: string;
  public entreCalles: string;
  public gerente: string;
  public predeterminada: boolean;

  public idListaPrecioDefault: number;
  public idListaPrecioLinea: number;
  public idAlmacenProduccion: number;
  public idAlmacenCaduco: number;

  constructor() {}
}
