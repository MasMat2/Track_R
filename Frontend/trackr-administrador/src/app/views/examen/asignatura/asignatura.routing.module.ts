import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ACCESO_ASIGNATURA } from '@utils/codigos-acceso/examen.acceso';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { AsignaturaComponent } from './asignatura.component';

const routes: Routes = [
  {
    path: '',
    component: AsignaturaComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Asignaturas',
      acceso: ACCESO_ASIGNATURA.Consultar
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AsignaturaRoutingModule {}
