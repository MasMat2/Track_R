import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { NotificacionesPageComponent } from './notificaciones/notificacionesPage/notificaciones-page.component';
import { UsuarioService } from '@services/usuario.service';
import { UsuarioDto } from '@dtos/perfil/usuario-dto';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    NotificacionesPageComponent
  ]
})
export class HeaderComponent implements OnInit {

  @Input() mostrarTitulo: boolean = false;
  @Input() titulo?: string;

  protected miUsuario : UsuarioDto;

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
