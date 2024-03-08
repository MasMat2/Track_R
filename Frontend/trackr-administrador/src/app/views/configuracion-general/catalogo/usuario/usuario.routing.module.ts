import { UsuarioFormularioComponent } from './usuario-formulario/usuario-formulario.component';
import { UsuarioComponent } from './usuario.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'consultar',
    pathMatch: 'full',
  },
  {
    path: '',
    data: {
      title: 'Usuarios',
    },
    children: [
      {
        path: '',
        component: UsuarioComponent,
        canActivate: [AdministradorAuthGuard],
        data: {
          title: 'Consulta',
          acceso: CodigoAcceso.CONSULTAR_USUARIO,
        }
      },
      {
        path: 'agregar',
        component: UsuarioFormularioComponent,
        canActivate: [AdministradorAuthGuard],
        data: {
          title: 'Alta',
          acceso: CodigoAcceso.AGREGAR_USUARIO,
        }
      },
      {
        path: 'editar',
        component: UsuarioFormularioComponent,
        canActivate: [AdministradorAuthGuard],
        data: {
          title: 'Modificar',
          acceso: CodigoAcceso.EDITAR_USUARIO,
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UsuarioRoutingModule {}
