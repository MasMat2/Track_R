import { AccesoService } from '@http/seguridad/acceso.service';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { NavItem } from '@components/layout-administrador/layout-administrador.component';
import { ArchivoService } from '@http/catalogo/archivo.service';
import { Acceso } from '@models/seguridad/acceso';
import { AccesoAyudaSeccionado } from '@models/seguridad/acceso-ayuda-seccionado';
import { UsuarioImagenService } from '@services/usuario-imagen.service';
import { GeneralConstant } from '@utils/general-constant';
import { filter, map, Observable, Subscription } from 'rxjs';
import { AccesoAyudaService } from '@http/seguridad/acceso-ayuda.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AyudaModalComponent } from './ayuda-modal/ayuda-modal.component';
import { ActivatedRoute, ActivatedRouteSnapshot, NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-nav-dropdown',
  templateUrl: './sidebar-nav-dropdown.component.html',
  styleUrls: ['./sidebar-nav-dropdown.component.scss']
})
export class SidebarNavDropdownComponent implements OnInit {

  @Input() navItems: NavItem[] = [];

  @Output() logoutRequest = new EventEmitter<void>();

  protected readonly imagenUsuario = 'assets/img/pruebas/user-image.png';
  protected readonly imagenLogotipo = 'assets/img/logo-trackr.png';
  protected urlImagen?: SafeUrl = undefined;

  // Sección de Ayudas
  public ayudas: AccesoAyudaSeccionado[] = [];
  public acceso: Acceso;
  public subs: Array<Subscription> = [];
  public claveAcceso: string;
  public idSecciones: number[];

  constructor(
    private sanitizer: DomSanitizer,
    private usuarioImagenService: UsuarioImagenService,
    private accesoService: AccesoService,
    private accesoAyudaService: AccesoAyudaService,
    private modalService: BsModalService,
    private router: Router,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
  ) { }
  
  ngOnInit() {
    this.usuarioImagenService.consultarImagen();

    this.usuarioImagenService.imagenBase64$
      .subscribe({
        next: (imagenBase64) => {
          const imagen = imagenBase64 === undefined ? this.imagenUsuario : imagenBase64;
          this.urlImagen = this.sanitizer.bypassSecurityTrustUrl(imagen);
        }
      });

    this.actualizarAcceso();

    this.router.events
      .pipe(filter((event): event is NavigationEnd => event instanceof NavigationEnd))
      .subscribe(() => {
        this.actualizarAcceso()
      });
  }

  private actualizarAcceso(): void {
    let route = this.route.snapshot;

    while (route.firstChild) {
      route = route.firstChild;
    }

    this.claveAcceso = route.data?.['acceso'];
    this.consultarAcceso();
  }

  public logout() {
    this.logoutRequest.emit();
    this.usuarioImagenService.actualizarImagen('');
    
  }

  public consultarAcceso() {
    if (this.claveAcceso === undefined || this.claveAcceso === null) {
      return
    }
    this.accesoService.consultarPorClave(this.claveAcceso).subscribe(data => {
      this.acceso = data;
      this.accesoAyudaService.consultarPorAccesoPorSeccion(this.acceso.idAcceso).subscribe((data1) => {
        if(data1 == null){
          return
        }
        this.ayudas = data1;
      });
    });
  }

  public consultarAyuda() {
    const initialState = {
      ayudas: this.ayudas,
      acceso: this.acceso
    };

    this.bsModalRef = this.modalService.show(AyudaModalComponent, {
      initialState,
      ...GeneralConstant.CONFIG_MODAL_SMALL
    });

    this.bsModalRef.content!.onClose = (cerrar: boolean) => {
      if (cerrar) {
      }
      this.bsModalRef.hide();
    };
  }
}
