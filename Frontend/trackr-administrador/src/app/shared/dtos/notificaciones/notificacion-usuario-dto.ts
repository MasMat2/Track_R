export class NotificacionUsuarioDTO {
    public idNotificacionUsuario: number;
    public idNotificacion: number;
    public idUsuario: number;
    public origen: string;
    public descripcion: string;
    public fechaAlta: Date;
    public visto: boolean;
}
