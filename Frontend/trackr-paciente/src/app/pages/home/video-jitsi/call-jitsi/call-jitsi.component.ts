import { Component, OnInit, AfterViewInit} from '@angular/core';
import { Router } from '@angular/router';

import {AudioInterface, ParticipantInterface} from '../interfaces/jitsi-interface';

declare var JitsiMeetExternalAPI: any;

@Component({
  selector: 'app-call-jitsi',
  templateUrl: './call-jitsi.component.html',
  styleUrls: ['./call-jitsi.component.scss'],
})
export class CallJitsiComponent  implements OnInit {

  constructor() { }

  ngOnInit() { }
}
