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
export class CentroCustoService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getCentroCustoPorUnidade(id: string): Observable<any> {
    const params: HttpParams = new HttpParams().set('idUnidade', id);

    return this.http.get(`${environment.baseUrl}${API.centroCusto.buscarPorUnidade}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os centros de custo', []))
      );
  }

  cadastrarCentroCusto(data: any): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.centroCusto.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao cadastrar o centro de custo', []))
      );
  }

  deletarCentroCusto(id: any): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idCentroCusto', id);

    return this.http.get(`${environment.baseUrl}${API.centroCusto.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao deletar o centro de custo', []))
      );
  }
}
