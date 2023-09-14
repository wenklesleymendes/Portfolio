import { CurrencyPipe } from '@angular/common';
import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { AlunoFinanceiroService } from 'src/app/services/aluno/aluno-financeiro.service';
import { DatePtBrPipe } from 'src/app/utils/pipes/date-pt-br.pipe';

@Component({
  selector: 'app-mpp-detalhe-pagamento',
  templateUrl: './mpp-detalhe-pagamento.component.html',
  styleUrls: ['./mpp-detalhe-pagamento.component.scss']
})
export class MppDetalhePagamentoComponent implements OnInit {
  error: boolean = false;
  isLoadingResults: boolean = false;
  displayedColumns: string[] = ['label', 'value'];
  dataSource = new MatTableDataSource([]);

  constructor(
    public dialogRef: MatDialogRef<MppDetalhePagamentoComponent>,
    private alunoFinanceiroService: AlunoFinanceiroService,
    private currencyPipe: CurrencyPipe,
    private datePtBrPipe: DatePtBrPipe,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.getData();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getData(): void {
    this.isLoadingResults = true;
    this.alunoFinanceiroService.buscarDetalhePagamento(this.data?.element?.id).subscribe(val => {
      this.isLoadingResults = false;

      if(val?.tipoPagamento === 1 || val?.tipoPagamento === 2) this.showCartaoCredito(val);
      else if(val?.tipoPagamento === 3) this.showBoletoData(val);
      else {}
    })
  }

  showBoletoData(val: any): void {
    const { valor, dataVencimento, valorPago, dataPagamento, desconto } = this.data.element;

    this.dataSource.data = [
      { label: 'Valor',  value: this.currencyPipe.transform(valor) },
      { label: 'Data do Vencimento',  value: this.datePtBrPipe.transform(dataVencimento) },
      { label: 'Valor Pago',  value: this.currencyPipe.transform(valorPago) },
      { label: 'Data do Pagamento',  value: this.datePtBrPipe.transform(dataPagamento) },
      { label: 'Desconto Pontualidade',  value: `${desconto} %` }
    ];
  }

  showCartaoCredito(val: any): void {
    const { valorPago, dataPagamento, bandeiraCartao, codigoAutorizacao, acquirersEnum, valorTotal, numeroCartao, quantidadeParcela, valorParcela, tipoPagamento } = val;

    const data: any[] = [];
    data.push({ label: '',  value: 'Detalhe do pagamento'});
    data.push({ label: 'Valor Pago',  value: this.currencyPipe.transform(valorPago)});
    data.push({ label: 'Data do Pagamento',  value: this.datePtBrPipe.transform(dataPagamento)});
    if(tipoPagamento) data.push({ label: 'Tipo Pagamento',  value: this.tipoPagamento(tipoPagamento)});
    if(numeroCartao) data.push({ label: 'Número Cartão',  value: `**** **** **** ${numeroCartao}`});
    if(bandeiraCartao) data.push({ label: 'Bandeira',  value: bandeiraCartao});
    if(codigoAutorizacao) data.push({ label: 'Código Autorização / NSU-DOC',  value: codigoAutorizacao});
    if(acquirersEnum) data.push({ label: 'Adquirente',  value: (acquirersEnum === 1 ? 'REDE' : 'CIELO')});
    data.push({ label: '',  value: 'Histórico da transação'});
    data.push({ label: 'Valor Total',  value: this.currencyPipe.transform(valorTotal)});
    data.push({ label: 'Número de Parcelas',  value: quantidadeParcela});
    data.push({ label: 'Valor Parcela',  value: this.currencyPipe.transform(valorParcela)});

    this.dataSource.data = data;
}

  tipoPagamento(tipo): string {
    if(tipo === 1) return 'Cartão de crédito';
    else if(tipo === 2) return 'Cartão de débito';
    else if(tipo === 3) return 'Boleto bancário';
    else if(tipo === 7) return 'Despesa recorrente';
    else return '';
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------


}
