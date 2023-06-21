import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Acceso } from '@models/seguridad/acceso';
import { AccesoAyudaSeccionado } from '@models/seguridad/acceso-ayuda-seccionado';

@Component({
  selector: 'app-layout-administrador',
  templateUrl: './ayuda-modal.component.html',
  styleUrls: ['./ayuda-modal.component.scss']
})
export class AyudaModalComponent implements OnInit {
  public sidebarMinimized = false;
  public navItems: any = [];
  public nombreUsuario: string;
  public imagenUsuario = 'assets/img/svg/ico-36x36-header-usuario.svg';
  public cantidadChatsPendientes = 0;
  public onClose: any;
  public ayudas: AccesoAyudaSeccionado[] = [];
  public acceso: Acceso = new Acceso();
  public urlVideoAyuda: string;

  constructor(private sanitizer: DomSanitizer) {}

  public ngOnInit() {
    if(this.acceso.urlVideoAyuda != null)
      {
        this.urlVideoAyuda = this.sanitize(this.acceso.urlVideoAyuda);
      }
  }

  public cancelar() {
    this.onClose(true);
  }

  public sanitize(url: string): any {
    if (url) {
      const safeurl = this.sanitizer.bypassSecurityTrustResourceUrl(url);
      return safeurl;
    }
    return '';
  }

  public transform(imagen: any){
    return this.sanitizer.bypassSecurityTrustResourceUrl(`data:imagen/;base64, ${imagen}`)
  }
}
