import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalModule, BsModalService } from 'ngx-bootstrap/modal';
import { ModalExitoComponent } from './mensaje-exito.component';
import { ModalConfirmacionComponent } from './mensaje-confirmacion.component';
import { ModalErrorComponent } from './mensaje-error.component';
import { MensajeService } from './mensaje.service';
import { MensajeComponent } from './mensaje';

@NgModule({
  imports: [CommonModule, ModalModule.forRoot()],
  declarations: [ModalExitoComponent, ModalConfirmacionComponent, ModalErrorComponent, MensajeComponent],
  entryComponents: [ModalExitoComponent, ModalConfirmacionComponent, ModalErrorComponent, MensajeComponent],
  providers: [MensajeService, BsModalService]
})
export class MensajeModule {}
