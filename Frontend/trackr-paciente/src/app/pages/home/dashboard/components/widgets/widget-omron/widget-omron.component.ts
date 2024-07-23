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
    addIcons({
      'bar-chart': 'assets/img/svg/bar-chart-4.svg'
    })
   }

  ngOnInit() {}

  async openOmronConnect() {
    try {
      const canOpen = await AppLauncher.canOpenUrl({ url: 'jp.co.omron.healthcare.omron_connect' });

      if (canOpen.value) {
        await AppLauncher.openUrl({ url: 'jp.co.omron.healthcare.omron_connect' });
        console.log('Abriendo Omron');
      } else {
        // Abrir Play Store si la app no está instalada
        await AppLauncher.openUrl({ url: 'market://details?id=jp.co.omron.healthcare.omron_connect' });
        console.log('Omron no está instalada. Abriendo Play Store.');
      }
    } catch (error) {
      console.error('No se puede abrir la aplicación Omron', error);
      // Abrir Play Store si ocurre algún error
      await AppLauncher.openUrl({ url: 'market://details?id=jp.co.omron.healthcare.omron_connect' });
    }
  }

}
