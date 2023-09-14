import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ErrorHandlerService } from '../services/error-handler.service';
import { environment } from '../../environments/environment';
import { API } from '../services/api';

@Injectable({ providedIn: 'root' })
export class AuthService {

  constructor(
    private http: HttpClient,
    private errorHandlerService: ErrorHandlerService
  ) { }

  authenticate(username: string, password: string): Observable<any> {
    window.localStorage.setItem('accessToken', `username: ${username} - password: ${password}`);
    return of('Login');
  }

  login(username, senha): Observable<any> {
    return this.http.post(`${environment.baseUrl}${API.usuario.login}`, {username, senha})
      .pipe(
        tap(result => window.localStorage.setItem('accessToken', JSON.stringify(result))),
        catchError(this.errorHandlerService.errorHandler('Login ou senha inválido', []))
      );
  }

  cadastrar(data): Observable<any>  {
    return this.http.post(`${environment.baseUrl}${API.usuario.cadastrar}`, data)
      .pipe(
        tap((result: any) => window.localStorage.setItem('accessToken', JSON.stringify(result?.token))),
        catchError(this.errorHandlerService.errorHandler('Falha ao cadastrar', []))
      );
  }

  getToken(): any {
    return JSON.parse(window.localStorage.getItem('accessToken'));
  }

  getUnidade(unidade): any {
    return JSON.parse(unidade);
  }

  logout(): void {
    window.localStorage.removeItem('accessToken');
  }

  isAluno(): boolean {
    const token = this.getToken();
    return !!token?.user?.isAluno;
  }
}
