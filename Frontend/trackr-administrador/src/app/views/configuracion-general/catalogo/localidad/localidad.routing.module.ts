import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { LocalidadComponent } from './localidad.component';

const routes: Routes = [
    {
      path: '',
      component: LocalidadComponent,
      canActivate: [AdministradorAuthGuard],
      data: {
        title: 'Consulta',
        acceso: CodigoAcceso.CONSULTAR_LOCALIDAD
      }
    }
  ];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class LocalidadRoutingModule {}
