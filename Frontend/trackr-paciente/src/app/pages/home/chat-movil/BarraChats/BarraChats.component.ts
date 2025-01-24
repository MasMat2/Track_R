import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { TableModule } from 'primeng/table';
import { map, Observable, switchMap, tap } from 'rxjs';
import { ChatDTO } from 'src/app/shared/Dtos/Chat/chat-dto';
import { ChatHubServiceService } from '../../../../services/dashboard/chat-hub-service.service';
import { IonicModule, ModalController } from '@ionic/angular';
import { MisDoctoresService } from '@http/gestion-expediente/mis-doctores.service';
import { UsuarioDoctoresDto } from 'src/app/shared/Dtos/usuario-doctores-dto';
import { ChatPersonaService } from '../../../../shared/http/chat/chat-persona.service';
import { FormsModule } from '@angular/forms';
import { addIcons } from 'ionicons';
import { NuevoChatDoctoresComponent } from './nuevo-chat-doctores/nuevo-chat-doctores.component';
import { SearchbarComponent } from '@sharedComponents/searchbar/searchbar.component';
import { ChatMensajeDTO } from 'src/app/shared/Dtos/Chat/chat-mensaje-dto';
import { ChatMensajeHubService } from 'src/app/services/dashboard/chat-mensaje-hub.service';
import { FechaService } from '@services/fecha.service';
import { TabService } from 'src/app/services/dashboard/tab.service';

@Component({
  selector: 'app-barra-chats',
  templateUrl: './BarraChats.component.html',
  styleUrls: ['./BarraChats.component.scss'],
  standalone: true,
  imports: [
    TableModule, 
    CommonModule,
    IonicModule,
    FormsModule,
    SearchbarComponent
  ],
  providers : [

  ]
})
export class BarraChatsComponent {
  protected chats$: Observable<ChatDTO[]>;
  protected chats: ChatDTO[];
  protected chatMensajes$: Observable<ChatMensajeDTO[][]>;
  protected mensajes: ChatMensajeDTO[][] = [];
  protected misDoctores: UsuarioDoctoresDto[];
  protected chatsFiltradosPorBusqueda: ChatDTO[];
  protected filtrando: boolean = false;
  private fallbackTimeoutId: any;


  constructor(
    private router: Router,
    private ChatHubServiceService:ChatHubServiceService,
    private chatMensajeHubService: ChatMensajeHubService,
    private doctoresService : MisDoctoresService,
    private modalCtrl:ModalController,
    private fechaService: FechaService,
    private tabService : TabService
  ) {
    addIcons({
      'chat-plus': 'assets/img/svg/chat-plus.svg'
    });

    tabService.tabChange$.subscribe((tabId) => {
      if(tabId == "chat-movil"){
        this.consultarDoctores();
      }
    });
  }

  ngOnInit(){
    this.obtenerChats();

    // Fallback after 5 seconds:
    this.fallbackTimeoutId = setTimeout(async () => {
      if (!this.mensajes || this.mensajes.length === 0) {
        console.warn('[BarraChats] Fallback triggered');
        try {
          await this.chatMensajeHubService.obtenerMensajesDesdeServidor();
        } catch (err) {
          console.error('[BarraChats] Fallback fetch failed:', err);
        }
      }
    }, 5000);
    
    this.consultarDoctores();
  }

  
  //OBTENER SÓLO LOS CHATS
  /* private obtenerChats() {
    this.chats$ = this.ChatHubServiceService.chat$;
    this.chats$.pipe(
      map((data) => {
        return data.map(chat => {
          if (chat.imagenBase64 != null) {
            let base64String = "data:" + chat.tipoMime + ';base64,' + chat.imagenBase64;
            chat.urlImagen = base64String;
          }
          return chat;
        });
      })
    ).subscribe((chats) => {
      this.chats = chats;
    });

  } */

  //OBTENER LOS CHATS Y EL ULTIMO MENSAJE
  private obtenerChats() {
    console.log('[BarraChats] Iniciando obtención de chats');
    
    this.chats$ = this.ChatHubServiceService.chat$;
    this.chatMensajes$ = this.chatMensajeHubService.chatMensaje$;
    console.log('[BarraChats] Observables inicializados');
   
    this.chats$.pipe(
      map((chats) => {
        console.log('[BarraChats] Procesando chats:', chats);
        return chats.map(chat => {
          if (chat.imagenBase64 != null) {
            console.log('[BarraChats] Procesando imagen para chat');
            let base64String = "data:" + chat.tipoMime + ';base64,' + chat.imagenBase64;
            chat.urlImagen = base64String;
          }
          return chat;
        });
      }),
      switchMap((chats) => {
        console.log('[BarraChats] Chats procesados:', chats);
        this.chats = chats;
        return this.chatMensajes$;
      })
    ).subscribe((mensajes) => {
      console.log('[BarraChats] Mensajes recibidos:', mensajes);
      this.mensajes = mensajes;
      
      if (mensajes && mensajes.length > 0 && this.fallbackTimeoutId) {
        clearTimeout(this.fallbackTimeoutId);
      }

      this.obtenerUltimoMensaje();
      console.log('[BarraChats] Último mensaje actualizado');
    });
   }

  private consultarDoctores() {
    this.doctoresService.consultarExpedienteConImagenes().pipe(
      map((data) => {
        return data.map(doctor => {
          if(doctor.imagenBase64 != null){
            let base64String = "data:" + doctor.tipoMime + ';base64,' + doctor.imagenBase64;
            doctor.urlImagen = base64String;
          }
          return doctor;
        });
      })
    ).subscribe((doctores) => {
      this.misDoctores = doctores;
    });
  }
  
  protected enviarIdChat(idChat: number) {
    this.router.navigate(['home/chat-movil/chat',idChat]);
  }

  protected async mostrarListaDoctores(){
    const modal = await this.modalCtrl.create({
      component: NuevoChatDoctoresComponent,
      componentProps: {
        doctores: this.misDoctores
      }
    });

    modal .present();
  }

  protected listaVacia(): boolean{
    return (this.chats?.length == 0);
  }

  protected handleSearch(searchTerm: string): void {
    if( searchTerm == ""){
      this.filtrando = false;
      return
    }
    else{
      this.filtrando = true;
      this.chatsFiltradosPorBusqueda = this.chats.filter(chat => 
        chat.ultimoMensaje?.toLowerCase().includes(searchTerm.toLowerCase()) ||
        chat.titulo?.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }
  }

  private obtenerUltimoMensaje(): void {
    console.log('[BarraChats] Iniciando obtención de últimos mensajes');
   
    const ultimoMensaje = this.mensajes.map(arr => {
      console.log('[BarraChats] Procesando último mensaje para chat:', arr[0]?.idChat);
      return {
        mensajes: arr[arr.length - 1]?.mensaje || '',
        chat: arr[0]?.idChat || 0
      }
    });
   
    const fechaUltimoMensaje = this.mensajes.map(arr => {
      console.log('[BarraChats] Procesando fecha último mensaje para chat:', arr[0]?.idChat);
      return {
        fecha: arr[arr.length - 1]?.fecha || this.fechaService.obtenerFechaActualISOString(),
        chat: arr[0]?.idChat || 0
      }
    });
   
    this.chats.map(chat => {
      console.log('[BarraChats] Actualizando chat:', chat.idChat);
      chat.ultimoMensaje = ultimoMensaje.filter(y => y.chat == chat.idChat)[0]?.mensajes || '';
      chat.fechaUltimoMensaje = fechaUltimoMensaje.filter(y => y.chat == chat.idChat)[0]?.fecha || 
        this.fechaService.obtenerFechaActualISOString();
      return chat;
    });
    
    console.log('[BarraChats] Finalizada actualización de últimos mensajes');
   }
}
