import { Routes, RouterModule, Route } from '@angular/router';
import { ChatMovilComponent } from './chat-movil.page';
import { BarraChatsComponent } from './BarraChats/BarraChats.component';
import { MensajesComponent } from './mensajes/mensajes.component';
export default [
  {
    path: '',
    component: ChatMovilComponent,
    children: [
      {
        path: '',
        component: BarraChatsComponent,
      },
      {
        path: 'chat/:id',
        component: MensajesComponent
      },
      { path: '**', redirectTo: '', pathMatch: 'full' }
    ]
  },
]  as Route[];
