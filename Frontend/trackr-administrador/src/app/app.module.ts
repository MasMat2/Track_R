import { AdministradorAuthService } from './auth/administrador-auth.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginService } from './shared/services/seguridad/login.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TokenInterceptor } from './auth/token.interceptor';
import { AdministradorAuthGuard } from './auth/administrador-auth-guard.service';
import { BsModalRef, ModalModule } from 'ngx-bootstrap/modal';
import { MensajeModule } from './shared/components/mensaje/mensaje.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ModalModule.forRoot(),
    MensajeModule,
  ],
  providers: [ LoginService,
    AdministradorAuthService,
    AdministradorAuthGuard,
    BsModalRef,
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }
  ],
    bootstrap: [AppComponent]
})
export class AppModule { }
