import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { VarDirective } from '@shared/directives/var.directive';
import { WidgetDictionary, WIDGETS, WidgetType } from '../../interfaces/widgets';
import { WidgetComponent } from '../widget/widget.component';

@Component({
  selector: 'app-widget-container',
  templateUrl: './widget-container.component.html',
  styleUrls: ['./widget-container.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    VarDirective,
    WidgetComponent
  ]
})
export class WidgetContainerComponent implements OnInit {
  protected readonly WIDGETS: WidgetDictionary = WIDGETS;

  @Input() public widgetKeys: WidgetType[];

  constructor() { }

  ngOnInit() {
  }

}
