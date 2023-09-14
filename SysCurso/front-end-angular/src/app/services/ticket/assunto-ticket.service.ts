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
export class AssuntoTicketService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getAll(): Observable<any> {
    return this.http.get(`${environment.baseUrl}${API.assuntoTicket.buscarTodos}`)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os assuntos dos tickets', []))
      );
  }

  getPorUnidadeDepartamento(idUnidade, idDepartamento): Observable<any> {
    this.animationsService.showProgressBar(true);
    let params: HttpParams;

    if (idUnidade && idDepartamento){
      params = new HttpParams().set('idUnidade', idUnidade).set('idDepartamento', idDepartamento);
    }
    else{
    if (idUnidade) {
      params = new HttpParams().set('idUnidade', idUnidade);
    }
    if (idDepartamento) {
      params = new HttpParams().set('idDepartamento', idDepartamento);
    }}

    return this.http.get(`${environment.baseUrl}${API.assuntoTicket.buscarPorUnidadeDepartamento}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os assuntos dos tickets', []))
      );
  }

  getPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idAssuntoTicket', id);
    return this.http.get(`${environment.baseUrl}${API.assuntoTicket.buscarPorId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o assunto do ticket', []))
      );
  }

  cadastrar(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.assuntoTicket.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  deletarPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('id', id);

    return this.http.get(`${environment.baseUrl}${API.assuntoTicket.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir o assunto do ticket', []))
      );
  }
}
