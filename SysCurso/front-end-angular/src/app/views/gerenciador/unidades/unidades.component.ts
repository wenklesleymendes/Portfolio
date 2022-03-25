import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { CentroCustoComponent } from './centro-custo/centro-custo.component';
import { AnexoComponent } from './anexo/anexo.component';
import { DeleteService } from 'src/app/services/delete.service';
import { CelMask, TelMask } from 'src/app/utils/mask/mask';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-unidades',
  templateUrl: './unidades.component.html',
  styleUrls: ['./unidades.component.scss']
})
export class UnidadesComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  isLoadingResults: boolean = false;
  error: boolean = false;
  displayedColumns: string[] = [
    'nome',
    'cnpj',
    'razaoSocial',
    'nomeFantasia',
    'contato',
    'vigenciaTerminoAVCB',
    'vigenciaTerminoAlvara',
    'options'
  ];

  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private unidadeService: UnidadeService,
    private dialog: MatDialog,
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

    this.unidadeService.getAll()
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else this.dataSource.data = val;
      });
  }

  stillValid(date: string): boolean {
    const today: Date = new Date();
    const passedDay: Date = new Date(date);
    passedDay.setDate(passedDay.getDate() + 1);
    return today <= passedDay;
  }

  chooserMask(contact: string): string {
    return contact.length > 10 ? CelMask : TelMask;
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goUnidadeIndividual(id: string = '0'): void {
    this.router.navigate([`gerenciador/unidades/adicionar/${id}`])
  }

  openCentroCusto(id: string = '0'): void {
    const dialogRef = this.dialog.open(CentroCustoComponent, {
      width: '90vw',
      data: { id },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => { });
  }

  openAnexo(id: string = '0', nome: string = ''): void {
    const dialogRef = this.dialog.open(AnexoComponent, {
      width: '90vw',
      data: { id, nome },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => { });
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.unidadeService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
