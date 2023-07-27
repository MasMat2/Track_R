import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-menu-anidado',
  templateUrl: './menu-anidado.component.html',
  styleUrls: ['./menu-anidado.component.scss']
})
export class MenuAnidadoComponent implements OnInit {
  @Input() items: any[];
  @ViewChild('menuHijo', {static: true}) public menuHijo: any;

  constructor() { }

  ngOnInit() {
  }

}
