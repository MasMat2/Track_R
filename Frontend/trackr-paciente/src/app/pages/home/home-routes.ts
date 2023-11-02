import { Route } from "@angular/router";
import { ChatPage } from "./chat/chat.page";
import { CuestionariosPage } from "./cuestionarios/cuestionarios.page";
import { DashboardPage } from "./dashboard/dashboard.page";
import { HomePage } from "./home.page";
import { MuestrasPage } from "./muestras/muestras.page";
import { ConfiguracionDashboardPage } from "./configuracion-dashboard/configuracion-dashboard.page";


export default [
  {
    path: '',
    component: HomePage,
    children: [
      { path: 'chat', component: ChatPage },
      { path: 'muestras', component: MuestrasPage },
      { path: 'dashboard', loadChildren: () => import('./dashboard/dashboard-routes') },
      { path: 'config-dashboard', component: ConfiguracionDashboardPage},
      { path: 'cuestionarios', component: CuestionariosPage },
      { path: 'perfil', loadChildren: () => import('./perfil/perfil-routes') },
      { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  },
] as Route[];
