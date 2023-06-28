import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ACCESO_MI_EXAMEN } from '@utils/codigos-acceso/examen.acceso';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { ExamenFormularioComponent } from './examen-formulario.component';

const routes: Routes = [
  {
    path: '',
    component: ExamenFormularioComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Presentar Examen',
      acceso: ACCESO_MI_EXAMEN.Presentar,
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExamenFormularioRoutingModule {}
