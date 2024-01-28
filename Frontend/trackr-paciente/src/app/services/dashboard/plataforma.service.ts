import { Injectable } from '@angular/core';
import { Platform } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class PlataformaService {

constructor(private platform : Platform) { }

  public isWeb(): boolean {
    return this.platform.is('desktop') || this.platform.is('mobileweb');
  }

  public isMobile(): boolean {
    return this.platform.is('android') || this.platform.is('ios');
  }

}
