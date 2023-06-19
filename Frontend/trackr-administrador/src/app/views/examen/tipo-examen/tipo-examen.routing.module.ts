import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ACCESO_TIPO_EXAMEN } from '@utils/codigos-acceso/examen.acceso';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { TipoExamenComponent } from './tipo-examen.component';

const routes: Routes = [
  {
    path: '',
    component: TipoExamenComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Contenido de Examen',
      acceso: ACCESO_TIPO_EXAMEN.Consultar,
    },
  },
  // {
  //   path: 'contenido-examen',
  //   loadChildren: () => import('./contenido-examen/contenido-examen.module').then(m => m.ContenidoExamenModule),
  // }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TipoExamenRoutingModule {}
