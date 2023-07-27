import { Component, OnInit, Input } from '@angular/core';
import { NavItem } from '@components/layout-administrador/layout-administrador.component';
import { GeneralConstant } from '@utils/general-constant';

@Component({
  selector: 'app-menu-item',
  templateUrl: './menu-item.component.html',
  styleUrls: ['./menu-item.component.scss']
})
export class MenuItemComponent implements OnInit {

  @Input() public item: NavItem;

  protected readonly claveTipoAccesoSistema = GeneralConstant.CLAVE_TIPO_ACCESO_SISTEMA;

  constructor() { }

  ngOnInit(): void {
  }

}
