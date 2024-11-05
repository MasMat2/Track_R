import { Component, OnInit } from '@angular/core';
import { IonicModule, ModalController } from '@ionic/angular';

import { OmronCustom } from 'bauma-plugin';
import { CommonModule } from '@angular/common';
import { addIcons } from 'ionicons';
import { refresh, close } from 'ionicons/icons';
import { AlertController } from '@ionic/angular';
import { LoadingController } from '@ionic/angular';

@Component({
  selector: 'app-bauma-reader',
  templateUrl: './bauma-reader.component.html',
  styleUrls: ['./bauma-reader.component.scss'],
  standalone: true,
  imports: [
    IonicModule,    
    CommonModule,
  ]
})
export class BaumaReaderComponent  implements OnInit {

  devices: Array<{ model: string; identifier: string }> = [];
  isLoadingDevices = true;

  constructor(private modalController: ModalController,
    private alertController: AlertController,
    private loadingController: LoadingController) {
    addIcons({ refresh, close })
  }

  ngOnInit() {
    this.scanForDevices();
  }

  async scanForDevices() {
    try {
      this.isLoadingDevices = true;
      const result = await OmronCustom.scanDevices();
      // const result = {"message":"No devices found.","code":"UNAVAILABLE","errorMessage":"No devices found.","data":{}};
      if(result.devices){
        this.devices = result.devices;
      }
    } catch (error) {
      console.error('Error scanning devices:', error);
    } finally {
      this.isLoadingDevices = false;
    }
  }

  async selectDevice(device: { identifier: any; }) {
    const loading = await this.loadingController.create({
      message: 'Consultando lecutras...'
    });
    await loading.present();

    try {
      const readingsResult = await OmronCustom.getReadings({ deviceId: device.identifier });
      await loading.dismiss();

      const readings = readingsResult.readings;
      if (readings.length === 0) {
        const alert = await this.alertController.create({
          header: 'El dispositivo seleccionado no tiene lecturas',
          buttons: ['OK']
        });
        await alert.present();
        await alert.onDidDismiss();
      }
      
      this.modalController.dismiss({ readings });

    } catch (error) {
      await loading.dismiss();
      console.error('Error getting readings:', error);
    }
  }

  closeModal() {
    this.modalController.dismiss();
  }
}

