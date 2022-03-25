import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpEventType } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';
import { ErrorHandlerService } from '../error-handler.service';
import { AnimationsService } from '../animations.service';
import { environment } from '../../../environments/environment';
import { API } from '../api';

@Injectable({
  providedIn: 'root'
})
export class AlunoService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getAll(): Observable<any> {
    return this.http.get(`${environment.baseUrl}${API.aluno.buscarTodos}`)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter alunos', []))
      );
  }

  getAllFilter(data): Observable<any> {
    return this.http.post(`${environment.baseUrl}${API.aluno.filtrarAluno}`, data)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter alunos', []))
      );
  }

  getPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idAluno', id);
    return this.http.get(`${environment.baseUrl}${API.aluno.buscarPorId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o aluno', []))
      );
  }

  getPorCpf(cpf): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('cpf', cpf);
    return this.http.get(`${environment.baseUrl}${API.aluno.buscarPorCPF}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o aluno', []))
      );
  }

  cadastrar(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.aluno.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  deletarPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idAluno', id);

    return this.http.get(`${environment.baseUrl}${API.aluno.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir aluno', []))
      );
  }

  upload(data) {
    this.animationsService.showProgressBar(true);
    return this.http.post<any>(`${environment.baseUrl}${API.aluno.uploadFoto}`, data, { reportProgress: true, observe: 'events' })
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
    const params: HttpParams = new HttpParams().set('idAluno', id);
    return this.http.get(`${environment.baseUrl}${API.aluno.selecionarFoto}`, { params })
      .pipe(
        tap(res => this.animationsService.showProgressBar(false)),
        map((res: { extensao: string, foto: string, alunoId: number }) => {
          const { extensao, foto } = res;
          if(!extensao || !foto) return null;
          return `data:${extensao};base64,${foto}`
        }),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter imagem de perfil', []))
      );
  }
}
