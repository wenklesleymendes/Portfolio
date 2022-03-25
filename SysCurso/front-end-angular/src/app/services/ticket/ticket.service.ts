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
export class TicketService {
  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getAll(): Observable<any> {
    return this.http.get(`${environment.baseUrl}${API.ticket.buscarTodos}`)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os tickets', []))
      );
  }

  getDashboard(usuarioLogadoId): Observable<any> {
    const params: HttpParams = new HttpParams().set('usuarioLogadoId', usuarioLogadoId);
    return this.http.get(`${environment.baseUrl}${API.ticket.consultarDashboard}`,{ params })
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os dashboards', []))
      );
  }

  getPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idTicket', id);
    return this.http.get(`${environment.baseUrl}${API.ticket.buscarMensagensTicket}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter a timeline', []))
      );
  }

  getTimeline(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('ticketId', id);
    return this.http.get(`${environment.baseUrl}${API.ticket.buscarMensagensTicket}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o ticket', []))
      );
  }

  filtrarTicket(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.ticket.filtrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao filtrar ticket', []))
      );
  }

  cadastrar(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.ticket.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  responderTicket(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.ticket.responderTicket}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  deletarPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('id', id);

    return this.http.get(`${environment.baseUrl}${API.ticket.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir o ticket', []))
      );
  }
}
