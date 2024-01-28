import { CommonModule } from '@angular/common';
import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ChatPersonaService } from '@http/chat/chat-persona.service';
import { IonicModule } from '@ionic/angular';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { Observable } from 'rxjs';
import { ChatMensajeHubService } from 'src/app/services/dashboard/chat-mensaje-hub.service';
import { ChatDTO } from 'src/app/shared/Dtos/Chat/chat-dto';
import { ChatMensajeDTO } from 'src/app/shared/Dtos/Chat/chat-mensaje-dto';
import { ChatHubServiceService } from '../../../../services/dashboard/chat-hub-service.service';
import { ArchivoService } from '../../../../shared/http/archivo/archivo.service';
import { ArchivoFormDTO } from '../../../../shared/Dtos/archivos/archivo-form-dto';

//Libreria de capacitor para grabar audio
import { VoiceRecorder, VoiceRecorderPlugin, RecordingData, GenericResponse, CurrentRecordingStatus } from 'capacitor-voice-recorder';

//Escribir archivos
import { Filesystem, Directory, Encoding } from '@capacitor/filesystem'
import { PlataformaService } from 'src/app/services/dashboard/plataforma.service';



@Component({
  selector: 'app-mensajes',
  templateUrl: './mensajes.component.html',
  styleUrls: ['./mensajes.component.scss'],
  standalone: true,
  imports: [FormsModule, CommonModule, IonicModule, HeaderComponent],
})
export class MensajesComponent {
  protected mensajes: ChatMensajeDTO[];
  protected idChat: number;
  protected msg: string;
  protected idUsuario: number;
  protected chatMensajes$: Observable<ChatMensajeDTO[][]>
  protected chatMensajes: ChatMensajeDTO[][]
  protected chat: ChatDTO = {
    fecha: new Date(),
    habilitado: true,
    titulo: 'Chat',
    idCreadorChat: 0,
  };
  protected archivo?: File = undefined;
  @ViewChild('fileInput') fileInput!: ElementRef;
  @ViewChild('scrollContainer') private scrollContainer: ElementRef;

  //Variables para el audio
  protected isAudio: boolean = false;
  protected grabacionIniciada: boolean = false;
  protected audio?: string = '';
  protected audio2?: string;

  constructor(
    private ChatMensajeHubService: ChatMensajeHubService,
    private ChatPersonaService: ChatPersonaService,
    private router: ActivatedRoute,
    private route: Router,
    private ChatHubServiceService: ChatHubServiceService,
    private ArchivoService: ArchivoService,
    private plataformaService : PlataformaService
  ) { }

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
    this.ArchivoService.getArchivo(idArchivo).subscribe(res => {
      if(this.plataformaService.isMobile())
      {
        this.downloadFileMobile(res.archivo, res.nombre, res.archivoMime)
      }
      else if(this.plataformaService.isWeb())
      {
        this.downloadFileWeb(res.archivo, res.nombre, res.archivoMime)
      }
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

  downloadFileWeb(fileBase64:string,nombre?:string,mime?:string) {
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
    this.fileInput.nativeElement.click();
  }

  imprimirFecha(fecha: Date): string {
    let x = new Date(fecha)
    return `${x.getDate()}/${x.getMonth() + 1}/${x.getFullYear()} - ${x.getHours()}:${x.getMinutes()}`
  }

  // Esta función se llama después de cada actualización de la vista
  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  // Función para desplazar automáticamente hacia abajo al final de la lista
  scrollToBottom(): void {
    try {
      this.scrollContainer.nativeElement.scrollTop = this.scrollContainer.nativeElement.scrollHeight;
    } catch (err) { }
  }

  eliminarArchivo() {
    this.archivo = undefined;
  }

  changeAudio() {
    this.isAudio = !this.isAudio;
  }

  solicitarPermisos() {
    VoiceRecorder.hasAudioRecordingPermission().then(permision => {
      if (!permision.value) {
        VoiceRecorder.requestAudioRecordingPermission();
      }
    })
  }

  grabar() {
    if (this.grabacionIniciada) {
      return;
    }
    this.grabacionIniciada = true;
    VoiceRecorder.startRecording();
  }

  detenerGrabacion() {
    if (!this.grabacionIniciada) {
      return;
    }
    VoiceRecorder.stopRecording().then(audio => {
      this.grabacionIniciada = false;
      if (audio.value) {
        this.audio = audio.value.recordDataBase64;
        this.audio2 = 'data:audio/wav;base64,' + audio.value.recordDataBase64;
      }
    })
  }

}
