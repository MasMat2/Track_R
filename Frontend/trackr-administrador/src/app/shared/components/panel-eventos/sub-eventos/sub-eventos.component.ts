import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { EventoPanel } from '../evento-panel';

@Component({
  selector: 'app-sub-eventos',
  templateUrl: './sub-eventos.component.html',
  styleUrls: ['./sub-eventos.component.scss']
})
export class SubEventosComponent implements OnInit {
  public get modalRef(): BsModalRef {
    return this._modalRef;
  }
  public set modalRef(value: BsModalRef) {
    this._modalRef = value;
  }
  public titulo: string;
  public elementoActivo: any;
  public eventos: EventoPanel[];

  @Output() public onClicked: EventEmitter<EventoPanel> = new EventEmitter<EventoPanel>();
 
  constructor(private _modalRef: BsModalRef) { }

  ngOnInit(): void {
  }

  public emitOnClick(evento: EventoPanel): void {
    this.onClicked.emit(evento);
    this.modalRef.hide();
  }
  
}