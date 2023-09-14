import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environment';
import { DadosCDB } from '../models/DadosCDB';

@Injectable({
    providedIn: 'root'
})
export class InvestimentoService {

    constructor(private httpClient: HttpClient) { }

    private readonly baseURL = environment.endPoint;
    
    calcularRendimento(investimento: DadosCDB): Observable<DadosCDB> {
        var retorno = this.httpClient.post<DadosCDB>(`${this.baseURL}/CalcularRendimento`, investimento);
        console.log('Método calcularRendimento chamado com investimento:', investimento);
        return retorno;
    }
}
