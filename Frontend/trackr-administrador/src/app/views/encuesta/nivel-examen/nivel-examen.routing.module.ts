import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ACCESO_NIVEL_EXAMEN } from '@utils/codigos-acceso/examen.acceso';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { NivelExamenComponent } from './nivel-examen.component';

const routes: Routes = [
  {
    path: '',
    component: NivelExamenComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Nivel de Examen',
      acceso: ACCESO_NIVEL_EXAMEN.Consultar
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NivelExamenRoutingModule {}
