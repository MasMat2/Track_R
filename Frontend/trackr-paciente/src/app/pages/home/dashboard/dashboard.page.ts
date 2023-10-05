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
    WidgetSeguimientoComponent
  ],
  providers: [
    UsuarioWidgetService,
    WidgetService
  ]
})
export class DashboardPage implements OnInit {

  protected padecimientosUsuarioList : UsuarioPadecimientosDTO[];
  protected padecimientosList : PadecimientoDTO[];

  constructor(
    private widgetService : WidgetService
  ) { }

  public ngOnInit(): void {
  
  }

  public ionViewDidEnter() : void {
    this.widgetService.consultarPadecimientos().subscribe((data) => {
      this.padecimientosUsuarioList = data;
      this.padecimientosList = this.padecimientosUsuarioList[0].secciones;
    });

  }


}
