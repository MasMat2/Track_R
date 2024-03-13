type Evento = {
  nombre: string;
  icono?: string;
  claseIcono?: string;
}

type EventoAgrupador = Evento & {
  subEventos: EventoPanel[];

  funcionHabilitado?: never;
  onClick?: never;
}

type EventoFinal = Evento & {
  funcionHabilitado?: (elemento: any) => (boolean | Promise<boolean>);
  onClick: (elemento: any) => (any | Promise<any>);

  subEventos?: never;
}

export type EventoPanel = EventoAgrupador | EventoFinal;

export function esEventoAgrupador(evento: EventoPanel): evento is EventoAgrupador {
  return (evento as EventoAgrupador).subEventos !== undefined;
}
