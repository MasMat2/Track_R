import { ExamenReactivoExcelDto } from "./examen-reactivo-excel-dto";

export class RespuestasExcelDto {
    preguntas: string[];
    respuestas: (ExamenReactivoExcelDto[])[];
}