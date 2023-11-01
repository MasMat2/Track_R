import { Route } from "@angular/router";
import { ChatPage } from "./chat/chat.page";
import { CuestionariosPage } from "./cuestionarios/cuestionarios.page";
import { DashboardPage } from "./dashboard/dashboard.page";
import { HomePage } from "./home.page";
import { MuestrasPage } from "./muestras/muestras.page";


export default [
  {
    path: '',
    component: HomePage,
    children: [
      { path: 'chat', component: ChatPage, data: { breadcrumb: 'Chat' } },
      { path: 'muestras', component: MuestrasPage, data: { breadcrumb: 'Muestras' } },
      { path: 'dashboard', loadChildren: () => import('./dashboard/dashboard-routes'), data: { breadcrumb: 'Inicio' }},
      { path: 'cuestionarios', component: CuestionariosPage, data: { breadcrumb: 'Cuestionario' } },
      { path: 'perfil', loadChildren: () => import('./perfil/perfil-routes'), data: { breadcrumb: 'Perfil' } },
      { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  },
] as Route[];
