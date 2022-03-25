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
export class TurmaService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getAll(): Observable<any> {
    return this.http.get(`${environment.baseUrl}${API.turma.buscarTodos}`)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter as turmas', []))
      );
  }

  getPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idTurma', id);

    return this.http.get(`${environment.baseUrl}${API.turma.buscarPorId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter a turma', []))
      );
  }

  getTurmasDisponiveis(data: any): Observable<any> {
    this.animationsService.showProgressBar(true);
    const { cursoId, unidadeId, usuarioLogadoId } = data;
    const params: HttpParams = new HttpParams().set('cursoId', cursoId)
                                               .set('unidadeId', unidadeId)
                                               .set('usuarioLogadoId', usuarioLogadoId);

    return this.http.get(`${environment.baseUrl}${API.turma.buscarTurmasDisponiveis}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter as turmas', []))
      );
  }

  cadastrar(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.turma.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  transferirDeTurma(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.turma.transferirDeTurma}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao mudar turma', []))
      );
  }

  deletarPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('id', id);

    return this.http.get(`${environment.baseUrl}${API.turma.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir a turma', []))
      );
  }

}
