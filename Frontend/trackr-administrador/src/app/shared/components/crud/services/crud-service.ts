import { Observable } from 'rxjs';

export interface ICrudService {
  consultar(id: number): Observable<any>;
  agregar(cuerpo: any): Observable<any>;
  editar(cuerpo: any): Observable<void>;
  eliminar(id: number): Observable<void>;
}