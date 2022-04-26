import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, combineLatest } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataProvider {

  private infomacoes           : BehaviorSubject<any>;
  private dadosBancario        : BehaviorSubject<any>;
  private contratoLocacao      : BehaviorSubject<any>;
  private historicoOcorrencias : BehaviorSubject<any>;
  private validate             : BehaviorSubject<any>;

  constructor( ) {  }

  getAll(): Observable<any> {
    return combineLatest(
      this.infomacoes,
      this.dadosBancario,
      this.contratoLocacao,
      this.historicoOcorrencias,
      ( infomacoes, dadosBancario, contratoLocacao, historicoOcorrencias ) => {
        return { infomacoes, dadosBancario, contratoLocacao, historicoOcorrencias }
      }
    )
  } 

  getValidate(): Observable<any> {
    return this.validate;
  }

  initAllSubjects(): void {
    this.infomacoes = new BehaviorSubject(null);
    this.dadosBancario = new BehaviorSubject(null);
    this.contratoLocacao = new BehaviorSubject(null);
    this.historicoOcorrencias = new BehaviorSubject([]);
    this.validate = new BehaviorSubject(null);
  }

  completeAllSubjects(): void {
    this.infomacoes.complete();
    this.dadosBancario.complete();
    this.contratoLocacao.complete();
    this.historicoOcorrencias.complete();
    this.validate.complete();
  }

  validateNext(val): void { this.validate.next(val); }
  infomacoesNext(val): void { this.infomacoes.next(val); }
  dadosBancariosNext(val): void { this.dadosBancario.next(val); }
  contratoLocacaoNext(val): void { this.contratoLocacao.next(val); }
  historicoOcorrenciaNext(val): void { this.historicoOcorrencias.next(val); }
}
