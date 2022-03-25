import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { FinanceiroStoreState, FinanceiroStoreSelectors, FinanceiroStoreActions } from 'src/app/_store/financeiro-store';

@Component({
  selector: 'app-mfp-debito',
  templateUrl: './mfp-debito.component.html',
  styleUrls: ['./mfp-debito.component.scss']
})
export class MfpDebitoComponent implements OnInit, OnDestroy {
  form: FormGroup;
  planoPagamento$: Subscription;
  financeiroCadastrado$: Subscription;
  planos: any[] = [];

  constructor(
    private fb: FormBuilder,
    private storeFinanceiro: Store<FinanceiroStoreState.Financeiro>
  ) { }

  ngOnInit(): void {
    this.getPlanos();
    this.buildForm();

    this.getFinanceiro()
      .then(res => {
        if(res?.planoPagamentoAluno?.planoPagamentoId) this.form.get('planoPagamentoId').setValue(res.planoPagamentoAluno.planoPagamentoId);
        this.form.get('planoPagamentoId').disable();
      })
      .catch(() => { })
  }

  ngOnDestroy(): void {
    this.planoPagamento$.unsubscribe();
    this.financeiroCadastrado$.unsubscribe();
  }

  getPlanos(): void {
    this.planoPagamento$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectPlanoPagamentoFinanceiro)).subscribe(val => {
      if(val && val[0]) this.planos = val;
      else this.planos = [];
    });
  }

  getFinanceiro(): Promise<any> {
    return new Promise((res, rej) => {
      this.financeiroCadastrado$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectFinanceiroCadastrado)).subscribe(val => {
        if(val?.pagamento?.length > 0) res(val);
        else rej(null);
      });
    })
  }

  buildForm(): void {
    this.form = this.fb.group({
      planoPagamentoId: [null, [Validators.required]]
    });

    this.form.valueChanges.subscribe(val => this.storeFinanceiro.dispatch(FinanceiroStoreActions.updateDadosSelecionados({ payload: val })));
  }
}
