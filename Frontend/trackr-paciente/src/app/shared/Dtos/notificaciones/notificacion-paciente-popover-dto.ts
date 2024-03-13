export class NotificacionPacientePopOverDto
{
    public idTipoNotificacion : number
    public id: number;
    public titulo: string;
    public mensaje: string;
    public fecha: Date;
    public visto: boolean;
    public idChat?:number; 
}