import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { UrlImagenPipe } from './url-imagen.pipe';
import { JoinPipe } from './join.pipe';

@NgModule({
  declarations: [
    UrlImagenPipe,
    JoinPipe,
  ],
  imports: [
    CommonModule
  ],
  exports: [
    UrlImagenPipe,
    JoinPipe,
  ],
  providers: [],
})
export class PipesModule {}
