import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ACCESO_PROGRAMACION_EXAMEN } from '@utils/codigos-acceso/examen.acceso';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { ProgramacionExamenComponent } from './programacion-examen.component';

const routes: Routes = [
  {
    path: '',
    component: ProgramacionExamenComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Programación de Cuestionarios',
      acceso: ACCESO_PROGRAMACION_EXAMEN.Consultar,
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProgramacionExamenRoutingModule {}
