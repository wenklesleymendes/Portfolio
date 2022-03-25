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
export class AlunoFinanceiroService {
  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  consultarPainelFinanceiro(matriculaId): Observable<any> {
    const params: HttpParams = new HttpParams().set('matriculaId', matriculaId);
    return this.http.get(`${environment.baseUrl}${API.alunoFinanceiroContrato.consultarPainelFinanceiro}`, { params })
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter painel financeiro', []))
      );
  }

  contratarPlano(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.alunoFinanceiroContrato.contratarPlano}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError((err) : Observable<any> => {
          this.animationsService.showProgressBar(false);
          // Console error
          // console.error(`Falha ao enviar os dados: ${err?.message}`);
          console.error(`Erro ao enviar os dados: ${err}`);
          // Stop loading bar
          // SnackBar error
          this.animationsService.showErrorSnackBar(`Falha ao enviar os dados: ${err}`);
          // Return error
          return of({ status: 'error'});
        })
      );
  }

  efetuarPagamento(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.alunoFinanceiroContrato.efetuarPagamento}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }


  baixaManual(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.alunoFinanceiroContrato.baixaManual}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }
  enviarBoletoPorEmail(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.alunoFinanceiroContrato.enviarBoletoPorEmail}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
      );
  }

  enviarBoletoPorEmailOuRecalcular(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.alunoFinanceiroContrato.enviarBoletoPorEmailOuRecalcular}`, data)
    .pipe(
      tap(() => this.animationsService.showProgressBar(false)),
      catchError(this.errorHandlerService.errorHandlerAnexo('Falha ao realizar download', []))
    )
  }

  gerarBoletoResidual(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.alunoFinanceiroContrato.gerarBoletoResidual}`, data)
    .pipe(
      tap(() => this.animationsService.showProgressBar(false)),
      catchError(this.errorHandlerService.errorHandler('Falha ao enviar os dados', []))
    )
  }

  consultarEmail(pagamentoId): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('pagamentoId', pagamentoId);
    return this.http.get(`${environment.baseUrl}${API.alunoFinanceiroContrato.consultarEmail}`, { params })
    .pipe(
      tap(() => this.animationsService.showProgressBar(false)),
      catchError(this.errorHandlerService.errorHandlerAnexo('Falha ao consultar e-mail', []))
    )
  }

  buscarDetalhePagamento(pagamentoId): Observable<any> {
    this.animationsService.showProgressBar(true);
    const params: HttpParams = new HttpParams().set('pagamentoId', pagamentoId);
    return this.http.get(`${environment.baseUrl}${API.alunoFinanceiroContrato.buscarDetalhePagamento}`, { params })
    .pipe(
      tap(() => this.animationsService.showProgressBar(false)),
      catchError(this.errorHandlerService.errorHandlerAnexo('Falha ao consultar detalhe do pagamento', []))
    )
  }

  efetuarPagamentoCartaoCredito(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.baseUrl}${API.alunoFinanceiroContrato.efetuarPagamentoCartaoCredito}`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha ao realizar pagamento', []))
      );
  }
}
