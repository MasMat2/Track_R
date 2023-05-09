import { Component, OnInit } from '@angular/core';
import { Acceso } from '@models/seguridad/acceso';
import { map } from 'rxjs';
import { AccesoService } from '../../../shared/http/seguridad/acceso.service';

export interface NavItem {
  name: string;
  claveTipoAcceso: string;
  url: string;
  icon: string;
  children?: NavItem[];
};

@Component({
  selector: 'app-layout-administrador',
  templateUrl: './layout-administrador.component.html',
  styleUrls: ['./layout-administrador.component.css']
})
export class LayoutAdministradorComponent implements OnInit {
  protected navItems: NavItem[];

  constructor(
    private accesoService: AccesoService,
  ) {}

  public ngOnInit(): void {
    this.consultarMenu();
  }

  private consultarMenu(): void {
    this.accesoService.consultarMenu()
      .pipe(
        map((accesos: Acceso[]) => {
          return accesos.map((acceso: Acceso) => this.mapAccesoToNavItem(acceso))
        })
      )
      .subscribe((navItems: NavItem[]) => {
        this.navItems = navItems;
      });
  }

  private mapAccesoToNavItem(acceso: Acceso): NavItem {
    return {
      name: acceso.nombre,
      claveTipoAcceso: acceso.claveTipoAcceso,
      // class: 'sidebar-boton-menu',
      url: acceso.url === null ? '/a' : acceso.url,
      icon: acceso.claseIcono,
      children: this.generarHijos(acceso)
    };
  }

  private generarHijos(acceso: Acceso): NavItem[] {
    if (
      acceso.hijos === undefined ||
      acceso.hijos === null ||
      acceso.hijos.length === 0
    ) {
      return [];
    }

    return acceso.hijos.map((acceso: Acceso) => this.mapAccesoToNavItem(acceso));
  }

}
