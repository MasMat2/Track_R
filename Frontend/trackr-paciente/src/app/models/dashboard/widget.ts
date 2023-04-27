import { WidgetType } from "src/app/pages/paciente/dashboard/interfaces/widgets";

export interface Widget {
  idWidget: number;
  clave: WidgetType;
  nombre: string;
}
