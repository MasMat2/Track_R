import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICrudService } from './crud-service';

/**
 * CRUD Service que utiliza un mismo modelo para consultar, agregar y editar.
 * @implements ICrudService
 */
export class SingleModelCrudService<Modelo> implements ICrudService {
  constructor(
    protected readonly http: HttpClient,
    protected readonly entityName: string,
  ) { }

  public consultar(id: number): Observable<Modelo> {
    return this.http.get<Modelo>(`${this.entityName}/consultar/${id}`);
  }

  public agregar(cuerpo: Modelo): Observable<Modelo> {
    return this.http.post<Modelo>(`${this.entityName}/agregar`, cuerpo);
  }

  public editar(cuerpo: Modelo): Observable<void> {
    return this.http.put<void>(`${this.entityName}/editar`, cuerpo);
  }

  public eliminar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.entityName}/eliminar/${id}`);
  }
}