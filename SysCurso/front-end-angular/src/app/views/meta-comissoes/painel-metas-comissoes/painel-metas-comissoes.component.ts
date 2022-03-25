import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { Observable, of } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { Router, Navigation } from '@angular/router';
import { MetasService } from 'src/app/services/metas-comissoes/metas.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';

@Component({
  selector: 'app-painel-metas-comissoes',
  templateUrl: './painel-metas-comissoes.component.html',
  styleUrls: ['./painel-metas-comissoes.component.scss']
})
export class PainelMetasComissoesComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  form: FormGroup;
  isLoadingResults: boolean = false;
  error: boolean = false;
  filterUnidades: Observable<any[]>;
  unidadesDefault: any[] = null;
  filterMetas: Observable<any[]>;
  metasDefault = null;
  graficoDiario: any = null;
  graficoMensal: any = null;
  graficoFinal:  any = null;
  metaComissoes: any = null;
  valorComissaoEquipe: number = null;
  valorComissaoIndividual: number = null;
  state: any = null;
  firstLoad: boolean = true;

  constructor(
    private metasService: MetasService,
    private unidadeService: UnidadeService,
    private animationsService: AnimationsService,
    private formService: FormService,
    private router: Router,
    private fb: FormBuilder
  ) { 
    const currentNavigation: Navigation = this.router.getCurrentNavigation();
    this.state = currentNavigation?.extras?.state;
  }
  
  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit() {
    this.buildForm();
    this.getAll();

    Promise.all([this.getunidades(), this.getNomeMetas()]).then(res => {
      if (this.state) {
        const { unidadeId, descricao } = this.state.element;
        if (unidadeId) {
          this.form.get('unidadeId').setValue(unidadeId);
          const unidade = this.unidadesDefault?.length >= 0 ? this.unidadesDefault.find(elem => elem.id == unidadeId) : null;
          if (unidade?.nome) this.form.get('unidadeSelect').setValue(unidade.nome);
        }
        if (descricao) this.form.get('nomeMeta').setValue(descricao);
    
        if (unidadeId || descricao) this.getAll();
      }
    })

  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      unidadeId: [null],
      unidadeSelect: [null, [Validators.required]],
      nomeMeta: [null, [Validators.required]]
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
    });

    this.filterMetas = this.form.get('nomeMeta').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterMetas(elem) : this.metasDefault.slice())
      );
  }

  getAll(): void {
    if (!this.firstLoad) {
        this.formService.validateAllFields(this.form);
        if (!this.form.valid) {
          this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
          return;
      }
    }

    this.isLoadingResults = true;
    this.error = false;

    const data = { ...this.form.value };
    delete data.unidadeSelect;

    this.metasService.getDashboard(data)
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else {
          this.valorComissaoEquipe = val?.valorComissaoEquipe;
          this.valorComissaoIndividual = val?.valorComissaoIndividual;
          this.graficoDiario = val?.visaoDiaria;
          this.graficoMensal = val?.visaoMensal;
          this.graficoFinal = {
            meta: val?.metaTotal,
            realizado: val?.totalMatriculasRealizadas
          };
          this.metaComissoes = { 
            total: val?.totalMinhasComissoes,
            minhasComissoes: val?.minhasComissoes
          }
        }
      });
  }

  getunidades(): Promise<any> {
    return new Promise(res => {
      this.unidadeService.getAll()
        .subscribe(val => {
          if (val['status'] === 'error') this.error = true;
          else this.unidadesDefault = val;
          this.form.get('unidadeSelect').setValue('');
          res('unidade');
        });
    });
  }

  getNomeMetas(): Promise<any> {
    return new Promise(res => {
      this.metasService.getListaNomes()
        .subscribe(val => {
          if (val['status'] === 'error') this.error = true;
          else this.metasDefault = val;
          this.form.get('nomeMeta').setValue('');
          res('meta');
        });
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
}
