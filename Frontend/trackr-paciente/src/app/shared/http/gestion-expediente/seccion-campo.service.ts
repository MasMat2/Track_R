import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PadecimientoMuestraDTO } from '@dtos/gestion-expediente/padecimiento-muestra-dto';

@Injectable()
export class SeccionCampoService {
  private dataUrl = 'seccionCampo/';

  constructor(public http: HttpClient) {}

  consultarPorSeccion() {
    return this.http.get<PadecimientoMuestraDTO[]>(this.dataUrl + `seccionesPadecimientosGeneral/`);
  }

}
