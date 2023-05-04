import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InicioComponent } from '../inicio/inicio.component';

const routes: Routes = [
  {
    path: '',
    component: InicioComponent,
  },
  {
    path: 'configuracion-general',
    loadChildren: () =>
      import(
        'src/app/views/configuracion-general/configuracion-general.module'
      ).then((m) => m.ConfiguracionGeneralModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LayoutAdminitradorRoutingModule {}
