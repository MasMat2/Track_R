import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { NotificacionesComponent } from './notificaciones/notificaciones.component';
import { SearchbarComponent } from './searchbar/searchbar.component';
import {MatToolbarModule} from '@angular/material/toolbar';
import { Bell, Circle, CircleStop, File, LucideAngularModule, Mic, Search, Send, Trash2 } from 'lucide-angular';
import { MatButtonModule } from '@angular/material/button';



@NgModule({
  declarations: [
    ToolbarComponent,
    NotificacionesComponent,
    SearchbarComponent
  ],
  imports: [
    CommonModule,
    MatToolbarModule,
    LucideAngularModule.pick({Search, Bell, Circle, File, Mic, Trash2, Send, CircleStop }),
    MatButtonModule

  ],
  exports: [ToolbarComponent]
})
export class ToolbarModule { }
