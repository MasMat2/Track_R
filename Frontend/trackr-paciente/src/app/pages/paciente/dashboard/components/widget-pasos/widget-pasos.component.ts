import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';

@Component({
  selector: 'app-widget-pasos',
  templateUrl: './widget-pasos.component.html',
  styleUrls: ['./widget-pasos.component.scss'],
  standalone: true,
  imports: [
    WidgetComponent,
  ]
})
export class WidgetPasosComponent  implements OnInit {

  constructor() { }

  ngOnInit() {}

}
