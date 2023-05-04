import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SatFormaPago } from '@models/facturacion/sat-forma-pago';

@Injectable()
export class SatFormaPagoService {
  private dataUrl = 'satFormaPago/';

  constructor(public http: HttpClient) { }

  public consultarParaSelector() {
    return this.http.get<SatFormaPago[]>(this.dataUrl + 'consultarParaSelector');
  }

}
