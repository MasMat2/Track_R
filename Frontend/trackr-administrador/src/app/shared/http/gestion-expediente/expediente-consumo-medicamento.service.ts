import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ExpedienteConsumoMedicamentoGridDto } from '@dtos/gestion-expediente/expediente-consumo-medicamento-grid-dto';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ExpedienteConsumoMedicamentoService {
    private dataUrl = 'expedienteConsumoMedicamento/';

    constructor(private http: HttpClient) {}
    
    public consultarParaGrid(idUsuario: number): Observable<ExpedienteConsumoMedicamentoGridDto[]> {
        return this.http.get<ExpedienteConsumoMedicamentoGridDto[]>(this.dataUrl + `consultarParaGrid/${idUsuario}`);
    }
    
}