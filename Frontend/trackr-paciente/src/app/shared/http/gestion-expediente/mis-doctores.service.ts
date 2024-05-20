import { Injectable } from '@angular/core';
import { UsuarioDoctoresDto } from '../../Dtos/usuario-doctores-dto';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UsuarioDoctoresSelectorDto } from '../../Dtos/usuario-doctores-selector-dto';
import { UsuarioDoctorDto } from '../../Dtos/usuario-doctor-dto';
import { Usuario } from '@models/usuario';

@Injectable({
  providedIn: 'root'
})
export class MisDoctoresService {

  private url = 'expedienteDoctor/';
  constructor(public http: HttpClient) { }

  consultarExpediente(): Observable<UsuarioDoctoresDto[]> {
    return this.http.get<UsuarioDoctoresDto[]>(this.url);
  }

  consultarExpedienteConImagenes(): Observable<UsuarioDoctoresDto[]> {
    return this.http.get<UsuarioDoctoresDto[]>(this.url + "conImagenes");
  }

  consultarSelector(): Observable<UsuarioDoctoresSelectorDto[]> {
    return this.http.get<UsuarioDoctoresSelectorDto[]>(this.url + "selector")
  }

  consultarPorUsuarioParaSelector(): Observable<UsuarioDoctoresSelectorDto[]> {
    return this.http.get<UsuarioDoctoresSelectorDto[]>(this.url + "selectorPorUsuario")
  }

  public eliminar(doctor: UsuarioDoctorDto) {
    const options = {
      body: doctor // Pasar el objeto doctor en el cuerpo de la solicitud DELETE
    };

    return this.http.delete(this.url, options);
  }


  public agregar(doctor: UsuarioDoctorDto) {
    return this.http.post(this.url, doctor);
  }

}
