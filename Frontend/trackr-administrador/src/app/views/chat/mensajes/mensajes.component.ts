import { Component, ElementRef, Input, ViewChild, OnInit, AfterViewInit, AfterViewChecked, OnChanges, AfterContentInit, ViewChildren, QueryList } from '@angular/core';
import { ChatMensajeDTO } from '@dtos/chats/chat-mensaje-dto';
import { ChatMensajeHubService } from '../../../shared/services/chat-mensaje-hub.service';
import { ChatPersonaService } from '../../../shared/http/chats/chat-persona.service';
import { ChatPersonaSelectorDTO } from '@dtos/chats/chat-persona-selector-dto';
import { ArchivoService } from '../../../shared/http/archivo/archivo.service';
import { Router } from '@angular/router';
import { Subject, finalize, map, takeUntil, takeWhile, timer } from 'rxjs';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PdfVisorComponent } from '@sharedComponents/pdf-visor/pdf-visor.component';
import { ImgVisorComponent } from '@sharedComponents/img-visor/img-visor.component';
import { ChatHubServiceService } from '../../../shared/services/chat-hub-service.service';
import { MensajeService } from '../../../shared/components/mensaje/mensaje.service';
import { NgAudioRecorderService, OutputFormat } from 'ng-audio-recorder';
import { GeneralConstant } from '@utils/general-constant';
import { AlertifyService } from '@services/alertify.service';

@Component({
  selector: 'app-mensajes',
  templateUrl: './mensajes.component.html',
  styleUrls: ['./mensajes.component.scss'],
})
export class MensajesComponent implements OnInit, OnChanges ,AfterViewInit, AfterViewChecked, AfterContentInit {

  @Input() mensajes: ChatMensajeDTO[];
  @Input() idChat: number;
  @Input() tituloChat: string;
  @Input() imagenChat: string;
  @Input() tipoMime: string;
  @ViewChild('fileInput') fileInput!: ElementRef;
  @ViewChild('scrollContainer') private scrollContainer: ElementRef;


  //varias
  protected msg: string;
  protected cantidadMensajes: number;

  //info chat
  protected idUsuario: number;
  protected personas: ChatPersonaSelectorDTO[];
  protected idPersonas: number[];

  //variables archivo
  protected archivo?: File = undefined;
  protected audio?: string = '';
  protected audio2?: string;

  //Variables audio
  protected isAudioPlaying: boolean = false;
  protected isAudio: boolean = false;
  protected grabacionIniciada: boolean = false;
  protected duracion: number = 0;
  protected duracionAudio: string = '';
  private stop$ = new Subject<any>();
  protected timer$: any;

  private media: MediaRecorder;
  private audioChunks: Blob[] = [];
  protected audioSubject = new Subject<string>();

  constructor(
    private ChatMensajeHubService: ChatMensajeHubService,
    private ChatPersonaService: ChatPersonaService,
    private ArchivoService: ArchivoService,
    private router: Router,
    private ChatHubServiceService: ChatHubServiceService,
    private mensaje: MensajeService,
    private audioRecorderService: NgAudioRecorderService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef,
    private alertifyService: AlertifyService,
  ) {}
 
  ngOnInit() {
    this.obtenerIdUsuario();
    this.obtenerPersonasEnChat();
    this.setCantidadMensajes(this.mensajes.length);
  }

  ngAfterViewInit() {
    this.scrollToBottom();
  }

  ngAfterContentInit(){
  }

  // Esta función se llama después de cada actualización de la vista
  ngAfterViewChecked() {
    //scroll cuando llega un nuevo mensaje
    if (this.cantidadMensajes < this.mensajes.length) {
      this.setCantidadMensajes(this.mensajes.length);
      this.scrollToBottom();
    }
  }

  ngOnChanges() {
    this.obtenerPersonasEnChat();
  }

  private obtenerIdUsuario() {
    this.ChatPersonaService.obtenerIdUsuario().subscribe((res) => {
      this.idUsuario = res;
    });
  }
  
  private obtenerPersonasEnChat() {
    this.ChatPersonaService.obtenerPersonasEnChatSelector(
      this.idChat
    ).subscribe((res) => {
      this.personas = res;
      this.idPersonas = this.personas.map((x) => x.idUsuario);
    });
  }

  protected async eliminarChat() {
     this.ChatPersonaService.obtenerPersonasEnChatSelector(
      this.idChat
    ).subscribe(async (res) => {
      var respuesta = await this.presentAlertEliminarChat();
      if(!respuesta)
        return
  
      if (res.length == 1) {
        this.ChatHubServiceService.eliminarChat(this.idChat);
        await this.presentAlertExito();
      } else {
        await this.presentAlertError();
      }
    }); 
  }

  protected esMensajeMio(idPersona: number) {
    return idPersona == this.idUsuario;
  }

  protected hayAdjuntoEnMensaje(mensaje: ChatMensajeDTO) {
    return (
      mensaje.idArchivo !== 0 &&
      mensaje.idArchivo !== null &&
      mensaje.idArchivo !== undefined
    );
  }
  
  protected async onVerArchivo(mensaje: ChatMensajeDTO) {
    if (mensaje.archivoTipoMime == 'application/pdf') {
      this.abrirModalPdf(mensaje.idArchivo);
    }
    if (
      mensaje.archivoTipoMime == 'image/png' ||
      mensaje.archivoTipoMime == 'image/jpeg' ||
      mensaje.archivoTipoMime == 'image/gif'
    ) {
      this.abrirModalImagen(mensaje.idArchivo);
    } else {
      return;
    }
  }

  private abrirModalImagen(idArchivo: number) {
    this.ArchivoService.getArchivo(idArchivo).subscribe({
      next: (data) => {
        const initialState = {
          nombreArchivo: data.nombre,
          archivo: data.archivo,
          archivoTipoMime: data.archivoMime,
          nombre: data.nombre,
        };

        this.bsModalRef = this.modalService.show(ImgVisorComponent, {
          initialState,
          ...GeneralConstant.CONFIG_MODAL_DEFAULT,
        });
        this.bsModalRef.content.onClose = (cerrar: boolean) => {
          this.bsModalRef.hide();
        };
      },
    });
  }

  private abrirModalPdf(idArchivo: number) {
    this.ArchivoService.getArchivo(idArchivo).subscribe({
      next: (data) => {
        this.bsModalRef = this.modalService.show(
          PdfVisorComponent,
          GeneralConstant.CONFIG_MODAL_DEFAULT
        );
        this.bsModalRef.content.archivo = data.archivo;
        this.bsModalRef.content.nombre = data.nombre;
        this.bsModalRef.content.archivoNombre = data.nombre;
        this.bsModalRef.content.onClose = (cerrar: boolean) => {
          this.bsModalRef.hide();
        };
      },
    });
  }
   
  protected esAudio(mensaje: ChatMensajeDTO) {
    return (mensaje.archivoTipoMime === 'audio/wav' || mensaje.archivoTipoMime === 'audio/webm');
  }

  protected openFileInput(): void {
    this.fileInput.nativeElement.click();
  }

  protected onFileSelected(event: any): void {
    this.eliminarAudio();
    this.archivo = event.target.files[0];
  }

  protected eliminarArchivo() {
    this.archivo = undefined;
    this.fileInput.nativeElement.value = "";
  }

  protected grabar() {
    if (this.grabacionIniciada) {
      return;
    }
    this.archivo = undefined;
    this.grabacionIniciada = true;
    this.audioRecorderService.startRecording();
    this.calcularDuracion();
  }

  protected async detenerGrabacion() {
    if (!this.grabacionIniciada) {
      return;
    }
    this.grabacionIniciada = false;
    //this.media.stop();
    this.audioRecorderService
      .stopRecording(OutputFormat.WEBM_BLOB)
      .then(async (output) => {
        if (output instanceof Blob) {
          let record =
            'data:audio/webm;base64,' +
            (await this.convertBlobToBase64(output));
          this.audio = record;
          this.audio2 = record;
        } else {
          console.error('El output no es un Blob.');
        }
      });
  }

  protected cancelarGrabacion() {
    this.audioRecorderService.stopRecording(OutputFormat.WEBM_BLOB);
    this.grabacionIniciada = false;
  }

  protected eliminarAudio() {
    this.audio = '';
    this.audio2 = undefined;
  }

  protected onAudioPlay(option: boolean) {
    this.isAudioPlaying = option;
  }
  
  private calcularDuracion() {
    if (!this.grabacionIniciada) {
      this.duracion = 0;
      this.duracionAudio = '';
      return;
    }

    this.timer$ = timer(0, 1000).pipe(
      takeUntil(this.stop$),
      takeWhile((_) => this.duracion >= 0), //Aqui se puede colocar un máximo para el contador
      finalize(() => {
        this.duracion = 0; //detiene y reinicia el contador cuando se deja de grabar
      }),
      map((_) => {
        this.duracion = this.duracion + 1;
        const minutos = Math.floor(this.duracion / 60);
        const segundos = (this.duracion % 60).toString().padStart(2, '0');
        this.duracionAudio = `${minutos}:${segundos}`;
        return this.duracion;
      })
    );
  }

  protected hayAdjuntoAgregado() {
    return (
      this.archivo != undefined ||
      (this.audio != undefined &&
        this.audio != '' &&
        this.audio2 != undefined &&
        this.audio2 != '')
    );
  }
 
  protected permisosGrabacion() {
    navigator.mediaDevices.getUserMedia({ audio: true }).then((stream) => {
      this.media = new MediaRecorder(stream);

      this.media.onstart = () => {
        this.audioChunks = [];
      };

      this.media.ondataavailable = (event) => {
        if (event.data.size > 0) {
          this.audioChunks.push(event.data);
        }
      };

      this.media.onstop = async () => {
        const audioBlob = new Blob(this.audioChunks, { type: 'audio/wav' });
        const reader = new FileReader();
        reader.onload = () => {
          const base64data =
            reader.result != null ? reader.result.toString().split(',')[1] : '';
          this.audio = base64data;
          this.audio2 = 'data:audio/wav;base64,' + base64data;
          this.audioSubject.next('data:audio/wav;base64,' + base64data);
        };

        reader.readAsDataURL(audioBlob);
        this.audioChunks = [];
      };
    });
  }

  protected contestarLlamada(meetCode: string) {
    this.router.navigate(['administrador', 'jitsi-meet', meetCode]);
  }

  protected esMensajeValido(mensaje: ChatMensajeDTO): boolean {
    if (this.esMensajeMio(mensaje.idPersona)) {
      return false;
    }
  
    let regex = /trackr-\d+-\d+/;
    if (mensaje.mensaje.includes('trackr-' + this.idChat) && mensaje.mensaje.match(regex)) {
      return true;
    }
  
    regex = /webrtc-\d+-(\d+)/;
    if (mensaje.mensaje.includes('webrtc-' + this.idChat) && mensaje.mensaje.match(regex)) {
      return true;
    }
  
    return false;
  }

  protected validarMeet(mensaje: ChatMensajeDTO) {
    if(this.esMensajeMio(mensaje.idPersona)){
      return;
    }

    if (mensaje.mensaje.includes('trackr-' + this.idChat)) {
      const regex = /trackr-\d+-\d+/;
      const match = mensaje.mensaje.match(regex);
      if (match && match.length > 0) {
        const codigo = match[0];
        this.contestarLlamada(codigo);
      } else {
        console.error('Error al validar codigo meet jitsi.');
      }
    }

    if (mensaje.mensaje.includes('webrtc-' + this.idChat)) {
      const regex = /webrtc-\d+-(\d+)/;
      const match = mensaje.mensaje.match(regex);
      if (match && match.length > 0) {
        const codigo = match[1];
        this.router.navigate(['/administrador/webrtc', codigo]);
      } else {
        console.error('Error al validar codigo meet webrtc.');
      }
    }
  }

  private convertBlobToBase64(blob: Blob): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onloadend = () => {
        const arrayBuffer = reader.result as ArrayBuffer;
        let binary = '';
        const bytes = new Uint8Array(arrayBuffer);
        const len = bytes.byteLength;
        for (let i = 0; i < len; i++) {
          binary += String.fromCharCode(bytes[i]);
        }
        const base64String = window.btoa(binary);
        resolve(base64String);
      };
      reader.onerror = reject;
      reader.readAsArrayBuffer(blob);
    });
  }

  private convertirBase64String(): Promise<string> {
    return new Promise((resolve, reject) => {
      if (this.archivo) {
        const lector = new FileReader();

        lector.onload = (e: any) => {
          const resultadoBase64 = e.target.result;
          resolve(resultadoBase64);
        };

        lector.onerror = (e) => {
          reject(e);
        };

        lector.readAsDataURL(this.archivo);
      } else {
        reject('No se ha seleccionado ningún archivo.');
      }
    });
  }

  // Función para desplazar automáticamente hacia abajo al final de la lista
  private scrollToBottom(): void {
    try {
      this.scrollContainer.nativeElement.scrollTop =
        this.scrollContainer.nativeElement.scrollHeight;
    } catch (err) {}
  }

  protected setColorAudio(idPersona: number): 'light' | 'dark' {
    if (this.idUsuario == idPersona) {
      return 'dark';
    } else {
      return 'light';
    }
  }

  protected setCantidadMensajes(cantidad: number) {
    this.cantidadMensajes = cantidad;
  }

  protected sePuedeEnviarMensaje() {
    return (
      (this.hayAdjuntoAgregado() ||
        (this.msg !== undefined && this.msg !== '')) &&
      !this.grabacionIniciada
    );
  }

  protected async enviarMensaje(): Promise<void> {
    const regex = /^\n+$/;
    if (regex.test(this.msg)) {
      this.msg = '';
      return;
    }

    let msg: ChatMensajeDTO = {
      fecha: new Date(),
      idChat: this.idChat,
      mensaje: this.msg,
      idPersona: this.idUsuario,
      archivo: '',
      idArchivo: 0,
    };

    //Agregar logica para subir archivo
    if (this.archivo) {
      let byte = await this.convertirBase64String();
      byte = byte.split(',')[1];
      msg.archivo = byte;
      msg.archivoNombre = this.archivo.name;
      msg.archivoTipoMime = this.archivo.type;
      msg.fechaRealizacion = new Date();
      msg.nombre = this.archivo.name;
    }

    if (this.audio != '') {
      msg.archivo = this.audio;
      msg.archivoNombre = `audio-${Date.now()}.webm`;
      msg.archivoTipoMime = 'audio/webm';
      msg.fechaRealizacion = new Date();
      msg.nombre = `audio-${Date.now()}.webm`;
    }

    this.ChatMensajeHubService.enviarMensaje(msg);
    if (this.mensajes.length == 0) {
      this.ChatMensajeHubService.chatMensaje$.subscribe((res) => {
        this.mensajes =
          res.find((array) => array.some((x) => x.idChat === this.idChat)) ||
          [];
      });
    }
    this.msg = "";

    this.eliminarArchivo();
    this.eliminarAudio();
    this.isAudio = false;
  }


private presentAlertEliminarChat(): Promise<Boolean> {
  return new Promise((resolve) => {
    this.alertifyService.presentAlert({
      header: '¿Seguro(a) que desea eliminar este chat?',
      subHeader: 'No podrás recuperarlo',
      Icono: 'trash',
      Color: 'error',
      twoButtons: true,
      cancelButtonText: 'No, regresar',
      confirmButtonText: "Si, eliminar"
    }, (result) => {
      if(result == "confirm"){
        resolve(true);
      } else {
        resolve(false);
      }
    });
  });
}

private presentAlertExito(): Promise<Boolean> {
  return new Promise((resolve) => {
    this.alertifyService.presentAlert({
      header: 'Chat eliminado exitosamente',
      subHeader: '',
      Icono: 'check',
      Color: 'primary',
      twoButtons: false,
      confirmButtonText: "De acuerdo",
      cancelButtonText: ''
    }, (result) => {
      if(result == "confirm"){
        resolve(true);
      } else {
        resolve(false);
      }
    });
  });
}

private presentAlertError(): Promise<Boolean> {
  return new Promise((resolve) => {
    this.alertifyService.presentAlert({
      header: 'Error al eliminar el chat',
      subHeader: 'Todos los participantes deben salir primero.',
      Icono: 'info',
      Color: 'error',
      twoButtons: false,
      confirmButtonText: "Cerrar",
      cancelButtonText: ''
    }, (result) => {
      if(result == "confirm"){
        resolve(true);
      } else {
        resolve(false);
      }
    });
  });
}

}
