import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnswerMeetComponent } from './answer-meet/answer-meet.component';
import { JitsiMeetRoutingModule } from './jitsi-meet.routing.module';



@NgModule({
  declarations: [
    AnswerMeetComponent
  ],
  imports: [
    CommonModule,
    JitsiMeetRoutingModule
    
  ]
})
export class JitsiMeetModule { }
