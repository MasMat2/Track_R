import { Injectable } from '@angular/core';
import { UnidadMedidaFormularioCapturaDto } from '../../dtos/catalogo/unidad-medida-formulario-captura-dto';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UnidadMedidaGridDto } from '@dtos/catalogo/unidad-medida-grid-dto';

@Injectable({
  providedIn: 'root'
})
export class UnidadMedidaService {

private dataUrl = 'unidadMedida/';

constructor(private http: HttpClient) {}

consultarParaFormulario(idUnidadMedida: number): Observable<UnidadMedidaFormularioCapturaDto> {
  return this.http.get<UnidadMedidaFormularioCapturaDto>(this.dataUrl + `formulario/${idUnidadMedida}`);
}

consultarParaGrid(): Observable<UnidadMedidaGridDto[]> {
  return this.http.get<UnidadMedidaGridDto[]>(this.dataUrl + 'grid');
}

agregar(unidadMedida: UnidadMedidaFormularioCapturaDto): Observable<void>
{
  return this.http.post<void>(this.dataUrl, unidadMedida);
} 


editar(unidadMedida: UnidadMedidaFormularioCapturaDto): Observable<void> {
  return this.http.put<void>(this.dataUrl, unidadMedida);
}

eliminar(idUnidadMedida: number): Observable<void> {
  return this.http.delete<void>(this.dataUrl + `${idUnidadMedida}`);
}
}
