import { Component, OnInit, ViewChild, Input, OnChanges, SimpleChanges } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { CompareInitialEndDate } from 'src/app/utils/form-validation/initial-end-dates.validation';
import { startWith, map } from 'rxjs/operators';
import { MetasService } from 'src/app/services/metas-comissoes/metas.service';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { SelectionModel } from '@angular/cdk/collections';

export const MY_FORMATS = {
  parse: {
    dateInput: 'MM/YYYY',
  },
  display: {
    dateInput: 'MM/YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'app-minhas-comissoes',
  templateUrl: './minhas-comissoes.component.html',
  styleUrls: ['./minhas-comissoes.component.scss'],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS]
    },

    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ],
})
export class MinhasComissoesComponent implements OnInit, OnChanges {
  @Input() metaComissoes: any = [];
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  filterUnidades: Observable<any[]>;
  form: FormGroup;
  unidadesDefault = [];
  tipoComissao: FormControl = new FormControl(3);
  dataSource = new MatTableDataSource([]);
  dataDefault: any[] = [];
  isLoadingResults: boolean = false;
  error: boolean = false;
  displayedColumns: string[] = ['unidade', 'data', 'quantidadePrimeiraParcelaPaga', 'valorComissao'];
  total: number = null;
  selection = new SelectionModel<any>(true, []);

  constructor(
    private unidadeService: UnidadeService,
    private metaService: MetasService,
    private animationsService: AnimationsService,
    private formService: FormService,
    private fb: FormBuilder
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnChanges(changes: SimpleChanges): void {
    if (!changes && !changes.metaComissoes) return;
    const { currentValue } = changes.metaComissoes;
    this.dataSource.data = currentValue?.minhasComissoes ? currentValue.minhasComissoes : [];
    this.dataDefault = currentValue?.minhasComissoes ? currentValue.minhasComissoes : [];
    this.total = currentValue?.total;
  }

  ngOnInit() {
    this.buildForm();

    this.dataSource.paginator = this.paginator;
    this.getunidades();
    this.dataSource.data = this.metaComissoes?.minhasComissoes ? this.metaComissoes.minhasComissoes : [];

    this.tipoComissao.valueChanges.subscribe(val => {
      if (val === 3) return this.dataSource.data = this.dataDefault;

      this.dataSource.data = this.dataDefault.filter(elem => {
        if (val === 1) return elem.comissaoEquipe === true;
        else if (val === 2) return elem.comissaoEquipe === false
        return elem.comissaoEquipe
      });
    });
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      unidadeId: [null],
      unidadeSelect: [''],
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

  getunidades(): void {
    this.unidadeService.getAll()
      .subscribe(val => {
        if (val['status'] === 'error') this.error = true;
        else this.unidadesDefault = val;
        this.form.get('unidadeSelect').setValue('');
      });
  }

  getAll() {
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    // this.isLoadingResults = true;
    // this.error = false;

    // const data = { ...this.form.value };
    // delete data.unidadeSelect;

    // this.metaComissoes.getDashboard(data)
    //   .subscribe(val => {
    //     this.isLoadingResults = false;
    //     if (val['status'] === 'error') this.error = true;
    //     else {

    //     }
    //   });
  }

  ajustarUnidade(unidadeId: any[]): string {
    if (this.unidadesDefault == null || this.unidadesDefault.length == 0) return ' - ';
    const unidade = this.unidadesDefault?.length >= 0 ? this.unidadesDefault.find(elem => elem.id == unidadeId) : null;
    return unidade ? unidade.nome : ' - ';
  }

  _filterUnidades(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  closeDatePicker(eventData: any, dp: any, control: string) {
    this.form.get(control).setValue(eventData)
    dp.close();
  }
}
