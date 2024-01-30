import { Component } from '@angular/core';
import { IonicModule } from '@ionic/angular';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
  standalone: true,
  imports: [
    IonicModule
  ]
})
export class AppComponent {
  androidFetchWorkaround() {
    const originalFetch = (window as any).fetch;
  
    (window as any).fetch = (...args:any) => {
      const [url] = args;
  
      if (typeof url === 'string' && url.match(/\.svg/)) {
        return new Promise((resolve, reject) => {
          const req = new XMLHttpRequest();
          req.open('GET', url, true);
          req.addEventListener('load', () => {
            resolve({ ok: true, text: () => Promise.resolve(req.responseText) });
          });
          req.addEventListener('error', reject);
          req.send();
        });
      } else {
        return originalFetch(...args);
      }
    };
  }
  
  constructor() {
    this.androidFetchWorkaround();
  }
}
