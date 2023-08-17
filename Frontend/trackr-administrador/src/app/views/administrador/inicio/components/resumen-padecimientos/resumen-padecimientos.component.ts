import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-resumen-padecimientos',
  templateUrl: './resumen-padecimientos.component.html',
  styleUrls: ['./resumen-padecimientos.component.scss']
})
export class ResumenPadecimientosComponent implements OnInit {

  protected padecimientos = [
    {nombre: "Diabetes", cantidad: 50},
    {nombre: "Hipertensión", cantidad: 300},
    {nombre: "Cáncer", cantidad: 22},
    {nombre: "Obesidad", cantidad: 12},
    {nombre: "Enfermedades del corazón", cantidad: 11},
  ];

  constructor() { }

  ngOnInit() {
  }

}
