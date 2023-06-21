import { AyudaModalComponent } from './sidebar-nav-dropdown/ayuda-modal/ayuda-modal.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuAnidadoComponent } from './menu-anidado/menu-anidado.component';
import { SidebarNavDropdownComponent } from './sidebar-nav-dropdown/sidebar-nav-dropdown.component';
import { RouterModule } from '@angular/router';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { MenuItemComponent } from './menu-item/menu-item.component';

@NgModule({
  declarations: [
    MenuAnidadoComponent,
    SidebarNavDropdownComponent,
    MenuItemComponent,
    AyudaModalComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    MatMenuModule,
    MatSidenavModule,
    MatButtonModule,
  ],
  exports: [SidebarNavDropdownComponent]
})
export class SidebarNavDropdownModule { }
