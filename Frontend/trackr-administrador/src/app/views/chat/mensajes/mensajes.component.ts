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
  protected archivo: File;

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

  enviarMensaje(): void{
    let msg: ChatMensajeDTO = {
      fecha: new Date(),
      idChat: this.idChat,
      mensaje: this.msg,
      idPersona:5333
    }

    this.ChatMensajeHubService.enviarMensaje(msg);
    if(this.mensajes.length == 0){
      this.ChatMensajeHubService.chatMensaje$.subscribe(res => {
        this.mensajes = res.find(array => array.some(x => x.idChat === this.idChat)) || [];
      })
    }
    this.msg = "";

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

  async subirArchivo(){
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
