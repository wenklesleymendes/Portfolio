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
export class FuncionarioService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getAllFiltro(filter): Observable<any> {
    return this.http.post(`${environment.baseUrl}${API.funcionario.buscarTodos}`, filter)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os funcionários', []))
      );
  }

  getPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idFuncionario', id);
    return this.http.get(`${environment.baseUrl}${API.funcionario.buscarPorId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o funcionário', []))
      );
  }

  getPorCpf(cpf): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('cpf', cpf);
    return this.http.get(`${environment.baseUrl}${API.funcionario.buscarPorCPF}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o funcionário', []))
      );
  }

  getDetalhamento(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idFuncionario', id);
    return this.http.get(`${environment.baseUrl}${API.funcionario.buscarDetalhamentoFerias}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os detalhes do funcionário', []))
      );
  }

  getPontoEletronico(formValue): Observable<any> {
    const { cpf, dataInicio, dataFim } = formValue;
    this.animationsService.showProgressBar(true);
    let params: HttpParams = new HttpParams();
    if (cpf) params = params.set('cpf', cpf);
    if (dataInicio) params = params.set('dataInicio', dataInicio);
    if (dataFim) params = params.set('dataFim', dataFim);
    return this.http.get(`${environment.baseUrl}${API.funcionario.buscarPontoEletronico}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o funcionário', []))
      );
  }

  getFerias(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idFuncionario', id);
    return this.http.get(`${environment.baseUrl}${API.funcionario.buscarFeriasPorFuncionario}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os dados', []))
      );
  }

  cadastrar(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.funcionario.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  atualizarPontoEletronico(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.funcionario.atualizarPontoEletronico}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  cadastrarFerias(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.funcionario.cadastrarFerias}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  desativarAtivar(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idFuncionario', id);

    return this.http.get(`${environment.baseUrl}${API.funcionario.desativarAtivar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao mudar o status do funcionário', []))
      );
  }

  deletarPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idFuncionario', id);

    return this.http.get(`${environment.baseUrl}${API.funcionario.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir o funcionário', []))
      );
  }

  deletarPorFérias(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idFerias', id);

    return this.http.get(`${environment.baseUrl}${API.funcionario.deletarFerias}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir o funcionário', []))
      );
  }
}
