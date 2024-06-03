import { Route } from "@angular/router";
import { CuestionariosPage } from "./cuestionarios.page";
import { MisCuestionariosComponent } from "./mis-cuestionarios/mis-cuestionarios.component";
import { ResponderCuestionarioComponent } from "./responder-cuestionario/responder-cuestionario.component";
import { VerCuestionarioComponent } from "./ver-cuestionario/ver-cuestionario.component";
import { ExitGuard } from "src/app/shared/guards/exit.guard";

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
          path: 'responder/:id',
          canDeactivate : [ExitGuard],
          component: ResponderCuestionarioComponent
        },
        {
          path: 'ver/:id',
          component: VerCuestionarioComponent
        },
        { path: '**', redirectTo: 'misCuestionarios', pathMatch: 'full' }
      ]
    },
  ]  as Route[];