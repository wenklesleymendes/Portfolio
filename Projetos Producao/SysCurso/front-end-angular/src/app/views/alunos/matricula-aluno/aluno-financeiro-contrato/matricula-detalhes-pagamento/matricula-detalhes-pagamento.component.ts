import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-matricula-detalhes-pagamento',
  templateUrl: './matricula-detalhes-pagamento.component.html',
  styleUrls: ['./matricula-detalhes-pagamento.component.scss']
})
export class MatriculaDetalhesPagamentoComponent implements OnInit {
  displayedColumns: string[] = ['label', 'value'];
  dataSource: any[] = [
    { label: 'Valor Pago: Cartão de Crédito', value: 'R$ 12x R$ 159,00' },
    { label: 'Valor Pago: Cartão de Débito',  value: '----------' },
    { label: 'Parcelamento Boleto',           value: '----------' },
    { label: 'Valor da Apostila',             value: 'R$ 138,00' },
    { label: 'Saldo Devedor',                 value: 'R$ 0,00' },
    { label: 'Valor A Pagar',                 value: 'R$ 1.908,00' }
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
