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
      },
      {
        path: 'nivel-examen',
        loadChildren: () => import('./nivel-examen/nivel-examen.module').then((m) => m.NivelExamenModule)
      },
      {
        path: 'examen',
        loadChildren: () => import('./mi-examen/examen.module').then((m) => m.ExamenModule)
      },
      {
        path: 'programacion-examen',
        loadChildren: () => import('./programacion-examen/programacion-examen.module').then((m) => m.ProgramacionExamenModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExamenRoutingModule {}
