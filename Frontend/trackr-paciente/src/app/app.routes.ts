import { Routes } from '@angular/router';
import { AuthGuard } from './auth/auth-guard.service';

export const routes: Routes = [
  {
    path: 'home',
    canActivate: [AuthGuard],
    loadChildren: () => import('./pages/home/home-routes')
  },
  {
    path: 'acceso',
    loadChildren: () => import('./pages/acceso/acceso-routes')
  },
  {
    path: '**',
    redirectTo: 'acceso',
    pathMatch: 'full',
  },

];
