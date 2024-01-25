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
import { Router, RouterModule } from '@angular/router';
import { SeguimientoPadecimientoComponent } from '../seguimiento-padecimiento/seguimiento-padecimiento.component';
import { WidgetFrecuenciaComponent } from '../widget-frecuencia/widget-frecuencia.component';
import { WidgetPasosComponent } from '../widget-pasos/widget-pasos.component';
import { WidgetPesoComponent } from '../widget-peso/widget-peso.component';
import { WidgetSuenoComponent } from '../widget-sueno/widget-sueno.component';
import { NotificacionPacienteService } from '@http/gestion-perfil/notificacion-paciente.service';

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

  constructor(
    private widgetService : WidgetService,
    private usuarioWidgetService: UsuarioWidgetService,
    private router: Router,
    private notificacionPacienteService : NotificacionPacienteService
  ) { 
    this.notificacionPacienteService.actualizarWidgets$.subscribe(() => {
      this.consultarInfoWidgetsSeguimiento();
    })
  }

  ngOnInit() {

  }

  public ionViewWillEnter(){
    //this.consultarInfoWidgetsSeguimiento();
    this.consultarWidgets();
  }

  public consultarInfoWidgetsSeguimiento(){
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
    this.router.navigate(['home/dashboard/seguimiento', idPadecimiento]);
  }


}
