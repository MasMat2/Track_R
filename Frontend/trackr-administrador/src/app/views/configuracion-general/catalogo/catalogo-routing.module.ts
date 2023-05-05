import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'compania',
    loadChildren: () => import('./compania/compania.module').then((m) => m.CompaniaModule)
  },
  {
    path: 'locacion',
    loadChildren: () => import('./locacion/locacion.module').then((m) => m.HospitalModule)
  },
  {
    path: 'usuario',
    loadChildren: () => import('./usuario/usuario.module').then((m) => m.UsuarioModule)
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CatalogoRoutingModule {}
