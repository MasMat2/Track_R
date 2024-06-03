import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RespuestasClasificacionPreguntaComponent } from './respuestas-clasificacion-pregunta.component';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { ACCESO_RESPUESTA_CLASPREGUNTA } from '@utils/codigos-acceso/examen.acceso';

const routes: Routes = [
  {
    path: '',
    component: RespuestasClasificacionPreguntaComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Consulta',
      acceso:ACCESO_RESPUESTA_CLASPREGUNTA.Consultar
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RespuestaClasificacionPreguntaRoutingModule { }
