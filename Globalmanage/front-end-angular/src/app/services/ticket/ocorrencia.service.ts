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
export class OcorrenciaService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
    ) { }

    buscarTimeline(id): Observable<any> {
      this.animationsService.showProgressBar(true);
      const params: HttpParams = new HttpParams().set('idOcorrencia', id);
      return this.http.get(`${environment.baseUrl}${API.ocorrencia.buscarTimeline}`, { params })
        .pipe(
          tap(() => this.animationsService.showProgressBar(false)),
          catchError(this.errorHandlerService.errorHandler('Falha ao obter a timeline', []))
        );
    }

    cadastrar(data): Observable<any> {
      this.animationsService.showProgressBar(true);
      return this.http.post(`${environment.baseUrl}${API.ocorrencia.cadastrar}`, data)
        .pipe(
          tap(() => this.animationsService.showProgressBar(false)),
          catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
        );
    }
}
