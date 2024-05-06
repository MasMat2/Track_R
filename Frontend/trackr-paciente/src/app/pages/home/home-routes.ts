import { Route } from "@angular/router";
import { HomePage } from "./home.page";
import { MuestrasPage } from "./muestras/muestras.page";
import { VideoChatPage } from "./video-chat/video-chat.page";


export default [
  {
    path: '',
    component: HomePage,
    children: [
      { path: 'chat/:id', component: VideoChatPage, data: { breadcrumb: 'Chat' } },
      { path: 'chat', component: VideoChatPage, data: { breadcrumb: 'Chat' } },
      { path: 'video-jitsi', loadChildren: () => import('./video-jitsi/video-jitsi-routes') },
      { path: 'chat-movil', loadChildren: () => import('./chat-movil/chat-movil-routes') },
      { path: 'clinicos', component: MuestrasPage, data: { breadcrumb: 'Muestras' } },
      { path: 'dashboard', loadChildren: () => import('./dashboard/dashboard-routes'), data: { breadcrumb: 'Inicio' }},
      { path: 'cuestionarios',loadChildren: () => import('./cuestionarios/cuestionarios-routes')  ,data: { breadcrumb: 'Cuestionario' } },
      { path: 'perfil', loadChildren: () => import('./perfil/perfil-routes'), data: { breadcrumb: 'Perfil' } },
      { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  },
] as Route[];
