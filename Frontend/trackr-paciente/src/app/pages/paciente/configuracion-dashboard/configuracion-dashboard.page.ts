import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { Widget } from 'src/app/models/dashboard/widget';
import { UsuarioWidgetService } from 'src/app/services/dashboard/usuario-widget.service';
import { WidgetService } from 'src/app/services/dashboard/widget.service';
import { Observable, combineLatestWith, map, tap } from 'rxjs';
import { FormsModule } from '@angular/forms';

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
    FormsModule
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
  ) { }

  protected widgets: WidgetSeleccionado[] = [];

  ngOnInit(): void {
    
  }

  public ionViewWillEnter() {
    // TODO: 2023-04-26 -> Take 1

    const widgets$ = this.widgetService.consultar();
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

  public onChangeCheckbox(event: any) {
    console.log(event);
  }

  protected onAceptar(): void {
    const seleccionados = this.widgets
      .filter(widget => widget.seleccionado)
      .map(widget => widget.clave);
  }
}
