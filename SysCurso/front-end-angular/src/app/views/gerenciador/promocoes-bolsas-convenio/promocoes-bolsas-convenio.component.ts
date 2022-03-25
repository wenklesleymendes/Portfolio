import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { CampanhaService } from 'src/app/services/gerenciador/campanha.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-promocoes-bolsas-convenio',
  templateUrl: './promocoes-bolsas-convenio.component.html',
  styleUrls: ['./promocoes-bolsas-convenio.component.scss']
})
export class PromocoesBolsasConvenioComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  isLoadingResults: boolean = false;
  error: boolean = false;
  displayedColumns: string[] = [
    'nome',
    'descontoAplicado',
    'plano',
    'matricula',
    'materialDidatico',
    'validadeDesconto',
    'curso',
    'unidades',
    'options'
  ];

  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private campanhaService: CampanhaService,
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

    this.campanhaService.getAll()
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else this.dataSource.data = val;
      });
  }

  ajustarUnidade(unidades: any[]): string {
    if (!unidades || unidades.length == 0) return ' - ';
    let text = '';
    unidades.forEach(elem => text += `${elem.nomeUnidade}, `)
    return text.substring(0, text.length - 2);
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`gerenciador/promocoes-bolsas-convenio/adicionar/${id}`)
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.campanhaService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
