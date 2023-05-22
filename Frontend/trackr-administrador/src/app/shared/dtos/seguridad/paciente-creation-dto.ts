import { PadecimientoCreationDTO } from './padecimiento-creation-dto';

export class PacienteCreationDTO {
    numeroExpediente: number;
    fechaAlta: Date;
    genero: string;
    nombre: string;
    apellidoPaterno: string;
    apellidoMaterno: string;
    fechaNacimiento: Date;
    edad: string;
    telefonoMovil: string;
    correo: string;
    peso: number;
    cintura: number;
    estatura: number;
    domicilio: string;
    padecimientos: PadecimientoCreationDTO[];
  }

