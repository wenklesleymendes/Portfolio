import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { ColegioAutorizadoService } from 'src/app/services/provas/colegio-autorizado.service';
import { TelMask, CNPJMask } from 'src/app/utils/mask/mask';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-colegio-autorizado',
  templateUrl: './colegio-autorizado.component.html',
  styleUrls: ['./colegio-autorizado.component.scss']
})
export class ColegioAutorizadoComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  isLoadingResults: boolean = false;
  error: boolean = false;
  displayedColumns: string[] = [
    'nome',
    'razaoSocial',
    'cnpj',
    'contatoNome',
    'telefone',
    'email',
    'options'
  ];

  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  cnpjMask = CNPJMask;
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private colegioAutorizadoService: ColegioAutorizadoService,
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

    this.colegioAutorizadoService.getAll()
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else this.dataSource.data = val;
      });
  }

  ajustarMaskTelefone(telefoneFixo: string): string {
    if (!telefoneFixo) return '';
    else if (telefoneFixo.length === 10) return TelMask;
    else if (telefoneFixo.length === 11) return TelMask;
    return '';
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`provas/colegio-autorizado/adicionar/${id}`)
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.colegioAutorizadoService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
