import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CompaniaInformacionFormularioComponent } from './compania-informacion-formulario.component';

const routes: Routes = [
  {
    path: '',
    data: {},
    children: [
      {
        path: '',
        component: CompaniaInformacionFormularioComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompaniaInformacionFormularioRoutingModule { }