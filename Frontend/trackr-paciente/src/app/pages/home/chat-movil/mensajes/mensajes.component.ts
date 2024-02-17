import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ChatPersonaService } from '@http/chat/chat-persona.service';
import { IonContent, IonicModule } from '@ionic/angular';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { Observable } from 'rxjs';
import { ChatMensajeHubService } from 'src/app/services/dashboard/chat-mensaje-hub.service';
import { ChatDTO } from 'src/app/shared/Dtos/Chat/chat-dto';
import { ChatMensajeDTO } from 'src/app/shared/Dtos/Chat/chat-mensaje-dto';
import { ChatHubServiceService } from '../../../../services/dashboard/chat-hub-service.service';
import { ArchivoService } from '../../../../shared/http/archivo/archivo.service';
import { ArchivoFormDTO } from '../../../../shared/Dtos/archivos/archivo-form-dto';
import { addIcons } from 'ionicons';
import {cameraOutline, paperPlane, videocamOutline, chevronBack, trash, mic, micOutline, documentOutline } from 'ionicons/icons';
//Libreria de capacitor para grabar audio
import { VoiceRecorder, VoiceRecorderPlugin, RecordingData, GenericResponse, CurrentRecordingStatus } from 'capacitor-voice-recorder';

//Escribir archivos
import { Filesystem, Directory, Encoding } from '@capacitor/filesystem'
import { PlataformaService } from 'src/app/services/dashboard/plataforma.service';
import { ModalController } from '@ionic/angular';
import { ArchivoPrevisualizarComponent } from './archivo-previsualizar/archivo-previsualizar.component';

import { timer, Subject } from 'rxjs';
import { finalize, map, takeUntil, takeWhile } from 'rxjs/operators';
import { Haptics, ImpactStyle } from '@capacitor/haptics'
import { PressDirective } from 'src/app/shared/directives/press.directive';
import { SwipeDirective } from 'src/app/shared/directives/swipe.directive';


@Component({
  selector: 'app-mensajes',
  templateUrl: './mensajes.component.html',
  styleUrls: ['./mensajes.component.scss'],
  standalone: true,
  imports: [FormsModule, CommonModule, IonicModule, HeaderComponent, PressDirective, SwipeDirective],
})
export class MensajesComponent{
  protected mensajes: ChatMensajeDTO[];
  protected idChat: number;
  protected msg: string = '';
  protected idUsuario: number;
  protected chatMensajes$: Observable<ChatMensajeDTO[][]>
  protected chatMensajes: ChatMensajeDTO[][]
  protected chat: ChatDTO = {
    fecha: new Date(),
    habilitado: true,
    titulo: 'Chat',
    idCreadorChat: 0,
  };

  protected escribiendo: boolean = false;
  private stop$ = new Subject<any>();
  duracionAudio = '';
  duracion = 0;
  protected timer$: any;

  protected archivo?: File = undefined;
  @ViewChild('fileInput') fileInput!: ElementRef;
  @ViewChild(IonContent) content: IonContent;
  @ViewChild('movableSpan') movableSpan: ElementRef;


  //Variables para el audio
  protected isAudio: boolean = false;
  protected grabacionIniciada: boolean = false;
  protected grabacionCancelada: boolean = false;
  protected audio?: string = '';
  protected audio2?: string;

  constructor(
    private ChatMensajeHubService: ChatMensajeHubService,
    private ChatPersonaService: ChatPersonaService,
    private router: ActivatedRoute,
    private route: Router,
    private ChatHubServiceService: ChatHubServiceService,
    private ArchivoService: ArchivoService,
    private plataformaService: PlataformaService,
    private ModalController:ModalController
  ) { 
      addIcons({videocamOutline, 
        chevronBack, 
        cameraOutline, 
        paperPlane, 
        trash, 
        documentOutline,
        mic,
        micOutline}); 
    }

  ionViewWillEnter() {
    this.obtenerIdUsuario();
    this.obtenerIdChat();
    this.solicitarPermisos();
  }

  obtenerIdChat() {
    this.router.params.subscribe(params => {
      this.idChat = Number(params['id'])
      this.obtenerMensajes();
      this.obtenerChat();
    })
  }

  obtenerChat() {
    this.ChatHubServiceService.chat$.subscribe(res => {
      this.chat = res.find(x => x.idChat == this.idChat) || { fecha: new Date(), habilitado: false, idCreadorChat: 0 }
    })
  }

  async enviarMensaje(): Promise<void> {
    const regex = /^\n+$/;
    if (regex.test(this.msg)) {
      this.msg = '';
      return;
    }

    let msg: ChatMensajeDTO = {
      fecha: new Date(),
      idChat: this.idChat,
      mensaje: this.msg,
      idPersona: 5333,
      archivo: '',
      idArchivo: 0
    }

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
      msg.archivoNombre = `audio-${Date.now()}.wav`
      msg.archivoTipoMime = "audio/wav"
      msg.fechaRealizacion = new Date();
      msg.nombre = `audio-${Date.now()}.wav`
    }

    this.ChatMensajeHubService.enviarMensaje(msg);
    if (this.mensajes.length == 0) {
      this.ChatMensajeHubService.chatMensaje$.subscribe(res => {
        this.mensajes = res.find(array => array.some(x => x.idChat === this.idChat)) || [];
      })
    }
    this.msg = "";

    this.archivo = undefined;
    this.audio = '';
    this.audio2 = '';
    this.isAudio = false;
  }

  obtenerIdUsuario() {
    this.ChatPersonaService.obtenerIdUsuario().subscribe((res) => {
      this.idUsuario = res;
    });
  }

  mostrarMensaje(id: number) {
    return id == this.idUsuario;
  }

  obtenerMensajes() {
    this.chatMensajes$ = this.ChatMensajeHubService.chatMensaje$;

    this.chatMensajes$.subscribe((res) => {
      this.chatMensajes = res;
      this.obtenerChatSeleccionado();
    });
  }

  obtenerChatSeleccionado() {
    this.mensajes = this.chatMensajes.find((array) =>
      array.some((x) => x.idChat == this.idChat)
    ) || [];
  }

  regresarBtn() {
    this.route.navigate(['home/chat-movil'])
  }

  onFileSelected(event: any): void {
    this.archivo = event.target.files[0];
  }

  readFileAsByteArray(file: File): Promise<Uint8Array> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();

      reader.onload = (e: any) => {
        const result: ArrayBuffer = e.target.result;
        const byteArray = new Uint8Array(result);
        resolve(byteArray);
      };

      reader.onerror = (error) => {
        reject(error);
      };

      reader.readAsArrayBuffer(file);
    });
  }

  convertirBase64String(): Promise<string> {
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

  async subirArchivo() {
    if (this.archivo) {
      let byte = await this.readFileAsByteArray(this.archivo);

      let aux: ArchivoFormDTO = {
        idUsuario: 5333,
        archivo: Array.from(byte),
        archivoNombre: this.archivo.name,
        archivoTipoMime: this.archivo.type,
        fechaRealizacion: new Date(),
        nombre: this.archivo.name
      }

      this.ArchivoService.subirArchivo(aux).subscribe(res => {
        console.log(res)
      })
    }
  }

  clickArchivo(idArchivo: number) {
    this.ArchivoService.getArchivo(idArchivo).subscribe( async res => {
      const modal =  await this.ModalController.create({component: ArchivoPrevisualizarComponent,componentProps:{archivo:res}});
      modal.present();

    });
  }

  async downloadFileMobile(fileBase64: string, nombre?: string, mime?: string) {
    try {

      let downloadDirectory = Directory.Documents

      // Crear un archivo en el sistema de archivos
      const result = await Filesystem.writeFile({
        path: `${Directory.Data}/${nombre}`,
        data: fileBase64,
        directory: Directory.External,
        recursive: true,
        //encoding: Encoding.UTF8,
      });

      // Obtener la URL del archivo creado
      const url = result.uri;

      console.log(url)

    } catch (error) {
      console.error('Error al descargar el archivo:', error);
    }
  }

  downloadFileWeb(fileBase64: string, nombre?: string, mime?: string) {
    // Decodificar la cadena Base64
    const decodedData = atob(fileBase64);

    // Convertir a un array de bytes
    const byteNumbers = new Array(decodedData.length);
    for (let i = 0; i < decodedData.length; i++) {
      byteNumbers[i] = decodedData.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);

    // Crear un Blob con los datos binarios
    const blob = new Blob([byteArray], { type: 'application/octet-stream' });

    // Crear un object URL y asignarlo al enlace de descarga
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    document.body.appendChild(a);
    a.style.display = 'none';
    a.href = url;

    a.download = `${nombre}`;

    // Nombre del archivo
    a.click();

    // Limpiar el object URL después de la descarga
    URL.revokeObjectURL(url);
  }

  openFileInput(): void {
    if(this.audio != ''){
      this.audio = '';
      this.audio2 = '';
      this.isAudio = false;
    }
    this.fileInput.nativeElement.click();
  }

  imprimirFecha(fecha: Date): string {
    let x = new Date(fecha)
    return `${x.getDate()}/${x.getMonth() + 1}/${x.getFullYear()} - ${x.getHours()}:${x.getMinutes()}`
  }

  // Esta función se llama después de cada actualización de la vista
  ngAfterViewChecked() {
    this.scrollContentToBottom();
  }

  // Función para desplazar automáticamente hacia abajo al final de la lista
  scrollContentToBottom(){
    this.content.scrollToBottom();
  }

  eliminarArchivo() {
    this.archivo = undefined;
  }

  changeAudio() {
    this.audio = '';
    this.audio2 = '';
    this.isAudio = !this.isAudio;
  }

  solicitarPermisos() {
    VoiceRecorder.hasAudioRecordingPermission().then((permision: GenericResponse) => {
      if (!permision.value) {
        VoiceRecorder.requestAudioRecordingPermission();
      }
    })
  }

  grabar() {
    if (this.grabacionIniciada) {
      return;
    }

    //this.changeAudio();
    this.grabacionIniciada = true;

    VoiceRecorder.startRecording().then(_ => {
      this.calcularDuracion();
    });
    
  }

  detenerGrabacion() {
    if (!this.grabacionIniciada || this.grabacionCancelada) {
      this.grabacionCancelada = false;
      return;
    }
    if(this.archivo){
      this.archivo = undefined;
    }
    VoiceRecorder.stopRecording().then((audio: RecordingData) => {
      this.grabacionIniciada = false;
      this.isAudio = true;
      if (audio.value) {
        this.audio = audio.value.recordDataBase64;
        this.audio2 = 'data:audio/wav;base64,' + audio.value.recordDataBase64;
      }
    });
  }

  cancelarGrabacion() {
    if (!this.grabacionIniciada) {
      return;
    }
    if(this.archivo){
      this.archivo = undefined;
    }
    VoiceRecorder.stopRecording().then((audio: RecordingData) => {
      this.grabacionCancelada = true;
      this.grabacionIniciada = false;
    });
  }

  calcularDuracion(){
    if(!this.grabacionIniciada){
      this.duracion = 0;
      this.duracionAudio = '';
      return;
    }
    
    this.timer$ = timer(0, 1000).pipe(
      takeUntil(this.stop$),
      takeWhile(_ => this.duracion >= 0), //Aqui se puede colocar un máximo para el contador
      finalize(() => {
        this.duracion = 0; //detiene y reinicia el contador cuando se deja de grabar
      }),
      map(_ => {
        this.duracion= this.duracion + 1;
        const minutos = Math.floor(this.duracion / 60);
        const segundos = (this.duracion % 60).toString().padStart(2, '0');
        this.duracionAudio = `${minutos}:${segundos}`;
        return this.duracion;
      })
    );
  }

  crearLlamada() {
    this.route.navigate(['/home/video-jitsi/create-call', this.idChat]);
  }

  contestarLlamada(meetCode: string) {
    this.route.navigate(['/home/video-jitsi/answer-call', meetCode]);
  }

  validarMeet(msj: string) {

    if (msj.includes('trackr-' + this.idChat)) {
      const regex = /trackr-\d{3}-\d+/;
      const match = msj.match(regex);
      if (match && match.length > 0) {
        const codigo = match[0];
        this.contestarLlamada(codigo);
      } else {
        console.log("Error al validar codigo meet jitsi.");
      }


    }
  }

  //verificar si se está escribiendo un mensaje (mensaje no vacío)
  escribiendoMensaje(){
    return !(/^ *$/.test(this.msg))
  }

  protected presionarGrabarAudio(event: any) {
    //al presionar empezar a grabar
    if(event == 'start'){
      Haptics.impact({style: ImpactStyle.Light}); //pequeña vibración para feedback
      this.grabar();
    }

    //al soltar detener la grabación
    if(event == 'end'){
      Haptics.impact({style: ImpactStyle.Light}); //pequeña vibración para feedback
      this.detenerGrabacion();
    }
  }

  deslizarCancelarAudio(event: any) {

    if(event.dirX == 'left'){

      //Cuando se mueva 100px desde el origen
      //Se cancela la grabación
      if(event.currentX <= event.startX - 100){
        this.cancelarGrabacion();
      }

      const relativeX = event.currentX - event.startX + 15; //15px es el threshold especificado en la directiva
      if(!this.grabacionCancelada){
        this.updateSpanPosition(relativeX);
      }

    }

  }

  //función para el efecto de arrastre del span al cancelar el audio
  updateSpanPosition(posicionX: number): void {
    //el span se moverá posicionX pixeles a la izquierda, 
    //donde posicionX es la diferencia entre la posicion actual de tu dedo y el boton del microfono (el inicio)
    this.movableSpan.nativeElement.style.transform = `translateX(calc(-50% + ${posicionX}px))`;
  }




}
