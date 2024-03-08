import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { WidgetSeguimientoComponent } from '../widget-seguimiento/widget-seguimiento.component';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { WidgetContainerComponent } from '../widget-container/widget-container.component';
import { UsuarioWidgetService } from 'src/app/services/dashboard/usuario-widget.service';
import { WidgetService } from 'src/app/services/dashboard/widget.service';
import { WidgetType } from '../../interfaces/widgets';
import { UsuarioPadecimientosDTO } from 'src/app/shared/Dtos/gestion-expediente/usuario-padecimientos-dto';
import { PadecimientoDTO } from 'src/app/shared/Dtos/gestion-expediente/padecimiento-dto';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { SeguimientoPadecimientoComponent } from '../seguimiento-padecimiento/seguimiento-padecimiento.component';
import { WidgetFrecuenciaComponent } from '../widget-frecuencia/widget-frecuencia.component';
import { WidgetPasosComponent } from '../widget-pasos/widget-pasos.component';
import { WidgetPesoComponent } from '../widget-peso/widget-peso.component';
import { WidgetSuenoComponent } from '../widget-sueno/widget-sueno.component';
import { NotificacionPacienteService } from '@http/gestion-perfil/notificacion-paciente.service';
import { filter, lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.page.html',
  styleUrls: ['./inicio.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    WidgetSeguimientoComponent,
    HeaderComponent,
    WidgetContainerComponent,
    RouterModule,
    WidgetPasosComponent,
    WidgetPesoComponent,
    WidgetSuenoComponent,
    WidgetFrecuenciaComponent,
    SeguimientoPadecimientoComponent,
  ],
  providers: [
    UsuarioWidgetService,
    WidgetService
  ]
})
export class InicioPage implements OnInit {

  protected selectedUserWidgets: WidgetType[] = [];
  protected padecimientosUsuarioList : UsuarioPadecimientosDTO[];
  protected padecimientosList : PadecimientoDTO[];
  private previousUrl: string;

  constructor(
    private widgetService : WidgetService,
    private usuarioWidgetService: UsuarioWidgetService,
    private router: Router,
    private notificacionPacienteService : NotificacionPacienteService
  ) { 
    this.notificacionPacienteService.actualizarWidgets$.subscribe(() => {
      this.consultarInfoWidgetsSeguimiento();
    });

    this.navegarConfigADashboardChanges();
 
  }

  ngOnInit() {
  }


  public ionViewWillEnter(){
    this.consultarWidgets();
    this.consultarInfoWidgetsSeguimiento();
  }

  private navegarConfigADashboardChanges(){

    this.router.events
    .pipe(filter((event): event is NavigationEnd => event instanceof NavigationEnd))
    .subscribe(async (event: NavigationEnd) =>
     {
      const currentUrl = event.urlAfterRedirects;
      if (currentUrl === '/home/dashboard/inicio') 
      {
        await this.consultarInfoWidgetsSeguimiento();

        await this.consultarWidgets();

      }
      this.previousUrl = currentUrl;
    });

  }

  public async consultarInfoWidgetsSeguimiento() {
    this.padecimientosUsuarioList = await lastValueFrom(this.widgetService.consultarPadecimientos());
    this.padecimientosList = this.padecimientosUsuarioList[0]?.secciones;
  }

  public async consultarWidgets(){
    this.selectedUserWidgets = await lastValueFrom(this.usuarioWidgetService.consultarPorUsuarioEnSesion());
  }

  protected mostrarSeguimiento(idPadecimiento: any){
    this.router.navigate(['home/dashboard/seguimiento', idPadecimiento]);
  }

  protected mostrarConfiguracionDashboard(){
    this.router.navigate(['/home/config-dashboard']);
  }


}
