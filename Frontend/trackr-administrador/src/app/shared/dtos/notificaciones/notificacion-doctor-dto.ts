import { NotificacionUsuarioBaseDTO } from "./notificacion-usuario-base-dto";
import { SafeUrl } from '@angular/platform-browser';

export interface NotificacionDoctorDTO extends NotificacionUsuarioBaseDTO {
  nombrePaciente: string;
  mensaje: string;
  idPaciente: string;
  imagen?:string;
}
