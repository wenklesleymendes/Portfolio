import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ErrorHandlerService } from '../error-handler.service';
import { AnimationsService } from '../animations.service';
import { environment } from '../../../environments/environment';
import { API } from '../api';

@Injectable({
  providedIn: 'root'
})
export class ContasPagarService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getAll(data): Observable<any> {
    return this.http.post(`${environment.baseUrl}${API.contasPagar.buscarTodos}`, data)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter as despesas', []))
      );
  }

  getPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idDespesa', id);
    return this.http.get(`${environment.baseUrl}${API.contasPagar.buscarPorId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter a despesa', []))
      );
  }

  getDetalhe(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idDespesa', id);
    return this.http.get(`${environment.baseUrl}${API.contasPagar.buscarDetalheDespesa}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o detalhe despesa', []))
      );
  }

  getDocumentosDespesa(id, documento: boolean): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams()
      .set('despesaId', id)
      .set('documento', documento.toString());

    return this.http.get(`${environment.baseUrl}${API.anexo.buscarDocumentosDespesa}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o anexo', []))
      );
  }

  cadastrar(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.contasPagar.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  liquidar(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.contasPagar.liquidarPagamento}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  deletarPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idDespesa', id);

    return this.http.get(`${environment.baseUrl}${API.contasPagar.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir a despesa', []))
      );
  }

  deletarParcela(data): Observable<any> {
    this.animationsService.showProgressBar(true);

    return this.http.post(`${environment.baseUrl}${API.contasPagar.deletarPagamento}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir a parcela', []))
      );
  }

  imprimirRecibo(data, contentType = 'application/pdf') {
    this.animationsService.showProgressBar(true);

    this.http.post(`${environment.baseUrl}${API.contasPagar.imprimirRecibo}`, data)
      .pipe(
        catchError(this.errorHandlerService.errorHandlerAnexo('Falha ao imprimir recibo', []))
      )
      .subscribe(res => {
        if (res?.status === 'error') return;
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
