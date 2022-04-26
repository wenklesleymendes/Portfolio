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
export class ControlePontoService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getAll(): Observable<any> {
    return this.http.get(`${environment.baseUrl}${API.controlePonto.buscarTodos}`)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os pontos', []))
      );
  }

  buscarSaldoHorasExtras(data): Observable<any> {
    const { cpf, inicioHoraExtraPaga, terminoHoraExtraPaga } = data;
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams()
      .set('cpf', cpf)
      .set('dataInicio', inicioHoraExtraPaga)
      .set('dataFim', terminoHoraExtraPaga)

    return this.http.get(`${environment.baseUrl}${API.controlePonto.buscarSaldoHorasExtras}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o saldo de horas extra', []))
      );
  }

  subirPonto(file: FormData) {
    return this.http.post<any>(`${environment.baseUrl}${API.controlePonto.subir}`, file, { reportProgress: true, observe: 'events' })
      .pipe(
        map((event) => {
          switch (event.type) {
            case HttpEventType.UploadProgress:
              // Uploading progress
              const progress = Math.round(100 * event.loaded / event.total);
              return { status: 'progress', progress: progress };
            case HttpEventType.Response:
              // SnackBar success
              this.animationsService.showSuccessSnackBar('Enviado com sucesso');
              return { status: 'done', response: event.body };
            default:
              return
          }
        }),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar anexo', []))
      );
  }

  deletarPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idArquivoPonto', id);

    return this.http.get(`${environment.baseUrl}${API.controlePonto.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir ponto', []))
      );
  }

  deletarPontoEletronico(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idPontoEletronico', id);

    return this.http.get(`${environment.baseUrl}${API.controlePonto.deletarPontoEletronico}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir ponto', []))
      );
  }
}
