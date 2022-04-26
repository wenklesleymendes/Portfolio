import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, combineLatest } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataProvider {

  private dadosPessoais    : BehaviorSubject<any>;
  private dadosContratacaoAll : BehaviorSubject<any>;
  private salarioUnidade   : BehaviorSubject<any>;
  private validate         : BehaviorSubject<any>;

  constructor( ) {  }

  getAll(): Observable<any> {
    return combineLatest(
      this.dadosPessoais,
      this.dadosContratacaoAll,
      this.salarioUnidade,
      ( dadosPessoais, dadosContratacaoAll, salarioUnidade ) => {
        return { dadosPessoais, dadosContratacaoAll, salarioUnidade }
      }
    )
  } 

  getValidate(): Observable<any> {
    return this.validate;
  }

  initAllSubjects(): void {
    this.dadosPessoais    = new BehaviorSubject(null);
    this.dadosContratacaoAll = new BehaviorSubject(null);
    this.salarioUnidade   = new BehaviorSubject([]);
    this.validate         = new BehaviorSubject(null);
  }

  completeAllSubjects(): void {
    this.dadosPessoais.complete();
    this.dadosContratacaoAll.complete();
    this.salarioUnidade.complete();
    this.validate.complete();
  }

  validateNext(val): void { this.validate.next(val); }
  dadosPessoaisNext(val): void { this.dadosPessoais.next(val); }
  dadosContratacaoNext(val): void { this.dadosContratacaoAll.next(val); }
  salarioUnidadeNext(val): void { this.salarioUnidade.next(val); }
}
