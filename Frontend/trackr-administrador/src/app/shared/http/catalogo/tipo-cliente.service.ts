import {Injectable} from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { TipoCliente } from '@models/catalogo/tipo-cliente';

@Injectable()
export class TipoClienteService {

  private dataUrl = 'tipoCliente/';

  constructor(public http: HttpClient) {}

  public consultarGeneral(): Observable<TipoCliente[]> {
    return this.http.get<TipoCliente[]>(this.dataUrl + 'consultarGeneral');
  }

}
