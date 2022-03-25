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
export class UsuarioService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getAll(id): Observable<any> {

    const params: HttpParams = new HttpParams().set('idUsuario', id);
    return this.http.get(`${environment.baseUrl}${API.usuario.buscarTodos}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os usuários', []))
      );
  }

  getPorId(id): Observable<any> {

    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idUsuario', id);
    return this.http.get(`${environment.baseUrl}${API.usuario.buscarPorId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o usuário', []))
      );
  }

  getFiltrar(data): Observable<any> {

    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.usuario.filtrarUsuario}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o usuário', []))
      );
  }

  getBusarUsuarioPorUnidade(data): Observable<any> {

    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.usuario.buscarPorUnidade}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o usuário', []))
      );
  }

  login(data): Observable<any> {

    return this.http.post(`${environment.baseUrl}${API.usuario.login}`, data)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao logar', []))
      );
  }

  cadastrar(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.usuario.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  getUsuarioAtendente(): Observable<any> {
    // //debugger
    return this.http.get(`${environment.baseUrl}${API.usuario.buscarAtendente}`)
      .pipe(
        catchError(this.errorHandlerService
          .errorHandler('Falha ao obter atendende.', []))
      );
  }

  desativarAtivar(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idUsuario', id);

    return this.http.get(`${environment.baseUrl}${API.usuario.desativarAtivar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao mudar o status do usuário', []))
      );
  }

  deletarPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('id', id);

    return this.http.get(`${environment.baseUrl}${API.usuario.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir o usuário', []))
      );
  }
}
