import { Routes, RouterModule, Route } from '@angular/router';
import { DashboardPage } from './dashboard.page';
import { InicioPage } from './components/inicio/inicio.page';
import { SeguimientoPadecimientoComponent } from './components/seguimiento-padecimiento/seguimiento-padecimiento.component';

export default [
  {
    path: '',
    component: DashboardPage,
    children: [
      {
        path: 'inicio',
        component: InicioPage
      },
      {
        path: 'seguimiento/:id',
        data: { breadcrumb: "Seguimiento"},
        component: SeguimientoPadecimientoComponent
      },
      { path: '**', redirectTo: 'inicio', pathMatch: 'full' },
    ]
  },
]  as Route[];
