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
export class VideoAulaService {
  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  buscarUltimaSessao(id): Observable<any> {
    this.animationsService.showProgressBar(true);    
    const params: HttpParams = new HttpParams().set('matriculaId', id);
    return this.http.get(`${environment.baseUrl}${API.videoAula.buscarUltimaSessao}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter a ultima sessão', []))
      );
  }

  buscarPorMateria(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('materiaId', id);
    return this.http.get(`${environment.baseUrl}${API.videoAula.buscarPorMateria}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter as aulas', []))
      );
  }

  getPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('aulaOnlineId', id);
    return this.http.get(`${environment.baseUrl}${API.videoAula.buscarPorId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter a aula', []))
      );
  }

  cadastrar(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.videoAula.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  salvarUltimaSessao(data): Observable<any> {
    return this.http.post(`${environment.baseUrl}${API.videoAula.salvarUltimaSessao}`, data);
  }

  deletePorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('aulaOnlineId', id);

    return this.http.get(`${environment.baseUrl}${API.videoAula.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha excluir', []))
      );
  }
}
