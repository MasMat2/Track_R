import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertasComponent } from './alertas.component';
import { TarjetaModule } from './tarjeta/tarjeta.module';

@NgModule({
  declarations: [AlertasComponent],
  imports: [
    CommonModule,
    TarjetaModule
  ],
  exports: [AlertasComponent],
  providers: [],
})
export class AlertasModule {}
