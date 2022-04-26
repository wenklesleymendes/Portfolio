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
export class DocumentoAlunoService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  consultarDocumentosPendentes(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('matriculaId', id);
    return this.http.get(`${environment.baseUrl}${API.documentosAluno.consultarDocumentosPendentes}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os documentos', []))
      );
  }

  reciboPagamentoMensalidade(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.documentosAluno.reciboPagamentoMensalidade}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar recibos', []))
      );
  }

  gerarPendenciaDocumental(id): void {
    const params: HttpParams = new HttpParams().set('matriculaId', id);
    this.http.get(`${environment.baseUrl}${API.documentosAluno.gerarPendenciaDocumental}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandlerAnexo('Falha ao baixar anexo', []))
      )
      .subscribe(res => {
        if (res?.status === 'error') return;
        const b64Data = res;
        const blob = this.b64toBlob(b64Data, 'application/pdf');
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = 'Termo de Pendência de Documentos.pdf';
        link.click();
        this.animationsService.showProgressBar(false);
      });
  }

  upload(data) {
    this.animationsService.showProgressBar(true);
    return this.http.post<any>(`${environment.baseUrl}${API.documentosAluno.uploadDeclaracaoPendenciaDocumental}`, data, { reportProgress: true, observe: 'events' })
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

  download(id) {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('matriculaId', id);

    this.http.get(`${environment.baseUrl}${API.documentosAluno.downloadDeclaracaoPendenciaDocumental}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandlerAnexo('Falha ao baixar anexo', [])),
      )
      .subscribe(res => {
        if (res?.status === 'error') return;
        if(res) {
          const b64Data = res;
          const blob = this.b64toBlob(b64Data, 'application/pdf');
          const url = window.URL.createObjectURL(blob);
          const link = document.createElement('a');
          link.href = url;
          link.download = 'Termo de Pendência de Documentos.pdf';
          link.click();
        } else {
          this.animationsService.showErrorSnackBar('Não possui declaração de pendência');
        }
      });
  }

  downloadComprovante(id) {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('matriculaId', id);

    this.http.get(`${environment.baseUrl}${API.documentosAluno.buscarComprovanteBolsaConvenio}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandlerAnexo('Falha ao baixar anexo', [])),
      )
      .subscribe(res => {
        if (res?.status === 'error') return;
        if(res) {
          const b64Data = res?.arquivo;
          const blob = this.b64toBlob(b64Data, res?.extensao);
          const url = window.URL.createObjectURL(blob);
          const link = document.createElement('a');
          link.href = url;
          link.download = res?.arquivoString;
          link.click();
        } else {
          this.animationsService.showErrorSnackBar('Não possui comprovante');
        }
      });
  }

  imprimir(id) {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('matriculaId', id);

    this.http.get(`${environment.baseUrl}${API.documentosAluno.downloadDeclaracaoPendenciaDocumental}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandlerAnexo('Falha ao imprimir anexo', []))
      )
      .subscribe(res => {
        if (res?.status === 'error') return;
        if(res) {
          const b64Data = res;
          const blob = this.b64toBlob(b64Data, 'application/pdf');
          const url = window.URL.createObjectURL(blob);
          window.open(url, '_blank');
        }else {
          this.animationsService.showSuccessSnackBar('Não possui declaração de pendência');
        }
      });
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
  }
}
