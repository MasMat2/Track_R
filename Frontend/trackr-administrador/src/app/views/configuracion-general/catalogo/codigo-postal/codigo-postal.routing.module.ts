import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { CodigoPostalComponent } from './codigo-postal.component';

const routes: Routes = [
  {
    path: '',
    component: CodigoPostalComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Consulta',
      acceso: CodigoAcceso.CONSULTAR_CODIGO_POSTAL
    }

  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CodigoPostalRoutingModule {}
