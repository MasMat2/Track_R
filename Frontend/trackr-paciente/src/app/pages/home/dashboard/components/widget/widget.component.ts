import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';

@Component({
  selector: 'app-widget',
  templateUrl: './widget.component.html',
  styleUrls: ['./widget.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
  ]
})
export class WidgetComponent  implements OnInit {

  @Input() public title: string;
  @Input() public iconClass?: string;
  @Input() public colorBackground?: string;

  constructor() { }

  ngOnInit() {}

}
