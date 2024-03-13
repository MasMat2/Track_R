import { Injectable } from '@angular/core';
import { Platform } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class PlataformaService {

constructor(private platform : Platform) { }

public isWeb(): boolean {
  const platforms = this.platform.platforms();
  return platforms.includes('desktop') || platforms.includes('mobileweb');
}

public isMobile(): boolean {
  const platforms = this.platform.platforms();
  return (platforms.includes('android') || platforms.includes('ios')) && !this.isWeb();
}

}
