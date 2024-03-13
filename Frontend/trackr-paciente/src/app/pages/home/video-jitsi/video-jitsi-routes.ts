import { Routes, RouterModule, Route } from '@angular/router';
import { CreateJitsiMeetComponent } from './create-jitsi-meet/create-jitsi-meet.component';
import { AnswerMeetComponent } from './answer-meet/answer-meet.component';
import { VideoJitsiPage } from './video-jitsi.page';

export default [
    {
        path: '',
        children: [
            {
                path: 'answer-call/:meet-name',
                component: AnswerMeetComponent,
            },
            {
                path: 'create-call/:id-chat',
                component: CreateJitsiMeetComponent,
            },
            //{ path: '**', redirectTo: '', pathMatch: 'full' }            
        ],

    }

] as Route[];