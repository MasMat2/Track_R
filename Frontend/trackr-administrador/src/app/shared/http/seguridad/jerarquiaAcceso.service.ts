import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { JerarquiaAcceso } from '@models/seguridad/jerarquia-acceso';
import { SingleModelCrudService } from '@sharedComponents/crud/services/single-model-crud.service';

@Injectable()
export class JerarquiaAccesoService extends SingleModelCrudService<JerarquiaAcceso> {

  constructor(public override http: HttpClient) {
    super(http, "jerarquiaAcceso");
  }

  public consultarParaGrid(): Observable<JerarquiaAcceso[]> {
    return this.http.get<JerarquiaAcceso[]>(`${this.entityName}/consultarParaGrid`);
  }

  public consultarParaSelector(): Observable<JerarquiaAcceso[]> {
    return this.http.get<JerarquiaAcceso[]>(`${this.entityName}/consultarParaSelector`);
  }

}
