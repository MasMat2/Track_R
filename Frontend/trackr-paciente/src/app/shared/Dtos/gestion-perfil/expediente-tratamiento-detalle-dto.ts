export class ExpedienteTratamientoDetalleDto{
    idExpedienteTratamiento?: number;
    idExpediente?: number;
    farmaco : string;
    // fechaRegistro : Date;
    fechaInicio?: Date;
    fechaFin?: Date;
    cantidad: number
    unidad : string;
    indicaciones : string;
    padecimiento? : string;
    idPadecimiento : number;
    idUsuarioDoctor: number
    nombreDoctor?: string;
    apellidosDoctor?: string;
    tituloDoctor?: string;
    archivo: any;
    archivoTipoMime:string='';
    archivoNombre:string='';
    imagenBase64: string;
    tipoMime? : string;
    recordatorioActivo: boolean; // Recordatorio de tomas
    diaSemana: boolean[]; // Checkboxes dias de la semana
    horas: string[]; // Horario(h)
    bitacora?: Date[]; // Bitacora Consumo de Medicamentos
}
