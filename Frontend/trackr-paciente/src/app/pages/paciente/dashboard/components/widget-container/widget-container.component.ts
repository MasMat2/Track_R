import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { WidgetComponent } from '../widget/widget.component';

@Component({
  selector: 'app-widget-container',
  templateUrl: './widget-container.component.html',
  styleUrls: ['./widget-container.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    WidgetComponent
  ]
})
export class WidgetContainerComponent implements OnInit {
  constructor() { }

  ngOnInit() {
  }

}
