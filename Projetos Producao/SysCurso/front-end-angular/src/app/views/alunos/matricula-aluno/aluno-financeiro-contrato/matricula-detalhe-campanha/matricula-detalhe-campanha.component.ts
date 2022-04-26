import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { FinanceiroStoreActions, FinanceiroStoreState, FinanceiroStoreSelectors } from 'src/app/_store/financeiro-store';
import { PercentPipe } from '@angular/common';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-matricula-detalhe-campanha',
  templateUrl: './matricula-detalhe-campanha.component.html',
  styleUrls: ['./matricula-detalhe-campanha.component.scss']
})
export class MatriculaDetalheCampanhaComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = ['label', 'value'];
  campanha$: Subscription = null;
  dataSource = new MatTableDataSource([]);

  constructor(
    private storeFinanceiro: Store<FinanceiroStoreState.Financeiro>,
    private percentPipe: PercentPipe,
  ) { }

  ngOnInit(): void {
    this.getCampanha();
  }

  ngOnDestroy(): void {
    this.campanha$.unsubscribe();
    this.storeFinanceiro.dispatch(FinanceiroStoreActions.deleteCampanha());
  }

  getCampanha(): void {
    this.campanha$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectCampanhaFinanceiro)).subscribe(val => {
      if(val?.id) {
        this.adaptTabela(val);
      } else {
        this.dataSource.data = [];
      }
    });
  }

  adaptTabela(data): void {
    const dataTable: any [] = [];
    if(data?.descontoPlanoPagamento) dataTable.push({ label: 'Desconto Plano Pagamento', value: this.adaptTabelaPercentage(data?.descontoPlanoPagamento) });
    if(data?.descontoTaxaMatricula) dataTable.push({ label: 'Desconto Taxa Matrícula', value: this.adaptTabelaPercentage(data?.descontoTaxaMatricula) });
    if(data?.descontoTaxaMateriaDidatico) dataTable.push({ label: 'Desconto Taxa Materia Didático', value: this.adaptTabelaPercentage(data?.descontoTaxaMateriaDidatico) });
    if(data?.descontoTaxaInscricaoProvas) dataTable.push({ label: 'Desconto Taxa Inscrição Prova', value: this.adaptTabelaPercentage(data?.descontoTaxaInscricaoProvas) });

    this.dataSource.data = dataTable;
  }

  adaptTabelaPercentage(val): string {
    return val > 0 ? `${val} %` : '0 %';
  }
}
