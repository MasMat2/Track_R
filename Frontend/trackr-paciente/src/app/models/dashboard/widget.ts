import { WidgetType } from "src/app/pages/home/dashboard/interfaces/widgets";

export interface Widget {
  idWidget: number;
  clave: WidgetType;
  nombre: string;
}
