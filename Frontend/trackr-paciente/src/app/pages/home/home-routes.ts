import { Route } from "@angular/router";
import { ChatPage } from "./chat/chat.page";
import { CuestionariosPage } from "./cuestionarios/cuestionarios.page";
import { DashboardPage } from "./dashboard/dashboard.page";
import { HomePage } from "./home.page";
import { MuestrasPage } from "./muestras/muestras.page";
import { ChatMovilComponent as ChatMovilPage } from "./chat-movil/chat-movil.page";
import { ConfiguracionDashboardPage } from "./configuracion-dashboard/configuracion-dashboard.page";


export default [
  {
    path: '',
    component: HomePage,
    children: [
      { path: 'chat', component: ChatPage, data: { breadcrumb: 'Chat' } },
      { path: 'chat-movil', loadChildren: () => import('./chat-movil/chat-movil-routes') },
      { path: 'muestras', component: MuestrasPage, data: { breadcrumb: 'Muestras' } },
      { path: 'dashboard', loadChildren: () => import('./dashboard/dashboard-routes'), data: { breadcrumb: 'Inicio' }},
      { path: 'config-dashboard', component: ConfiguracionDashboardPage},
      { path: 'cuestionarios', component: CuestionariosPage, data: { breadcrumb: 'Cuestionario' } },
      { path: 'perfil', loadChildren: () => import('./perfil/perfil-routes'), data: { breadcrumb: 'Perfil' } },
      { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  },
] as Route[];
