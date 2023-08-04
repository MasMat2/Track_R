import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-widget-diabetes',
  templateUrl: './widget-diabetes.component.html',
  styleUrls: ['./widget-diabetes.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    WidgetComponent,
  ]
})
export class WidgetDiabetesComponent  implements OnInit {

  protected glucosa: number = 119;
  protected insulina: number = 2;
  protected plum: number = 25;
  constructor() { }

  ngOnInit() {}

}
