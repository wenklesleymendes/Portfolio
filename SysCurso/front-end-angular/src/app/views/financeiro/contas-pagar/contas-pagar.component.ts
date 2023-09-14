import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { CPFMask, CNPJMask, CPFCNPJMask } from 'src/app/utils/mask/mask';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { CompareInitialEndDate } from 'src/app/utils/form-validation/initial-end-dates.validation';
import { startWith, map } from 'rxjs/operators';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { MatDialog } from '@angular/material/dialog';
import { AnexoContasPagarComponent } from './anexo-contas-pagar/anexo-contas-pagar.component';
import { ImprimirContasPagarComponent } from './imprimir-contas-pagar/imprimir-contas-pagar.component';
import { ContasPagarDetalheComponent } from './contas-pagar-detalhe/contas-pagar-detalhe.component';
import { ContasPagarService } from 'src/app/services/financeiro/contas-pagar.service';
import { CategoriaService } from 'src/app/services/financeiro/categoria.service';
import { validarCPF } from 'src/app/utils/form-validation/cpf.validation';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-contas-pagar',
  templateUrl: './contas-pagar.component.html',
  styleUrls: ['./contas-pagar.component.scss']
})
export class ContasPagarComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  form: FormGroup;
  isLoadingResults: boolean = false;
  error: boolean = false;
  cpfCnpjMask = CPFCNPJMask;
  cpfMask = CPFMask;
  filterUnidades: Observable<any[]>;
  unidadesDefault: any[] = null;
  displayedColumns: string[] = [
    'unidade',
    'nomeDespesa',
    'categoria',
    'formaPagamento',
    'fornecedor',
    'vencimento',
    'numeroParcela',
    'valorParcela',
    'status',
    'options'
  ];
  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  totalDespesa: number = null;
  filterCategorias: Observable<any[]> = null;
  categoriasDefault = null;
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private contasPagarService: ContasPagarService,
    private categoriaService: CategoriaService,
    private unidadeService: UnidadeService,
    private dialog: MatDialog,
    private fb: FormBuilder,
    private router: Router
  ) { }
  
  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.buildForm();
    this.getunidades();
    this. getCategoria();
    this.getAll();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      tipoPessoa: [null],
      unidadeId: [null],
      unidadeSelect: [''],
      inicioPeriodo: [null],
      fimPeriodo: [null],
      statusPagamento: [null],
      tipoPagamento: [null],
      categoria: [null],
      cpf: [null, [validarCPF]],
    });

    this.form.setValidators([CompareInitialEndDate('inicioPeriodo', 'fimPeriodo')]);

    this.form.get('cpf').valueChanges.subscribe((val: string) => {
      this.cpfCnpjMask = val.length > 11 ? CNPJMask : CPFCNPJMask;
    })

    this.filterUnidades = this.form.get('unidadeSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
      );

    this.form.get('unidadeSelect').valueChanges.subscribe(val => {
      const unidadeId = this.unidadesDefault?.length > 0 ? this.unidadesDefault.find(elem => elem.nome == val) : null;
      if (unidadeId && unidadeId.id) this.form.get('unidadeId').setValue(unidadeId.id);
      else this.form.get('unidadeId').setValue(null);
    })

    this.filterCategorias = this.form.get('categoria').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterCategoria(elem) : this.categoriasDefault.slice())
      );
  }

  getAll() {
    this.isLoadingResults = true;
    this.error = false;

    const formValue = this.form.value;
    delete formValue.unidadeSelect;

    this.contasPagarService.getAll(formValue)
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else {
          this.dataSource.data = val.despesa;
          this.totalDespesa = val.valorTotalDespesa;
        };
      });
  }

  getunidades(): void {
    this.unidadeService.getAll()
      .subscribe(val => {
        if (val['status'] === 'error') this.error = true;
        else this.unidadesDefault = val;
        this.form.get('unidadeSelect').setValue('');
      });
  }

  getCategoria(): void {
    this.categoriasDefault = [];
    this.form.get('categoria').setValue('');

    this.categoriaService.getAll()
      .subscribe(val => {
        if (val['status'] === 'error') {
          this.error = true;
        }
        else this.categoriasDefault = val;
        this.form.get('categoria').setValue('');
      });
  }

  _filterUnidades(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  _filterCategoria(value): any[] {
    if(!value) return;
    const filterValue: string = null;
    if(value?.descricao) value.descricao.toLowerCase();
    else value.toLowerCase();
    return this.categoriasDefault.filter(elem => elem.descricao.toLowerCase().indexOf(filterValue) === 0);
  }

  ajustarFormaPagamento(tipoPagamento): string {
    if (!tipoPagamento) return ' - ';
    if (tipoPagamento === 1) return 'Cartão de Crédito';
    if (tipoPagamento === 2) return 'Cartão de Débito';
    if (tipoPagamento === 3) return 'Boleto Bancário';
    return ' - ';
  }

  ajustarCorStatus(statusDespesa): string {
    if (!statusDespesa) return '';
    else if (statusDespesa == 1) return 'bg-yellow';
    else if (statusDespesa == 2) return 'bg-green';
    else if (statusDespesa == 3) return 'bg-orange';
    else if (statusDespesa == 4) return 'bg-red';
    else return '';
  }

  ajustarLabelStatus(statusDespesa): string {
    if (!statusDespesa) return ' - ';
    else if (statusDespesa == 1) return 'Em Aberto';
    else if (statusDespesa == 2) return 'Liquidado';
    else if (statusDespesa == 3) return 'Despesa Recorrente';
    else if (statusDespesa == 4) return 'Cancelado';
    else return ' - ';
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`financeiro/contas-pagar/adicionar/${id}`)
  }

  openAnexo(id: string = '0', nome: string = ''): void {
    const dialogRef = this.dialog.open(AnexoContasPagarComponent, {
      width: '90vw',
      data: { id, nome },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getAll();
    });
  }

  openImprimir(id: string = '0', nome: string = ''): void {
    const dialogRef = this.dialog.open(ImprimirContasPagarComponent, {
      width: '50vw',
      data: { id, nome },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getAll();
    });
  }

  openDetalhe(id: string = '0', nome: string = ''): void {
    const dialogRef = this.dialog.open(ContasPagarDetalheComponent, {
      width: '90vw',
      data: { id, nome },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getAll();
    });
  }


  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.contasPagarService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
