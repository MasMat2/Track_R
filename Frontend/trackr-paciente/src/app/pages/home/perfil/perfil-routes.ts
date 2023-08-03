import { Routes, RouterModule, Route } from '@angular/router';
import { PerfilPage } from './perfil.page';
import { MisDoctoresComponent } from './mis-doctores/mis-doctores.component';

export default [
  {
    path: '',
    component: PerfilPage,
    children: [
      { path: 'mis-doctores', loadChildren: () => import('./mis-doctores/mis-doctores-routes') }
    ]
  },
] as Route[];


