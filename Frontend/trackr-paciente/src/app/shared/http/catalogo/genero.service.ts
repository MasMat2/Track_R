import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GeneroSelectorDto } from '../../Dtos/catalogo/genero-selector-dto';

@Injectable({
  providedIn: 'root'
})
export class GeneroService {

  private dataUrl = 'genero/';

  constructor(private http: HttpClient) { }

  public consultarGeneros(){
    return this.http.get<GeneroSelectorDto[]>(this.dataUrl);
  }

}
