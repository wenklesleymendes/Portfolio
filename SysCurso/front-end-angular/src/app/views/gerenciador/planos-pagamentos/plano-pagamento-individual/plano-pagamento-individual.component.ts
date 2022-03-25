import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { HourMinuteMask } from 'src/app/utils/mask/mask';
import { MatTableDataSource } from '@angular/material/table';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DeleteService } from 'src/app/services/delete.service';
import { ActivatedRoute } from '@angular/router';
import { CursoService } from 'src/app/services/gerenciador/curso.service';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { Observable, combineLatest } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { AnimationsService } from 'src/app/services/animations.service';
import { PlanoPagamentoService } from 'src/app/services/gerenciador/plano-pagamento.service';
import { FormService } from 'src/app/services/form.service';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-plano-pagamento-individual',
  templateUrl: './plano-pagamento-individual.component.html',
  styleUrls: ['./plano-pagamento-individual.component.scss']
})
export class PlanoPagamentoIndividualComponent implements OnInit {
  form: FormGroup;
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;
  hourMinute = HourMinuteMask;
  displayedColumns: string[] = ['descricao', 'options'];
  dataUnidadeSource = new MatTableDataSource();
  dataCursoSource = new MatTableDataSource();
  unidadesDefault = [];
  unidades = [];
  cursosDefault = [];
  cursos = [];
  filterUnidades: Observable<any[]>;
  filterCursos: Observable<any[]>;
  onlyNumbers = /^-?(0|[1-9]\d*)?$/;
  parcelas = Array(12).fill('').map((x,i) => i+1);
  valorTotalInscricaoProva2x: number = 0;
  selectionCursos = new SelectionModel<any>(true, []);
  selectionUnidades = new SelectionModel<any>(true, []);

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private deleteService: DeleteService,
    private unidadeService: UnidadeService,
    private cursoService: CursoService,
    private animationsService: AnimationsService,
    private planoPagamentoService: PlanoPagamentoService,
    private routerActive: ActivatedRoute,
    private formService: FormService
  ) {
    // Get id
    const id = this.routerActive.snapshot.paramMap.get('id');
    this.id = id ? parseInt(id) : 0;
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.buildForm();
    this.getunidades();
    this.getCursos();
    this.loadData();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      tipoPagamento: [1],
      valorTotalPlano: [null, [Validators.required]],
      quantidadeParcela: [null, [Validators.required]],
      valorParcela: [{ disabled: true, value: null}, [Validators.required]],
      porcentagemDescontoPontualidade: [{ disabled: true, value: null}, [Validators.required]],
      valorTotalInscricaoProva: [{ disabled: true, value: null}, [Validators.required]],
      valorMaterialDidatico: [null, [Validators.required]],
      isentarMaterialDidatico: [false],
      valorTaxaMatricula: [null, [Validators.required]],
      isentarMatricula: [false],
      isActive: [false],
      cursoSelected: [null],
      unidadeSelected: [null]
    });

    this.filterUnidades = this.form.get('unidadeSelected').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
      );

    this.filterCursos = this.form.get('cursoSelected').valueChanges
    .pipe(
      startWith(''),
      map(elem => elem ? this._filterCursos(elem) : this.cursosDefault.slice())
    );

    this.form.get('valorTotalInscricaoProva').valueChanges.subscribe(val => {
      if (!val || val == '') return this.valorTotalInscricaoProva2x = 0;
      this.valorTotalInscricaoProva2x = parseFloat(val) / 2;
    })

    this.form.get('isentarMaterialDidatico').valueChanges.subscribe(val => {
      if (val) this.form.get('valorMaterialDidatico').disable();
      else this.form.get('valorMaterialDidatico').enable();
      if (val) this.form.get('valorMaterialDidatico').reset();
    })

    this.form.get('isentarMatricula').valueChanges.subscribe(val => {
      if (val) this.form.get('valorTaxaMatricula').disable();
      else this.form.get('valorTaxaMatricula').enable();
      
      if (val) this.form.get('valorTaxaMatricula').reset();
    })

    this.form.get('tipoPagamento').valueChanges.subscribe(val => {
      if(val === 3) {
        this.formService.enableField(this.form.get('porcentagemDescontoPontualidade'));
        this.formService.enableField(this.form.get('valorTotalInscricaoProva'));
      } else {
        this.formService.disableField(this.form.get('porcentagemDescontoPontualidade'));
        this.formService.disableField(this.form.get('valorTotalInscricaoProva'));
      }

      if(val === 2) this.formService.disableField(this.form.get('quantidadeParcela'), 1);
      else this.formService.enableField(this.form.get('quantidadeParcela'));
    });

    combineLatest(
      this.form.get('valorTotalPlano').valueChanges,
      this.form.get('quantidadeParcela').valueChanges
    ).subscribe(val => {
      let valor: number = 0;
      if(val[0] && val[1]) valor = (val[0] / val[1]);
      this.form.get('valorParcela').setValue(valor);
    });
    

    this.form.get('id').setValue(this.id);
  }

  loadData(): void {
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.planoPagamentoService.getPorId(this.id).subscribe(val => {
        if (!val) return;
        if (val['status'] === 'error') this.error = true;
        const { curso, unidade } = val;
        this.form.patchValue(val);

        // Add 'curso'
        curso.forEach(elem => this.cursos.push({ id: elem.id, descricao: elem.descricao }));
        this.dataCursoSource.data = this.cursos;
        // Add 'unidade'
        unidade.forEach(elem => this.unidades.push({ id: elem.id, nome: elem.nome }));
        this.dataUnidadeSource.data = this.unidades;

        this.isLoadingResults = false;
      })
    }
  }

  getCursos(): void {
    this.cursoService.getCursos().subscribe(val => {
      if (!val) return;
      if (val['status'] === 'error') this.error = true;
      this.cursosDefault = val;
      this.form.get('cursoSelected').reset();
    });
  }

  getunidades(): void {
    this.unidadeService.getAll()
      .subscribe(val => {
        if (!val) return;
        if (val['status'] === 'error') this.error = true;
        else this.unidadesDefault = val;
        this.form.get('unidadeSelected').reset();
        this.isLoadingResults = false;
      });
  }

  addUnidade(): void {
    // Get 'unidade' select
    const { unidadeSelected } = this.form.value;
    if (!unidadeSelected) return;
    // Check if 'unidade' exists
    const unidadeId = this.unidadesDefault?.length > 0 ? this.unidadesDefault.find(elem => elem.nome == unidadeSelected) : null;
    if (!unidadeId || unidadeId.length == 0) return;
    // Check if isn't alredy selected
    const alredySelected = this.unidades.find(elem => elem == unidadeId);
    if (alredySelected) {
      this.animationsService.showErrorSnackBar('Unidade já selecionada');
      return;
    }
    // Add 'unidade'
    this.unidades.push({ id: unidadeId.id, nome: unidadeId.nome });
    this.dataUnidadeSource.data = this.unidades;

    this.form.get('unidadeSelected').reset();
  }

  addCurso(): void {
    // Get 'curso' select
    const { cursoSelected } = this.form.value;
    if (!cursoSelected) return;
    // Check if 'curso' exists
    const cursoId = this.cursosDefault?.length > 0 ? this.cursosDefault.find(elem => elem.descricao == cursoSelected) : null;
    if (!cursoId || cursoId.length == 0) return;
    // Check if isn't alredy selected
    const alredySelected = this.cursos.find(elem => elem.id == cursoId);
    if (alredySelected) {
      this.animationsService.showErrorSnackBar('Curso já selecionado');
      return;
    }
    // Add 'curso'
    this.cursos.push({ id: cursoId.id, descricao: cursoId.descricao });
    this.dataCursoSource.data = this.cursos;

    this.form.get('cursoSelected').reset();
  }

  _filterUnidades(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  _filterCursos(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.cursos.filter(elem => elem.descricao.toLowerCase().indexOf(filterValue) === 0);
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  voltar(): void {
    this.location.back();
  }

  salvarData(): void {
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    const formValue = this.form.getRawValue()
    // Casting form values
    // formValue['tipoPagamento']                    = parseFloat(formValue['tipoPagamento']);
    formValue['quantidadeParcela']                = parseFloat(formValue['quantidadeParcela']);
    formValue['valorParcela']                     = parseFloat(formValue['valorParcela']);
    formValue['valorTotalPlano']                  = parseFloat(formValue['valorTotalPlano']);
    formValue['porcentagemDescontoPontualidade']  = parseFloat(formValue['porcentagemDescontoPontualidade']);
    formValue['valorTotalInscricaoProva']         = parseFloat(formValue['valorTotalInscricaoProva']);
    formValue['valorMaterialDidatico']            = formValue['valorMaterialDidatico'] ? parseFloat(formValue['valorMaterialDidatico']) : 0;
    formValue['valorTaxaMatricula']               = formValue['valorTaxaMatricula'] ? parseFloat(formValue['valorTaxaMatricula']) : 0;
    // Adding 'unidades'
    delete formValue.unidadeSelected;
    let unidade = [];
    this.unidades.forEach(elem => unidade.push({ id: elem.id }));
    // Adding 'curso'
    delete formValue.cursoSelected;
    let curso = [];
    this.cursos.forEach(elem => curso.push({ id: elem.id }));
    // Unifying data
    const data = {...formValue, unidade, curso };
    // Make request
    this.planoPagamentoService.cadastrar(data).subscribe(val => {
      if (val?.status) return;
      if(val?.isSuccess === false && val?.mensagem) {
        this.animationsService.showErrorSnackBar(val?.mensagem);
        return;
      }
      this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
      this.id = val.id;
      this.form.get('id').setValue(val.id);
    })
  }

  removeUnidade(index: number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      let data = this.dataUnidadeSource.data;
      data.splice(index, 1);
      this.dataUnidadeSource.data = data;
    });
  }

  removeCurso(index: number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      let data = this.dataCursoSource.data;
      data.splice(index, 1);
      this.dataCursoSource.data = data;
    });
  }

}
