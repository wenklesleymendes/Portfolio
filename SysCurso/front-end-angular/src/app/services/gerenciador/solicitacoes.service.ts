import { Injectable } from '@angular/core';
import { HttpClient, HttpEventType, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { ErrorHandlerService } from '../error-handler.service';
import { AnimationsService } from '../animations.service';
import { environment } from '../../../environments/environment';
import { API } from '../api';

@Injectable({
  providedIn: 'root'
})
export class SolicitacoesService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getAll(): Observable<any> {
    return this.http.get(`${environment.baseUrl}${API.solicitacoes.buscarTodos}`)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter as solicitações', []))
      );
  }

  getPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('solicitacaoId', id);
    return this.http.get(`${environment.baseUrl}${API.solicitacoes.buscarPorId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter a solicitação', []))
      );
  }

  historicoSolicitacao(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('matriculaId', id);
    return this.http.get(`${environment.baseUrl}${API.solicitacaoAluno.historicoSolicitacao}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter histórico solicitação', []))
      );
  }

  buscarPorCursoId(id, matriculaId, usuarioId): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('cursoId', id).set('matriculaId', matriculaId).set('usuarioId', usuarioId);
    return this.http.get(`${environment.baseUrl}${API.solicitacoes.buscarPorCursoId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter a solicitação', []))
      );
  }

  cadastrar(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.solicitacoes.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  efetuarSolicitacao(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.solicitacaoAluno.efetuarSolicitacao}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  deletarPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('solicitacaoId', id);
    return this.http.get(`${environment.baseUrl}${API.solicitacoes.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir a solicitação', []))
      );
  }

  gerarSolicitacao({solicitacaoId, usuarioLogadoId, matriculaId}): void {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams()
                                  .set('solicitacaoId', solicitacaoId)
                                  .set('usuarioLogadoId', usuarioLogadoId)
                                  .set('matriculaId', matriculaId)
    this.http.get(`${environment.baseUrl}${API.solicitacaoAluno.gerarSolicitacao}`, { params })
    .pipe(
      tap(() => this.animationsService.showProgressBar(false)),
      catchError(this.errorHandlerService.errorHandlerAnexo('Falha ao gerar solicitação', []))
    )
    .subscribe(res => {
      if (res?.status === 'error') return;
      if(res) {
        const b64Data = res;
        const blob = this.b64toBlob(b64Data, 'application/pdf');
        const url = window.URL.createObjectURL(blob);
        window.open(url, '_blank');
      }else {
        this.animationsService.showSuccessSnackBar('Não possui solicitação');
      }
    });
  }

  private b64toBlob = (b64Data, contentType = '', sliceSize = 512) => {
    const byteCharacters = atob(b64Data);
    const byteArrays = [];
    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize ) {
      const slice = byteCharacters.slice(offset, offset + sliceSize);
      const byteNumbers = new Array(slice.length);
      for (let i = 0; i < slice.length; i++) {
        byteNumbers[i] = slice.charCodeAt(i);
      }
      const byteArray = new Uint8Array(byteNumbers);
      byteArrays.push(byteArray);
    }
    const blob = new Blob(byteArrays, { type: contentType });
    return blob;
  }

  uploadFoto(data) {
    this.animationsService.showProgressBar(true);
    return this.http.post<any>(`${environment.baseUrl}${API.solicitacoes.uploadFoto}`, data, { reportProgress: true, observe: 'events' })
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
    const params: HttpParams = new HttpParams().set('solicitacaoId', id);
    return this.http.get(`${environment.baseUrl}${API.solicitacoes.selecionarFoto}`, { params })
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

  removerFoto(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('solicitacaoId', id);
    return this.http.get(`${environment.baseUrl}${API.solicitacoes.removerFoto}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir a solicitação', []))
      );
  }
}
