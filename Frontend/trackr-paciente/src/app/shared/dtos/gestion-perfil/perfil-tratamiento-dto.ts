export class PerfilTratamientoDto{
    idExpediente?: number;
    farmaco : string;
    fechaRegistro : Date;
    cantidad: number
    unidad : string;
    indicaciones : string;
    padecimiento? : string;
    idPadecimiento : number;
    idUsuarioDoctor: number
    imagenBase64: string;
    recordatorioActivo: boolean; // Recordatorio de tomas
    diaSemana: boolean[]; // Checkboxes dias de la semana
    horas: string[]; // Horario(h)
    bitacora?: Date[]; // Bitacora Consumo de Medicamentos
}
