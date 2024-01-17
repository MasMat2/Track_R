import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { HeaderComponent } from '../../layout/header/header.component';
import { FormsModule } from '@angular/forms';
import { SignalingHubService } from '@services/signaling-hub.service';

@Component({
  selector: 'app-video-chat',
  templateUrl: './video-chat.page.html',
  styleUrls: ['./video-chat.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    HeaderComponent,
    FormsModule
  ]
})
export class VideoChatPage extends EventTarget implements OnInit {
  protected pc: RTCPeerConnection;
  protected localStream: MediaStream;
  protected remoteStream: MediaStream;

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

  protected callInput: string;
  protected buttonState = true;
  protected hangupButtonState = true;

  constructor(private signalingHubService: SignalingHubService) {
    super();
    this.pc = new RTCPeerConnection(this.servers);
  }

  ngOnInit(): void {
    this.setUpObserver();
  }

  private setUpObserver() {
    this.signalingHubService.message$.subscribe((json_string: string) => {
      if(json_string.length <= 0) return;

      var message = JSON.parse(json_string);
      console.log(message);
      switch (message.type) {

        case "local-id":
          this.callInput = message.local_id;
          break;

        case "new-ice-candidate":
          this.pc.addIceCandidate(message.candidate);
          break;

        case "video-offer":  
          if (!this.pc.currentRemoteDescription && message?.offer) {
            const offerDescription = new RTCSessionDescription(message.offer);
            this.pc.setRemoteDescription(offerDescription);
            this.dispatchEvent(new Event('offerReceived'));
          };
          break;

        case "video-answer":
          if (!this.pc.currentRemoteDescription && message?.answer) {
            const answerDescription = new RTCSessionDescription(message.answer);
            this.pc.setRemoteDescription(answerDescription);
          };
          break;
      }
    })
  }


  // 1. Setup media sources

  webcamButtonClick = async () => {
    this.localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
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

    this.buttonState = false;

  };

  // 2. Create an offer
  callButtonClick = async () => {
    this.signalingHubService.crearLlamada();

    // Wait for peer id to send offer and candidates
    const waitForPeerConnection = new Promise((resolve) => {
      this.signalingHubService.addEventListener('peerconnected', resolve);
    });
    
    await waitForPeerConnection;

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
  };

  // 3. Answer the call with the unique ID
  answerButtonClick = async () => {
    const callId = this.callInput;

    console.log("wait");

    // Wait for offer to send answer and candidates
    const waitForOffer = new Promise((resolve) => {
      console.log("resolving")
      this.addEventListener('offerReceived', resolve);
    });
    
    await this.signalingHubService.crearLlamada(callId);

    await waitForOffer;
    

    console.log("offerReceived");
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
}