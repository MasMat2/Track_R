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
import { ChatPersonaService } from '../../../shared/http/chats/chat-persona.service';

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
  protected tituloChat?: string;
  private idUsuario: number;
  protected idPacientesPadecimiento:number[];

  constructor(private entidadEstructuraService:EntidadEstructuraService,
              private expedienteTrackrService:ExpedienteTrackrService,
              private ChatHubServiceService:ChatHubServiceService,
              private SessionService:SessionService,
              private ChatPersonaService:ChatPersonaService) {}

  
  ngOnInit(){
    this.obtenerPacientes();
    this.obtenerPadecimientos();
    this.obtenerIdUsario()
  }

  obtenerPadecimientos(){
    this.entidadEstructuraService.consultarPadecimientosParaSelector().subscribe(res => {
      this.padecimientos = res
    })
  }

  obtenerPacientes(){
    this.expedienteTrackrService.consultarParaGrid().subscribe(res =>{
      this.expedientes = res;
    })
  }
  
  obtenerIdUsario(){
    this.ChatPersonaService.obtenerIdUsuario().subscribe(res => {
      this.idUsuario = res;
    })
  }

  crearChat(){
    if(this.tituloChat === ""){
      this.tituloChat = undefined;
    }
    if(this.tipo == 3){
      this.personas.push(this.idUsuario)
    let chat: ChatDTO ={
      fecha: new Date(),
      habilitado: true,
      titulo: this.tituloChat
    }
      this.ChatHubServiceService.agregarChat(chat,this.personas)
    }

    else if(this.tipo == 2){
      this.idPacientesPadecimiento.push(this.idUsuario);
      let chat:ChatDTO ={
        fecha: new Date(),
        habilitado: true,
        titulo: this.tituloChat
      }
      this.ChatHubServiceService.agregarChat(chat,this.idPacientesPadecimiento);
    }
    

  }

  obtenerIdPacientesPadecimiento(){
    this.ChatPersonaService.obtenerIdPacientesPadecimiento(this.padecimiento).subscribe(res =>{
      this.idPacientesPadecimiento = res;
    })
  }

  desabilitarTitulo(){
    if(this.personas){
      if(this.personas.length == 1){
        return true
      }
      return false
    }
    return false;
  }
  
  
}
