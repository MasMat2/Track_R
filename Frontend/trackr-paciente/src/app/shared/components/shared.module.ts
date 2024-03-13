import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
// import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

//componentes
import { GridGeneralModule } from './grid-general/grid-general.module';
// import { SpinnerComponent } from './spinner/spinner.component';

@NgModule({
  imports: [
    // MatProgressSpinnerModule
  ],
  declarations: [
    // SpinnerComponent
   ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    GridGeneralModule,
    NgSelectModule,
    // SpinnerComponent,
    // MatProgressSpinnerModule
  ],
  providers: []
})
export class SharedModule {
  constructor() {
  }
}
