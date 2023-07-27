import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { HospitalFormularioComponent } from './locacion-formulario/locacion-formulario.component';
import { HospitalComponent } from './locacion.component';


const routes: Routes = [
  {
    path: '',
    component: HospitalComponent,
    data: {
      title: 'Consulta',
      acceso: CodigoAcceso.CONSULTAR_IMPUESTO
    }
  },
  {
    path: 'form',
    component: HospitalFormularioComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HospitalRoutingModule { }
