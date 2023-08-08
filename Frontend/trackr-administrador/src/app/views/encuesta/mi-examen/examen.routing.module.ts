import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ACCESO_MI_EXAMEN } from '@utils/codigos-acceso/examen.acceso';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { ExamenComponent } from './examen.component';

const routes: Routes = [
  {
    path: '',
    component: ExamenComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Exámenes',
      acceso: ACCESO_MI_EXAMEN.Consultar,
    }
  },
  {
    path: 'presentar',
    loadChildren: () => import('./examen-formulario/examen-formulario.module').then(m => m.ExamenFormularioModule),
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExamenRoutingModule {}
