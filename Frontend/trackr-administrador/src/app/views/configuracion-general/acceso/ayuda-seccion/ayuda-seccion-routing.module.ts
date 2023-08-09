import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { AdministradorAuthGuard } from 'src/app/auth/administrador-auth-guard.service';
import { AyudaSeccionComponent } from './ayuda-seccion.component';


const routes: Routes = [
  {
    path: '',
    component: AyudaSeccionComponent,
    canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Consulta Ayuda Sección',
      acceso: CodigoAcceso.CONSULTAR_AYUDA_SECCION
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AyudaSeccionRoutingModule { }
