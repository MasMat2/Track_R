import { Injectable } from '@angular/core';
import { ConfirmarCorreoDto } from '../../Dtos/seguridad/confirmar-correo-dto';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ConfirmacionCorreoService {
  private dataUrl = 'confirmacionCorreo/'

  constructor(public http: HttpClient) { }

  public enviarCorreoConfirmacion(correoUsuario: ConfirmarCorreoDto):Observable<void>{
    return this.http.post<void>(this.dataUrl + 'enviarCorreoConfirmacion', correoUsuario);
  }


}
