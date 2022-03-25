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
export class TransporteProvaService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  buscarProximoOnibus(agendaProvaId, unidadeId): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('agendaProvaId', agendaProvaId).set('unidadeId', unidadeId);
    return this.http.get(`${environment.baseUrl}${API.transporteProva.buscarProximoOnibus}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter transporte', []))
      );
  }

  buscarOnibus(UnidadeTransporteProvaId): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('UnidadeTransporteProvaId', UnidadeTransporteProvaId);
    return this.http.get(`${environment.baseUrl}${API.transporteProva.buscarOnibus}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter transporte', []))
      );
  }
}
