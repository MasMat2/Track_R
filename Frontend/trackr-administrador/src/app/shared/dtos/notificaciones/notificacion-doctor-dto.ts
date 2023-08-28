import { NotificacionUsuarioBaseDTO } from "./notificacion-usuario-base-dto";

export interface NotificacionDoctorDTO extends NotificacionUsuarioBaseDTO {
  nombrePaciente: string;
  mensaje: string;
  idPaciente: string;
}
