import { NgModule } from '@angular/core';
import { RouteReuseStrategy, RouterModule, Routes } from '@angular/router';
import { LayoutAdministradorComponent } from './views/administrador/layout-administrador/layout-administrador.component';
import { CustomRouteReuseStrategy } from '@utils/custom-route-reuse-strategy';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'login',
  },
  {
    path: 'administrador',
    component: LayoutAdministradorComponent,
    loadChildren: () =>
      import(
        './views/administrador/layout-administrador/layout-administrador.module'
      ).then((m) => m.LayoutAdminsitradorModule),
  },
  {
    path: 'login',
    loadComponent: () =>
    import('./views/administrador/login-administrador/login-administrador.component')
    .then((m) => m.LoginAdministradorComponent)
  },
  {
    path: 'confirmar-correo',
    loadComponent: () =>
    import('./views/configuracion-general/acceso/confirmar-correo/confirmar-correo.component')
    .then((m) => m.ConfirmarCorreoComponent)
  },
  {
    path: 'restablecer-contrasena',
    loadComponent: () =>
    import('./views/configuracion-general/acceso/restablecer-contrasena/restablecer-contrasena.component')
    .then((m) => m.RestablecerContrasenaComponent)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule],
  providers: [
    {
      provide: RouteReuseStrategy,
      useClass: CustomRouteReuseStrategy
    },
  ],
})
export class AppRoutingModule {}
