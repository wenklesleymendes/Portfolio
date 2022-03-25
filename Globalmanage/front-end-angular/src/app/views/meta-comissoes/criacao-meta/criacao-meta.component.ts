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
import { MetasService } from 'src/app/services/metas-comissoes/metas.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-criacao-meta',
  templateUrl: './criacao-meta.component.html',
  styleUrls: ['./criacao-meta.component.scss']
})
export class CriacaoMetaComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  form: FormGroup;
  isLoadingResults: boolean = false;
  error: boolean = false;
  cpfMask = CPFMask;
  filterUnidades: Observable<any[]>;
  unidadesDefault = null;
  filterMetas: Observable<any[]>;
  metasDefault = null;
  displayedColumns: string[] = [
    'descricao',
    'unidadeId',
    'inicioMeta',
    'terminoMeta',
    'quantidade',
    'status',
    'options'
  ];

  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private netasService: MetasService,
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
    this.getNomeMetas();
    this.getAll();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      unidadeId: [null],
      unidadeSelect: [''],
      nomeMeta: ['']
    });

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

    this.filterMetas = this.form.get('nomeMeta').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterMetas(elem) : this.metasDefault.slice())
      );
  }

  getAll() {
    this.isLoadingResults = true;
    this.error = false;

    const formValue = this.form.value;
    delete formValue.unidadeSelect;

    this.netasService.getAllFiltro(formValue)
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

  getNomeMetas(): void {
    this.netasService.getListaNomes()
      .subscribe(val => {
        if (val['status'] === 'error') this.error = true;
        else this.metasDefault = val;
        this.form.get('nomeMeta').setValue('');
      });
  }

  _filterUnidades(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  _filterMetas(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.metasDefault.filter(elem => elem.toLowerCase().indexOf(filterValue) === 0);
  }

  ajustarUnidade(unidadeId: any[]): string {
    if (this.unidadesDefault == null || this.unidadesDefault.length == 0) return ' - ';
    const unidade =  this.unidadesDefault.find(elem => elem.id == unidadeId);
    return unidade ? unidade.nome : ' - ';
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0', replicar: boolean = false): void {
    this.router.navigateByUrl(`metas-comissoes/criacao-metas/adicionar/${id}`, { state: { replicar } })
  }

  goPainel(element: any): void {
    this.router.navigateByUrl(`metas-comissoes/painel-metas-comissoes`, { state: { element } });
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.netasService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
