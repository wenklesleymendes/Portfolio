import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { CPFMask} from 'src/app/utils/mask/mask';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { MatDialog } from '@angular/material/dialog';
import { FolhaPagamentoDetalheComponent } from './folha-pagamento-detalhe/folha-pagamento-detalhe.component';
import { FolhaPagamentoService } from 'src/app/services/financeiro/folha-pagamento.service';
import { validarCPF } from 'src/app/utils/form-validation/cpf.validation';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-folha-pagamento',
  templateUrl: './folha-pagamento.component.html',
  styleUrls: ['./folha-pagamento.component.scss']
})
export class FolhaPagamentoComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  form: FormGroup;
  isLoadingResults: boolean = false;
  error: boolean = false;
  cpfMask = CPFMask;
  filterUnidades: Observable<any[]>;
  unidadesDefault = [];
  displayedColumns: string[] = [
    'unidade',
    'competencia',
    'nomeColaborador',
    'regimeContratacao',
    'valorPagamento',
    'status',
    'options'
  ];
  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private folhaPagamentoService: FolhaPagamentoService,
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
    this.getAll();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      fornecedor: [null],
      unidadeId: [null],
      nome: [null],
      unidadeSelect: [''],
      inicioPeriodo: [null],
      fimPeriodo: [null],
      cpf: [null, [validarCPF]],
    });

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
  }

  getAll() {
    this.isLoadingResults = true;
    this.error = false;

    const formValue = this.form.value;
    delete formValue.unidadeSelect;

    this.folhaPagamentoService.getAll(formValue)
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else this.dataSource.data = val;
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

  _filterUnidades(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  ajustarRegime(regimeContratacao): string {
    if (!regimeContratacao) return null;
    let text = '';
    if (regimeContratacao === 1) text = 'CLT Seg a Sex';
    if (regimeContratacao === 2) text = 'Estágio Seg a Sex';
    if (regimeContratacao === 3) text = 'Professor Autônomo';
    if (regimeContratacao === 4) text = 'Professor CLT';
    if (regimeContratacao === 5) text = 'Profissional Autônomo';
    if (regimeContratacao === 6) text = 'CLT Seg a Sab';
    if (regimeContratacao === 7) text = 'Estágio Seg a Sab';
    if (regimeContratacao === 8) text = 'Autônomo Pré CLT Seg a Sex';
    if (regimeContratacao === 9) text = 'Autônomo Pré CLT Seg a Sab';
    if (regimeContratacao === 10) text = 'Autônomo Pré Estágio Seg a Sex';
    if (regimeContratacao === 11) text = 'Autônomo Pré Estágio Seg a Sab';
    return text;
  }
  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`financeiro/folha-pagamento/adicionar/${id}`)
  }

  openDetalhe(id: string = '0', nome: string = '', documentosPendentes: string[]): void {
    const dialogRef = this.dialog.open(FolhaPagamentoDetalheComponent, {
      width: '90vw',
      data: { id },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getAll();
    });
  }

  imprimir(id): void {
    this.folhaPagamentoService.imprimir(id);
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.folhaPagamentoService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
