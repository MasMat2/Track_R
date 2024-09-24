import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertasComponent } from './alertas.component';
import { TarjetaModule } from './tarjeta/tarjeta.module';
import { BadgeAlert, LucideAngularModule, MessageSquareMore, Video } from 'lucide-angular';

@NgModule({
  declarations: [AlertasComponent],
  imports: [
    CommonModule,
    TarjetaModule,
    LucideAngularModule.pick({BadgeAlert, MessageSquareMore, Video})
  ],
  exports: [AlertasComponent],
  providers: [],
})
export class AlertasModule {}
