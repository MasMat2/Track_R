import { NgSelectModule } from '@ng-select/ng-select';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccesoService } from '@http/seguridad/acceso.service';
import { AgGridModule } from 'ag-grid-angular';
import { GridActionButtonComponent } from './grid-action-button/grid-action-button.component';
import { GridCheckboxComponent } from './grid-checkbox/grid-checkbox.component';
import { GridGeneralComponent } from './grid-general.component';
import { GridGeneralService } from './grid-general.service';
import { CustomLoadingCellRenderer } from './loading-cell-renderer/loading-cell-renderer.component';

@NgModule({
  imports: [
    CommonModule,
    NgSelectModule,
    FormsModule,
    AgGridModule,
    GridActionButtonComponent,
    GridCheckboxComponent
  ],
  declarations: [
    GridGeneralComponent,
    CustomLoadingCellRenderer
  ],
  exports: [
    GridGeneralComponent,
    GridActionButtonComponent,
    GridCheckboxComponent
  ],
  providers: [GridGeneralService, AccesoService],
})
export class GridGeneralModule {}
