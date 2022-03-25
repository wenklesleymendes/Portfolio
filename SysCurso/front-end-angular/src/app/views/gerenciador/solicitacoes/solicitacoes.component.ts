import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { DeleteService } from 'src/app/services/delete.service';
import { SolicitacoesService } from 'src/app/services/gerenciador/solicitacoes.service';
import { Router } from '@angular/router';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-solicitacoes',
  templateUrl: './solicitacoes.component.html',
  styleUrls: ['./solicitacoes.component.scss']
})
export class SolicitacoesComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  dataSource = new MatTableDataSource([]);
  isLoadingResults: boolean = false;
  error: boolean = false;
  displayedColumns: string[] = ['descricao', 'sla', 'tipo', 'options'];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private solicitacoesService: SolicitacoesService,
    private deleteService: DeleteService,
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

    this.solicitacoesService.getAll()
    .subscribe(val => {
      if (!val) return;
      if (val['status'] === 'error') this.error = true;
      else this.dataSource.data = val;
      this.isLoadingResults = false;
    });
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`gerenciador/solicitacoes/adicionar/${id}`)
  }

  delete(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.solicitacoesService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
