import { Type } from "@angular/core";
import { WidgetPasosComponent } from "../components/widget-pasos/widget-pasos.component";
import { WidgetPesoComponent } from "../components/widget-peso/widget-peso.component";

// Todos los tipos de widgets posibles
export const ALL_WIDGET_TYPES = [
  'w-pasos',
  'w-peso',
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
  'w-pasos': { class: WidgetPasosComponent, columns: 8 },
  'w-peso': { class: WidgetPesoComponent, columns: 4 },
};
