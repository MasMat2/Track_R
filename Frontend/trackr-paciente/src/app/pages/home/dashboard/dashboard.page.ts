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
import { WidgetDiabetesComponent } from './components/widget-diabetes/widget-diabetes.component';


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
    WidgetDiabetesComponent
  ],
  providers: [
    UsuarioWidgetService,
  ]
})
export class DashboardPage implements OnInit {

  constructor() { }

  public ngOnInit(): void {
  }

}
