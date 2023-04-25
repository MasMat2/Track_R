import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { take } from 'rxjs';
import { UsuarioWidgetService } from 'src/app/services/dashboard/usuario-widget.service';
import { WidgetContainerComponent } from './components/widget-container/widget-container.component';
import { WidgetType } from './interfaces/widgets';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    WidgetContainerComponent,
  ],
  providers: [
    UsuarioWidgetService,
  ]
})
export class DashboardPage implements OnInit {

  constructor(
    private usuarioWidgetService: UsuarioWidgetService,
  ) { }

  protected widgets: WidgetType[] = [];

  public ngOnInit(): void {
    this.usuarioWidgetService
      .consultarPorUsuarioEnSesion()
      .pipe(take(1))
      .subscribe(widgets => {
        this.widgets = widgets;
      });
  }

}
