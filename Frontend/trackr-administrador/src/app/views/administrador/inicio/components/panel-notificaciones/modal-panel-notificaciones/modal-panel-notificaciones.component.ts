import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalBaseComponent } from '@sharedComponents/modal-base/modal-base.component';
import { ModalBaseModule } from '../../../../../../shared/components/modal-base/modal-base.module';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-modal-panel-notificaciones',
  standalone: true,
  imports: [CommonModule, ModalBaseModule,FormsModule],
  templateUrl: './modal-panel-notificaciones.component.html',
  styleUrls: ['./modal-panel-notificaciones.component.scss']
})
export class ModalPanelNotificacionesComponent {
  protected notificacion:any;

}
