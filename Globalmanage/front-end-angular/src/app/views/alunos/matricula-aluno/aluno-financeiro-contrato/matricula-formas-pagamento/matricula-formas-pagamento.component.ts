import { Component, OnInit, Output, EventEmitter, OnDestroy } from '@angular/core';
import { PlanoPagamentoService } from 'src/app/services/gerenciador/plano-pagamento.service';
import { Subscription } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { AlunoStoreState, AlunoStoreSelectors } from 'src/app/_store/aluno-store';
import { FinanceiroStoreActions, FinanceiroStoreSelectors, FinanceiroStoreState } from 'src/app/_store/financeiro-store';

@Component({
  selector: 'app-matricula-formas-pagamento',
  templateUrl: './matricula-formas-pagamento.component.html',
  styleUrls: ['./matricula-formas-pagamento.component.scss']
})
export class MatriculaFormasPagamentoComponent implements OnInit, OnDestroy {
  @Output() onChangeTipoPagamento: EventEmitter<number> = new EventEmitter();
  selectedIndex: number = 0;
  parcelas = Array(12).fill('').map((x,i) => i+1);
  matricula$: Subscription;
  unidadeId: number = null;
  cursoId: number = null;
  financeiroCadastrado$: Subscription;
  hasFinanceiroCadastrado: boolean = false;
  tipoPagamento: number = null

  constructor(
    private storeAluno: Store<AlunoStoreState.Aluno>,
    private planoPagamentoService: PlanoPagamentoService,
    private storeFinanceiro: Store<FinanceiroStoreState.Financeiro>
  ) { }

  ngOnInit(): void {
    this.getMatricula();
    this.getFinanceiro()
      .then(res => {
        this.hasFinanceiroCadastrado = true;
        this.tipoPagamento = res?.planoPagamentoAluno?.tipoPagamento;
        this.getPlanoPagamento(res?.planoPagamentoAluno?.tipoPagamento);
        if(res?.planoPagamentoAluno?.tipoPagamento === 1) this.selectedIndex = 0;
        else if(res?.planoPagamentoAluno?.tipoPagamento === 2) this.selectedIndex = 1;
        else if(res?.planoPagamentoAluno?.tipoPagamento === 3) this.selectedIndex = 3;
        else if(res?.planoPagamentoAluno?.tipoPagamento === 7) this.selectedIndex = 2;
        else {}
      })
      .catch(() => {
        this.hasFinanceiroCadastrado = false;
        this.getPlanoPagamento(1);
      })
  }

  ngOnDestroy(): void {
    this.financeiroCadastrado$.unsubscribe();
    this.matricula$.unsubscribe();
  }

  onChangeTab(e): void {
    if(this.hasFinanceiroCadastrado) return;
    this.storeFinanceiro.dispatch(FinanceiroStoreActions.deleteFinanceiro());

    this.selectedIndex = e;
    if(e === 0) {
      this.onChangeTipoPagamento.emit(1);
      this.getPlanoPagamento(1);
    }
    else if(e === 1) {
      this.onChangeTipoPagamento.emit(2);
      this.getPlanoPagamento(2);
    }
    else if(e === 2) {
      this.onChangeTipoPagamento.emit(3);
      this.getPlanoPagamento(3);
    }
    else {}
  }

  onAviso(e) {
    this.selectedIndex = e;
  }

  getFinanceiro(): Promise<any> {
    return new Promise((res, rej) => {
      this.financeiroCadastrado$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectFinanceiroCadastrado)).subscribe(val => {
        if(val?.pagamento?.length > 0) res(val);
        else rej(null);
      });
    })
  }

  enableTab(tipoPagamento): boolean {
    if(!this.hasFinanceiroCadastrado) return false;    
    return tipoPagamento !== this.tipoPagamento;
  }

  getMatricula(): void {
    this.matricula$ = this.storeAluno.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => {
      if(val?.id) {
        this.unidadeId = val?.unidadeId;
        this.cursoId = val?.cursoId;
      }
    });
  }

  getPlanoPagamento(formaPagamento: number): void {
    const data: any = {
      formaPagamento,
      unidadeId: this.unidadeId,
      cursoId: this.cursoId
    };

    this.planoPagamentoService.getPlanoPagamento(data).subscribe(val => {
      if(val) this.storeFinanceiro.dispatch(FinanceiroStoreActions.updatePlanoPagamento({ payload: val }));
    })
  }
}
