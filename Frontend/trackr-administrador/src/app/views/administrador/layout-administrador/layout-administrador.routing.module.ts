import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InicioComponent } from '../inicio/inicio.component';
import { ChatComponent } from '../../chat/chat.component';
import { JitsiMeetModule } from '../../jitsi-meet/jitsi-meet.module';
import { AnswerMeetComponent } from '../../jitsi-meet/answer-meet/answer-meet.component';

const routes: Routes = [
  {
    path: '',
    component: InicioComponent,
  },
  {
    path: 'configuracion-general',
    loadChildren: () =>
      import('src/app/views/configuracion-general/configuracion-general.module')
      .then((m) => m.ConfiguracionGeneralModule),
  },
  {
    path: 'gestion-paciente',
    loadChildren: () =>
      import('src/app/views/gestion-paciente/gestion-paciente.module')
      .then((m) => m.GestionPacienteModule),
  },
  {
    path: 'examen',
    loadChildren: () =>
      import('src/app/views/encuesta/encuesta.module')
      .then((m) => m.EncuestaModule),
  },
  {
    path: 'chat',
    component: ChatComponent
  },
  {
    path: 'chat/:id',
    component: ChatComponent
  },
  {
    path: 'jitsi-meet/:meet-name',
    //component: AnswerMeetComponent
    loadChildren: () =>
      import('src/app/views/jitsi-meet/jitsi-meet.module')
      .then((m) => m.JitsiMeetModule),
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LayoutAdminitradorRoutingModule {}
