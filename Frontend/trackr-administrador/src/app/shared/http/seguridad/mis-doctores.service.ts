import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UsuarioDoctorDto } from "@dtos/seguridad/usuario-doctor-dto";
import { Observable } from "rxjs";


@Injectable({
  providedIn: 'root'
})
export class MisDoctoresService {

  private url = 'expedienteDoctor/';
  constructor(public http: HttpClient) { }



public eliminar(doctor: UsuarioDoctorDto) {
    const options = {
        body: doctor
    };
    return this.http.delete(this.url, options);
}

}
