import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule, AlertController } from '@ionic/angular';
import { Widget } from 'src/app/models/dashboard/widget';
import { UsuarioWidgetService } from 'src/app/services/dashboard/usuario-widget.service';
import { WidgetService } from 'src/app/services/dashboard/widget.service';
import { Observable, combineLatestWith, map, tap } from 'rxjs';
import { FormsModule } from '@angular/forms';
//import { HeaderComponent } from '../layout/header/header.component';
import { Router, RouterModule } from '@angular/router';
import { addIcons } from 'ionicons';
import { chevronBack } from 'ionicons/icons';
import { ModalController } from '@ionic/angular/standalone';
import { Constants } from '@utils/constants/constants';

interface WidgetSeleccionado extends Widget {
  seleccionado: boolean;
}

@Component({
  selector: 'app-configuracion-dashboard',
  templateUrl: './configuracion-dashboard.page.html',
  styleUrls: ['./configuracion-dashboard.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    FormsModule,
    RouterModule
  ],
  providers: [
    WidgetService,
    UsuarioWidgetService,
  ]
})
export class ConfiguracionDashboardPage  implements OnInit {

  constructor(
    private widgetService: WidgetService,
    private usuarioWidgetService: UsuarioWidgetService,
    private alertController: AlertController,
    private router: Router,
    private modalController: ModalController
  ) { addIcons({chevronBack})}

  protected widgets: WidgetSeleccionado[] = [];
  protected submiting = false;

  ngOnInit() {
    this.consultarWidgets();
  }

  private consultarWidgets(){
    const widgets$ = this.widgetService.consultarWidgetsSeguimientoUsuario();

    const usuarioWidgets$ = this.usuarioWidgetService.consultarPorUsuarioEnSesion();

    const widgetsSeleccionados$ = widgets$.pipe(
      combineLatestWith(usuarioWidgets$),
      map(([widgets, usuarioWidgets]) => {
        return widgets.map(widget => {
          const widgetSeleccionado: WidgetSeleccionado = {
            ...widget,
            seleccionado: usuarioWidgets.includes(widget.clave),
          };

          return widgetSeleccionado;
        });
      })
    );

    widgetsSeleccionados$.subscribe({
      next: widgets => {
        this.widgets = widgets;
      }
    });

  }

  
  protected onAceptar(): void {
    this.submiting = true;
    const seleccionados = this.widgets
      .filter(widget => widget.seleccionado)
      .map(widget => widget.clave);

      this.usuarioWidgetService.modificarPorUsuarioEnSesion(seleccionados).subscribe({
        next: () => {
          this.presentAlertSuccess();
          this.submiting = false;
        }
      });

  }


  protected async presentAlertSuccess() {

    const alertSuccess = await this.alertController.create({
      header: 'Widgets actualizados',
      subHeader: 'La información se ha actualizado correctamente.',
      message: Constants.ALERT_SUCCESS,
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm',
        handler: () => {
          this.cerrarModalConfirmar();
        }
      }],
      cssClass: 'custom-alert-success',
    });

    await alertSuccess.present();
  }

  protected cerrarModalCancelar(){
    return this.modalController.dismiss(null, 'cancel');
  }

  protected cerrarModalConfirmar(){
    return this.modalController.dismiss(null, 'confirm');
  }

  
}
