import { OverlayModule } from '@angular/cdk/overlay';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SidebarModule } from '@coreui/angular';
import { BsModalRef, ModalModule } from 'ngx-bootstrap/modal';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdministradorAuthGuard } from './auth/administrador-auth-guard.service';
import { AdministradorAuthService } from './auth/administrador-auth.service';
import { TokenInterceptor } from './auth/token.interceptor';
import { MensajeModule } from './shared/components/mensaje/mensaje.module';
import { LoginService } from './shared/services/seguridad/login.service';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { TableModule } from 'primeng/table';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';

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
    FormsModule
  ],
  providers: [ LoginService,
    AdministradorAuthService,
    AdministradorAuthGuard,
    BsModalRef,
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
