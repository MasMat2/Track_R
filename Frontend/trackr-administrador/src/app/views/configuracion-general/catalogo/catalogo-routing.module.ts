import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'compania',
    loadChildren: () => import('./compania/compania.module').then((m) => m.CompaniaModule)
  },
  {
    path: 'locacion',
    loadChildren: () => import('./locacion/locacion.module').then((m) => m.HospitalModule)
  },
  {
    path: 'perfil',
    loadChildren: () => import('./perfil/perfil.module').then((m) => m.PerfilModule)
  },
  {
    path: 'rol',
    loadChildren: () => import('./rol/rol.module').then((m) => m.RolModule)
  },
  {
    path: 'usuario',
    loadChildren: () => import('./usuario/usuario.module').then((m) => m.UsuarioModule)
  },
  {
    path: 'configuracion-entidades',
    loadChildren: () => import('./configuracion-entidad/configuracion-entidad.module').then((m) => m.ConfiguracionEntidadModule)
  },
  {
    path: 'configuracion-secciones',
    loadChildren: () => import('./configuracion-secciones/configuracion-secciones.module').then((m) => m.ConfiguracionSeccionesModule)
  },
  {
    path: 'dominio',
    loadChildren: () => import('./dominio/dominio.module').then((m) => m.DominioModule)
  },
  {
    path: 'colonia',
    loadChildren: () => import('./colonia/colonia.module').then((m) => m.ColoniaModule)
  },
  {
    path: 'codigo-postal',
    loadChildren: () => import('./codigo-postal/codigo-postal.module').then((m) => m.CodigoPostalModule)
  },
  {
    path: 'estado',
    loadChildren: () => import('./estado/estado.module').then((m) => m.EstadoModule)
  },
  {
    path: 'localidad',
    loadChildren: () => import('./localidad/localidad.module').then((m) => m.LocalidadModule)
  },
  {
    path: 'municipio',
    loadChildren: () => import('./municipio/municipio.module').then((m) => m.MunicipioModule)
  },
  {
    path: 'pais',
    loadChildren: () => import('./pais/pais.module').then((m) => m.PaisModule)
  },
  {
    path: 'genero',
    loadChildren: () => import('./genero/genero.module').then((m) => m.GeneroModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CatalogoRoutingModule {}
