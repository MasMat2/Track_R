import { Injectable } from '@angular/core';
import { ChatPersonaFormDTO } from '../../dtos/chats/chat-persona-form-dto';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatPersonaService {
  private dataUrl = 'ChatPersona/'

  constructor(private http:HttpClient) { }

  agregarPersonas(ChatPersonaFormDTO:ChatPersonaFormDTO): Observable<void>{
    return this.http.post<void>(this.dataUrl,ChatPersonaFormDTO)
  }
}
