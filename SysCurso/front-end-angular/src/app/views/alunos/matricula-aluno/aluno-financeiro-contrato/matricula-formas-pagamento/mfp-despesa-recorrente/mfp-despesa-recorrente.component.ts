import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { FinanceiroStoreState, FinanceiroStoreSelectors, FinanceiroStoreActions } from 'src/app/_store/financeiro-store';

@Component({
  selector: 'app-mfp-despesa-recorrente',
  templateUrl: './mfp-despesa-recorrente.component.html',
  styleUrls: ['./mfp-despesa-recorrente.component.scss']
})
export class MfpDespesaRecorrenteComponent implements OnInit, OnDestroy {
  form: FormGroup;
  planoPagamento$: Subscription;
  planos: any[] = [];

  constructor(
    private fb: FormBuilder,
    private storeFinanceiro: Store<FinanceiroStoreState.Financeiro>
  ) { }

  ngOnInit(): void {
    this.planoPagamento$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectPlanoPagamentoFinanceiro)).subscribe(val => {
      if(val && val[0]) this.planos = val;
      else this.planos = [];
    });

    this.buildForm();
  }

  ngOnDestroy(): void {
    this.planoPagamento$.unsubscribe();
  }
  buildForm(): void {
    this.form = this.fb.group({
      planoPagamentoId: [null, [Validators.required]]
    });

    this.form.valueChanges.subscribe(val => this.storeFinanceiro.dispatch(FinanceiroStoreActions.updateDadosSelecionados({ payload: val })));
  }
}
