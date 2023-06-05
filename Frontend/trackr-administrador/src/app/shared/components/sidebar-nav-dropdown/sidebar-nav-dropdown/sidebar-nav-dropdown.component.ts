import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { NavItem } from '@components/layout-administrador/layout-administrador.component';
import { ArchivoService } from '@http/catalogo/archivo.service';
import { UsuarioImagenService } from '@services/usuario-imagen.service';
import { GeneralConstant } from '@utils/general-constant';
import { Observable } from 'rxjs';

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

  constructor(
    private sanitizer: DomSanitizer,
    private usuarioImagenService: UsuarioImagenService
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
  }

  public logout() {
    this.logoutRequest.emit();
    this.usuarioImagenService.actualizarImagen('');
    
  }

}
