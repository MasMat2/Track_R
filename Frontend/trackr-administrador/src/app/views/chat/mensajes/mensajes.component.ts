import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { ChatMensajeDTO } from '@dtos/chats/chat-mensaje-dto';
import { ChatMensajeHubService } from '../../../shared/services/chat-mensaje-hub.service';
import { ChatPersonaService } from '../../../shared/http/chats/chat-persona.service';
import { ChatPersonaSelectorDTO } from '@dtos/chats/chat-persona-selector-dto';
import { ArchivoService } from '../../../shared/http/archivo/archivo.service';
import { ArchivoFormDTO } from '@dtos/archivos/archivo-form-dto';
import { Router } from '@angular/router';
import { Subject, lastValueFrom } from 'rxjs';

//Libreria de capacitor para grabar audio
/* import { VoiceRecorder, VoiceRecorderPlugin, RecordingData, GenericResponse, CurrentRecordingStatus } from 'capacitor-voice-recorder'; */

declare var Recorder: any;

@Component({
  selector: 'app-mensajes',
  templateUrl: './mensajes.component.html',
  styleUrls: ['./mensajes.component.scss']
})
export class MensajesComponent {
  @Input() mensajes : ChatMensajeDTO[];
  @Input() idChat: number;
  @Input() tituloChat:string;
  protected msg: string;
  protected idUsuario:number;
  protected personas: ChatPersonaSelectorDTO[];
  protected idPersonas:number[]
  protected archivo ?: File =undefined;
  @ViewChild('fileInput') fileInput!: ElementRef;
  @ViewChild('scrollContainer') private scrollContainer: ElementRef;

  //Variables para el audio
  protected isAudio:boolean = false;
  protected grabacionIniciada: boolean = false;
  protected audio?:string = '';
  protected audio2?:string;
  private media: MediaRecorder;
  private audioChunks: Blob[] = [];
  protected audioSubject = new Subject<string>();
  private record:string;

  constructor(private ChatMensajeHubService:ChatMensajeHubService,
              private ChatPersonaService:ChatPersonaService,
              private ArchivoService:ArchivoService,
              private router: Router) {}
  
  ngOnInit(){
    this.obtenerIdUsuario();
    this.obtenerPersonasEnChat();
    this.permisosGrabacion();
/*     this.solicitarPermisos(); */
  }

  ngOnChanges(){
    this.obtenerPersonasEnChat();
  }

  obtenerPersonasEnChat(){
    this.ChatPersonaService.obtenerPersonasEnChatSelector(this.idChat).subscribe(res =>{
      this.personas = res;
      this.idPersonas = this.personas.map(x => x.idUsuario)
    })
  }

  async enviarMensaje(): Promise<void>{
    const regex = /^\n+$/;
    if(regex.test(this.msg)){
      this.msg = '';
      return;
    }
    
    let msg: ChatMensajeDTO = {
      fecha: new Date(),
      idChat: this.idChat,
      mensaje: this.msg,
      idPersona:5333,
      archivo: '',
      idArchivo: 0,
    }

    //Agregar logica para subir archivo
    if(this.archivo){
      let byte = await this.convertirBase64String();
      byte = byte.split(',')[1];
      msg.archivo = byte;
      msg.archivoNombre = this.archivo.name;
      msg.archivoTipoMime = this.archivo.type;
      msg.fechaRealizacion = new Date();
      msg.nombre = this.archivo.name;
    }

    if(this.audio != ''){
      msg.archivo = this.audio;
      msg.archivoNombre = `audio-${Date.now()}.wav`
      msg.archivoTipoMime = "audio/wav"
      msg.fechaRealizacion = new Date();
      msg.nombre = `audio-${Date.now()}.wav`
    }

    this.ChatMensajeHubService.enviarMensaje(msg);
    if(this.mensajes.length == 0){
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

  obtenerIdUsuario(){
    this.ChatPersonaService.obtenerIdUsuario().subscribe(res => {
      this.idUsuario = res;
    })
  }

  mostrarMensaje(id:number){
    return id == this.idUsuario;
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

  convertirBase64String() :Promise<string> {
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

  async subirArchivo(){
    if(this.archivo){
      let byte = await this.readFileAsByteArray(this.archivo);

    let aux:ArchivoFormDTO = {
      idUsuario: 5333,
      archivo: Array.from(byte),
      archivoNombre: this.archivo.name,
      archivoTipoMime: this.archivo.type,
      fechaRealizacion: new Date(),
      nombre: this.archivo.name
    }
    console.log(aux.archivo)

    this.ArchivoService.subirArchivo(aux).subscribe(res => {
      console.log(res)
    })
    }
  }

  clickArchivo(idArchivo:number){
    this.ArchivoService.getArchivo(idArchivo).subscribe(res => {
      this.downloadFile(res.archivo,res.nombre,res.archivoMime)
    });
  }

  downloadFile(fileBase64:string,nombre?:string,mime?:string) {
    
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

  imprimirFecha(fecha:Date):string{
    let x = new Date(fecha)
    return `${x.getDate()}/${x.getMonth()+1}/${x.getFullYear()} - ${x.getHours()}:${x.getMinutes()}`
  }

  enviarAlPresionarEnter(event:any){
    console.log(event)
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

  eliminarArchivo(){
    this.archivo = undefined;
  }

  changeAudio(){
    this.isAudio = !this.isAudio;
  }

/*   solicitarPermisos(){
    VoiceRecorder.hasAudioRecordingPermission().then(permision => {
      if(!permision.value){
        VoiceRecorder.requestAudioRecordingPermission();
      }
    })
  } */

/*   grabar(){
    if(this.grabacionIniciada){
      return;
    }
    this.grabacionIniciada = true;
    VoiceRecorder.startRecording();
  }
 */
/*   detenerGrabacion(){
    if(!this.grabacionIniciada){
      return;
    }
    VoiceRecorder.stopRecording().then(audio => {
      this.grabacionIniciada = false;
      if(audio.value){
        this.audio = audio.value.recordDataBase64;
        this.audio2 = 'data:audio/wav;base64,' + audio.value.recordDataBase64;
      }
    })
  } */

  permisosGrabacion(){
    navigator.mediaDevices.getUserMedia({audio:true})
                          .then( (stream) => {
                            this.media = new MediaRecorder(stream)

                            this.media.onstart = () => {
                              this.audioChunks = [];
                            }
                            
                            this.media.ondataavailable = (event) => {
                              if (event.data.size > 0) {
                                this.audioChunks.push(event.data);
                              }
                            };
                    
                            this.media.onstop = async () => {
                              const audioBlob = new Blob(this.audioChunks, { type: 'audio/wav' });
                              const reader = new FileReader();
                              reader.onload = () => {
                                const base64data = reader.result != null ? reader.result.toString().split(',')[1]: '';
                                this.audio = base64data;
                                this.audio2 = 'data:audio/wav;base64,' + base64data;
                                this.audioSubject.next('data:audio/wav;base64,'+base64data);
                              };
                    
                              reader.readAsDataURL(audioBlob);
                              this.audioChunks = [];
                            };
                          })
  }

  grabar(){
    if(this.grabacionIniciada){
      return;
    }
    this.grabacionIniciada = true;
    this.media.start();
    console.log('grabacion iniciada')
    
  }

  async detenerGrabacion(){
    if(!this.grabacionIniciada){
      console.log('dds')
      return;
    }
      console.log('grabacion detenida')
      this.grabacionIniciada = false;
      this.media.stop();
      this.audioSubject.subscribe(record => {
        
        this.audio = record;
        this.audio2 = 'data:audio/wav;base64,' + record;
      })
      
    
    
  }
  contestarLlamada(meetCode: string) {
    this.router.navigate(['administrador','jitsi-meet', meetCode]);
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

}
