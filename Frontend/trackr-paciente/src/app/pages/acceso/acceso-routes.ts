import { Route } from '@angular/router';
import { RegistroPage } from './registro/registro.page';
import { LoginPage } from './login/login.page';
import { AccesoPage } from './acceso.page';
import { PeticionRestablecerContrasenaComponent } from './peticion-restablecer-contrasena/peticion-restablecer-contrasena.component';

export default [
  { path: '', component: AccesoPage },
  { path: 'login', component: LoginPage },
  { path: 'registro', component: RegistroPage },
  {
    path: 'peticion-restablecer-contrasena',
    component: PeticionRestablecerContrasenaComponent,
  },
] as Route[];
