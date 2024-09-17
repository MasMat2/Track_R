import { TipoUsuario } from './tipo-usuario';

export class Usuario {
  public idUsuario: number;
  public nombre: string;
  public apellidoPaterno: string;
  public apellidoMaterno: string;
  public correo: string;
  public contrasena: string;
  public saldoFavor: number;
  public idEstado: number;
  public idMunicipio: number;
  public idLocalidad: number;
  public idColonia: number;
  public idTituloAcademico: number;
  public tituloAcademico : string;
  public idDepartamento: number;
  public idArea: number;
  public ciudad: string;
  public idTipoUsuario: number;
  public confirmado: boolean;
  public telefonoMovil: string;
  public idCompania: number;
  public idPerfil: number;
  public tipoUsuario: TipoUsuario;
  public idHospital: number;
  public chatActivado: boolean;
  public username: string;
  public sexo: string;
  public idLada: number;
  public calle: string;
  public numeroInterior: string;
  public numeroExterior: string;
  public colonia: string;
  public codigoPostal: string;
  public correoPersonal: string;
  public idPuntoVenta: number;
  public idRegimenFiscal: number;
  public cedula: string;
  public rfc: string;
  public sueldoDiario: number;
  public habilitado: boolean;
  public numeroLicencia: string;
  public diasPago: number;
  public idTipoCliente: number;
  public idListaPrecio: number;
  public idSatFormaPago?: number;
  public idMetodoPago?: number;
  public entreCalles?: string;
  public hospital : string;

  // UsuarioDto
  public clave: string;
  public contrasenaActualizada: string;
  public nombreCompleto: string;
  public nombrePerfil: string;
  public idPais: number;
  public idsPadecimientos : number[];

  public imagenBase64: string;
  public imagenTipoMime: string;
  public nombreTipoUsuario: string;
  public claveTipoUsuario: string;

  // Selector
  public selectorLabel: string;

  //Canal
  public claveCompania: string;
  // Medico
  public direccion: string;
  public idExpediente: number;

  public idsRol: any;
  public idsCompania: any;
  public nombreCompania: string;
  public idsEspecialidad: number[];
  constructor() {}
}
