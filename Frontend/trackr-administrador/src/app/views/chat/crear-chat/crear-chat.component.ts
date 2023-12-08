import { Component } from '@angular/core';
import { EntidadEstructuraService } from '../../../shared/http/gestion-entidad/entidad-estructura.service';
import { ExpedienteTrackR } from '../../../shared/models/seguridad/expediente-trackr';
import { ExpedienteTrackrService } from '../../../shared/http/seguridad/expediente-trackr.service';
import { PadecimientoDTO } from '../../../../../../trackr-paciente/src/app/shared/Dtos/gestion-expediente/padecimiento-dto';
import { UsuarioExpedienteGridDTO } from '@dtos/seguridad/usuario-expediente-grid-dto';
import { ExpedientePadecimientoSelectorDTO } from '../../../shared/dtos/seguridad/expediente-padecimiento-selector-dto';
import { ChatHubServiceService } from '../../../shared/services/chat-hub-service.service';
import { ChatDTO } from '@dtos/chats/chat-dto';
import { SessionService } from '../../../shared/services/session.service';

@Component({
  selector: 'app-crear-chat',
  templateUrl: './crear-chat.component.html',
  styleUrls: ['./crear-chat.component.scss']
})
export class CrearChatComponent {
  protected tipo:number;
  protected padecimientos: ExpedientePadecimientoSelectorDTO[];
  protected expedientes: UsuarioExpedienteGridDTO[];
  protected padecimiento: number;
  protected personas: number[];
  protected tituloChat: string;

  constructor(private entidadEstructuraService:EntidadEstructuraService,
              private expedienteTrackrService:ExpedienteTrackrService,
              private ChatHubServiceService:ChatHubServiceService,
              private SessionService:SessionService) {}

  
  ngOnInit(){
    this.obtenerPacientes();
    this.obtenerPadecimientos();
  }

  obtenerPadecimientos(){
    this.entidadEstructuraService.consultarPadecimientosParaSelector().subscribe(res => {
      this.padecimientos = res
    })
  }

  obtenerPacientes(){
    this.expedienteTrackrService.consultarParaGrid().subscribe(res =>{
      this.expedientes = res;
      console.log(res)
    })
  }

  crearChat(){
    let idUsuario = this.SessionService.obtenerIdUsuarioSesion();
    this.personas.push(idUsuario || 0)
    let chat: ChatDTO ={
      fecha: new Date(),
      habilitado: true,
      titulo: this.tituloChat
    }
    this.ChatHubServiceService.agregarChat(chat,this.personas)

  }
  
  
}
