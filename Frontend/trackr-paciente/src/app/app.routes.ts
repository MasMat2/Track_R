import { Routes } from '@angular/router';
import { HomePage } from './pages/home/home.page';
import { AccesoPage } from './pages/acceso/acceso.page';

export const routes: Routes = [
  {
    path: 'home',
    // component: HomePage,
    loadChildren: () => import('./pages/home/home-routes')
  },
  {
    path: 'acceso',
    // component: AccesoPage,
    loadChildren: () => import('./pages/acceso/acceso-routes')
  },
  {
    path: '**',
    redirectTo: 'acceso',
    pathMatch: 'full',
  },

];
