import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompaniaFormularioComponent } from './compania-formulario.component';

const routes: Routes = [
  {
    path: '',
    data: {},
    children: [
      {
        path: '',
        component: CompaniaFormularioComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompaniaFormularioRoutingModule { }