import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { TableModule } from 'primeng/table';
import { Observable } from 'rxjs';
import { ChatDTO } from 'src/app/shared/Dtos/Chat/chat-dto';
import { ChatHubServiceService } from '../../../../services/dashboard/chat-hub-service.service';
import { IonicModule } from '@ionic/angular';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { ChatMensajeHubService } from '../../../../services/dashboard/chat-mensaje-hub.service';
import { ChatMensajeDTO } from 'src/app/shared/Dtos/Chat/chat-mensaje-dto';
import { ArchivoService } from '@services/archivo.service';
import { DomSanitizer } from '@angular/platform-browser';
import { MisDoctoresService } from '@http/seguridad/mis-doctores.service';
import { UsuarioDoctoresDto } from 'src/app/shared/Dtos/usuario-doctores-dto';
import { ChatPersonaService } from '../../../../shared/http/chat/chat-persona.service';
import { FormsModule } from '@angular/forms';
import { addIcons } from 'ionicons';
import { addCircle, chatboxOutline } from 'ionicons/icons'

@Component({
  selector: 'app-barra-chats',
  templateUrl: './BarraChats.component.html',
  styleUrls: ['./BarraChats.component.scss'],
  standalone: true,
  imports: [
    TableModule, 
    CommonModule,
    IonicModule,
    HeaderComponent, 
    FormsModule,
  ],
  providers : [

  ]
})
export class BarraChatsComponent {
  private idUsuario:number;
  protected chats$: Observable<ChatDTO[]>;
  protected chats: ChatDTO[];
  //protected chatMensajes$: Observable<ChatMensajeDTO[][]>;
  //protected mensajes: ChatMensajeDTO[][];
  protected misDoctores: UsuarioDoctoresDto[];
  protected usuarios: number[] = [];
  protected tituloChat:string;

  protected doctorSeleccionado: boolean = false;
  protected verListaDoctores: boolean = false;
  protected chatsFiltradosPorBusqueda: any;

  constructor(
    private router: Router,
    private ChatHubServiceService:ChatHubServiceService,
    //private chatMensajeHubService:ChatMensajeHubService,
    private archivoService : ArchivoService,
    private sanitizer : DomSanitizer,
    private doctoresService : MisDoctoresService,
    private ChatPersonaService:ChatPersonaService
  ) {
    addIcons({addCircle, chatboxOutline});
  }

  ionViewWillEnter(){
    this.obtenerChats()
    this.consultarDoctores();
    this.obtenerIdUsuario();
    //this.obtenerMensajes();
  }

  obtenerIdUsuario(){
    this.ChatPersonaService.obtenerIdUsuario().subscribe(res => {
      this.idUsuario = res;
    })
  }

  obtenerChats() {
    this.chats$ = this.ChatHubServiceService.chat$;
    this.chats$.subscribe((chats) => {

      /*chats.forEach((chat) => {
        this.archivoService.obtenerUsuarioImagen(chat.idCreadorChat).subscribe((imgaen) => {
          let objectURL = URL.createObjectURL(imgaen);
          let urlImagen = objectURL;
          let url = this.sanitizer.bypassSecurityTrustUrl(urlImagen);
          chat.urlImagen = url;
        });
      });*/
      
      this.chats = chats;
      this.chatsFiltradosPorBusqueda = chats;
      //this.obtenerUltimoMensaje();
    });
  }

  // obtenerMensajes() {
  //   this.chatMensajes$ = this.chatMensajeHubService.chatMensaje$;

  //   this.chatMensajes$.subscribe((res) => {
  //     this.mensajes = res;
  //     this.obtenerUltimoMensaje();
  //   });
  // }

  // obtenerUltimoMensaje():void{
  //   if(this.mensajes){
  //     let ultimoMensaje = this.mensajes.map(arr => {console.log(arr); return arr[arr.length - 1]?.mensaje || ""})
  //     this.chats.forEach((x,index) => {x.ultimoMensaje = ultimoMensaje[index]})
  //   }
  // }

  /*obtenerUltimoMensaje(): void {
    let ultimoMensaje = this.mensajes.map(
      (arr) => arr[arr.length - 1]?.mensaje || ''
    );
    this.chats.forEach((x, index) => {
      x.ultimoMensaje = ultimoMensaje[index];
    });
  }*/

  enviarIdChat(idChat: number) {
    this.router.navigate(['home/chat-movil/chat',idChat]);
  }

  consultarDoctores() {
    this.doctoresService.consultarExpediente().subscribe((doctores => {
      /*doctores.forEach((doctor) => { 
        this.archivoService.obtenerUsuarioImagen(doctor.idUsuarioDoctor).subscribe((imgaen) => {
          let objectURL = URL.createObjectURL(imgaen);
          let urlImagen = objectURL;
          let url = this.sanitizer.bypassSecurityTrustUrl(urlImagen);
          doctor.urlImagen = url;
        });
      }
      )*/
      
      this.misDoctores = doctores;
    }));
  }

  doctorClick(idDoctor:number){
    this.doctorSeleccionado = ! this.doctorSeleccionado
    this.usuarios = []
    this.usuarios.push(this.idUsuario);
    this.usuarios.push(idDoctor);
  }

  crearChat(){
    let chat: ChatDTO = {
      fecha: new Date(),
      habilitado: true,
      idCreadorChat: this.usuarios[this.usuarios.length - 1],
      titulo: this.tituloChat
    };

    this.ChatHubServiceService.agregarChat(chat,this.usuarios);
    this.tituloChat = "";
    this.usuarios = []
    this.doctorSeleccionado = false;
  }

  protected mostrarListaDoctores(){
    this.verListaDoctores = ! this.verListaDoctores;
  }

  protected buscarChat(event: any){
    const text = event.target.value;
    this.chatsFiltradosPorBusqueda = this.chats;
    if(text && text.trim() != ''){
      this.chatsFiltradosPorBusqueda = this.chatsFiltradosPorBusqueda.filter((chat: any) =>{
          return (chat.titulo.toLowerCase().indexOf(text.toLowerCase()) > -1 || chat.ultimoMensaje.toLowerCase().indexOf(text.toLowerCase()) > -1 );
      })
    }
  }
}
