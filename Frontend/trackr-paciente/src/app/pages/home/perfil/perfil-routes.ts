import { Routes, RouterModule, Route } from '@angular/router';
import { PerfilPage } from './perfil.page';
import { MisDoctoresPage } from './mis-doctores/mis-doctores.page';
import { MisEstudiosFormularioComponent } from './mis-estudios/mis-estudios-formulario/mis-estudios-formulario.component';

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
        loadComponent: () => import('./mis-tratamientos/mis-tratamientos.page').then((m) => m.MisTratamientosPage)
      },
      {
        path: 'mis-tratamientos/agregar',
        loadComponent: () => import('./mis-tratamientos/agregar-tratamiento/agregar-tratamiento.page').then((m) => m.AgregarTratamientoPage)
      },
      {
        path: 'mis-estudios',
        loadComponent: () => import('./mis-estudios/mis-estudios.component').then((m) => m.MisEstudiosComponent)
      },
      {
        path: 'mis-estudios/agregar',
        loadComponent: () => import('./mis-estudios/mis-estudios-formulario/mis-estudios-formulario.component').then((m) => m.MisEstudiosFormularioComponent)
      },
      { path: '**', redirectTo: 'informacion-general', pathMatch: 'full' }
    ]
  },
]  as Route[];


