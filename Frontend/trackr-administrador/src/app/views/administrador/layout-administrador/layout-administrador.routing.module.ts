import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutAdministradorComponent } from './layout-administrador.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutAdministradorComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutAdminitradorRoutingModule {}
