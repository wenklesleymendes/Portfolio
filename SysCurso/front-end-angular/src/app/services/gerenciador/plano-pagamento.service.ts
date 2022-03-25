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
export class PlanoPagamentoService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getAll(): Observable<any> {
    return this.http.get(`${environment.baseUrl}${API.planoPagamento.buscarTodos}`)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter os planos de pagamentos', []))
      );
  }

  getPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idPlanoPagamento', id);

    return this.http.get(`${environment.baseUrl}${API.planoPagamento.buscarPorId}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o plano de pagamento', []))
      );
  }

  getPlanoPagamento(data): Observable<any> {

    this.animationsService.showProgressBar(true);
    const { formaPagamento, quantidadeParcela, cursoId, unidadeId} = data;
    let params: HttpParams = new HttpParams();
    if(formaPagamento) params = params.set('formaPagamento', formaPagamento);
    if(quantidadeParcela) params = params.set('quantidadeParcela', quantidadeParcela);
    if(cursoId) params = params.set('cursoId', cursoId);
    if(unidadeId) params = params.set('unidadeId', unidadeId);

    return this.http.get(`${environment.baseUrl}${API.planoPagamento.buscarPlanoPagamento}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao obter o plano de pagamento', []))
      );
  }

  cadastrar(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.planoPagamento.cadastrar}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  desativarAtivar(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('idPlanoPagamento', id);

    return this.http.get(`${environment.baseUrl}${API.planoPagamento.desativarAtivar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao mudar o status do plano de pagamento', []))
      );
  }

  deletarPorId(id): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('id', id);

    return this.http.get(`${environment.baseUrl}${API.planoPagamento.deletar}`, { params })
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao excluir o plano de pagamento', []))
      );
  }
}
