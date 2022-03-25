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
export class ComissoesService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getAll(): Observable<any> {
    return this.http.get(`${environment.baseUrl}${API.comissoes.buscarTodos}`)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter as comissões', []))
      );
  }

  getAllFiltro(data): Observable<any> {
    return this.http.post(`${environment.baseUrl}${API.comissoes.buscarTodosFiltro}`, data)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter as comissões', []))
      );
  }

  getDashboard(data): Observable<any> {
    return this.http.post(`${environment.baseUrl}${API.comissoes.dashboardMinhasComissoes}`, data)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter as minhas comissões', []))
      );
  }

  getPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idComissoes', id);
    return this.http.get(`${environment.baseUrl}${API.comissoes.buscarPorId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter a comissão', []))
      );
  }

  cadastrar(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.comissoes.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  deletarPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idComissoes', id);

    return this.http.get(`${environment.baseUrl}${API.comissoes.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir a comissão', []))
      );
  }
}
