import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompaniaComponent } from './compania.component';
import { ACCESO_COMPANIA } from 'src/app/shared/utils/codigos-acceso/catalogo.accesos';

const routes: Routes = [
  {
    path: '',
    component: CompaniaComponent,
    data: {
      title: 'Consulta',
      acceso: ACCESO_COMPANIA.Consultar
    }
  },
  {
    path: 'form',
    loadChildren: () =>
      import('./compania-formulario/compania-formulario.module').then(
        (m) => m.CompaniaFormularioModule
      )
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompaniaRoutingModule { }
