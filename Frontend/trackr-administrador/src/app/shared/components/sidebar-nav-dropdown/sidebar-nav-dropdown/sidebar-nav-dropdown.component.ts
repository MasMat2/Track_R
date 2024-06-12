import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { NavItem } from '@components/layout-administrador/layout-administrador.component';
import { AccesoAyudaService } from '@http/seguridad/acceso-ayuda.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Acceso } from '@models/seguridad/acceso';
import { AccesoAyudaSeccionado } from '@models/seguridad/acceso-ayuda-seccionado';
import { UsuarioImagenService } from '@services/usuario-imagen.service';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable, Subscription, filter } from 'rxjs';
import { AyudaModalComponent } from './ayuda-modal/ayuda-modal.component';
import { PanelNotificacionesComponent } from '@components/inicio/components/panel-notificaciones/panel-notificaciones.component';
import { Usuario } from '@models/seguridad/usuario';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { MatDialog } from '@angular/material/dialog';
import { CustomAlertComponent } from '@sharedComponents/custom-alert/custom-alert.component';
import { CustomAlertData } from '@sharedComponents/interface/custom-alert-data';

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
  protected usuario$: Observable<Usuario>;
  
  constructor(
    private sanitizer: DomSanitizer,
    private usuarioImagenService: UsuarioImagenService,
    private accesoService: AccesoService,
    private accesoAyudaService: AccesoAyudaService,
    private modalService: BsModalService,
    private router: Router,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
    private usuarioService: UsuarioService,
    private dialog: MatDialog
  ) { }

  ngOnInit() {
    // this.usuarioImagenService.consultarImagen();

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

      this.usuario$ = this.usuarioService.consultarMiPerfil();
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

  public mostrarNotificaciones(){
    this.bsModalRef = this.modalService.show(PanelNotificacionesComponent,{
      class: 'modal-izquierda'
    })
    this.bsModalRef.content!.onClose = (cerrar: boolean) => {
      if (cerrar) {
      }
      this.bsModalRef.hide();
    };
  }

  protected presentAlertCerrarSesion(){
    const alert = this.dialog.open(CustomAlertComponent, {
      panelClass: 'custom-alert',
      data:{
        header: 'Cerrar sesión',
        subHeader: '¿Seguro(a) que desea cerrar sesión?',
        Icono: 'info',
        Color: 'error',
        twoButtons: true,
        cancelButtonText: 'No, cancelar',
        confirmButtonText: "Si, aceptar"
      } as CustomAlertData
    });
    alert.beforeClosed().subscribe(result => {
      console.log(result); //Rol confirm o cancel
      if(result == "confirm"){
        this.logout();
      }
    })
  }
}
