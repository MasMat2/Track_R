import { ArchivoDto } from "../archivos/archivo-dto";

export interface InformacionPerfilDto {

    nombre: string;
    apellidoPaterno: string;
    apellidoMaterno: string;
    correo: string;
    imagenBase64: ArchivoDto;
}