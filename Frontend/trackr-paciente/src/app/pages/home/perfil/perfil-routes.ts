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
      },
      {
        path: 'informacion-general',
        loadComponent: () => import('./informacion-general/informacion-general.component').then((m) => m.InformacionGeneralComponent)
      },
      {
        path: 'mis-tratamientos',
        loadComponent: () => import('./mis-tratamientos/mis-tratamientos.component').then((m) => m.MisTratamientosComponent)
      },
      { path: '**', redirectTo: 'informacion-general', pathMatch: 'full' }
    ]
  },
]  as Route[];


