import { Injectable } from '@angular/core';
import { AlertController } from '@ionic/angular/standalone';

@Injectable({
  providedIn: 'root'
})
export class LoadingSpinnerService {

constructor(private alertController : AlertController) { }

private loading: any;

async presentLoading() {
  this.loading = await this.alertController.create({
    cssClass: "custom-alert-loading"
  })
  return await this.loading.present();
}

async dismissLoading() {
  if (this.loading) {
    await this.loading.dismiss();
    this.loading = null;
  }
}

}
