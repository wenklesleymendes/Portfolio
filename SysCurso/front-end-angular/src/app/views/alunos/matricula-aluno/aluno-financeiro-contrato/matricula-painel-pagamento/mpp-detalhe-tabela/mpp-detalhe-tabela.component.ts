import { Component, OnInit, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MppDetalheEmailComponent } from '../mpp-detalhe-email/mpp-detalhe-email.component';
import { MppDetalhePagamentoComponent } from '../mpp-detalhe-pagamento/mpp-detalhe-pagamento.component';
import { MatTableDataSource } from '@angular/material/table';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-mpp-detalhe-tabela',
  templateUrl: './mpp-detalhe-tabela.component.html',
  styleUrls: ['./mpp-detalhe-tabela.component.scss']
})
export class MppDetalheTabelaComponent implements OnInit {
  @Input() planoPagamento: any;
  defaultColumns: string[] = ['descricao', 'valor', 'desconto', 'promocao', 'tarifa', 'valorVencimento', 'data', 'numero', 'email', 'situacao'];
  displayedColumns: string[] = [];
  dataSource = new MatTableDataSource([]);
  selection = new SelectionModel<any>(true, []);

  constructor(
    private dialog: MatDialog
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    if(this.planoPagamento) {
      this.dataSource.data = this.planoPagamento;
      this.displayedColumns = this.validateColumns(this.planoPagamento);
    }
  }
  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  corLinhaParcela(row): string {
    if(row?.tipoSituacao === 1 || row?.tipoSituacao === 3) return 'bg-light-green ';
    else if(row?.tipoSituacao === 4 || row?.tipoSituacao === 5) return 'bg-light-red ';
    else if(row?.tipoSituacao === 6) return 'bg-light-orange';
    else return '';
  }

  colorPago(element): string {
    if(!element?.valorPago || !element?.valorVencimento || !element?.valor || !element?.dataVencimento || !element?.dataPagamento) return '';

    const { valorPago, valorVencimento, valor, tarifaBanco } = element;
    const dataVencimento = new Date(element.dataVencimento);
    const dataPagamento = new Date(element.dataPagamento);
    
    const tarifa = tarifaBanco ? tarifaBanco : 0;
    if((dataPagamento <= dataVencimento) && ((valorPago + tarifa) >= valorVencimento)) return 'green';
    else if((dataPagamento > dataVencimento) && ((valorPago + tarifa) >= valor)) return 'green';
    else return 'red' ;
  }

  validateColumns(pagamentos?: any[]): string[] {
    if(!(pagamentos?.length > 0)) return this.defaultColumns;
    let hasPromocao: boolean = false;
    pagamentos.forEach(elem => {
      if(elem?.promocaoBolsaConvenio) hasPromocao = true;
    });

    if(hasPromocao) return this.defaultColumns;
    else return this.defaultColumns.filter(elem => elem !== 'promocao');
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  openDetalheEmail(id): void {
    const dialogRef = this.dialog.open(MppDetalheEmailComponent, {
      width: '50vw',
      autoFocus: false,
      data: { id }
    });
    dialogRef.afterClosed().subscribe(result => {
      
    });
  }

  openDetalhePagamento(): void {
    const dialogRef = this.dialog.open(MppDetalhePagamentoComponent, {
      width: '50vw',
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      
    });
  }
}
