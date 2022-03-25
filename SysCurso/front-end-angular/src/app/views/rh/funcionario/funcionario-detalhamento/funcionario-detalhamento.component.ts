import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FuncionarioService } from 'src/app/services/rh/funcionario.service';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-funcionario-detalhamento',
  templateUrl: './funcionario-detalhamento.component.html',
  styleUrls: ['./funcionario-detalhamento.component.scss']
})
export class FuncionarioDetalhamentoComponent implements OnInit {
  error: boolean = false;
  isLoadingResults: boolean = false;
  detalhamento: any = null;
  displayedColumns: string[] = ['dataVencimento', 'feriasConcecidaInicio', 'feriasConcecidaTermino', 'tipoFerias', 'diasVencimento'];
  dataSource = new MatTableDataSource([]);
  selection = new SelectionModel<any>(true, []);

  constructor(
    public dialogRef: MatDialogRef<FuncionarioDetalhamentoComponent>,
    private funcionarioService: FuncionarioService,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.getAnexo();
  }

  labelAusensia(tipoFeriasFolgaFalta): string {
    if (tipoFeriasFolgaFalta == 1) return 'Folga';
    if (tipoFeriasFolgaFalta == 2) return 'Falta';
    if (tipoFeriasFolgaFalta == 3) return 'Férias Gozadas 30 dias de Descanso';
    if (tipoFeriasFolgaFalta == 4) return 'Férias Vendidas 30 dias';
    if (tipoFeriasFolgaFalta == 5) return 'Férias Gozadas 15 dias de Descanso + 15 dias vendidos';
    if (tipoFeriasFolgaFalta == 6) return 'Férias Gozadas 20 dias de Descanso + 10 dias vendidos';
    return ' - ';
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getAnexo(): void {
    this.isLoadingResults = true;
    this.funcionarioService.getDetalhamento(this.data['id']).subscribe(val => {
      this.isLoadingResults = false;
      this.detalhamento = val;
      this.dataSource.data = val?.feriasDataDetalhadas;
    })
  }
}
