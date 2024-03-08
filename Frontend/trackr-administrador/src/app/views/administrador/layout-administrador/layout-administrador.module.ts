import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SidebarModule } from '@coreui/angular';
import { SidebarNavDropdownModule } from '@sharedComponents/sidebar-nav-dropdown/sidebar-nav-dropdown.module';
import { LayoutAdministradorComponent } from './layout-administrador.component';
import { LayoutAdminitradorRoutingModule } from './layout-administrador.routing.module';
import { MatSidenavModule } from '@angular/material/sidenav';


@NgModule({
  imports: [
    CommonModule,
    LayoutAdminitradorRoutingModule,
    SidebarModule,
    SidebarNavDropdownModule,
    MatSidenavModule
  ],
  declarations: [
    LayoutAdministradorComponent
  ],
  entryComponents: [],
})
export class LayoutAdminsitradorModule {}
