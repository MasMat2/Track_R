import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { MunicipioComponent } from './municipio.component';

const routes: Routes = [
    {
      path: '',
      component: MunicipioComponent,
      canActivate: [AdministradorAuthGuard],
      data: {
        title: 'Consulta',
        acceso: CodigoAcceso.CONSULTAR_MUNICIPIO
      }
    }
  ];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class MunicipioRoutingModule {}
