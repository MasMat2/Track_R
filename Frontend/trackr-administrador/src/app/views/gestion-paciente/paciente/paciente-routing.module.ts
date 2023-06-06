import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { ExpedienteEstudioComponent } from './expediente-estudio/expediente-estudio.component';
import { ExpedienteFormularioComponent } from './expediente-formulario/expediente-formulario.component';
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
    path: 'expediente-formulario',
    component: ExpedienteFormularioComponent
  },
];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PacienteRoutingModule {}
