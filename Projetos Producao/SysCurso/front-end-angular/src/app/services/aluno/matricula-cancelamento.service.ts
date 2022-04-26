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
export class MatriculaCancelamentoService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService) { }

  buscarPorMatriculaId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('matriculaId', id);
    return this.http.get(`${environment.baseUrl}${API.cancelamentoMatricula.buscarPorMatriculaId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter condições de canselamento', []))
      );
  }

  efetuarCancelamento(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.cancelamentoMatricula.efetuarCancelamento}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao salvar pedido de canselamento', []))
      );
  }

  gerarMultaCancelamento(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.cancelamentoMatricula.gerarMultaCancelamento}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao salvar pedido de canselamento', []))
      );
  }

  salvarAutorizacaoIsencao(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.cancelamentoMatricula.salvarAutorizacaoIsencao}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao salvar pedido de canselamento', []))
      );
  }

  gerarCartaCancelamento(matriculaId, usuarioLogadoId, motivoCancelamento) {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('matriculaId', matriculaId)
                                               .set('usuarioLogadoId', usuarioLogadoId)
                                               .set('motivoCancelamento', motivoCancelamento);

    this.http.get(`${environment.baseUrl}${API.cancelamentoMatricula.gerarCartaCancelamento}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandlerAnexo('Falha ao baixar anexo', [])),
      )
      .subscribe(res => {
        if (res?.status === 'error') return;
        if (res) {
          const b64Data = res;
          const blob = this.b64toBlob(b64Data, 'application/pdf');
          const url = window.URL.createObjectURL(blob);
          const link = document.createElement('a');
          link.href = url;
          link.download = 'Carta de Cancelamento.pdf';
          link.click();
        } else {
          this.animationsService.showErrorSnackBar('Falha ao baixar Carta de Cancelamento');
        }
        this.animationsService.showProgressBar(false)
      });
  }

  private b64toBlob = (b64Data, contentType = '', sliceSize = 512) => {
    const byteCharacters = atob(b64Data);
    const byteArrays = [];

    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
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
}
