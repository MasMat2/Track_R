import { Component, Input } from '@angular/core';
import { ChatMensajeDTO } from '@dtos/chats/chat-mensaje-dto';
import { ChatMensajeHubService } from '../../../shared/services/chat-mensaje-hub.service';
import { ChatPersonaService } from '../../../shared/http/chats/chat-persona.service';
import { ChatPersonaSelectorDTO } from '@dtos/chats/chat-persona-selector-dto';
import { ArchivoService } from '../../../shared/http/archivo/archivo.service';
import { ArchivoFormDTO } from '@dtos/archivos/archivo-form-dto';

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
  protected archivo ?: File;

  constructor(private ChatMensajeHubService:ChatMensajeHubService,
              private ChatPersonaService:ChatPersonaService,
              private ArchivoService:ArchivoService) {}
  
  ngOnInit(){
    this.obtenerIdUsuario();
    this.obtenerPersonasEnChat();
  }

  ngOnChanges(){
    this.obtenerPersonasEnChat();
  }

  obtenerPersonasEnChat(){
    this.ChatPersonaService.obtenerPersonasEnChatSelector(this.idChat).subscribe(res =>{
      this.personas = res;
    })
  }

  async enviarMensaje(): Promise<void>{
    let msg: ChatMensajeDTO = {
      fecha: new Date(),
      idChat: this.idChat,
      mensaje: this.msg,
      idPersona:5333
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

    this.ChatMensajeHubService.enviarMensaje(msg);
    if(this.mensajes.length == 0){
      this.ChatMensajeHubService.chatMensaje$.subscribe(res => {
        this.mensajes = res.find(array => array.some(x => x.idChat === this.idChat)) || [];
      })
    }
    this.msg = "";

    this.archivo = undefined;

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

  downloadFile(fileBase64:string) {
    // Decodificar la cadena Base64
    console.log(fileBase64)
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
    a.download = 'archivo_descargado.png'; // Nombre del archivo
    a.click();

    // Limpiar el object URL después de la descarga
    URL.revokeObjectURL(url);
  }
}
