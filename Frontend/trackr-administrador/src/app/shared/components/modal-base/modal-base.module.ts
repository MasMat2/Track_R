import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ModalBaseComponent } from './modal-base.component';

@NgModule({
    declarations: [ModalBaseComponent],
    imports: [CommonModule],
    exports: [ModalBaseComponent],
    providers: [],
})
export class ModalBaseModule {}
