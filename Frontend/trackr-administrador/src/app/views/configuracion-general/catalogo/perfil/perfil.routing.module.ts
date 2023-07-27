import { PerfilFormularioComponent } from './perfil-formulario/perfil-formulario.component';
import { PerfilComponent } from './perfil.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'consultar',
    pathMatch: 'full'
  },
  {
    path: '',
    data: {
      title: 'Perfiles'
    },
    children: [
      {
        path: '',
        component: PerfilComponent,
        canActivate: [AdministradorAuthGuard],
        data: {
          title: 'Consulta',
          acceso: CodigoAcceso.CONSULTAR_PERFIL
        }
      },
      {
        path: 'agregar',
        component: PerfilFormularioComponent,
        canActivate: [AdministradorAuthGuard],
        data: {
          title: 'Alta',
          acceso: CodigoAcceso.AGREGAR_PERFIL
        }
      },
      {
        path: 'editar',
        component: PerfilFormularioComponent,
        canActivate: [AdministradorAuthGuard],
        data: {
          title: 'Modificar',
          acceso: CodigoAcceso.EDITAR_PERFIL
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PerfilRoutingModule {}
