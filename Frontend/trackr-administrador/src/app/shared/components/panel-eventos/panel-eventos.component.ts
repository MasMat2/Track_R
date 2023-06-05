import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { first } from 'rxjs/operators';

import { esEventoAgrupador, EventoPanel } from './evento-panel';
import { GeneralConstant } from '@utils/general-constant';
import { SubEventosComponent } from './sub-eventos/sub-eventos.component';


@Component({
  selector: 'app-panel-eventos',
  templateUrl: './panel-eventos.component.html',
  styleUrls: ['./panel-eventos.component.scss']
})

export class PanelEventosComponent implements OnInit, OnChanges {
 
  @Input() public elementoActivo: any;
  @Input() public eventos: EventoPanel[];

  @Output() public onClicked: EventEmitter<EventoPanel> = new EventEmitter<EventoPanel>();

  public cargando: boolean = false;
  public estatusEventos: { [key: string]: boolean } = {}

  constructor(
    private _modalService: BsModalService
  ) { }

  public ngOnInit(): void {
    this.estatusEventos = this.eventos.reduce((obj, evento) => {
      obj[evento.nombre] = false;
      return obj;
    }, {});  
  }
  
  public ngOnChanges(changes: SimpleChanges): void {
    this.actualizarEventos();
  }

  private deshabilitarPanel(): void {
    for (const key in this.estatusEventos) {
      this.estatusEventos[key] = false;
    }
  }

  public async handleClick(evento: EventoPanel): Promise<void> {
    if (!this.estatusEventos[evento.nombre]) {
      return;
    }

    this.deshabilitarPanel();
    this.cargando = true;

    try {
      if (esEventoAgrupador(evento)) {
        await this.abrirModalSubEventos(evento);
      }
      else {
        await evento.onClick(this.elementoActivo);
        this.onClicked.emit(evento);
      }
    }
    catch (error) {

    }

    this.cargando = false;
    this.actualizarEventos();
  }

  private async abrirModalSubEventos(evento: EventoPanel): Promise<void> {
    const initalState = {
      titulo: evento.nombre,
      elementoActivo: this.elementoActivo,
      eventos: evento.subEventos,
    };

    const modalRef = this._modalService.show(
      SubEventosComponent,
      {
        initialState: initalState,
        ...GeneralConstant.CONFIG_MODAL_SMALL
      }
    );

    const subEvento = await modalRef.content?.onClicked.pipe(first()).toPromise();

    if (subEvento !== undefined) {
      this.onClicked.emit(evento);
    }
  }

  private async estaHabilitado(evento: EventoPanel): Promise<boolean> {
    if (!this.elementoActivo) {
      return false;
    }

    if (evento.funcionHabilitado !== undefined) {
      return await evento.funcionHabilitado(this.elementoActivo);
    }
    else if (esEventoAgrupador(evento)) {
      // TODO: 2023-03-17 -> Revisar si es posible cancelar las promesas
      // si una promesa anterior ya resolvió a true
      // https://stackoverflow.com/questions/51160260/clean-way-to-wait-for-first-true-returned-by-promise
      // const promises: Promise<boolean>[] = evento.hijos
      //   .map((subEvento) => this.estaHabilitado(subEvento))
      //   .map(p => new Promise(
      //     (resolve, reject) => p.then(v => {return v === true && resolve(true), reject})
      //   ));

      // promises.push(Promise.all(promises).then(() => false));

      // return await Promise.race(promises);

      const promises: Promise<boolean>[] = evento.subEventos
        .map((subEvento) => this.estaHabilitado(subEvento));

      const resultados = await Promise.all(promises);

      return resultados.some((resultado) => resultado);
    }

    return true;
  }

  private async actualizarEventos(): Promise<void> {
    // Los eventos se deshabilitan hasta que se calcule el estado de todos los eventos
    this.deshabilitarPanel();

    // Permite que se muestre la animación de desactivar los eventos. Esta animación ayuda
    // a identificar que cambió el elemento seleccionado.
    await new Promise(r => setTimeout(r, 400));

    // Se calcula el estado de cada evento. Algunos eventos requieren esperar una respuesta
    // del servidor para calcular su estado, por lo que hay métodos asíncronos.
    const promises = this.eventos.map((panelEvento) => this.estaHabilitado(panelEvento));

    // Esperamos a que se resuelvan todas las promesas para mostrar el estado de los eventos
    // al mismo tiempo
    const resultados = await Promise.all(promises);

    const keys = Object.keys(this.estatusEventos);
    for (let i=0; i<keys.length; i++) {
      const key = keys[i];
      this.estatusEventos[key] = resultados[i];
    }
  }
  
}