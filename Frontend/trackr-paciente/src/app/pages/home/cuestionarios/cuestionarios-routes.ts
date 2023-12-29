import { Route } from "@angular/router";
import { CuestionariosPage } from "./cuestionarios.page";
import { MisCuestionariosComponent } from "./mis-cuestionarios/mis-cuestionarios.component";
import { ResponderCuestionarioComponent } from "./responder-cuestionario/responder-cuestionario.component";

export default [
    {
      path: '',
      component: CuestionariosPage,
      children: [
        {
          path: 'misCuestionarios',
          component: MisCuestionariosComponent,
        },
        {
          path: 'cuestionario/:id',
          component: ResponderCuestionarioComponent
        },
        { path: '**', redirectTo: 'misCuestionarios', pathMatch: 'full' }
      ]
    },
  ]  as Route[];