import { Routes, RouterModule, Route } from '@angular/router';
import { DoctoresFormularioComponent } from './doctores-formulario/doctores-formulario.component';
import { MisDoctoresComponent } from './mis-doctores.component';

export default [
  {
    path: '',
    component: MisDoctoresComponent,
    children: [
      { path: 'agregar', component: DoctoresFormularioComponent }
    ]
  },
] as Route[];


