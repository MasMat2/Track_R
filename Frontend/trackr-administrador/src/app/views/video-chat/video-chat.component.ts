import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SignalingHubService } from '@services/signaling-hub.service';
import { ActivatedRoute, Router } from '@angular/router';
import { takeUntil } from 'rxjs';
import { Subject } from 'rxjs';
import { LucideAngularModule, Phone, Video, Mic, MicOff, VideoOff } from 'lucide-angular';
import { LucideIconData } from 'lucide-angular/icons/types';


interface Control {
  isActive: boolean;
  icon: string;
  inactiveIcon?: string;
}

@Component({
  selector: 'app-video-chat',
  templateUrl: './video-chat.component.html',
  styleUrls: ['./video-chat.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    LucideAngularModule,
    FormsModule
  ]
}) 
export class VideoChatComponent extends EventTarget implements OnInit, OnDestroy {

  controls: Control[] = [
    { isActive: true, icon: "mic", inactiveIcon: "mic-off" },
    { isActive: true, icon: "video", inactiveIcon: "video-off" },
    { isActive: false, icon: "phone" },
  ];

  getIconImg(control: Control): string {
    return control.isActive ? control.icon : control.inactiveIcon || control.icon;
  }

  async toggleControl(index: number): Promise<void> {

    let control = this.controls[index];
    control.isActive = !this.controls[index].isActive;

    if(control.icon === "phone") {
      if(control.isActive){ 
        await this.crearLlamada();
      }else{
        this.closeStreams();
        await this.iniciarWebCam();
      }
    }

    this.setLocalStream();
  }

  setLocalStream() {
    for(let control of this.controls) {
      if(control.icon === "mic") {
        this.localStream.getAudioTracks().forEach(track => track.enabled = control.isActive);
      }
      
      if(control.icon === "video") {
        this.localStream.getVideoTracks().forEach(track => track.enabled = control.isActive);
      }
    }
  }

  protected pc: RTCPeerConnection;
  protected localStream: MediaStream;
  protected remoteStream: MediaStream;
  private destroy$ = new Subject<void>();

  protected servers = {
    iceServers: [
      {
        urls: "stun:stun.relay.metered.ca:80",
      },
      {
        urls: "turn:a.relay.metered.ca:80",
        username: "c10fe7103a699715c64f12c5",
        credential: "omCMfjnocoNTJWlq",
      },
      {
        urls: "turn:a.relay.metered.ca:80?transport=tcp",
        username: "c10fe7103a699715c64f12c5",
        credential: "omCMfjnocoNTJWlq",
      },
      {
        urls: "turn:a.relay.metered.ca:443",
        username: "c10fe7103a699715c64f12c5",
        credential: "omCMfjnocoNTJWlq",
      },
      {
        urls: "turn:a.relay.metered.ca:443?transport=tcp",
        username: "c10fe7103a699715c64f12c5",
        credential: "omCMfjnocoNTJWlq",
      },
    ],
    iceCandidatePoolSize: 10,
  };

  protected callId: string;

  constructor(
    private signalingHubService: SignalingHubService,
    private route: ActivatedRoute,
    private router: Router
    ) {
    super();
    console.log("VideoChatComponent constructor");
  }
  
  async ngOnInit(): Promise<void> {
    console.log("ngOnInit");
    await this.iniciarWebCam();
    this.setLocalStream();
    const callControlIndex = this.controls.findIndex(control => control.icon === "phone");
    console.log(callControlIndex);
    this.toggleControl(callControlIndex);
  }

  iniciarWebCam = async () => {
    this.localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });

  }

  crearLlamada = async () => {
    console.log('crearLlamada - INICIO');

    // set the control with icon as phone, with isActive true
    const phoneControl = this.controls.find(control => control.icon === "phone");
    if (phoneControl) {
      phoneControl.isActive = true;
      console.log(this.controls);
    }

    this.route.paramMap.pipe(takeUntil(this.destroy$))
    .subscribe(params => {
      this.callId = params.get('id')!;

      if(this.callId){
        this.signalingHubService.crearLlamada(this.callId);
      }
    });
    
    this.observers();
  };

  private observers() {
    this.signalingHubService.message$.pipe(takeUntil(this.destroy$))
    .subscribe((json_string: string) => {
      
      
      if(json_string.length <= 0) return;

      var message = JSON.parse(json_string);

      console.log(message.type);
      switch (message.type) {
        // Signaling messages
        case "local-id":
          // this.callId = message.local_id;
          break;

        case "callee-connected":
          this.calleeConnected();
          break;

        case "video-offer":  
          if (message?.offer) {
            this.offerReceived(message.offer);
          };
          break;

        case "remove-remote":
          this.removeRemote();
          break;
        
        // RTC messages
        case "new-ice-candidate":
          this.pc.addIceCandidate(message.candidate);
          break;

        case "video-answer":
          if (!this.pc.currentRemoteDescription && message?.answer) {
            const answerDescription = new RTCSessionDescription(message.answer);
            this.pc.setRemoteDescription(answerDescription);
          };
          break;
      }
    });
  }

  
  removeRemote = async() => {
    await this.closeStreams();
    console.log("streams cerrados");
    await this.iniciarWebCam();
    this.setLocalStream();
  }

  // Caller listener
  calleeConnected = async () => {

    this.startRTC();

    // Create offer
    const offerDescription = await this.pc.createOffer();
    await this.pc.setLocalDescription(offerDescription);

    const offer = {
      sdp: offerDescription.sdp,
      type: offerDescription.type,
    };

    await this.signalingHubService.sendMessage({
      type: "video-offer",
      offer: offer
    });

  }

  // Callee listener
  offerReceived = async (offer: any) => {

    this.startRTC();

    if (!this.pc.currentRemoteDescription) {
      const offerDescription = new RTCSessionDescription(offer);
      this.pc.setRemoteDescription(offerDescription);
    };

    const answerDescription = await this.pc.createAnswer();
    await this.pc.setLocalDescription(answerDescription);

    const answer = {
      type: answerDescription.type,
      sdp: answerDescription.sdp,
    };

    await this.signalingHubService.sendMessage(({
      type: "video-answer",
      answer: answer
    }));
    
  }

  startRTC (): void{
    this.pc = new RTCPeerConnection(this.servers);
    this.remoteStream = new MediaStream();

    // Push tracks from local stream to peer connection
    this.localStream.getTracks().forEach((track) => {
      if (this.localStream) this.pc.addTrack(track, this.localStream);
    });

    // Pull tracks from remote stream, add to video stream
    this.pc.ontrack = (event) => {
      event.streams[0].getTracks().forEach((track) => {
        this.remoteStream.addTrack(track);
      });
    };

    // Get candidates for caller, save to db
    this.pc.onicecandidate = (event) => {
      event.candidate && this.signalingHubService.sendMessage({
        type: "new-ice-candidate",
        candidate: event.candidate
      });
    };

  }


  async ngOnDestroy(): Promise<void> {
    this.closeStreams();

  }

  async closeStreams(): Promise<void> {
    this.destroy$.next();

    if (this.localStream) {
      this.localStream.getTracks().forEach(track => track.stop());
    }

    if (this.remoteStream) {
      this.remoteStream.getTracks().forEach(track => track.stop());
    }

    for (let control of this.controls) { 
      control.isActive = false; 
    }

    this.pc.close();
  }

  hangUp = () => {
    console.log("hangUp");
    this.router.navigate(['/administrador/chat']);
  }

  // 2. Create an offer
  callButtonClick = async () => {
    // this.is_caller = true;
    
    // console.log("calling");
    
    // this.signalingHubService.crearLlamada();
  };

  // 3. Answer the call with the unique ID
  answerButtonClick = async () => {
    // this.is_caller = false;
    // const caller_id = this.callerId;

    // console.log("wait");
    // console.log("resolving");

    // await this.signalingHubService.crearLlamada(caller_id);
  }

}