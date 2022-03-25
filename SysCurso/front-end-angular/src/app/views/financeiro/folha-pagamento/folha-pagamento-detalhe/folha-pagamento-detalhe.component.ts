import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { FolhaPagamentoService } from 'src/app/services/financeiro/folha-pagamento.service';

@Component({
  selector: 'app-folha-pagamento-detalhe',
  templateUrl: './folha-pagamento-detalhe.component.html',
  styleUrls: ['./folha-pagamento-detalhe.component.scss']
})
export class FolhaPagamentoDetalheComponent implements OnInit {
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;
  descricoes: Array<{label: string, value: string}> = [];
  historico: any = null;

  constructor(
    public dialogRef: MatDialogRef<FolhaPagamentoDetalheComponent>,
    private currencyPipe: CurrencyPipe,
    private datePipe: DatePipe,
    private folhaPagamentoService: FolhaPagamentoService,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.id = this.data['id'];
    this.loadData();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------

  loadData(): void {
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.folhaPagamentoService.getPorId(this.id)
      .subscribe(val => {
        if (!val) return;
        if (val['status'] === 'error') return this.error = true;
        const { nomeUsuario, funcionario, salarioBruto, salarioLiquido, alimentacao, transporte, comissaoPrimeiraParcelaPaga, valorDiasDSR, horaExtra, valorAdicional, competencia, quantidadeDias,
          monitoriaProva, valorFerias, valorDecimoTerceiro, valorTotalPagamento, dataCadastro, dataPagamento, statusPagamento, bonusMetaPeriodo, valorTotalDesconto, bancoPagamento } = val;
        const { nome, dadosContratacao } = funcionario;

        this.descricoes = [ ];
        
        if (competencia)         this.descricoes.push({ label: 'Competência', value: this.datePipe.transform(competencia, 'MM/yyyy') });
        if (dadosContratacao?.tipoRegimeContratacao) this.descricoes.push({ label: 'Regime', value: this.ajustarRegime(dadosContratacao?.tipoRegimeContratacao) });
        if (nome)                this.descricoes.push({ label: 'Nome do Colaborador', value: nome });
        if (salarioBruto)        this.descricoes.push({ label: this.labelSalarioBruto(dadosContratacao?.tipoRegimeContratacao), value: this.currencyPipe.transform(salarioBruto) });
        if (quantidadeDias)      this.descricoes.push({ label: 'Quantidade de dias', value: quantidadeDias });
        if (salarioLiquido)      this.descricoes.push({ label: this.labelSalarioLiquido(dadosContratacao?.tipoRegimeContratacao), value: this.currencyPipe.transform(salarioLiquido) });
        if (alimentacao)         this.descricoes.push({ label: 'Alimentação', value: this.currencyPipe.transform(alimentacao) });
        if (transporte)          this.descricoes.push({ label: 'Transporte', value: this.currencyPipe.transform(transporte) });
        if (horaExtra?.length > 0) this.descricoes.push({ label: 'Hora extra', value: this.ajustarHoraExtra(horaExtra) });
        if (comissaoPrimeiraParcelaPaga) this.descricoes.push({ label: 'Comissão 1° Parcela Paga', value: this.currencyPipe.transform(comissaoPrimeiraParcelaPaga) });
        if (valorTotalDesconto)  this.descricoes.push({ label: 'Total desconto', value: this.currencyPipe.transform(valorTotalDesconto) });
        if (bonusMetaPeriodo)    this.descricoes.push({ label: 'Bônus Meta Período', value: this.currencyPipe.transform(bonusMetaPeriodo) });
        if (valorDiasDSR)        this.descricoes.push({ label: 'DSR (Descanso Semanal Remunerado)', value: this.currencyPipe.transform(valorDiasDSR) });
        if (monitoriaProva)      this.descricoes.push({ label: 'Monitoria Prova', value: this.currencyPipe.transform(monitoriaProva) });
        if (valorAdicional)      this.descricoes.push({ label: 'Adicional', value: this.currencyPipe.transform(valorAdicional) });
        if (valorFerias)         this.descricoes.push({ label: 'Férias', value: this.currencyPipe.transform(valorFerias) });
        if (valorDecimoTerceiro) this.descricoes.push({ label: 'Décimo Terceiro', value: this.currencyPipe.transform(valorDecimoTerceiro) });
        if (valorTotalPagamento) this.descricoes.push({ label: 'Valor Total', value: this.currencyPipe.transform(valorTotalPagamento) });
        if (statusPagamento)     this.descricoes.push({ label: 'Status Pagamento', value: statusPagamento === 2 ? 'Pago' : 'A Receber' });

        this.historico = { dataCadastro, dataPagamento, valorTotalPagamento, nomeUsuario, bancoPagamento };
        this.isLoadingResults = false;
      })
    }
  }

  ajustarRegime(tipoRegimeContratacao: number): string {
    if (tipoRegimeContratacao === 1) return 'CLT Seg a Sex';
    if (tipoRegimeContratacao === 2) return 'Estágio Seg a Sex';
    if (tipoRegimeContratacao === 3) return 'Professor Autônomo';
    if (tipoRegimeContratacao === 4) return 'Professor CLT';
    if (tipoRegimeContratacao === 5) return 'Profissional Autônomo';
    if (tipoRegimeContratacao === 6) return 'CLT Seg a Sab';
    if (tipoRegimeContratacao === 7) return 'Estágio Seg a Sab';
    if (tipoRegimeContratacao === 8) return 'Autônomo Pré CLT Seg a Sex';
    if (tipoRegimeContratacao === 9) return 'Autônomo Pré CLT Seg a Sab';
    if (tipoRegimeContratacao === 10)return 'Autônomo Pré Estágio Seg a Sex';
    if (tipoRegimeContratacao === 11)return 'Autônomo Pré Estágio Seg a Sab';
    return ' - ';
  }

  labelSalarioBruto(tipoRegimeContratacao: number): string {
    if (tipoRegimeContratacao == 1 || tipoRegimeContratacao == 6 || tipoRegimeContratacao == 4) return 'Salário bruto';
    if (tipoRegimeContratacao == 2 || tipoRegimeContratacao == 7) return 'Bolsa auxílio';
    if (tipoRegimeContratacao == 3) return 'Valor aula';
    if (tipoRegimeContratacao == 5) return 'Valor remuneração por dia';
    return 'Salário bruto';
  }

  labelSalarioLiquido(tipoRegimeContratacao: number): string {
    if (tipoRegimeContratacao == 1 || tipoRegimeContratacao == 6 || tipoRegimeContratacao == 4) return 'Salário líquido';
    if (tipoRegimeContratacao == 2 || tipoRegimeContratacao == 7) return 'Bolsa auxílio';
    if (tipoRegimeContratacao == 3) return 'Remuneração total de aulas';
    if (tipoRegimeContratacao == 5) return 'Remuneração total de dias';
    return 'Salário líquido';
  }

  ajustarHoraExtra(horaExtra: any[]): string {
    if (!horaExtra || horaExtra?.length === 0) return ' - ';
    let soma: number = 0;
    horaExtra.forEach(elem => soma += elem.valor);
    return  this.currencyPipe.transform(soma);
  }
}
