import { OverlayModule } from '@angular/cdk/overlay';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SidebarModule } from '@coreui/angular';
import { BsModalRef, ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TableModule } from 'primeng/table';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdministradorAuthGuard } from './auth/administrador-auth-guard.service';
import { AdministradorAuthService } from './auth/administrador-auth.service';
import { TokenInterceptor } from './auth/token.interceptor';
import { MensajeModule } from './shared/components/mensaje/mensaje.module';
import { LoginService } from './shared/services/seguridad/login.service';
import { ChatModule } from './views/chat/chat.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { EntidadEstructuraService } from '@http/gestion-entidad/entidad-estructura.service';
import { MatDialogModule } from '@angular/material/dialog';
import { ToolbarModule } from '@sharedComponents/toolbar/toolbar.module';
import { PanelNotificacionesModule } from './views/administrador/inicio/components/panel-notificaciones/panel-notificaciones.module';

import { registerLocaleData } from '@angular/common';
import localeEsMx from '@angular/common/locales/es-MX'
import { SidebarNavDropdownModule } from '@sharedComponents/sidebar-nav-dropdown/sidebar-nav-dropdown.module';
registerLocaleData(localeEsMx, 'es-MX');

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ModalModule.forRoot(),
    MensajeModule,
    SidebarModule,
    OverlayModule,
    MatMenuModule,
    MatSidenavModule,
    TableModule,
    PaginationModule.forRoot(),
    FormsModule,
    ChatModule,
    NgSelectModule,
    MatDialogModule,
    ToolbarModule,
    PanelNotificacionesModule,
    SidebarNavDropdownModule
  ],
  providers: [ LoginService,
    AdministradorAuthService,
    AdministradorAuthGuard,
    EntidadEstructuraService,
    BsModalRef,
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
