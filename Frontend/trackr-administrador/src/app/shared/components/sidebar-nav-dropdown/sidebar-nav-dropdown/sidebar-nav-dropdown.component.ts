import { Component, Input, OnInit } from '@angular/core';
import { NavItem } from '@components/layout-administrador/layout-administrador.component';
import { GeneralConstant } from '@utils/general-constant';

@Component({
  selector: 'app-nav-dropdown',
  templateUrl: './sidebar-nav-dropdown.component.html',
  styleUrls: ['./sidebar-nav-dropdown.component.scss']
})
export class SidebarNavDropdownComponent implements OnInit {

  @Input() navItems: NavItem[] = [];

  protected readonly imagenUsuario = 'assets/img/pruebas/user-image.png';
  protected readonly imagenLogotipo = 'assets/img/logo-trackr.png';

  constructor() { }

  ngOnInit() {
  }

}
