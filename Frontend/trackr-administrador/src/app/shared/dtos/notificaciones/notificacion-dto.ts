export interface Notificacion {
  id: number;
  idTipoNotificacion: number;
  paciente: string;
  mensaje: string;
  fecha: Date;
  imagen?: string;
  visto: boolean;
  idChat?: number;
}