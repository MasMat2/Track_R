import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AlertasModule } from './components/alertas/alertas.module';
import { GraficaNotificacionesModule } from './components/grafica-notificaciones/grafica-notificaciones.module';
import { GraficaTratamientosModule } from './components/grafica-tratamientos/grafica-tratamientos.module';
import { ResumenPadecimientosModule } from './components/resumen-padecimientos/resumen-padecimientos.module';
import { UsuarioService } from '../../../shared/http/seguridad/usuario.service';
import { Usuario } from '@models/seguridad/usuario';
import { Observable } from 'rxjs';
import { PanelNotificacionesModule } from './components/panel-notificaciones/panel-notificaciones.module';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { PanelNotificacionesComponent } from './components/panel-notificaciones/panel-notificaciones.component';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    AlertasModule,
    ResumenPadecimientosModule,
    GraficaNotificacionesModule,
    GraficaTratamientosModule,
    PanelNotificacionesModule,
    PopoverModule,
  ]
})
export class InicioComponent implements OnInit {

  protected usuario$: Observable<Usuario>;

  constructor(
    private usuarioService: UsuarioService
  ) { }

  ngOnInit(): void {
    this.usuario$ = this.usuarioService.consultarMiPerfil();
  }

}
