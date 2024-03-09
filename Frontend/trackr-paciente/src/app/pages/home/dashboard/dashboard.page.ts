import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { UsuarioWidgetService } from 'src/app/services/dashboard/usuario-widget.service';
import { HeaderComponent } from '../layout/header/header.component';
import { WidgetPasosComponent } from './components/widget-pasos/widget-pasos.component';
import { WidgetPesoComponent } from './components/widget-peso/widget-peso.component';
import { WidgetSuenoComponent } from './components/widget-sueno/widget-sueno.component';
import { WidgetFrecuenciaComponent } from './components/widget-frecuencia/widget-frecuencia.component';
import { WidgetSeguimientoComponent } from './components/widget-seguimiento/widget-seguimiento.component';
import { WidgetService } from 'src/app/services/dashboard/widget.service';
import { UsuarioPadecimientosDTO } from 'src/app/shared/Dtos/gestion-expediente/usuario-padecimientos-dto';
import { PadecimientoDTO } from 'src/app/shared/Dtos/gestion-expediente/padecimiento-dto';
import { Router, RouterModule } from '@angular/router'; 
import { SeguimientoPadecimientoComponent } from './components/seguimiento-padecimiento/seguimiento-padecimiento.component';
import { WidgetContainerComponent } from './components/widget-container/widget-container.component';
import { WidgetType } from './interfaces/widgets';
import { ChatMensajeHubService } from 'src/app/services/dashboard/chat-mensaje-hub.service';
import { ChatHubServiceService } from 'src/app/services/dashboard/chat-hub-service.service';
import { HealthConnectService } from 'src/app/services/dashboard/health-connect.service';
import { PermissionsStatus } from './interfaces/healthconnect-interfaces';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    HeaderComponent,
    RouterModule,
  ],
  providers: [
    UsuarioWidgetService,
    WidgetService
  ]
})
export class DashboardPage implements OnInit {

  private hasAllPermissionsHealthConnect: boolean = false;

  constructor(
    private widgetService : WidgetService,
    private usuarioWidgetService: UsuarioWidgetService,
    private router: Router,
    private ChatMensajeHubService:ChatMensajeHubService,
    private ChatHubServiceService:ChatHubServiceService,
    private healthConnectService : HealthConnectService
  ) { }

  public ngOnInit(): void {
    this.ChatHubServiceService.iniciarConexion();
    console.log('Solicitando permisos:'+JSON.stringify(this.solicitarPermisos()));
    (async () => {
      console.log('Solicitando permisos...');
      await this.solicitarPermisos(); // Asegúrate de esperar a que se complete
      if (await this.validarPermisosHealthConnect()) {
        console.log('La aplicación cuenta con todos los permisos de HealthConnect');
      } else {
        console.log('La aplicación no cuenta con todos los permisos de HealthConnect');
        // Considera si necesitas volver a solicitar los permisos aquí o manejar la situación de otra manera
        this.validarDisponibilidad();
      }
    })();
  }

  async solicitarPermisos(){
    const res = await this.healthConnectService.requestPermisons();
    console.log('Permisos solicitados:'+JSON.stringify(res));
    return res;
  }

  async validarDisponibilidad(){
    console.log('Validando disponibilidad de HealthConnect');
    console.log(await this.healthConnectService.checkAvailability());
  }
  async openAppSetting(){
    await this.healthConnectService.openHealthConnectSetting();
  }

  async validarPermisosHealthConnect() : Promise<boolean> {
    const res : PermissionsStatus = await this.healthConnectService.checkHealthPermissions();
    this.hasAllPermissionsHealthConnect = res.hasAllPermissions;
    return this.hasAllPermissionsHealthConnect;
  }
}
