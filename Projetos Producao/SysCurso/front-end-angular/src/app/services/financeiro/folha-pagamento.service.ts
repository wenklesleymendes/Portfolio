import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap, mergeMap, map } from 'rxjs/operators';
import { ErrorHandlerService } from '../error-handler.service';
import { AnimationsService } from '../animations.service';
import { environment } from '../../../environments/environment';
import { API } from '../api';

@Injectable({
  providedIn: 'root'
})
export class FolhaPagamentoService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getAll(data): Observable<any> {
    return this.http.post(`${environment.baseUrl}${API.folhaPagamento.buscarTodos}`, data)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter as folhas de pagamento', []))
      );
  }

  getPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idFolhaPagamento', id);
    return this.http.get(`${environment.baseUrl}${API.folhaPagamento.buscarPorId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter a folha de pagamento', []))
      );
  }

  cadastrar(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.folhaPagamento.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  deletarPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idFolhaPagamento', id);

    return this.http.get(`${environment.baseUrl}${API.folhaPagamento.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir a folha de pagamento', []))
      );
  }

  download(id) {
    const filename: string = `Comprovante - ${new Date().getUTCDay()}/${new Date().getUTCMonth()}/${new Date().getUTCFullYear()}.pdf`;
    const contentType: string = '.pdf';
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idFolhaPagamento', id);

    this.http.get(`${environment.baseUrl}${API.folhaPagamento.downloadComprovanteBancario}`, { params })
      .pipe(
        catchError(res => {
            // This error it's thow because the respose itsn't JSON
            if (res && res['error'] && ['text']){
              const b64Data = res['error']['text'];
              return of(b64Data)
            } else {
              catchError(this.errorHandlerService.errorHandler('Falha ao baixar comprovante', []));
            }
        })
      )
      .subscribe(res => {
        const b64Data = res;
        const blob = this.b64toBlob(b64Data, contentType);
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = filename;
        link.click();
        this.animationsService.showProgressBar(false);
      })
  }

  imprimir(id, contentType = 'application/pdf') {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idFolhaPagamento', id);

    this.http.get(`${environment.baseUrl}${API.folhaPagamento.ImprimirReciboPagamento}`, { params })
      .pipe(
        catchError(res => {
            // This error it's thow because the respose itsn't JSON
            if (res && res['error'] && ['text']){
              const b64Data = res['error']['text'];
              return of(b64Data)
            } else {
              catchError(this.errorHandlerService.errorHandler('Falha ao imprimir recibo', []));
            }
        })
      )
      .subscribe(res => {
        const b64Data = res;
        const blob = this.b64toBlob(b64Data, contentType);
        const url = window.URL.createObjectURL(blob);
        this.animationsService.showProgressBar(false);
        window.open(url, '_blank');
      })
  }

  /**
   * @description Convert a base64 to Blob
   * @param b64Data Base64
   * @param contentType File's type
   * @param sliceSize Slice size
   * @returns Blob
   */
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
  };
}

