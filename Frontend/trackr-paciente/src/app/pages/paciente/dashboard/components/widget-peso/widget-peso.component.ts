import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';

@Component({
  selector: 'app-widget-peso',
  templateUrl: './widget-peso.component.html',
  styleUrls: ['./widget-peso.component.scss'],
  standalone: true,
  imports: [
    WidgetComponent,
  ]
})
export class WidgetPesoComponent  implements OnInit {

  constructor() { }

  ngOnInit() {}

}
