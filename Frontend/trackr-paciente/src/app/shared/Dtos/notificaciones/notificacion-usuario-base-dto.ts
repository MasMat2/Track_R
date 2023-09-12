export interface NotificacionUsuarioBaseDTO
{
  idNotificacionUsuario: number;
  idNotificacion: number;
  idUsuario: number;
  fechaAlta: Date;
  idTipoNotificacion: number;
  visto: boolean;
}
