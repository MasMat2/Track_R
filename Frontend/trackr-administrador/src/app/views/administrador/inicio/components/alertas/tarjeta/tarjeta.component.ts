import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-tarjeta',
  templateUrl: './tarjeta.component.html',
  styleUrls: ['./tarjeta.component.scss']
})
export class TarjetaComponent implements OnInit {

  @Input() valor: string;
  @Input() etiqueta: string;
  @Input() claseIcono: string;

  constructor() { }

  ngOnInit() {
  }

}
