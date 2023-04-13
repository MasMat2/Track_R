import { Routes } from '@angular/router';
import { LayoutPacientePage } from './pages/paciente/layout-paciente/layout-paciente.page';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'paciente',
    pathMatch: 'full',
  },
  {
    path: 'paciente',
    component: LayoutPacientePage,
    loadChildren: () => import('./pages/paciente/paciente-routes')
  }
];
