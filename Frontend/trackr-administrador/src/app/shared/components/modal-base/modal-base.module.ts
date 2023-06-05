import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalBaseComponent } from './modal-base.component';
import { SharedModule } from '../../shared.module';



@NgModule({
  declarations: [ModalBaseComponent],
  imports: [SharedModule],
  exports: [ModalBaseComponent],
 
})
export class ModalBaseModule { }
