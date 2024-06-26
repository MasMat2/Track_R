import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SidebarModule } from '@coreui/angular';
import { SidebarNavDropdownModule } from '@sharedComponents/sidebar-nav-dropdown/sidebar-nav-dropdown.module';
import { LayoutAdministradorComponent } from './layout-administrador.component';
import { LayoutAdminitradorRoutingModule } from './layout-administrador.routing.module';
import { MatSidenavModule } from '@angular/material/sidenav';
import { ToolbarModule } from '@sharedComponents/toolbar/toolbar.module';

@NgModule({
  imports: [
    CommonModule,
    LayoutAdminitradorRoutingModule,
    SidebarModule,
    SidebarNavDropdownModule,
    MatSidenavModule,
    ToolbarModule,
  ],
  declarations: [
    LayoutAdministradorComponent
  ],
  entryComponents: [],
})
export class LayoutAdminsitradorModule {}
