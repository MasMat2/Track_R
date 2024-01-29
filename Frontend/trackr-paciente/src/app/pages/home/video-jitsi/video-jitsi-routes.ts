import { Routes, RouterModule, Route } from '@angular/router';
import { CreateJitsiMeetComponent } from './create-jitsi-meet/create-jitsi-meet.component';
export default [
    {
        path: ':id-chat',
        component: CreateJitsiMeetComponent,

    },
] as Route[];