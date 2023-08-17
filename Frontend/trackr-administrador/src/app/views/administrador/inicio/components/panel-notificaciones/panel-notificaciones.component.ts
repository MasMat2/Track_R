import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-panel-notificaciones',
  templateUrl: './panel-notificaciones.component.html',
  styleUrls: ['./panel-notificaciones.component.scss']
})
export class PanelNotificacionesComponent implements OnInit {


  protected notificaciones: {
    id: number,
    paciente: string,
    mensaje: string,
    fecha: Date,
    imagen?: string
  }[] = [
    // give me mock data for this interface
    { id: 1, paciente: 'Juan López', mensaje: 'Fusce in faucibus mauris. Vivamus nisi lacus, pellentesque ac libero sed, varius posuere nisi. ', fecha: new Date(), imagen: undefined },
    { id: 2, paciente: 'Roberto Hernández', mensaje: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ', fecha: new Date(), imagen: undefined },
    { id: 3, paciente: 'Lucero Castro', mensaje: 'Ultrices tincidunt arcu non sodales neque sodales ut etiam.', fecha: new Date(), imagen: undefined },
    { id: 4, paciente: 'Erick Villarreal', mensaje: 'Vitae et leo duis ut. Nibh praesent tristique magna sit amet purus gravida quis blandit.', fecha: new Date(), imagen: undefined },
    { id: 5, paciente: 'Daniela Guadalupe', mensaje: 'Sed adipiscing diam donec adipiscing tristique. Semper eget duis at tellus at.', fecha: new Date(), imagen: undefined },
    { id: 6, paciente: 'Gloria Martínez', mensaje: 'Massa id neque aliquam vestibulum morbi blandit cursus risus.', fecha: new Date(), imagen: undefined },
  ];

  constructor() { }

  ngOnInit() {
  }

}
