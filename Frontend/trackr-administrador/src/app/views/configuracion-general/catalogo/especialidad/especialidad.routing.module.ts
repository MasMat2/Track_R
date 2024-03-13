import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { EspecialidadComponent } from './especialidad.component';
import { CodigoAcceso } from '@utils/codigo-acceso';

const routes: Routes = [
  {
    path: '',
    component: EspecialidadComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Consulta',
      acceso: CodigoAcceso.CONSULTAR_ESPECIALIDAD
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EspecialidadRoutingModule {}
