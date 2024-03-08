import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ChatPersonaFormDTO } from '../../Dtos/Chat/chat-persona-form-dto';

@Injectable({
  providedIn: 'root'
})
export class ChatPersonaService {
  private dataUrl = 'ChatPersona/'

  constructor(private http:HttpClient) { }

  agregarPersonas(ChatPersonaFormDTO:ChatPersonaFormDTO): Observable<void>{
    return this.http.post<void>(this.dataUrl,ChatPersonaFormDTO)
  }

  obtenerIdUsuario():Observable<number>{
    return this.http.get<number>(`${this.dataUrl}IdUsuario`)
  }

  obtenerIdPacientesPadecimiento(idPadecimiento:number):Observable<number[]>{
    return this.http.get<number[]>(`${this.dataUrl}Padecimiento/${idPadecimiento}`)
  }
}
