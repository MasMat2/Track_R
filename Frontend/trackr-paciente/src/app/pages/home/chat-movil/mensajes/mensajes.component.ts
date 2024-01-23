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

@Component({
  selector: 'app-mensajes',
  templateUrl: './mensajes.component.html',
  styleUrls: ['./mensajes.component.scss'],
  standalone: true,
  imports: [FormsModule, CommonModule, IonicModule,HeaderComponent],
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
  protected archivo ?: File =undefined;
  @ViewChild('fileInput') fileInput!: ElementRef;
  @ViewChild('scrollContainer') private scrollContainer: ElementRef;

  constructor(
    private ChatMensajeHubService: ChatMensajeHubService,
    private ChatPersonaService: ChatPersonaService,
    private router: ActivatedRoute,
    private route: Router,
    private ChatHubServiceService:ChatHubServiceService,
    private ArchivoService:ArchivoService
  ) {}

  ionViewWillEnter() {
    this.obtenerIdUsuario();
    this.obtenerIdChat();
  }

  obtenerIdChat(){
    this.router.params.subscribe(params => {
      this.idChat = Number(params['id'])
      this.obtenerMensajes();
      this.obtenerChat();
    })
  }

  obtenerChat(){
    this.ChatHubServiceService.chat$.subscribe(res => {
      this.chat = res.find(x => x.idChat == this.idChat) || {fecha: new Date(), habilitado: false , idCreadorChat: 0}
    })
  }

  async enviarMensaje(): Promise<void> {
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
      idArchivo: 0
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

  regresarBtn(){
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
  
}
