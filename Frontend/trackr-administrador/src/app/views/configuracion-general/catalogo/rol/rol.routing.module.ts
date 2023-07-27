import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { RolComponent } from './rol.component';
import { CodigoAcceso } from '@utils/codigo-acceso';


const routes: Routes = [
  {
    path: '',
    component: RolComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Consulta',
      acceso: CodigoAcceso.CONSULTAR_ROL
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RolRoutingModule {}
