import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { water, walk, scale, moon  } from 'ionicons/icons'

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
  @Input() public colorTitle?: string;

  constructor() {
    addIcons({water, walk, scale, moon})
   }

  ngOnInit() {}

}
