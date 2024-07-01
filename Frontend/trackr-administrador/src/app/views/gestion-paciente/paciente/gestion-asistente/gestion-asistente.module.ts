import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GestionAsistenteComponent } from './gestion-asistente.component';
import { ModalBaseModule } from '@sharedComponents/modal-base/modal-base.module';
import { TableModule } from 'primeng/table';
import { PipesModule } from 'src/app/shared/pipes/pipes.module';
import { MatTabsModule } from '@angular/material/tabs';
import { MatChipsModule } from '@angular/material/chips';
import { Check, LucideAngularModule, Search, X } from 'lucide-angular';
import { LoadingSpinnerService } from '@services/loading-spinner.service';

@NgModule({
  imports: [
    CommonModule,
    ModalBaseModule,
    TableModule,
    PipesModule,
    MatTabsModule,
    MatChipsModule,
    LucideAngularModule.pick({ X , Search , Check})
  ],
  declarations: [GestionAsistenteComponent],
  providers: [LoadingSpinnerService],
})
export class GestionAsistenteModule { }
