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
    WidgetPasosComponent,
    WidgetPesoComponent,
    WidgetSuenoComponent,
    WidgetFrecuenciaComponent,
    WidgetSeguimientoComponent,
    RouterModule,
    SeguimientoPadecimientoComponent,
    WidgetContainerComponent,
  ],
  providers: [
    UsuarioWidgetService,
    WidgetService
  ]
})
export class DashboardPage implements OnInit {

  protected selectedUserWidgets: WidgetType[] = [];
  protected padecimientosUsuarioList : UsuarioPadecimientosDTO[];
  protected padecimientosList : PadecimientoDTO[];

  constructor(
    private widgetService : WidgetService,
    private usuarioWidgetService: UsuarioWidgetService,
    private router: Router,
    private ChatMensajeHubService:ChatMensajeHubService,
    private ChatHubServiceService:ChatHubServiceService
  ) { }

  public ngOnInit(): void {
    this.ChatHubServiceService.iniciarConexion();
    this.ChatMensajeHubService.iniciarConexion();
  }

  public ionViewWillEnter() : void {
    this.consultarWidgetsSeguimiento();
    this.consultarWidgets();
  }

  public consultarWidgetsSeguimiento(){
    this.widgetService.consultarPadecimientos().subscribe((data) => {
      this.padecimientosUsuarioList = data;
      this.padecimientosList = this.padecimientosUsuarioList[0].secciones;
    });
  }

  public consultarWidgets(){
    this.usuarioWidgetService.consultarPorUsuarioEnSesion().subscribe(
      (data) => {
        this.selectedUserWidgets = data;
      }
    );
  }

  protected mostrarSeguimiento(idPadecimiento: any){
    this.router.navigate(['home/perfil/seguimiento', idPadecimiento]);
  }


}
