import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { PacienteFormularioComponent } from './paciente-formulario/paciente-formulario.component';
import { PacienteComponent } from './paciente.component';

const routes: Routes = [
  {
    path: '',
    component: PacienteComponent,
    // canActivate: [AdministradorAuthGuard],
    data: {
      title: 'Consulta',
      acceso: CodigoAcceso.CONSULTAR_COLONIA
    }
  },
  {
    path: 'paciente-formulario',
    component: PacienteFormularioComponent
  }
];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PacienteRoutingModule {}
