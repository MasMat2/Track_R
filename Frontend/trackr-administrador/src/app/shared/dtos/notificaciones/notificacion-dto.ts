export interface Notificacion {
  id: number;
  idTipoNotificacion: number;
  paciente: string;
  mensaje: string;
  fecha: string;
  imagen?: string;
  visto: boolean;
  idChat?: number;
  idPadecimiento?: number;
  idPaciente?: string;
}