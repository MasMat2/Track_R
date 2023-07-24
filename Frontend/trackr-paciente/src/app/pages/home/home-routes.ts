import { Route } from "@angular/router";
import { ChatPage } from "./chat/chat.page";
import { CuestionariosPage } from "./cuestionarios/cuestionarios.page";
import { DashboardPage } from "./dashboard/dashboard.page";
import { HomePage } from "./home.page";
import { MuestrasPage } from "./muestras/muestras.page";
import { PerfilPage } from "./perfil/perfil.page";

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
      { path: '**', redirectTo: 'dashboard', pathMatch: 'full' },
    ]
  },
] as Route[];
