import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { ComponentsModule } from '@shared/components/components.module';

@Component({
  selector: 'app-layout-paciente',
  templateUrl: './layout-paciente.page.html',
  styleUrls: ['./layout-paciente.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule, 
    ComponentsModule
  ]
})
export class LayoutPacientePage implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
