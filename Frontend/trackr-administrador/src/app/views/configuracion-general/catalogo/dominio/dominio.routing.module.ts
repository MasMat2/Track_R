import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { DominioComponent } from './dominio.component';

const routes: Routes = [
  {
    path: '',
    component: DominioComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Consulta',
      acceso: CodigoAcceso.CONSULTAR_DOMINIO
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DominioRoutingModule {}
