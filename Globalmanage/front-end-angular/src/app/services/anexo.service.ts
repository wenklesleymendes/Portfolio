import { Injectable } from '@angular/core';
import { HttpClient, HttpEventType, HttpParams } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { ErrorHandlerService } from './error-handler.service';
import { environment } from 'src/environments/environment';
import { API } from './api';
import { Observable, of } from 'rxjs';
import { AnimationsService } from './animations.service';

@Injectable({
  providedIn: 'root'
})
export class AnexoService {

  constructor(
    private http: HttpClient,
    private errorHandlerService: ErrorHandlerService,
    private animationsService: AnimationsService,
  ) { }

  /**
   * @description Send file to back-end
   * @param file File
   * @returns Observable<any>
   */
  upload(file: FormData) {
    this.animationsService.showProgressBar(true);
    return this.http.post<any>(`${environment.baseUrl}${API.anexo.cadastrar}`, file, { reportProgress: true, observe: 'events' })
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

  getAnexo(anexo): Observable<any> {
    return this.http.post(`${environment.baseUrl}${API.anexo.buscarAnexo}`, anexo)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os anexos', []))
      );
  }

  deletarAnexo(id: string): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idAnexo', id);

    return this.http.get(`${environment.baseUrl}${API.anexo.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao deletar anexo', []))
      );
  }

  recusarDocumento(data: any): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.anexo.recusarDocumento}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao recusar anexo', []))
      );
  }

  download(id, filename, contentType) {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idAnexo', id);

    this.http.get(`${environment.baseUrl}${API.anexo.download}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandlerAnexo('Falha ao baixar anexo', []))
      )
      .subscribe(res => {
        this.animationsService.showProgressBar(false);
        if (res?.status === 'error') return;
        const b64Data = res;
        const blob = this.b64toBlob(b64Data, contentType);
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = filename;
        link.click();
        document.body.removeChild(link);
      });
  }

  imprimir(id, filename, contentType) {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idAnexo', id);

    this.http.get(`${environment.baseUrl}${API.anexo.download}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandlerAnexo('Falha ao imprimir anexo', []))
      )
      .subscribe(res => {
        if (res?.status === 'error') return;
        const b64Data = res;
        const blob = this.b64toBlob(b64Data, contentType);
        const url = window.URL.createObjectURL(blob);
        window.open(url, '_blank');
        this.animationsService.showProgressBar(false);
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
