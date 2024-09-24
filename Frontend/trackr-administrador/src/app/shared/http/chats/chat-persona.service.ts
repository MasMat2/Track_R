import { Injectable } from '@angular/core';
import { ChatPersonaFormDTO } from '../../dtos/chats/chat-persona-form-dto';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { ChatPersonaSelectorDTO } from '@dtos/chats/chat-persona-selector-dto';

@Injectable({
  providedIn: 'root'
})
export class ChatPersonaService {
  private dataUrl = 'ChatPersona/'
  private idChatPadreSource = new Subject<number>();
  idChatPadre$ = this.idChatPadreSource.asObservable();

  constructor(private http:HttpClient) { }

  emitirIdChatPadre(idChat:number){
    this.idChatPadreSource.next(idChat);
  }

  agregarPersonas(ChatPersonaFormDTO:ChatPersonaFormDTO): Observable<void>{
    return this.http.post<void>(this.dataUrl,ChatPersonaFormDTO)
  }

  obtenerIdUsuario():Observable<number>{
    return this.http.get<number>(`${this.dataUrl}IdUsuario`)
  }

  obtenerIdPacientesPadecimiento(idPadecimiento:number):Observable<number[]>{
    return this.http.get<number[]>(`${this.dataUrl}Padecimiento/${idPadecimiento}`)
  }

  obtenerPersonasEnChatSelector(idChat:number):Observable<ChatPersonaSelectorDTO[]>{
    return this.http.get<ChatPersonaSelectorDTO[]>(`${this.dataUrl}PersonasEnChat/${idChat}`)
  }
}
