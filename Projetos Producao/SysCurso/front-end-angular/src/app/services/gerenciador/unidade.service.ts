import { Injectable } from '@angular/core';
import { HttpClient, HttpEventType, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { ErrorHandlerService } from '../error-handler.service';
import { AnimationsService } from '../animations.service';
import { environment } from '../../../environments/environment';
import { API } from '../api';
import { AuthService } from 'src/app/security/auth.service';

@Injectable({
  providedIn: 'root'
})
export class UnidadeService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService,
    private authService: AuthService,

  ) { }

  getAll(): Observable<any> {
    const usuario = this.authService.getToken();
    const params: HttpParams = new HttpParams().set('usuarioLogadoId', usuario?.user?.id);
    return this.http.get(`${environment.baseUrl}${API.unidade.buscarTodos}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter as unidades', []))
      );
  }

  getUnidadesTicket(): Observable<any> {
    const usuario = this.authService.getToken();
    const params: HttpParams = new HttpParams().set('usuarioLogadoId', usuario?.user?.id);
    return this.http.get(`${environment.baseUrl}${API.unidade.buscarUnidadesTicket}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter as unidades', []))
      );
  }

  getPorId(id): Observable<any> {
    const params: HttpParams = new HttpParams().set('idUnidade', id);

    return this.http.get(`${environment.baseUrl}${API.unidade.buscarPorId}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter unidade', []))
      );
  }

  postUnidadeForm(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.unidade.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  deletarPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idUnidade', id);

    return this.http.get(`${environment.baseUrl}${API.unidade.deletarPorId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir unidade', []))
      );
  }

  uploadFoto(data) {
    this.animationsService.showProgressBar(true);
    return this.http.post<any>(`${environment.baseUrl}${API.unidade.uploadFoto}`, data, { reportProgress: true, observe: 'events' })
      .pipe(
        map((event) => {
          switch (event.type) {
            case HttpEventType.UploadProgress:
              // Uploading progress
              const progress = Math.round(100 * event.loaded / event.total);
              return { status: 'progress', progress };
            case HttpEventType.Response:
              // SnackBar success
              this.animationsService.showProgressBar(false);
              this.animationsService.showSuccessSnackBar('Enviado com sucesso');
              return { status: 'done', response: event.body };
            default:
              return;
          }
        }),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar anexo', []))
      );
  }

  getImg(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('unidadeId', id);
    return this.http.get(`${environment.baseUrl}${API.unidade.selecionarFoto}`, { params })
      .pipe(
        tap(res => this.animationsService.showProgressBar(false)),
        map((res: { extensao: string, foto: string, alunoId: number }) => {
          const { extensao, foto } = res;
          if(!extensao || !foto) return null;
          return `data:${extensao};base64,${foto}`
        }),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter imagem da unidade', []))
      );
  }

}
