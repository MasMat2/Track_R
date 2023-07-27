import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalBaseModule } from '@sharedComponents/modal-base/modal-base.module';

import { PanelEventosComponent } from './panel-eventos.component';
import { SubEventosComponent } from './sub-eventos/sub-eventos.component';


@NgModule({
  declarations: [
    PanelEventosComponent,
    SubEventosComponent
  ],
  imports: [
    CommonModule,
    ModalBaseModule
  ],
exports: [
  PanelEventosComponent
],
providers: [],
  entryComponents: [
    SubEventosComponent
  ]
})
export class PanelEventosModule { }
