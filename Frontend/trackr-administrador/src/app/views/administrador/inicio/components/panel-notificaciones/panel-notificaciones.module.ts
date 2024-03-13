import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PanelNotificacionesComponent } from './panel-notificaciones.component';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';

@NgModule({
  declarations: [PanelNotificacionesComponent],
  imports: [
    CommonModule,
    FormsModule,
    NgSelectModule
  ],
  exports: [PanelNotificacionesComponent],
  providers: [],
})
export class PanelNotificacionesModule {}
