import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PacientesPorPadecimientoDTO } from '@dtos/padecimientos/pacientes-por-padecimiento-dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PadecimientoService {
  private readonly endpoint: string = 'padecimiento/';

  constructor(private http: HttpClient) { }

  public consultarPacientesPorPadecimiento(): Observable<PacientesPorPadecimientoDTO[]> {
    return this.http.get<PacientesPorPadecimientoDTO[]>(this.endpoint + 'pacientesPorPadecimiento');
  }
}
