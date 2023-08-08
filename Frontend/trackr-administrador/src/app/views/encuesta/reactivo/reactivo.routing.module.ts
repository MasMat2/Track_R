import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ACCESO_REACTIVO } from '@utils/codigos-acceso/examen.acceso';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { ReactivoComponent } from './reactivo.component';

const routes: Routes = [
  {
    path: '',
    component: ReactivoComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Reactivos',
      acceso: ACCESO_REACTIVO.Consultar
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReactivoRoutingModule {}
