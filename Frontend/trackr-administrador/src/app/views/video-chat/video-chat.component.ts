import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SignalingHubService } from '@services/signaling-hub.service';
import { ActivatedRoute, Router } from '@angular/router';
import { takeUntil } from 'rxjs';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-video-chat',
  templateUrl: './video-chat.component.html',
  styleUrls: ['./video-chat.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    // IonicModule,
    // HeaderComponent,
    FormsModule
  ]
}) 
export class VideoChatComponent extends EventTarget implements OnInit, OnDestroy {
  protected pc: RTCPeerConnection;
  protected localStream: MediaStream;
  protected remoteStream: MediaStream;
  private destroy$: Subject<void>;

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

  protected callerId: string;
  protected buttonState = true;
  protected hangupButtonState = true;
  protected is_caller = false;

  constructor(
    private signalingHubService: SignalingHubService,
    private route: ActivatedRoute,
    private router: Router
    ) {
    super();
  }
  
  async ngOnInit(): Promise<void> {

    console.log("init1  ");
    
    this.destroy$ = new Subject<void>();

    await this.signalingHubService.iniciarConexion();

    await this.webcamButtonClick();

    this.signalingObservers();
    

    this.route.paramMap.pipe(takeUntil(this.destroy$))
    .subscribe(params => {
      this.callerId = params.get('id')!;
      console.log(this.callerId);

      if(!this.callerId){
        console.log("calling");
        this.is_caller = true;
        this.signalingHubService.crearLlamada();

      }else{
        console.log("answering");
        this.is_caller = false;
        this.signalingHubService.crearLlamada(this.callerId);
      }
    });
  }

  private signalingObservers() {
    this.signalingHubService.message$.pipe(takeUntil(this.destroy$))
    .subscribe((json_string: string) => {
      if(json_string.length <= 0) return;

      var message = JSON.parse(json_string);
      console.log(message);
      switch (message.type) {

        case "local-id":
          this.callerId = message.local_id;
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
          console.log('remove-remote');
          this.remoteStream = new MediaStream();
          break;
      }
    });
  }

  // Caller listener
  calleeConnected = async () => {
    if(this.is_caller){

      this.startRTC();

      // Get candidates for caller, save to db
      this.pc.onicecandidate = (event) => {
        event.candidate && this.signalingHubService.sendMessage({
          type: "new-ice-candidate",
          candidate: event.candidate
        });
      };

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

      this.hangupButtonState = false;
    }    
  }

  // Callee listener
  offerReceived = async (offer: any) => {
    
    console.log("offerReceived");
    this.startRTC();

    if (!this.pc.currentRemoteDescription) {
      const offerDescription = new RTCSessionDescription(offer);
      this.pc.setRemoteDescription(offerDescription);
    };
    
    this.pc.onicecandidate = (event) => {
      event.candidate && this.signalingHubService.sendMessage({
        type: "new-ice-candidate",
        candidate: event.candidate
      });
    };

    console.log("answer");
    const answerDescription = await this.pc.createAnswer();
    await this.pc.setLocalDescription(answerDescription);

    const answer = {
      type: answerDescription.type,
      sdp: answerDescription.sdp,
    };

    console.log("send answer");
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

    this.rtcObservers();

  }


  private rtcObservers() {
    this.signalingHubService.message$.pipe(takeUntil(this.destroy$))
    .subscribe((json_string: string) => {
      if(json_string.length <= 0) return;

      var message = JSON.parse(json_string);
      // console.log(message);
      switch (message.type) {
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


  // 1. Setup media sources
  webcamButtonClick = async () => {
    this.localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });

    this.buttonState = false;
  };

  async ngOnDestroy(): Promise<void> {
    this.destroy$.next();
    this.destroy$.complete();
    this.signalingHubService.detenerConexion();
    this.closeStreams();
    console.log("destroy");
  }

  closeStreams = () => {
    console.log(this.localStream, this.remoteStream);

    if (this.localStream) {
      this.localStream.getTracks().forEach(track => track.stop());
    }

    if (this.remoteStream) {
      this.remoteStream.getTracks().forEach(track => track.stop());
    }
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