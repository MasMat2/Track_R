import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NumberInputComponent } from './number-input.component';
import { DirectiveModule } from '../../directives/directive.module';
import { SharedModule } from '@sharedComponents/shared.module';

@NgModule({
  imports: [SharedModule, 
    CommonModule, 
    DirectiveModule],
  declarations: [NumberInputComponent],
  exports: [NumberInputComponent],
  providers: []
})
export class NumberInputModule {}
