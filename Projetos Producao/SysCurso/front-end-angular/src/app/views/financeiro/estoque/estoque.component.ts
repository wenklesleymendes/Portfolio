import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { MatDialog } from '@angular/material/dialog';
import { HostoricoEstoqueComponent } from './hostorico-estoque/hostorico-estoque.component';
import { EstoqueService } from 'src/app/services/financeiro/estoque.service';
import { EstoqueItemComponent } from './estoque-item/estoque-item.component';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-estoque',
  templateUrl: './estoque.component.html',
  styleUrls: ['./estoque.component.scss']
})
export class EstoqueComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  isLoadingResults: boolean = false;
  error: boolean = false;
  displayedColumns: string[] = [
    'unidade',
    'nomeProduto',
    'codigoInterno',
    'qtdSaida',
    'estoque',
    'options'
  ];

  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private estoqueService: EstoqueService,
    private dialog: MatDialog,
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

    this.estoqueService.getAll()
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else this.dataSource.data = val;
      });
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`financeiro/estoque/adicionar/${id}`)
  }

  openDetalhe(id: number = 0): void {
    const dialogRef = this.dialog.open(HostoricoEstoqueComponent, {
      width: '90vw',
      data: { id },
      autoFocus: false,
      panelClass: 'full-width-dialog'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getAll();
    });
  }

  openItem(element): void {
    const dialogRef = this.dialog.open(EstoqueItemComponent, {
      width: '90vw',
      data: { element },
      autoFocus: false,
      panelClass: 'full-width-dialog'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getAll();
    });
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.estoqueService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
