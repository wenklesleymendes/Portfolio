import { Component, OnInit, Input } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-detalhe-pendente',
  templateUrl: './detalhe-pendente.component.html',
  styleUrls: ['./detalhe-pendente.component.scss']
})
export class DetalhePendenteComponent implements OnInit {
  @Input() despesaParcelas;
  displayedColumns: string[] = ['vencimento', 'parcela', 'formaPagamento', 'pago'];
  dataSource = new MatTableDataSource([]);
  selection = new SelectionModel<any>(true, []);

  constructor( ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    if (this.despesaParcelas) {
      this.dataSource.data = this.despesaParcelas;
    }
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  ajustarTipoPagamento(tipoPagamento): string {
    if (!tipoPagamento) return ' - ';
    else if (tipoPagamento === 1) return 'Cartão de crédito';
    else if (tipoPagamento === 2) return 'Cartão de débito';
    else if (tipoPagamento === 3) return 'Boleto bancário';
    else if (tipoPagamento === 4) return 'Transação bancária';
    else if (tipoPagamento === 5) return 'Dinheiro';
    else if (tipoPagamento === 6) return 'Guia de Pagamento';
    else return ' - ';
  }
}
