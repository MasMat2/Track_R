import { Route } from "@angular/router";
import { ChatPage } from "./chat/chat.page";
import { CuestionariosPage } from "./cuestionarios/cuestionarios.page";
import { DashboardPage } from "./dashboard/dashboard.page";
import { HomePage } from "./home.page";
import { MuestrasPage } from "./muestras/muestras.page";
import { PerfilPage } from "./perfil/perfil.page";
import { MisEstudiosComponent } from "./perfil/mis-estudios/mis-estudios.component";
import { MisEstudiosFormularioComponent } from "./perfil/mis-estudios/mis-estudios-formulario/mis-estudios-formulario.component";
export default [
  {
    path: '',
    component: HomePage,
    children: [
      { path: 'chat', component: ChatPage },
      { path: 'muestras', component: MuestrasPage },
      { path: 'dashboard', component: DashboardPage },
      { path: 'cuestionarios', component: CuestionariosPage },
      { path: 'perfil', component: PerfilPage },
      {path:'agregar',component:MisEstudiosFormularioComponent},
      {path:'mis-estudios',component:MisEstudiosComponent},
      { path: '**', redirectTo: 'dashboard', pathMatch: 'full' },
    ]
  },
] as Route[];
