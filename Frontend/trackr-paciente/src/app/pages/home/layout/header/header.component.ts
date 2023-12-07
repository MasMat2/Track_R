import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { IonicModule, PopoverController } from '@ionic/angular';
import { NotificacionesPageComponent } from './notificaciones/notificacionesPage/notificaciones-page.component';
import { UsuarioService } from '@services/usuario.service';
import { UsuarioDto } from 'src/app/shared/Dtos/perfil/usuario-dto';
// import { BreadcrumbModule } from 'angular-crumbs';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { LogoutComponent } from './logout/logout.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    NotificacionesPageComponent,
    RouterModule,
    LogoutComponent/* ,
    BreadcrumbModule */
  ]
})
export class HeaderComponent implements OnInit {

  @Input() mostrarTitulo: boolean = false;
  @Input() titulo?: string;

  public miUsuario : UsuarioDto;

  constructor(
    private usuarioService : UsuarioService,
    private router: Router,
    private popoverControler : PopoverController
  ) { }

  ngOnInit() {
    this.consultarMiUsuario();
  }

  async _openPopover(ev : any)
  {
    const popover = await this.popoverControler.create({
      component : LogoutComponent ,
      event : ev
    })
    return await popover.present();
  }

  protected consultarMiUsuario(){
    this.usuarioService.consultarMiUsuario().subscribe((data) => {
      this.miUsuario = data;
    });
  }

  protected mostrarConfiguracionDashboard(){
    this.router.navigate(['/home/config-dashboard']);
  }

}
