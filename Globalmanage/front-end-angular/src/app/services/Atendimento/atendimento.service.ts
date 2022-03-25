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

  getPorCelular(celular): Observable<any> {

    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('celularCliente', celular);

    return this.http.get(
      `${environment.baseUrl}${API.atendimento.buscaIdPorNumerodeCelular}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler(
          'Falha ao obter o atendimento', []
        )
        )
      );
  }

  getAgendamentos(id): Observable<any> {
    ////debugger
    const params: HttpParams = new HttpParams().set('idUnidade', id);
    return this.http.get(`${environment.baseUrl}${API.atendimento.buscarAgendamentos}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os atendimentos agendados', []))
      );
  }

  getNumeroDeAtendimentos(idUnidade): Observable<any> {

    const params: HttpParams = new HttpParams().set('idUnidade', idUnidade);
    return this.http.get(`${environment.baseUrl}${API.atendimento.numeroDeAtendimentos}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter a quantidade de atendimentos', []))
      );
  }

  getFiltraAtendimentos(filtro): Observable<any> {
    return this.http.post(`${environment.baseUrl}${API.atendimento.filtraAtndimentos}`, filtro)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os atendimentos', []))
      );
  }

  getAgendamentoIdAtendimento(id): Observable<any> {
    const params: HttpParams = new HttpParams().set('idAtendimento', id);
    return this.http.get(`${environment.baseUrl}${API.atendimento.buscarAgendamentosPorIdAtendimento}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o agendamento', []))
      );
  }

  getAgendamentoId(id): Observable<any> {
    const params: HttpParams = new HttpParams().set('id', id);
    return this.http.get(`${environment.baseUrl}${API.agendamento.buscarAgendamentoPorId}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o agendamento', []))
      );
  }

  cadastrar(data): Observable<any> {
    // //debugger
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

  atualizar(data): Observable<any> {
    // //debugger
    this.animationsService.showProgressBar(true);
    return this.http.post(
      `${environment.baseUrl}${API.atendimento.atualizar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler(
          'Falha ao enviar os dados', [])
        )
      );
  }

  editar(data): Observable<any> {
    // //debugger
    this.animationsService.showProgressBar(true);
    return this.http.post(
      `${environment.baseUrl}${API.atendimento.editar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler(
          'Falha ao enviar os dados', [])
        )
      );
  }

  cadastrarAgendamento(data): Observable<any> {
    // //debugger
    this.animationsService.showProgressBar(true);
    return this.http.post(
      `${environment.baseUrl}${API.agendamento.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler(
          'Falha ao enviar os dados', [])
        )
      );
  }

  atualizaAgendamento(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(
      `${environment.baseUrl}${API.atendimento.atualizaAgendamento}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler(
          'Falha ao enviar os dados', [])
        )
      );
  }

  getHistoricoAgendamentos(id): Observable<any> {
    ////debugger

    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idAtendimento', id);

    return this.http.get(
      `${environment.baseUrl}${API.atendimento.historicoAgendamentos}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler(
          'Não existem Historico!', []
        )
        )
      );
  }

  getFiltraAgendamentosPorData(data): Observable<any> {

    const params: HttpParams = new HttpParams().set('data', data);
    return this.http.get(`${environment.baseUrl}${API.agendamento.filtraAgendamentosPorData}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o agendamento', []))
      );
  }
}
