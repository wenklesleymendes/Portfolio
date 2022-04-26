import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ContasPagarService } from 'src/app/services/financeiro/contas-pagar.service';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-contas-pagar-detalhe',
  templateUrl: './contas-pagar-detalhe.component.html',
  styleUrls: ['./contas-pagar-detalhe.component.scss']
})
export class ContasPagarDetalheComponent implements OnInit, OnDestroy {
  error: boolean = false;
  isLoadingResults: boolean = false;
  id: number = 0;
  nome: string = '';
  descricoes: Array <{label: string, value: string}>;
  historicoDespesa: any[] = null;
  despesaParcelas: any[] = null;
  quitado: boolean = false;
  update: boolean = false;
  baixaManual: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<ContasPagarDetalheComponent>,
    private contasPagarService: ContasPagarService,
    private currencyPipe: CurrencyPipe,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.nome = this.data['nome'];
    this.id = this.data['id'];
    this.getAll();
  }

  ngOnDestroy(): void {
    this.dialogRef.close(this.update);
  }
  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getAll() {
    this.isLoadingResults = true;
    this.error = false;
    if (this.id == 0) return;

    this.contasPagarService.getDetalhe(this.data['id'])
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') return this.error = true;
        const { nomeDespesa, fornecedor, tipoParcela, proximoVencimento, valorDespesa, quantidadeParcela, historicoDespesa, despesaParcelas, quitado, baixaManual } = val;
        this.descricoes = [ ];
        if (nomeDespesa)       this.descricoes.push({ label: 'Nome da Despesa'         , value: nomeDespesa });
        if (fornecedor)        this.descricoes.push({ label: 'Fornecedor'              , value: fornecedor });
        if (proximoVencimento) this.descricoes.push({ label: 'Próximo Vencimento'      , value: proximoVencimento });
        if (quantidadeParcela) this.descricoes.push({ label: 'Quantidade de Parcela(s)', value: quantidadeParcela });
        if (valorDespesa)      this.descricoes.push({ label: 'Valor da Despesa'        , value: this.currencyPipe.transform(valorDespesa) });
        if (tipoParcela)       this.descricoes.push({ label: 'Tipo da Parcela'         , value: this.ajustarTipoParcela(tipoParcela) });

        this.historicoDespesa = historicoDespesa;
        this.despesaParcelas = despesaParcelas;
        this.quitado = quitado;
        this.baixaManual = baixaManual;
      });
  }

  onLiquidado(): void {
    this.getAll();
    this.update = true;
  }

  ajustarTipoParcela(tipoParcela): string {
    if (tipoParcela === 1) return 'Unica';
    if (tipoParcela === 2) return 'Parcelada';
    if (tipoParcela === 3) return 'Recorrente';
    return ' - ';
  }
  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------

}
