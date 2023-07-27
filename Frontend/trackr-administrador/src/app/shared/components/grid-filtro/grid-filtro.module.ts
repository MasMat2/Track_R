import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

import { GridGeneralModule } from '../grid-general/grid-general.module';
import { GridFiltroComponent } from './grid-filtro.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { MatTabsModule } from '@angular/material/tabs';

@NgModule({
  imports: [
    CommonModule,
    NgSelectModule,
    FormsModule,
    GridGeneralModule,
    MatTabsModule
  ],
  declarations: [
    GridFiltroComponent
  ],
  exports: [
    GridFiltroComponent
  ],
  providers: [
  ]
})
export class GridFiltroModule {}
