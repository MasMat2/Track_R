import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { ConfiguracionSeccionesComponent } from './configuracion-secciones.component';

const routes: Routes = [
  {
    path: '',
    component: ConfiguracionSeccionesComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Consulta',
      acceso: CodigoAcceso.CONFIGURADOR_SECCION
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ConfiguracionSeccionesRoutingModule {}
