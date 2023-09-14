import { Component, OnInit, Output, EventEmitter, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { BoletoAvisoComponent } from './boleto-aviso/boleto-aviso.component';
import { BoletoAviso2Component } from './boleto-aviso2/boleto-aviso2.component';
import { Subscription } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { FinanceiroStoreState, FinanceiroStoreSelectors, FinanceiroStoreActions } from 'src/app/_store/financeiro-store';
import { PlanoPagamentoService } from 'src/app/services/gerenciador/plano-pagamento.service';
// import { CompararDatasComponent } from './comparar-datas/comparar-datas.component';

@Component({
  selector: 'app-mfp-boleto',
  templateUrl: './mfp-boleto.component.html',
  styleUrls: ['./mfp-boleto.component.scss']
})
export class MfpBoletoComponent implements OnInit, OnDestroy {
  @Output() onAviso: EventEmitter<any> = new EventEmitter();
  form: FormGroup;
  parcelas = Array(12).fill('').map((x,i) => i+1);
  today = new Date();
  planoPagamento$: Subscription;
  planos: any[] = [];
  financeiroCadastrado$: Subscription;
  plano:any;
  exibirSegundoPagamento:boolean;
  // PrimeiraData:any;
  // SegundaData: any;
  // PrimeiroMes: any;
  // SegundoMes: any;
  
  constructor(
    private fb: FormBuilder,
    private dialog: MatDialog,
    private planoPagamentoService: PlanoPagamentoService,
    private storeFinanceiro: Store<FinanceiroStoreState.Financeiro>
  ) { }

  ngOnInit(): void {
    this.getPlanos();
    this.buildForm();

    this.getFinanceiro()
      .then(res => {
        if(res?.planoPagamentoAluno?.planoPagamentoId) this.form.get('planoPagamentoId').setValue(res.planoPagamentoAluno.planoPagamentoId);
        if(res?.planoPagamentoAluno?.dataPrimeiraParcela) this.form.get('primeiroPagamento').setValue(res.planoPagamentoAluno.dataPrimeiraParcela);
        if(res?.planoPagamentoAluno?.dataSegundaParcela) this.form.get('segundoPagamento').setValue(res.planoPagamentoAluno.dataSegundaParcela);

        this.form.get('planoPagamentoId').disable();
        this.form.get('primeiroPagamento').disable();
        this.form.get('segundoPagamento').disable();
      })
      .catch(() => {
        this.openAviso();
      })
  }

  ngOnDestroy(): void {
    this.financeiroCadastrado$.unsubscribe();
    this.planoPagamento$.unsubscribe();
  }

  buildForm(): void {
    this.form = this.fb.group({
      planoPagamentoId: [null, [Validators.required]],
      primeiroPagamento: [null, [Validators.required]],
      segundoPagamento: [null, [Validators.required]],
    });

    this.form.valueChanges.subscribe(val => this.storeFinanceiro.dispatch(FinanceiroStoreActions.updateDadosSelecionados({ payload: val })));
  }

  getFinanceiro(): Promise<any> {
    return new Promise((res, rej) => {
      this.financeiroCadastrado$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectFinanceiroCadastrado)).subscribe(val => {
        if(val?.pagamento?.length > 0) res(val);
        else rej(null);
      });
    })
  }
  
  getPlanos(): void {
    this.planoPagamento$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectPlanoPagamentoFinanceiro)).subscribe(val => {
      if(val && val[0]) this.planos = val;
      else this.planos = [];
    });
  }

  selecionarPlano(){

    this.planoPagamentoService.getPorId(this.plano).subscribe(val => {

      if(val.quantidadeParcela === 1){
        //this.form.get('segundoPagamento').
        this.exibirSegundoPagamento = true;
      } else{
        //this.form.get('segundoPagamento').setValue('none')
        this.exibirSegundoPagamento = false;
      }
    })
  
  }

  openAviso(): void {
    const dialogRef = this.dialog.open(BoletoAvisoComponent, {
      width: '50vw',
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result === 3) { }
      else if(typeof result === 'number') this.onAviso.emit(result);
      else { }
    });
  }

  openAviso2(): void {
    const dialogRef = this.dialog.open(BoletoAviso2Component, {
      width: '50vw',
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result === 3) { }
      else if(typeof result === 'number') this.onAviso.emit(result);
      else { }
    });
  }

  // compararDatas(): void {
  //   if (this.form.value.primeiroPagamento != null && this.form.value.segundoPagamento != null)
  //   {
  //     const PrimeiraData = new Date(this.form.value.primeiroPagamento)
  //     const SegundaData = new Date(this.form.value.segundoPagamento)
  //     const PrimeiroMes = PrimeiraData.getMonth()+1;
  //     const SegundoMes = SegundaData.getMonth()+1;

  //     if (PrimeiroMes == SegundoMes)
  //     {
  //       this.openCompararDatas();
  //     }
  //   }
  // }

  // openCompararDatas(): void {
  //   const isMobileResolution = window.innerWidth < 768 ? true : false;

  //   const dialogRefMsg = this.dialog.open(CompararDatasComponent, {
  //     width: isMobileResolution ? '98vw' : '30vw',
  //     data: { },
  //     autoFocus: false
  //   });
  //   dialogRefMsg.afterClosed().subscribe(result => {

  //   });
  // }
}
