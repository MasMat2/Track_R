import { Injectable } from "@angular/core";
import { Meta } from "@angular/platform-browser";
import { Router, ActivatedRoute } from "@angular/router";
import { ChatDTO } from "@dtos/chats/chat-dto";
import { ChatMensajeDTO } from "@dtos/chats/chat-mensaje-dto";
import { ChatHubServiceService } from "@services/chat-hub-service.service";
import { ChatMensajeHubService } from "@services/chat-mensaje-hub.service";
import { FechaService } from "@services/fecha.service";
import { lastValueFrom, Observable } from "rxjs";
import { ParticipantInterface, AudioInterface } from "src/app/views/jitsi-meet/interfaces/jitsi-interface";
import { ChatPersonaService } from "./chat-persona.service";


declare var JitsiMeetExternalAPI: any;

@Injectable({
  providedIn: 'root'
})
export class DataJitsiService {

  protected localStream: MediaStream;

  protected mensajes$: Observable<ChatMensajeDTO[][]>;
  protected mensajes : ChatMensajeDTO[][];
  protected chats$: Observable<ChatDTO[]>;
  protected chats: ChatDTO[] = [];

  private domain: string = "8x8.vc"; // For self hosted use your domain
  private options: any;
  private roomApi: any;

  private idChat : string;

  // For Custom Controls
  private isAudioMuted = false;
  private isVideoMuted = false;

  public room: string;
  public user: string;

  private AppIDJitsi = 'vpaas-magic-cookie-c25fa0da2cd344ba8d41a873768065ec';

  constructor(
    private router: Router,
    private meta: Meta,
    private mensajeHubService: ChatMensajeHubService,
    private chatMensajeHubServiceService: ChatHubServiceService,
    private route: ActivatedRoute,
    private chatPersonaService : ChatPersonaService,
    private fechaService: FechaService
  ) { }


  createNewRoom(numberCall : number): void {
    const newRoomName = `${this.AppIDJitsi}/trackr-${this.idChat}-${numberCall}`;
  
    this.mandarMensajeLlamada(`📞 Te espero en la sala trackr-${this.idChat}-${numberCall}`);
  
    const container = document.querySelector('#jaas-container');
    if (!container) {
      console.error('El contenedor #jaas-container no se encuentra en el DOM');
      return ;
    }
  

    const newRoomOptions = {
      roomName: newRoomName,
      width: '100%',
      height: '100%',
      configOverwrite: { prejoinPageEnabled: false },
      parentNode: container,
      userInfo: {
        displayName: "Trackr-user"
      }
    };
  
    try {
      this.roomApi = new JitsiMeetExternalAPI(this.domain, newRoomOptions);
      //this.setupEventListeners();
    } catch (error) {
      console.error('Error al crear JitsiMeetExternalAPI:', error);
    }

    this.roomApi.addEventListeners({
      readyToClose: this.handleClose,
      participantLeft: this.handleParticipantLeft,
      participantJoined: this.handleParticipantJoined,
      videoConferenceJoined: this.handleVideoConferenceJoined,
      videoConferenceLeft: this.handleVideoConferenceLeft,
      audioMuteStatusChanged: this.handleMuteStatus,
      videoMuteStatusChanged: this.handleVideoStatus
    });

  }

  async mandarMensajeLlamada(mensajeLlamada : string){
    const  idUsuario = await lastValueFrom( this.chatPersonaService.obtenerIdUsuario());
    let msg: ChatMensajeDTO = {
      fecha: this.fechaService.fechaLocalAFechaUTC(new Date()),
      idChat: parseInt(this.idChat),
      mensaje: mensajeLlamada,
      idPersona: idUsuario,
      archivo: '',
      idArchivo: 0,
      esVideoChat:true
    }

    this.mensajeHubService.enviarMensaje(msg);
  }

  iniciarWebCam = async () => {
    this.localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
  };

  obtenerChats(){
    this.chatMensajeHubServiceService.iniciarConexion();
    this.chats$ = this.chatMensajeHubServiceService.chat$;
    this.chats$.subscribe(res => {
      this.chats = res;
    })
  }
  obtenerMensajes(){
    this.mensajeHubService.iniciarConexion();
    this.mensajes$ = this.mensajeHubService.chatMensaje$;
    this.mensajes$.subscribe(res => {
      this.mensajes = res;
    })
  }

  comenzarLlamada(idchat: number){
    this.idChat=idchat.toString();
    this.route.paramMap.subscribe(params => {
      this.idChat = idchat.toString();
    });

    this.iniciarWebCam(); //iniciamos camara y microfono para que pueda ser iniciada una llamada en el iframe de jitsi

    const numberCall = Math.floor(Math.random() * 10000);
    const newRoomName = `${this.AppIDJitsi}/trackr-${this.idChat}-${numberCall}`;

    
    this.createNewRoom(numberCall); //Metodo para crear una llamada con jitsi
    this.router.navigate(['administrador', 'jitsi-meet', 'trackr-' + this.idChat + '-' + numberCall]);


  }

  contestarLlamada(meetCode: string) {
    this.iniciarWebCam(); //iniciamos camara y microfono para que pueda ser iniciada una llamada en el iframe de jitsi

    const newRoomName = `${this.AppIDJitsi}/${meetCode}`;

    //Configuración para la nueva sala
    const newRoomOptions = {
      roomName: newRoomName,
      width: '100%',
      height: '100%',
      configOverwrite: { prejoinPageEnabled: false },
      interfaceConfigOverwrite: {
        //overwrite interface properties
      },
      parentNode: document.querySelector('#jaas-container'),
      userInfo: {
        displayName: "Paciente"
      }

    };

    //Crear una nueva instancia de JitsiMeetExternalAPI para la nueva sala
    this.roomApi = new JitsiMeetExternalAPI(this.domain, newRoomOptions);

    // Event handlers para la nueva sala
    this.roomApi.addEventListeners({
      readyToClose: this.handleClose,
      participantLeft: this.handleParticipantLeft,
      participantJoined: this.handleParticipantJoined,
      videoConferenceJoined: this.handleVideoConferenceJoined,
      videoConferenceLeft: this.handleVideoConferenceLeft,
      audioMuteStatusChanged: this.handleMuteStatus,
      videoMuteStatusChanged: this.handleVideoStatus
    });
  }


  hangup() {
    console.log("hangup");
    this.roomApi.executeCommand('hangup');
    this.localStream.getTracks().forEach(track => track.stop());
    this.roomApi.dispose();
    this.roomApi.remove();
    this.roomApi=null;

    console.log("hangup");
    const container = document.getElementById('jaas-container');
    if (container) {
      container.innerHTML = '';
    }

    if (this.localStream) {
      this.localStream.getTracks().forEach(track => track.stop());
    }
  }

  handleClose = () => {

    console.log("handleClose");
    const container = document.getElementById('jaas-container');
      if (container) {
        container.innerHTML = '';
    }
  }

  handleParticipantLeft = async (participant: ParticipantInterface) => {
    console.log("handleParticipantLeft", participant);
     //console.log("handleParticipantLeft", participant); { id: "2baa184e" }
    const data = await this.getParticipants();
  }

  handleParticipantJoined = async (participant: ParticipantInterface) => {
    //console.log("handleParticipantJoined", participant);  { id: "2baa184e", displayName: "Shanu Verma", formattedDisplayName: "Shanu Verma" }
    const data = await this.getParticipants();
  }

  handleVideoConferenceJoined = async (participant: ParticipantInterface) => {
    //console.log("handleVideoConferenceJoined", participant);  { roomName: "bwb-bfqi-vmh", id: "8c35a951", displayName: "Akash Verma", formattedDisplayName: "Akash Verma (me)"}
    const data = await this.getParticipants();
  }

  handleVideoConferenceLeft = () => {
    console.log("handleVideoConferenceLeft");
    this.router.navigate(['/administrador']);
  }

  handleMuteStatus = (audio: AudioInterface) => {
   //console.log("handleMuteStatus", audio);  { muted: true }
  }

  handleVideoStatus = (video: AudioInterface) => {
    //console.log("handleVideoStatus", video);  { muted: true }
  }

  getParticipants() {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve(this.roomApi.getParticipantsInfo()); // get all participants
      }, 500)
    });
  }

}
