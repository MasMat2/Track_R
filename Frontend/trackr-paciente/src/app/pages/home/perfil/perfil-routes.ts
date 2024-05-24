import { Routes, RouterModule, Route } from '@angular/router';
import { PerfilPage } from './perfil.page';
import { MisDoctoresPage } from './mis-doctores/mis-doctores.page';
import { ExitGuard } from 'src/app/shared/guards/exit.guard';
export default [
  {
    path: '',
    component: PerfilPage,
    children: [
      {
        path: 'inicio-perfil',
        loadComponent: () => import('./inicio-perfil/inicio-perfil.component').then((m) => m.InicioPerfilComponent)
      },
      {
        path: 'mis-doctores',
        data: { breadcrumb: "Mis doctores"},
        loadComponent: () => import('./mis-doctores/mis-doctores.page').then((m) => m.MisDoctoresPage)
      },
      {
        path: 'informacion-general',
        canDeactivate : [ExitGuard],
        data: { breadcrumb: "Informacion General"},
        loadComponent: () => import('./informacion-general/components/info-general/informacion-general.component').then((m) => m.InformacionGeneralComponent)
      },
            {
        path: 'informacion-general/domicilio',
        canDeactivate : [ExitGuard],
        loadComponent: () => import('./informacion-general/components/info-domicilio/info-domicilio.component').then((m)=> m.InfoDomicilioComponent)
      },
      {
        path: 'informacion-general/antecedentes',
        loadComponent: () => import('./informacion-general/components/info-antecedentes/info-antecedentes.component').then((m)=> m.InfoAntecedentesComponent)
      },
      {
        path: 'informacion-general/diagnosticos',
        loadComponent: () => import('./informacion-general/components/info-diagnosticos/info-diagnosticos.component').then((m)=> m.InfoDiagnosticosComponent)
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
      { path: '**', redirectTo: 'inicio-perfil', pathMatch: 'full' }
    ]
  },
]  as Route[];


