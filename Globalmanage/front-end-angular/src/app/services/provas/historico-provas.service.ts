import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ErrorHandlerService } from '../error-handler.service';
import { AnimationsService } from '../animations.service';
import { environment } from '../../../environments/environment';
import { API } from '../api';

@Injectable({
  providedIn: 'root'
})

export class HistoricoProvasService {
  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  ListaColegioAutorizadoExcel(filtro: any): Observable<any> {
    return this.http.post(`${environment.baseUrl}${API.historicoProvas.ListaColegioAutorizadoExcel}`, filtro)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os colégios', []))
      );
  }
  
  ListaGeralDeInscritosParaProvaExcel(filtro: any): Observable<any> {
    return this.http.post(`${environment.baseUrl}${API.historicoProvas.ListaGeralDeInscritosParaProvaExcel}`, filtro)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os colégios', []))
      );
  }

  ListaDeChamadaOnibusExcel(filtro: any): Observable<any> {
    return this.http.post(`${environment.baseUrl}${API.historicoProvas.ListaDeChamadaOnibusExcel}`, filtro)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os colégios', []))
      );
  }

  getNumeroOnibus(idUnidade: string, dataInicioMatricula: string, dataFimMatricula: string): Observable<any> {
    const params: HttpParams = new HttpParams().set('idUnidade', idUnidade)
                                               .set('dataInicioMatricula', dataInicioMatricula)
                                               .set('dataFimMatricula', dataFimMatricula);
    return this.http.get(`${environment.baseUrl}${API.historicoProvas.NumeroOnibus}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os colégios', []))
      );
  }
}