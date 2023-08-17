import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AlertasModule } from './components/alertas/alertas.module';
import { GraficaPacientesModule } from './components/grafica-pacientes/grafica-pacientes.module';
import { GraficaTratamientosModule } from './components/grafica-tratamientos/grafica-tratamientos.module';
import { ResumenPadecimientosModule } from './components/resumen-padecimientos/resumen-padecimientos.module';
import { UsuarioService } from '../../../shared/http/seguridad/usuario.service';
import { Usuario } from '@models/seguridad/usuario';
import { Observable } from 'rxjs';
import { PanelNotificacionesModule } from './components/panel-notificaciones/panel-notificaciones.module';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    AlertasModule,
    ResumenPadecimientosModule,
    GraficaPacientesModule,
    GraficaTratamientosModule,
    PanelNotificacionesModule
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
