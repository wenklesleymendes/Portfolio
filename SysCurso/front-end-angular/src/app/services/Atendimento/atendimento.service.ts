import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { catchError, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AnimationsService } from '../animations.service';
import { API } from '../api';
import { ErrorHandlerService } from '../error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class AtendimentoService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getPorId(id): Observable<any> {

    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idAtendimento', id);

    return this.http.get(
      `${environment.baseUrl}${API.atendimento.buscarPorId}`, { params })
        .pipe(
          tap(() => this.animationsService.showProgressBar(false)),
          catchError(this.errorHandlerService.errorHandler(
            'Falha ao obter o atendimento', []
            )
          )
      );
  }

  getAgendamentos(id): Observable<any> {
    debugger
    const params: HttpParams = new HttpParams().set('idUnidade', id);
    return this.http.get(`${environment.baseUrl}${API.atendimento.buscarAgendamentos}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os atendimentos agendados', []))
      );
  }

  cadastrar(data): Observable<any> {
    // debugger
    this.animationsService.showProgressBar(true);
    return this.http.post(
      `${environment.baseUrl}${API.atendimento.cadastrar}`, data)
        .pipe(
          tap(() => this.animationsService.showProgressBar(false)),
          catchError(this.errorHandlerService.errorHandler(
            'Falha ao enviar os dados', [])
          )
        );
  }

}
