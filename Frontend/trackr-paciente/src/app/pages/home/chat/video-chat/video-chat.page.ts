import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { HeaderComponent } from '../../layout/header/header.component';
import * as firebase from 'firebase/app';
import 'firebase/firestore';

const firebaseConfig = {
  apiKey: "AIzaSyDxj7h-jJmroLl8hqrxlJOkFLji20H5ovs",
  authDomain: "videochat-3de3c.firebaseapp.com",
  projectId: "videochat-3de3c",
  storageBucket: "videochat-3de3c.appspot.com",
  messagingSenderId: "874522306310",
  appId: "1:874522306310:web:f6eab68621355847782087"
};

@Component({
  selector: 'app-video-chat',
  templateUrl: './video-chat.page.html',
  styleUrls: ['./video-chat.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    HeaderComponent,
  ]
})
export class VideoChatPage implements OnInit {
  protected pc: RTCPeerConnection;
  protected localStream: MediaStream;
  protected remoteStream: MediaStream;
  protected firestore: firebase.firestore.Firestore;

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

  protected callInput: any;
  protected buttonState = true;
  protected hangupButtonState = true;

  constructor() {
    if (!firebase.apps.length) {
      firebase.initializeApp(firebaseConfig);
    }
    this.firestore = firebase.firestore();
    this.pc = new RTCPeerConnection(this.servers);
  }

  ngOnInit(): void {
    this.setupWebcam();
  }

  private async setupWebcam(): Promise<void> {
    // HTML elements
  }

  private async createOffer(): Promise<void> {
    // Logic to create an offer
  }

  private async answerCall(): Promise<void> {
    // Logic to answer a call
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
    // Reference Firestore collections for signaling
    const callDoc = this.firestore.collection('calls').doc();
    const offerCandidates = callDoc.collection('offerCandidates');
    const answerCandidates = callDoc.collection('answerCandidates');

    this.callInput = callDoc.id;

    // Get candidates for caller, save to db
    this.pc.onicecandidate = (event) => {
      event.candidate && offerCandidates.add(event.candidate.toJSON());
    };

    // Create offer
    const offerDescription = await this.pc.createOffer();
    await this.pc.setLocalDescription(offerDescription);

    const offer = {
      sdp: offerDescription.sdp,
      type: offerDescription.type,
    };

    await callDoc.set({ offer });

    // Listen for remote answer
    callDoc.onSnapshot((snapshot: any) => {
      const data = snapshot.data();
      if (!this.pc.currentRemoteDescription && data?.answer) {
        const answerDescription = new RTCSessionDescription(data.answer);
        this.pc.setRemoteDescription(answerDescription);
      }
    });

    // When answered, add candidate to peer connection
    answerCandidates.onSnapshot((snapshot: any) => {
      snapshot.docChanges().forEach((change: any) => {
        if (change.type === 'added') {
          const candidate = new RTCIceCandidate(change.doc.data());
          this.pc.addIceCandidate(candidate);
        }
      });
    });

    this.hangupButtonState = false;
  };

  // 3. Answer the call with the unique ID
  answerButtonClick = async () => {
    const callId = this.callInput;
    const callDoc = this.firestore.collection('calls').doc(callId);
    const answerCandidates = callDoc.collection('answerCandidates');
    const offerCandidates = callDoc.collection('offerCandidates');

    this.pc.onicecandidate = (event) => {
      event.candidate && answerCandidates.add(event.candidate.toJSON());
    };

    const callData = (await callDoc.get()).data() ?? {offer: 0};

    const offerDescription = callData["offer"];
    await this.pc.setRemoteDescription(new RTCSessionDescription(offerDescription));

    const answerDescription = await this.pc.createAnswer();
    await this.pc.setLocalDescription(answerDescription);

    const answer = {
      type: answerDescription.type,
      sdp: answerDescription.sdp,
    };

    await callDoc.update({ answer });

    offerCandidates.onSnapshot((snapshot: any) => {
      snapshot.docChanges().forEach((change: any) => {
        console.log(change);
        if (change.type === 'added') {
          let data = change.doc.data();
          this.pc.addIceCandidate(new RTCIceCandidate(data));
        }
      });
    });
  }
}
