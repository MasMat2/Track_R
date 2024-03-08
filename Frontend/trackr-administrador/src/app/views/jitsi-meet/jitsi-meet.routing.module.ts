import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AnswerMeetComponent } from './answer-meet/answer-meet.component';

const routes: Routes = [
    {
        path: '',
        component: AnswerMeetComponent,
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class JitsiMeetRoutingModule { }
