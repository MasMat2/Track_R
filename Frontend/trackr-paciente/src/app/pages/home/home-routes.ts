import { Route } from "@angular/router";
import { ChatPage } from "./chat/chat.page";
import { CuestionariosPage } from "./cuestionarios/cuestionarios.page";
import { DashboardPage } from "./dashboard/dashboard.page";
import { HomePage } from "./home.page";
import { MuestrasPage } from "./muestras/muestras.page";
import { ChatMovilComponent as ChatMovilPage } from "./chat-movil/chat-movil.page";


export default [
  {
    path: '',
    component: HomePage,
    children: [
      { path: 'chat', component: ChatPage },
      { path: 'chat-movil', loadChildren: () => import('./chat-movil/chat-movil-routes') },
      { path: 'muestras', component: MuestrasPage },
      { path: 'dashboard', loadChildren: () => import('./dashboard/dashboard-routes') },
      { path: 'cuestionarios', component: CuestionariosPage },
      { path: 'perfil', loadChildren: () => import('./perfil/perfil-routes') },
      { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  },
] as Route[];
