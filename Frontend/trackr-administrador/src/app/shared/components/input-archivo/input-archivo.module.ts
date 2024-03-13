import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputArchivoComponent } from './input-archivo.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    InputArchivoComponent
  ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports:[
    InputArchivoComponent
  ]
})
export class InputArchivoModule { }
