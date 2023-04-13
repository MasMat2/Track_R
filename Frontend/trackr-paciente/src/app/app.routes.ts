import { Routes } from '@angular/router';
import { LayoutPacientePage } from './pages/paciente/layout-paciente/layout-paciente.page';

export const routes: Routes = [
  {
    path: 'paciente',
    component: LayoutPacientePage,
    loadChildren: () => import('./pages/paciente/paciente-routes')
  },
  {
    path: 'home-slide',
    loadComponent: () => import('./pages/home-slide/home-slide.page').then( m => m.HomeSlidePage)
  },
  {
    path: 'login',
    loadComponent: () => import('./pages/login/login.page').then( m => m.LoginPage)
  },
  {
    path: '**',
    redirectTo: 'home-slide',
    pathMatch: 'full',
  },

];
