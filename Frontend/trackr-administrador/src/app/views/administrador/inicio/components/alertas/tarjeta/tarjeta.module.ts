import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TarjetaComponent } from './tarjeta.component';
import { LucideAngularModule } from 'lucide-angular';

@NgModule({
  declarations: [TarjetaComponent],
  imports: [CommonModule,
    LucideAngularModule
  ],
  exports: [TarjetaComponent],
  providers: [],
})
export class TarjetaModule {}
