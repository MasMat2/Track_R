import { NgModule } from '@angular/core';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { CommonModule } from '@angular/common';
import { NumberInputComponent } from './number-input.component';
import { SharedModule } from '../../shared.module';
import { DirectiveModule } from '../../directives/directive.module';

@NgModule({
  imports: [SharedModule, CommonModule, PopoverModule, DirectiveModule],
  declarations: [NumberInputComponent],
  exports: [NumberInputComponent],
  providers: []
})
export class NumberInputModule {}
