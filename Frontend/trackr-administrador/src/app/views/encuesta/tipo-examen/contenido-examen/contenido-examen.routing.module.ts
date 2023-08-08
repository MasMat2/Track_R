import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { ContenidoExamenComponent } from './contenido-examen.component';
import { ACCESO_TIPO_EXAMEN } from '@utils/codigos-acceso/examen.acceso';

const routes: Routes = [
  {
    path: '',
    component: ContenidoExamenComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Contenido de Examen',
      acceso: ACCESO_TIPO_EXAMEN.Consultar
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ContenidoExamenRoutingModule {}
