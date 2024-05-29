import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClasificacionPreguntaComponent } from './clasificacion-pregunta.component';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { ACCESO_CLASIFICACION_PREGUNTA } from '@utils/codigos-acceso/examen.acceso';

const routes: Routes = [
  {
    path: '',
    component: ClasificacionPreguntaComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Consulta',
      acceso: ACCESO_CLASIFICACION_PREGUNTA.Consultar,
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClasificacionPreguntaRoutingModule { }
