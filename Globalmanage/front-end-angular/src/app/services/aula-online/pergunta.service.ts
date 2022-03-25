import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';
import { ErrorHandlerService } from '../error-handler.service';
import { AnimationsService } from '../animations.service';
import { environment } from '../../../environments/environment';
import { API } from '../api';

@Injectable({
  providedIn: 'root'
})
export class PerguntaService {
  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  buscarPorVideoAula(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('videoAulaId', id);
    return this.http.get(`${environment.baseUrl}${API.pergunta.buscarPorVideoAula}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter as questões', []))
      );
  }

  getPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('perguntaId', id);
    return this.http.get(`${environment.baseUrl}${API.pergunta.buscarPorId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter a aula', []))
      );
  }

  cadastrar(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.pergunta.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  deletarPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('perguntaId', id);

    return this.http.get(`${environment.baseUrl}${API.pergunta.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir', []))
      );
  }

  getImg(id, extensao): Observable<any> {
    // this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idAnexo', id);
    return this.http.get(`${environment.baseUrl}${API.anexo.download}`, { params })
      .pipe(
        // tap(res => this.animationsService.showProgressBar(false)),
        // map(foto => {
        //   return 'TESTE'
        //   if(!foto) return null;
        //   return `data:${extensao};base64, banana`
        //   // return `data:${extensao};base64,${foto}`
        // }),
        catchError(this.errorHandlerService.errorHandlerAnexo('Falha ao obter imagem', []))
      );
  }
}
