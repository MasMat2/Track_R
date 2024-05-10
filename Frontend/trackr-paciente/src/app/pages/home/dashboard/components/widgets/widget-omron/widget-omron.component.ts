import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { barChartOutline, scaleOutline } from 'ionicons/icons';
import { AppLauncher } from '@capacitor/app-launcher';

import { App } from '@capacitor/app';

App.addListener('appStateChange', ({ isActive }) => {
  console.log('App state changed. Is active?', isActive);
});

@Component({
  selector: 'app-widget-omron',
  templateUrl: './widget-omron.component.html',
  styleUrls: ['./widget-omron.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    WidgetComponent,
    IonicModule
  ]
})
export class WidgetOmronComponent  implements OnInit {

  constructor() {
    addIcons({barChartOutline})
   }

  ngOnInit() {}

  async openOmronConnect() {
    try {
      
      await AppLauncher.canOpenUrl({ url: 'omronconnect://' }); // El nombre del paquete se obtiene investigandolo desde la app OMRON connect
      await AppLauncher.openUrl({ url: 'omronconnect://'}); 
      console.log('Abriendo Omron');

    } catch (error) {
      console.error('No se puede abrir la aplicación Omron', error);
    }
  }

}
