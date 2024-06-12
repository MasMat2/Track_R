import { enableProdMode, importProvidersFrom } from '@angular/core';
import { bootstrapApplication } from '@angular/platform-browser';
import { RouteReuseStrategy, provideRouter, withHashLocation } from '@angular/router';
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { provideIonicAngular} from '@ionic/angular/standalone';

import { routes } from './app/app.routes';
import { AppComponent } from './app/app.component';
import { environment } from './environments/environment';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TokenInterceptor } from './app/auth/token.interceptor';
import { defineCustomElements } from '@ionic/pwa-elements/loader';
import { registerLocaleData } from '@angular/common';
import { LOCALE_ID } from '@angular/core';

import localeEsMx from '@angular/common/locales/es-MX'
registerLocaleData(localeEsMx, 'es-MX');

if (environment.production) {
  enableProdMode();
}

bootstrapApplication(AppComponent, {
  providers: [
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    // { provide: LOCALE_ID, useValue: 'es-MX'},
    importProvidersFrom(IonicModule.forRoot({})),
    importProvidersFrom(HttpClientModule),
    provideRouter(routes, withHashLocation()),
    provideIonicAngular({ mode: 'md', innerHTMLTemplatesEnabled:true , rippleEffect: false}),
  ],
});
defineCustomElements(window);