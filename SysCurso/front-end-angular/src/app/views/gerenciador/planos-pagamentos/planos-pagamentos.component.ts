import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { PlanoPagamentoService } from 'src/app/services/gerenciador/plano-pagamento.service';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-planos-pagamentos',
  templateUrl: './planos-pagamentos.component.html',
  styleUrls: ['./planos-pagamentos.component.scss']
})
export class PlanosPagamentosComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  isLoadingResults: boolean = false;
  error: boolean = false;
  displayedColumns: string[] = [
    'tipoPagamento',
    'quantidadeParcela',
    'valorParcela',
    'valorTotalPlano',
    'curso',
    'unidade',
    'isActive',
    'options'
  ];

  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private planoPagamentoService: PlanoPagamentoService,
    private router: Router
  ) { }
  
  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.getAll();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getAll() {
    this.isLoadingResults = true;
    this.error = false;

    this.planoPagamentoService.getAll()
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else this.dataSource.data = val;
      });
  }

  ajustarTipoPagamento(tipoPagamento): string {
    if (tipoPagamento == null || tipoPagamento.length == 0) return ' - ';
    let text = '';
    if (tipoPagamento === 1) text = 'Cartão de crédito';
    if (tipoPagamento === 2) text = 'Cartão de débito';
    if (tipoPagamento === 3) text = 'Boleto bancário';
    if (tipoPagamento === 7) text = 'Cobrança recorrente';
    return text;
  }

  ajustarUnidade(unidades: any[]): string {
    if (!unidades || unidades.length == 0) return ' - ';
    let text = '';
    unidades.forEach(elem => {
      if(elem != null)
      {
      text += `${elem.nome}, `
      }
    })
    return text.substring(0, text.length - 2);
  }

  ajustarCurso(cursos: any[]): string {
    if (!cursos || cursos.length == 0) return ' - ';
    let text = '';
    cursos.forEach(elem => text += `${elem.descricao}, `)
    return text.substring(0, text.length - 2);
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`gerenciador/planos-pagamentos/adicionar/${id}`)
  }

  mudarStatus(id: string = '0'): void {
    this.planoPagamentoService.desativarAtivar(id).subscribe(val => this.getAll());
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.planoPagamentoService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
