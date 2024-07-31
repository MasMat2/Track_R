export class NotificacionPacientePopOverDto
{
    public idTipoNotificacion : number
    public id: number;
    public titulo: string;
    public mensaje: string;
    public complementoMensaje?: string;
    public complementoEsFecha?: boolean;
    public fecha: string;
    public visto: boolean;
    public idChat?:number; 
}