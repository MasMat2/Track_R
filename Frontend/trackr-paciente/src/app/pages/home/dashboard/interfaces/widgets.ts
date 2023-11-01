import { Type } from "@angular/core";
import { WidgetPasosComponent } from "../components/widget-pasos/widget-pasos.component";
import { WidgetPesoComponent } from "../components/widget-peso/widget-peso.component";
import { WidgetFrecuenciaComponent } from "../components/widget-frecuencia/widget-frecuencia.component";
import { WidgetSuenoComponent } from "../components/widget-sueno/widget-sueno.component";
import { WidgetSeguimientoComponent } from "../components/widget-seguimiento/widget-seguimiento.component";

// Todos los tipos de widgets posibles
//Claves provisionales aquí y en la BD
export const ALL_WIDGET_TYPES = [
  'w-pas',
  'w-pes',
  'w-sue',
  'w-fre',
  'P001',
  'P002',
  'P003',
  'P004',
  'P005',
  'P006'

] as const;

// Union Type de los tipos de widget
export type WidgetType = typeof ALL_WIDGET_TYPES[number];

// Función Guard para saber si un string es un miembro de WidgetType
export function isWidgetType(value: string): value is WidgetType {
  return ALL_WIDGET_TYPES.includes(value as WidgetType);
}

// Valores necesarios para mostrar un widget de forma dinámica
export type WidgetDefinition = {
  class: Type<unknown>,
  columns: number
};

// Diccionario de Widgets que incluye todos los miembros de WidgetType como llaves
export type WidgetDictionary = Record<WidgetType, WidgetDefinition>;

export const WIDGETS: WidgetDictionary = {
  'w-pas': { class: WidgetPasosComponent, columns: 8 },
  'w-pes': { class: WidgetPesoComponent, columns: 4 },
  'w-fre' : {class: WidgetFrecuenciaComponent, columns: 4},
  'w-sue': {class: WidgetSuenoComponent, columns: 8},
  'P001' : {class: WidgetSeguimientoComponent, columns: 12},
  'P002' : {class: WidgetSeguimientoComponent, columns: 12},
  'P003' : {class: WidgetSeguimientoComponent, columns: 12},
  'P004' : {class: WidgetSeguimientoComponent, columns: 12},
  'P005' : {class: WidgetSeguimientoComponent, columns: 12},
  'P006' : {class: WidgetSeguimientoComponent, columns: 12},

};
