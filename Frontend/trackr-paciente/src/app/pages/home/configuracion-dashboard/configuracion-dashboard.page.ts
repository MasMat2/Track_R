import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule, AlertController } from '@ionic/angular';
import { Widget } from 'src/app/models/dashboard/widget';
import { UsuarioWidgetService } from 'src/app/services/dashboard/usuario-widget.service';
import { WidgetService } from 'src/app/services/dashboard/widget.service';
import { Observable, combineLatestWith, map, tap } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { HeaderComponent } from '../layout/header/header.component';
import { Router } from '@angular/router';

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
    HeaderComponent
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
    private router: Router
  ) { }

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
          this.presentAlert();
          this.submiting = false;
        }
      });

  }

  private async presentAlert() {
    const alert = await this.alertController.create({
      header: 'Widgets actualizados',
      message: 'La información se actualizó correctamente',
      buttons: [{
        text: 'Ok',
        handler: () => {
          this.router.navigate(['/home/dashboard']);
        }
      }]
    });

    await alert.present();
  }
}
