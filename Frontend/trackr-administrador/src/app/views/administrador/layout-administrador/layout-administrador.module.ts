import { NgModule } from '@angular/core';
import {MatSidenavModule} from '@angular/material/sidenav';
import { LayoutAdminitradorRoutingModule } from './layout-administrador.routing.module';
import { LayoutAdministradorComponent } from './layout-administrador.component';
import { InicioComponent } from '../inicio/inicio.component';
import { CommonModule } from '@angular/common';


@NgModule({
  imports: [
    CommonModule,
    LayoutAdminitradorRoutingModule,
    MatSidenavModule,
  ],
  declarations: [
    InicioComponent,
    LayoutAdministradorComponent
  ],
  entryComponents: [],
  providers: [
  ]
})
export class LayoutAdminsitradorModule {}
