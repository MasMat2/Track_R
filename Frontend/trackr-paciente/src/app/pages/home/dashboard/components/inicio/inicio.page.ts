import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { WidgetSeguimientoComponent } from '../widgets/widget-seguimiento/widget-seguimiento.component';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { WidgetContainerComponent } from '../widgets/widget-container/widget-container.component';
import { UsuarioWidgetService } from 'src/app/services/dashboard/usuario-widget.service';
import { WidgetService } from 'src/app/services/dashboard/widget.service';
import { WidgetType } from '../../interfaces/widgets';
import { UsuarioPadecimientosDTO } from 'src/app/shared/Dtos/gestion-expediente/usuario-padecimientos-dto';
import { PadecimientoDTO } from 'src/app/shared/Dtos/gestion-expediente/padecimiento-dto';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { SeguimientoPadecimientoComponent } from '../seguimiento-padecimiento/seguimiento-padecimiento.component';
import { NotificacionPacienteService } from '@http/gestion-perfil/notificacion-paciente.service';
import { Observable, lastValueFrom } from 'rxjs';
import { addIcons } from 'ionicons';
import { ModalController } from '@ionic/angular/standalone';
import { ConfiguracionDashboardPage } from '../configuracion-dashboard/configuracion-dashboard.page';
import { InformacionPerfilDto } from 'src/app/shared/Dtos/perfil/informacion-perfil-dto';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { NotificacionesPageComponent } from '../notificaciones/notificacionesPage/notificaciones-page.component';
import { TabService } from 'src/app/services/dashboard/tab.service';

@Component({
    selector: 'app-inicio',
    templateUrl: './inicio.page.html',
    styleUrls: ['./inicio.page.scss'],
    standalone: true,
    providers: [
        UsuarioWidgetService,
        WidgetService
    ],
    imports: [
        IonicModule,
        CommonModule,
        FormsModule,
        WidgetSeguimientoComponent,
        HeaderComponent,
        WidgetContainerComponent,
        RouterModule,
        SeguimientoPadecimientoComponent,
        NotificacionesPageComponent
    ]
})
export class InicioPage implements OnInit {

  protected selectedUserWidgets: WidgetType[] = [];
  protected listaWidgetsSaludVacia: boolean = false;

  protected padecimientosUsuarioList : UsuarioPadecimientosDTO[];
  protected padecimientosList : PadecimientoDTO[];
  //private previousUrl: string;

  protected informacionHeader$: Observable<InformacionPerfilDto>;
  protected infoHeader: InformacionPerfilDto;
  protected fotoPerfilUrl: string = "assets/img/svg/Image_placeholder.svg";

  constructor(
    private widgetService : WidgetService,
    private usuarioWidgetService: UsuarioWidgetService,
    private router: Router,
    private notificacionPacienteService : NotificacionPacienteService,
    private modalController: ModalController,
    private usuarioService: UsuarioService,
    private tabService : TabService,
  ) { 
    this.notificacionPacienteService.actualizarWidgets$.subscribe(() => {
      this.consultarInfoWidgetsSeguimiento();
    });

    this.tabService.tabChange$.subscribe((tabId) => {
      if (tabId === 'dashboard') {
        this.consultarInfoWidgetsSeguimiento();
        this.consultarWidgets();
      }
    });

    //this.navegarConfigADashboardChanges();
    
    addIcons({
      'settings': 'assets/img/svg/settings.svg'
    })
  }

  ngOnInit() {
  }


  public ionViewWillEnter(){
    this.obtenerDatosHeader();
    this.consultarWidgets();
    this.consultarInfoWidgetsSeguimiento();
  }

  private obtenerDatosHeader(){
    this.informacionHeader$ = this.usuarioService.consultarInformacionPerfil();

    this.informacionHeader$.subscribe({
      next: (info) => {
        this.infoHeader = info;
        this.infoHeader.nombre = info.nombre.split(" ")[0]; //solo el primer nombre
        if(this.infoHeader.imagenBase64 != null){
          this.fotoPerfilUrl = `data:${info.imagenBase64?.archivoMime};base64,` + info.imagenBase64?.archivo;
        }
      },
    });
  }

  protected async abrirConfiguracionDashboard(){
    const modal = await this.modalController.create({
      component: ConfiguracionDashboardPage,
    });

    modal.present();

    const {data, role} = await modal.onWillDismiss();
    if(role === 'confirm'){
      this.consultarInfoWidgetsSeguimiento();
      this.consultarWidgets();
    }

  }

  public async consultarInfoWidgetsSeguimiento() {
    this.padecimientosUsuarioList = await lastValueFrom(this.widgetService.consultarPadecimientos());
    this.padecimientosList = this.padecimientosUsuarioList[0]?.secciones;
  }

  public async consultarWidgets(){
    //this.selectedUserWidgets = await lastValueFrom(this.usuarioWidgetService.consultarPorUsuarioEnSesion());

    this.usuarioWidgetService.consultarPorUsuarioEnSesion().subscribe({
      next: (widgets) => {
        this.selectedUserWidgets = widgets;
        this.listaWidgetsSaludVacia = this.esListaWidgetsSaludVacia(widgets);
      }
    })
  }

  //TODO: cambiar a algo menos hardcodeado usando la bd y el api
  private esListaWidgetsSaludVacia(widgets: string[]){
    //los widgets de salud empiezan con "w" (w-pas, w-fre, etc)
    const widgetsSalud = widgets.filter(w => w.startsWith('w'));
    return widgetsSalud.length == 0;
  }

  protected mostrarSeguimiento(idPadecimiento: any){
    this.router.navigate(['home/dashboard/seguimiento', idPadecimiento]);
  }

}
