import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-alertas',
  templateUrl: './alertas.component.html',
  styleUrls: ['./alertas.component.scss']
})
export class AlertasComponent implements OnInit {

  protected pacientesFueraDeRango: number = 15;
  protected solicitudesDeChat: number = 30;
  protected solicitudesDeVideo: number = 22;

  constructor() { }

  ngOnInit() {
  }

}
