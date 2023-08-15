import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule
  ]
})
export class HeaderComponent implements OnInit {

  @Input() mostrarTitulo: boolean = false;
  @Input() titulo?: string;

  constructor() { }

  ngOnInit() {}

}
