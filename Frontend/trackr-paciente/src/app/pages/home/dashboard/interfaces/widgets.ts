import { Type } from "@angular/core";
import { WidgetPasosComponent } from "../components/widgets/widget-pasos/widget-pasos.component";
import { WidgetPesoComponent } from "../components/widgets/widget-peso/widget-peso.component";
import { WidgetFrecuenciaComponent } from "../components/widgets/widget-frecuencia/widget-frecuencia.component";
import { WidgetSuenoComponent } from "../components/widgets/widget-sueno/widget-sueno.component";
import { WidgetSeguimientoComponent } from "../components/widgets/widget-seguimiento/widget-seguimiento.component";
import { WidgetOmronComponent } from "../components/widgets/widget-omron/widget-omron.component";

// Todos los tipos de widgets posibles
//Claves provisionales aquí y en la BD
let ALL_WIDGET_TYPES = [
  'w-fre',
  'w-pas',
  'w-sue',
  'w-pes',
  'A001',
  'A002',
  'A003',
  'D001',
  'D002',
  'D003',
  'D004'

];

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
  /* 
  Para cambiar el orden de los widgets 
  se modificó el orden en el que aparecen en la tabla trackr.widget 
  */
  'w-fre': {class: WidgetFrecuenciaComponent, columns: 6},
  'w-pas': { class: WidgetPasosComponent, columns: 6 },
  'w-sue' : {class: WidgetSuenoComponent, columns: 6},
  'w-pes': { class: WidgetPesoComponent, columns: 6 },
  'w-omr': { class: WidgetOmronComponent, columns: 12 },
  'A001' : {class: WidgetSeguimientoComponent, columns: 12},
  'A002' : {class: WidgetSeguimientoComponent, columns: 12},
  'A003' : {class: WidgetSeguimientoComponent, columns: 12},
  'D001' : {class: WidgetSeguimientoComponent, columns: 12},
  'D002' : {class: WidgetSeguimientoComponent, columns: 12},
  'D003' : {class: WidgetSeguimientoComponent, columns: 12},
  'D004' : {class: WidgetSeguimientoComponent, columns: 12},

};

export function getAllWidgetTypes() {
  return ALL_WIDGET_TYPES;
}

export function setAllWidgetTypes(newTypes: string[]) {
  ALL_WIDGET_TYPES = newTypes;
}
