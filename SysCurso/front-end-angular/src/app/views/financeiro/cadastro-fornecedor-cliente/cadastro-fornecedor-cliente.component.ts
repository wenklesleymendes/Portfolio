import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { MatDialog } from '@angular/material/dialog';
import { AnexoFornecedorComponent } from './anexo-fornecedor/anexo-fornecedor.component';
import { FornecedorService } from 'src/app/services/financeiro/fornecedor.service';
import { CPFMask, CNPJMask, TelMask, CelMask } from 'src/app/utils/mask/mask';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-cadastro-fornecedor-cliente',
  templateUrl: './cadastro-fornecedor-cliente.component.html',
  styleUrls: ['./cadastro-fornecedor-cliente.component.scss']
})
export class CadastroFornecedorClienteComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  isLoadingResults: boolean = false;
  error: boolean = false;
  displayedColumns: string[] = [
    'nome',
    'tipoPessoa',
    'categoria',
    'cpf',
    'email',
    'telefone',
    'ramal',
    'isActive',
    'options'
  ];

  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private fornecedorService: FornecedorService,
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

    this.fornecedorService.getAll()
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else this.dataSource.data = val;
      });
  }

  ajustarTipoPessoa(tipoPessoa): string {
    if (tipoPessoa === 1) return 'Pessoa Jurídica';
    else if (tipoPessoa === 2) return 'Pessoa Física';
    else return ' - ';
  }

  ajustarCpfCnpj(cpfCnpj: string): string {
    if (cpfCnpj.length === 11) return CPFMask;
    else if (cpfCnpj.length === 14) return CNPJMask;
    else return '';
  }

  ajustarMaskTelefone(telefoneFixo: string): string {
    if (!telefoneFixo) return '';
    else if (telefoneFixo.length === 10) return TelMask;
    else if (telefoneFixo.length === 11) return CelMask;
    return '';
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`financeiro/cadastro-fornecedor-cliente/adicionar/${id}`)
  }

  openAnexo(id: string = '0', nome: string = ''): void {
    const dialogRef = this.dialog.open(AnexoFornecedorComponent, {
      width: '90vw',
      data: { id, nome },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => { });
  }

  mudarStatus(id: string = '0'): void {
    this.fornecedorService.desativarAtivar(id).subscribe(val => this.getAll());
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.fornecedorService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
