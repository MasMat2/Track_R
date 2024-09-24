import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PanelNotificacionesComponent } from './panel-notificaciones.component';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { ToolbarModule } from '@sharedComponents/toolbar/toolbar.module';
import { LucideAngularModule } from 'lucide-angular';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [PanelNotificacionesComponent],
  imports: [
    CommonModule,
    FormsModule,
    NgSelectModule,
    ToolbarModule,
    MatButtonModule,
    LucideAngularModule
  ],
  exports: [PanelNotificacionesComponent],
  providers: [],
  
})
export class PanelNotificacionesModule {}
