import { Routes, RouterModule, Route } from '@angular/router';
import { DashboardPage } from './dashboard.page';
export default [
  {
    path: '',
    component: DashboardPage,
    children: [
      {
        path: 'seguimiento/:id',
        loadComponent: () => import('./components/seguimiento-padecimiento/seguimiento-padecimiento.component').then((m) => m.SeguimientoPadecimientoComponent)
      },
      { path: '**', redirectTo: '', pathMatch: 'full' }
    ]
  }
]  as Route[];
