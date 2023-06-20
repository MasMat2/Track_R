import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: '',
    pathMatch: 'full',
  },
  {
    path: '',
    data: {
      title: 'Examen',
    },
    children: [
      {
        path: 'asignatura',
        loadChildren: () =>
          import('./asignatura/asignatura.module').then((m) => m.AsignaturaModule)
      },
      {
        path: 'tipo-examen',
        loadChildren: () => import('./tipo-examen/tipo-examen.module').then((m) => m.TipoExamenModule)
      },
      {
        path: 'reactivo',
        loadChildren: () => import('./reactivo/reactivo.module').then((m) => m.ReactivoModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExamenRoutingModule {}
