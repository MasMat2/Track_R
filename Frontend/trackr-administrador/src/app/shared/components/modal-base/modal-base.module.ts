import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ModalBaseComponent } from './modal-base.component';
import { LucideAngularModule, X } from 'lucide-angular';

@NgModule({
    declarations: [ModalBaseComponent],
    imports: [CommonModule, LucideAngularModule.pick({X})],
    exports: [ModalBaseComponent],
    providers: [],
})
export class ModalBaseModule {}
