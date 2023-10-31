import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { NotificacionesPageComponent } from './notificaciones/notificacionesPage/notificaciones-page.component';
import { UsuarioService } from '@services/usuario.service';
import { UsuarioDto } from 'src/app/shared/Dtos/perfil/usuario-dto';
import { BreadcrumbModule } from 'angular-crumbs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    NotificacionesPageComponent,
    BreadcrumbModule
  ]
})
export class HeaderComponent implements OnInit {

  @Input() mostrarTitulo: boolean = false;
  @Input() titulo?: string;

  public miUsuario : UsuarioDto;

  constructor(
    private usuarioService : UsuarioService
  ) { }

  ngOnInit() {
    this.consultarMiUsuario();
  }

  protected consultarMiUsuario(){
    this.usuarioService.consultarMiUsuario().subscribe((data) => {
      this.miUsuario = data;
    });
  }

}
