import { NotificacionUsuarioBaseDTO } from "./notificacion-usuario-base-dto";

export interface NotificacionPacienteDTO extends NotificacionUsuarioBaseDTO 
{
  titulo: string;
  mensaje: string;
}
