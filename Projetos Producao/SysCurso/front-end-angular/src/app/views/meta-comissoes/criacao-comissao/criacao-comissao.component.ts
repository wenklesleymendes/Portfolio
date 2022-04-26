import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { CPFMask } from 'src/app/utils/mask/mask';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { CompareInitialEndDate } from 'src/app/utils/form-validation/initial-end-dates.validation';
import { ComissoesService } from 'src/app/services/metas-comissoes/comissoes.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-criacao-comissao',
  templateUrl: './criacao-comissao.component.html',
  styleUrls: ['./criacao-comissao.component.scss']
})
export class CriacaoComissaoComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  form: FormGroup;
  isLoadingResults: boolean = false;
  error: boolean = false;
  cpfMask = CPFMask;
  filterUnidades: Observable<any[]>;
  unidadesDefault = [];
  displayedColumns: string[] = [
    'unidadeId',
    'dataInicioVirgencia',
    'dataFimVirgencia',
    'tipoPagamento',
    'options'
  ];

  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private comissoesService: ComissoesService,
    private unidadeService: UnidadeService,
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
      unidadeId: [null],
      unidadeSelect: [''],
      tipoPagamento: [null],
      dataInicio: [null],
      dataFim: [null]
    });

    this.form.setValidators([CompareInitialEndDate('dataInicio', 'dataFim')]);

    this.filterUnidades = this.form.get('unidadeSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
      );

    this.form.get('unidadeSelect').valueChanges.subscribe(val => {
      const unidadeId = this.unidadesDefault?.length >= 0 ? this.unidadesDefault.find(elem => elem.nome == val) : null;
      if (unidadeId && unidadeId.id) this.form.get('unidadeId').setValue(unidadeId.id);
      else this.form.get('unidadeId').setValue(null);
    })
  }

  getAll() {
    this.isLoadingResults = true;
    this.error = false;

    const formValue = this.form.value;
    delete formValue.unidadeSelect;

    this.comissoesService.getAllFiltro(formValue)
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

  ajustarUnidade(unidadeId: any[]): string {
    if (this.unidadesDefault == null || this.unidadesDefault.length == 0) return ' - ';
    const unidade =  this.unidadesDefault.find(elem => elem.id == unidadeId);
    return unidade ? unidade.nome : ' - ';
  }

  ajustarPagamento(tipoPagamento) : string {
    if (tipoPagamento == 1) return 'Cartão de crédito';
    else if (tipoPagamento == 2) return 'Cartão de débito';
    else if (tipoPagamento == 3) return 'Boleto bancário';
    else return ' - ';
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`metas-comissoes/criacao-comissao/adicionar/${id}`)
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.comissoesService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
