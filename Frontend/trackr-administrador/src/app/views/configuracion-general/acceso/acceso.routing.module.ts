import { AccesoFormularioComponent } from './acceso-formulario/acceso-formulario.component';
import { AccesoComponent } from './acceso.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { CodigoAcceso } from 'src/app/shared/utils/codigo-acceso';
import { AccesoAyudaComponent } from './acceso-ayuda/acceso-ayuda.component';
import { ReporteArbolAccesoComponent } from './reporte-arbol-acceso/reporte-arbol-acceso.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'consultar',
    pathMatch: 'full'
  },
  {
    path: '',
    data: {
      title: 'Accesos'
    },
    children: [
      {
        path: '',
        component: AccesoComponent,
        canActivate: [AdministradorAuthGuard],
        data: {
          acceso: CodigoAcceso.CONSULTAR_ACCESO
        }
      },
      {
        path: 'agregar',
        component: AccesoFormularioComponent,
        canActivate: [AdministradorAuthGuard],
        data: {
          acceso: CodigoAcceso.AGREGAR_ACCESO
        }
      },
      {
        path: 'editar',
        component: AccesoFormularioComponent,
        canActivate: [AdministradorAuthGuard],
        data: {
          acceso: CodigoAcceso.EDITAR_ACCESO
        }
      },
      {
        path: 'jerarquia-acceso',
        loadChildren: () =>
          import('../acceso/jerarquia-acceso/jerarquia-acceso.module').then(
            (m) => m.JerarquiaAccesoModule
          )
      },
      {
        path: 'ayuda-seccion',
        loadChildren: () =>
          import('../acceso/ayuda-seccion/ayuda-seccion.module').then(
            (m) => m.AyudaSeccionModule
          )
      },
      {
        path: 'acceso-ayuda',
        component: AccesoAyudaComponent,
        canActivate: [AdministradorAuthGuard],
        // TODO: Accesos de Agregar Ayuda
      },
      {
        path: 'reporte-arbol-acceso',
        component: ReporteArbolAccesoComponent,
        canActivate: [AdministradorAuthGuard],
        data: {
          acceso: CodigoAcceso.REPORTE_ARBOL_ACCESO
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccesoRoutingModule {}
