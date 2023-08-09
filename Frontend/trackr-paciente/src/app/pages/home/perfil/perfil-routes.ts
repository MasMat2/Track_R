import { Routes, RouterModule, Route } from '@angular/router';
import { PerfilPage } from './perfil.page';
import { MisDoctoresPage } from './mis-doctores/mis-doctores.page';

export default [
  {
    path: '',
    component: PerfilPage,
    children: [
      {
        path: 'mis-doctores',
        loadComponent: () => import('./mis-doctores/mis-doctores.page').then((m) => m.MisDoctoresPage)
      },
      {
        path: 'mis-doctores/agregar',
        loadComponent: () => import('./mis-doctores/doctores-formulario/doctores-formulario.page').then((m) => m.DoctoresFormularioPage)
     
      }
    ]
  },
]  as Route[];


