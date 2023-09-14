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
export class CursoService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getCursos(): Observable<any> {
    return this.http.get(`${environment.baseUrl}${API.curso.buscarTodos}`)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os cursos', []))
      );
  }

  getCursosPorUnidade(data: any): Observable<any> {
    this.animationsService.showProgressBar(true);
    const { unidadeId, usuarioLogadoId } = data;
    const params: HttpParams = new HttpParams().set('unidadeId', unidadeId)
                                               .set('usuarioLogadoId', usuarioLogadoId);
    return this.http.get(`${environment.baseUrl}${API.curso.buscarPorUnidade}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os cursos', []))
      );
  }

  getCursoComMateria(): Observable<any> {
    return this.http.get(`${environment.baseUrl}${API.curso.buscarCursosComMaterias}`)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o curso matéria', []))
      );
  }

  getCursoPorId(id: any): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idCurso', id);
    return this.http.get(`${environment.baseUrl}${API.curso.buscarPorId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter curso', []))
      );
  }

  cadastrarCurso(data: any): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.curso.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao cadastrar curso', []))
      );
  }

  cadastrarMateria(data: any): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.curso.cadastrarMateria}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao cadastrar matéria', []))
      );
  }

  deletarCurso(id: any): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('id', id);

    return this.http.get(`${environment.baseUrl}${API.curso.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao deletar curso', []))
      );
  }

  deletarMateria(id: any): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idMateria', id);

    return this.http.get(`${environment.baseUrl}${API.curso.deletarMateria}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao deletar curso', []))
      );
  }
}