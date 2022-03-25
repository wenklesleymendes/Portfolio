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
export class MatriculaAlunoService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  cadastrar(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.matriculaAluno.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  buscarPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('matriculaId', id);
    return this.http.get(`${environment.baseUrl}${API.matriculaAluno.buscarPorId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter matrícula', []))
      );
  }

  buscarMinhasMatriculas(id, usuarioLogadoId): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('alunoId', id).set('usuarioLogadoId', usuarioLogadoId);
    return this.http.get(`${environment.baseUrl}${API.matriculaAluno.buscarMinhasMatriculas}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter matrícula', []))
      );
  }

  consultarMeusProfessores(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('matriculaId', id);
    return this.http.get(`${environment.baseUrl}${API.matriculaAluno.consultarMeusProfessores}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter professores', []))
      );
  }

  jaExistenteMatricula(alunoId, usuarioLogadoId): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('alunoId', alunoId).set('usuarioLogadoId', usuarioLogadoId);
    return this.http.get(`${environment.baseUrl}${API.matriculaAluno.jaExistenteMatricula}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao consultar matriculas', []))
      );
  }

  consultarDocumentosPendentes(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('matriculaId', id);
    return this.http.get(`${environment.baseUrl}${API.matriculaAluno.consultarDocumentosPendentes}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter documentos', []))
      );
  }

  gerarPendenciaDocumental(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('matriculaId', id);
    return this.http.get(`${environment.baseUrl}${API.matriculaAluno.gerarPendenciaDocumental}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter documentos', []))
      );
  }

  deletar(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('matriculaId', id);
    return this.http.get(`${environment.baseUrl}${API.matriculaAluno.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir matrícula', []))
      );
  }
}
