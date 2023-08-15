import {ExpedientePadecimientoDto} from '../seguridad/expediente-padecimiento-dto'

export interface InformacionGeneralDto {

    nombre: string;
    apellidoPaterno: string;
    apellidoMaterno: string;
    fechaNacimiento: Date;
    idGenero: number;
    peso: number;
    cintura: number;
    estatura: number;
    correo: string;
    telefonoMovil: string;
    idPais: number;
    idEstado: number;
    idMunicipio: number;
    idLocalidad: number;
    idColonia: number;
    codigoPostal: string;
    calle: string;
    entreCalles: string;
    numeroInterior: string;
    numeroExterior: string;

    padecimientos: ExpedientePadecimientoDto[];
}
