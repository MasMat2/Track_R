import { Component, OnInit } from '@angular/core';
import { Acceso } from '@models/seguridad/acceso';
import { map } from 'rxjs';
import { AccesoService } from '../../../shared/http/seguridad/acceso.service';
import { GeneralConstant } from '@utils/general-constant';
import { Router } from '@angular/router';
import { AccesoMenuDto } from '@dtos/seguridad/acceso-menu-dto';

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
    private router: Router,
  ) {}

  public ngOnInit(): void {
    this.consultarMenu();
  }

  public logout(): void {
    localStorage.removeItem(GeneralConstant.TOKEN_KEY);
    this.router.navigate(['/login']);
  }

  public consultarMenu(): void {
    this.accesoService
      .consultarMenu()
      .subscribe((accesos: AccesoMenuDto[]) => {
        this.navItems = accesos.map((acceso: AccesoMenuDto) => this.mapearAcceso(acceso));
      });
  }

  private mapearAcceso(acceso: AccesoMenuDto): any {
    return {
      name: acceso.nombre,
      claveTipoAcceso: acceso.claveTipoAcceso,
      class: 'sidebar-boton-menu',
      url: acceso.url === null ? '/acceso' : acceso.url,
      icon: acceso.claseIcono,
      children: acceso.hijos.map((hijo: AccesoMenuDto) => this.mapearAcceso(hijo))
    };
  }

}
