import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RecomendacionGeneralComponent } from './recomendacion-general/recomendacion-general.component';
import { RecomendacionGeneralModule } from './recomendacion-general/recomendacion-general.module';

const routes: Routes = [
  {
    path: 'acceso',
    loadChildren: () => import('./acceso/acceso.module').then((m) => m.AccesoModule)
  },
  {
    path: 'catalogo',
    loadChildren: () => import('./catalogo/catalogo.module').then((m) => m.CatalogoModule)
  },
  {
    path : 'recomendaciones-generales',
    component : RecomendacionGeneralComponent
  }
];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes) , RecomendacionGeneralModule],
  exports: [RouterModule]
})
export class ConfiguracionGeneralRoutingModule {}
