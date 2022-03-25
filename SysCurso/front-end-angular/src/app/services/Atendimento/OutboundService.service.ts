import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { catchError, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AnimationsService } from '../animations.service';
import { API } from '../api';
import { ErrorHandlerService } from '../error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class OutboundService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  cadastrar(data): Observable<any> {
    // debugger
    
    this.animationsService.showProgressBar(true);

    return this.http.post(
      `${environment.baseUrl}${API.outbound.cadastrar}`, data)
        .pipe(
          tap(() => this.animationsService.showProgressBar(false)),
          catchError(this.errorHandlerService.errorHandler(
            'Falha ao enviar os dados', [])
          )
        );
  }

  getOutbound(id): Observable<any> {
    //debugger

    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idUnidade', id);

    return this.http.get(
      `${environment.baseUrl}${API.outbound.listaOutbond}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler(
          'Não existem registros para serem executado!', []
          )
        )
      );
  }

  getHistoricoTentativa(id): Observable<any> {
    //debugger

    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idAtendimento', id);

    return this.http.get(
      `${environment.baseUrl}${API.outbound.historicoTentativa}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler(
          'Não existem Historico!', []
          )
        )
      );
  }

}
