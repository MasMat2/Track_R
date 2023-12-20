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

@Component({
  selector: 'app-barra-chats',
  templateUrl: './BarraChats.component.html',
  styleUrls: ['./BarraChats.component.scss'],
  standalone: true,
  imports: [TableModule, CommonModule,IonicModule,HeaderComponent],
})
export class BarraChatsComponent {
  protected chats$: Observable<ChatDTO[]>;
  protected chats: ChatDTO[];
  protected chatMensajes$: Observable<ChatMensajeDTO[][]>;
  protected mensajes: ChatMensajeDTO[][];
  protected misDoctores: UsuarioDoctoresDto[];
  //@Output() idChatPadre = new EventEmitter<number>();
  //@Input() ultmoMensajes: string[];

  constructor(private router: Router,
              private ChatHubServiceService:ChatHubServiceService,
              private chatMensajeHubService:ChatMensajeHubService,
              private archivoService : ArchivoService,
              private sanitizer : DomSanitizer,
              private doctoresService : MisDoctoresService) {}

  ionViewWillEnter(){
    this.obtenerChats()
    this.consultarDoctores();
  }

  obtenerChats() {
    this.chats$ = this.ChatHubServiceService.chat$;
    this.chats$.subscribe((chats) => {

      chats.forEach((chat) => {
        this.archivoService.obtenerUsuarioImagen(chat.idCreadorChat).subscribe((imgaen) => {
          let objectURL = URL.createObjectURL(imgaen);
          let urlImagen = objectURL;
          let url = this.sanitizer.bypassSecurityTrustUrl(urlImagen);
          chat.urlImagen = url;
        });
      });
      
      this.chats = chats;
      this.obtenerUltimoMensaje();
    });
  }

  obtenerMensajes() {
    this.chatMensajes$ = this.chatMensajeHubService.chatMensaje$;

    this.chatMensajes$.subscribe((res) => {
      this.mensajes = res;
      this.obtenerUltimoMensaje();
    });
  }

  obtenerUltimoMensaje():void{
    if(this.mensajes){
      let ultimoMensaje = this.mensajes.map(arr => {console.log(arr); return arr[arr.length - 1]?.mensaje || ""})
      this.chats.forEach((x,index) => {x.ultimoMensaje = ultimoMensaje[index]})
    }
  }

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
      doctores.forEach((doctor) => { 
        this.archivoService.obtenerUsuarioImagen(doctor.idUsuarioDoctor).subscribe((imgaen) => {
          let objectURL = URL.createObjectURL(imgaen);
          let urlImagen = objectURL;
          let url = this.sanitizer.bypassSecurityTrustUrl(urlImagen);
          doctor.urlImagen = url;
        });
      }
      )
      
      this.misDoctores = doctores;
    }));
  }
}
