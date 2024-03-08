import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CertificadoLocacion } from '@models/catalogo/certificado-locacion';

@Injectable()
export class CertificadoLocacionService {
  private dataUrl = 'certificadoLocacion/';

  constructor(public http: HttpClient) {}

  public agregar(certificadoLocacionList: CertificadoLocacion[]) {
    return this.http.post<void>(this.dataUrl + 'agregar', certificadoLocacionList);
  }

}
