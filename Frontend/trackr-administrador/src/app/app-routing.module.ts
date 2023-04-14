import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutAdministradorComponent } from './views/administrador/layout-administrador/layout-administrador.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'administrador'
  },
  {
    path: 'administrador',
    component: LayoutAdministradorComponent,
    loadChildren: () => 
    import('./views/administrador/layout-administrador/layout-administrador.module')
    .then((m) => m.LayoutAdminsitradorModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
