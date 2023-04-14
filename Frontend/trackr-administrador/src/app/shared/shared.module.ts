import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [],
  declarations: [
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule
  ],
  providers: []
})
export class SharedModule {
  constructor() {
  }
}
