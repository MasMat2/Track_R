import { Component, Input, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-widget-seguimiento',
  templateUrl: './widget-seguimiento.component.html',
  styleUrls: ['./widget-seguimiento.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    WidgetComponent,
    IonicModule,
  ]
})
export class WidgetSeguimientoComponent  implements OnInit {

  @Input() nombrePadecimiento: string;
  @Input() tipoWidget : number;

  @Input() nombreVariable: string;
  @Input() valorVariable: string;
  @Input() unidadVariable: string;
  @Input() claseVariable: string;

  @Input() nombreTratamiento: string;
  @Input() valorTratamiento: string;
  @Input() unidadTratamiento: string;
  @Input() claseTratamiento: string;

  constructor() {
  }

  ngOnInit() {}

}
