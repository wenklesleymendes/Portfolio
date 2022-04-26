import { CurrencyPipe } from '@angular/common';
import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { AlunoFinanceiroService } from 'src/app/services/aluno/aluno-financeiro.service';
import { DatePtBrPipe } from 'src/app/utils/pipes/date-pt-br.pipe';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import internal from 'events';

@Component({
  selector: 'app-mpp-baixa-pagamento',
  templateUrl: './mpp-baixa-pagamento.component.html',
  styleUrls: ['./mpp-baixa-pagamento.component.scss']
})
export class MppBaixaPagamentoComponent implements OnInit {
  form: FormGroup;
  error: boolean = false;
  isLoadingResults: boolean = false;
  displayedColumns: string[] = ['label', 'value'];
  dataSource = new MatTableDataSource([]);
  cartao: boolean = false;
  boleto: boolean = false;
  today: Date = new Date();

  constructor(
    public dialogRef: MatDialogRef<MppBaixaPagamentoComponent>,
    private alunoFinanceiroService: AlunoFinanceiroService,
    private currencyPipe: CurrencyPipe,
    private datePtBrPipe: DatePtBrPipe,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }


  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.getData();
    this.buildForm();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      tipoPagamento: ["0", [Validators.required]],
      valorPago: [null, [Validators.required]],
      dataVencimento: [null, [Validators.required]],
      dataPagamento: [null, [Validators.required]],
      bandeiraCartao: [null, [Validators.required]],
      codigoAutorizacao: [null, [Validators.required]],
      valorTotal: [null, [Validators.required]],
      quantidadeParcela: [null, [Validators.required]],
      valorParcela: [null, [Validators.required]]
    });
    this.form.get('tipoPagamento').valueChanges.subscribe(async val => {
      this.cartao = false;
      this.boleto = false;
      switch (val) {
        case 1:
        case 2:
          this.cartao = true;
          break;
        case 3:
          this.boleto = true;
        default:
          break;
      }
    });
  }
  getData(): void {
    this.isLoadingResults = true;
    this.alunoFinanceiroService.buscarDetalhePagamento(this.data?.element?.id).subscribe(val => {
      this.isLoadingResults = false;

      if (val?.tipoPagamento === 1 || val?.tipoPagamento === 2) this.showCartaoCredito(val);
      else if (val?.tipoPagamento === 3) this.showBoletoData(val);
      else { }
    })
  }
  salvar() {

  }
  showBoletoData(val: any): void {
    const { valor, dataVencimento, valorPago, dataPagamento, desconto } = this.data.element;

    this.dataSource.data = [
      { label: 'Valor', value: this.currencyPipe.transform(valor) },
      { label: 'Data do Vencimento', value: this.datePtBrPipe.transform(dataVencimento) },
      { label: 'Valor Pago', value: this.currencyPipe.transform(valorPago) },
      { label: 'Data do Pagamento', value: this.datePtBrPipe.transform(dataPagamento) },
      { label: 'Desconto Pontualidade', value: `${desconto} %` }
    ];
  }

  showCartaoCredito(val: any): void {
    const { valorPago, dataPagamento, bandeiraCartao, codigoAutorizacao, acquirersEnum, valorTotal, numeroCartao, quantidadeParcela, valorParcela, tipoPagamento } = val;

    const data: any[] = [];
    data.push({ label: '', value: 'Detalhe do pagamento' });
    data.push({ label: 'Valor Pago', value: this.currencyPipe.transform(valorPago) });
    data.push({ label: 'Data do Pagamento', value: this.datePtBrPipe.transform(dataPagamento) });
    if (tipoPagamento) data.push({ label: 'Tipo Pagamento', value: tipoPagamento });
    if (numeroCartao) data.push({ label: 'Número Cartão', value: `**** **** **** ${numeroCartao}` });
    if (bandeiraCartao) data.push({ label: 'Bandeira', value: bandeiraCartao });
    if (codigoAutorizacao) data.push({ label: 'Código Autorização / NSU-DOC', value: codigoAutorizacao });
    if (acquirersEnum) data.push({ label: 'Adquirente', value: (acquirersEnum === 1 ? 'REDE' : 'CIELO') });
    data.push({ label: '', value: 'Histórico da transação' });
    data.push({ label: 'Valor Total', value: this.currencyPipe.transform(valorTotal) });
    data.push({ label: 'Número de Parcelas', value: quantidadeParcela });
    data.push({ label: 'Valor Parcela', value: this.currencyPipe.transform(valorParcela) });

    this.dataSource.data = data;
  }




  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------



}
