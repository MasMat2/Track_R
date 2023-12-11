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
        data: { breadcrumb: "Mis doctores"},
        loadComponent: () => import('./mis-doctores/mis-doctores.page').then((m) => m.MisDoctoresPage)
      },
      {
        path: 'mis-doctores/agregar',
        data: { breadcrumb: "Agregar"},
        loadComponent: () => import('./mis-doctores/doctores-formulario/doctores-formulario.page').then((m) => m.DoctoresFormularioPage)
      },
      {
        path: 'informacion-general',
        data: { breadcrumb: "Informacion General"},
        loadComponent: () => import('./informacion-general/informacion-general.component').then((m) => m.InformacionGeneralComponent)
      },
      {
        path: 'mis-tratamientos',
        data: { breadcrumb: "Mis tratamientos"},
        loadComponent: () => import('./mis-tratamientos/mis-tratamientos.page').then((m) => m.MisTratamientosPage)
      },
      {
        path: 'mis-tratamientos/agregar',
        loadComponent: () => import('./mis-tratamientos/agregar-tratamiento/agregar-tratamiento.page').then((m) => m.AgregarTratamientoPage)
      },
      {
        path: 'mis-estudios',
        data: { breadcrumb: "Mis estudios"},
        loadComponent: () => import('./mis-estudios/mis-estudios.component').then((m) => m.MisEstudiosPage)
      },
      {
        path: 'mis-estudios/agregar',
        loadComponent: () => import('./mis-estudios/mis-estudios-formulario/mis-estudios-formulario.component').then((m) => m.MisEstudiosFormularioPage)
      },
      {
        path: 'seguimiento/:id',
        loadComponent: () => import('../dashboard/components/seguimiento-padecimiento/seguimiento-padecimiento.component').then((m) => m.SeguimientoPadecimientoComponent)
      },
      { path: '**', redirectTo: 'informacion-general', pathMatch: 'full' }
    ]
  },
]  as Route[];


