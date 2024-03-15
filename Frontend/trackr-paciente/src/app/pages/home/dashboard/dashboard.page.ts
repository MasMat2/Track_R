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
import { HealthConnectAvailabilityStatus, PermissionsStatus } from './interfaces/healthconnect-interfaces';

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
  private availability: HealthConnectAvailabilityStatus = "Unavailable"; //Disponibilidad de healthConnect

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
    this.validarDisponibilidad();

    //Valida si cuenta con permisos de HealthConnect, en caso de que no los tenga se hace la peticion. 
    if (this.availability === "Available") {
      (async () => {
        if (await this.validarPermisosHealthConnect()) {
          console.log('La aplicación cuenta con todos los permisos de HealthConnect');
        } else {
          console.log('La aplicación no cuenta con todos los permisos de HealthConnect');
          await this.solicitarPermisos();
          this.validarDisponibilidad();
        }
      })();
    } else {
      console.log('HealthConnect no está disponible o Android no es 14 o superior, por lo tanto, no se solicitarán permisos.');
    }
  }

  async solicitarPermisos(){
    const res = await this.healthConnectService.requestPermisons();
    console.log('Permisos solicitados:'+JSON.stringify(res));
    return res;
  }

  async validarDisponibilidad(){
    const res = await this.healthConnectService.checkAvailability();
    this.availability = res.availability;
  }

  async validarPermisosHealthConnect() : Promise<boolean> {
    const res : PermissionsStatus = await this.healthConnectService.checkHealthPermissions();
    this.hasAllPermissionsHealthConnect = res.hasAllPermissions;
    return this.hasAllPermissionsHealthConnect;
  }
}
