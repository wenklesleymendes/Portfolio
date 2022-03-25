import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { HourMinuteMask } from 'src/app/utils/mask/mask';
import { MatTableDataSource } from '@angular/material/table';
import { FormGroup, FormBuilder, Validators, FormControl, AbstractControl } from '@angular/forms';
import { DeleteService } from 'src/app/services/delete.service';
import { Router, Navigation, ActivatedRoute } from '@angular/router';
import { Periodos } from 'src/app/utils/variables/turma';
import { CompareInitialEndHour } from 'src/app/utils/form-validation/initial-end-hour.validation';
import { CursoService } from 'src/app/services/gerenciador/curso.service';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { AnimationsService } from 'src/app/services/animations.service';
import { TurmaService } from 'src/app/services/gerenciador/turma.service';
import { FormService } from 'src/app/services/form.service';
import * as moment from 'moment';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-turma-individual',
  templateUrl: './turma-individual.component.html',
  styleUrls: ['./turma-individual.component.scss']
})
export class TurmaIndividualComponent implements OnInit {
  form: FormGroup;
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;
  hourMinute = HourMinuteMask;
  displayedColumns: string[] = ['descricao', 'options'];
  dataSource = new MatTableDataSource();
  state: any;
  replicar: boolean = false;
  estados = []
  periodos = Periodos;
  cursos = [];
  unidadesDefault = [];
  unidades = [];
  filterUnidades: Observable<any[]>;
  anos = Array(2).fill('').map((x,i) => (new Date().getUTCFullYear() + i).toString());
  salas = Array(20).fill('').map((x,i) => i+1);
  semestres = Array(4).fill('').map((x,i) => (i+1).toString());
  onlyNumbers = /^-?(0|[1-9]\d*)?$/;
  selection = new SelectionModel<any>(true, []);

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private deleteService: DeleteService,
    private cursoService: CursoService,
    private unidadeService: UnidadeService,
    private turmaService: TurmaService,
    private animationsService: AnimationsService,
    private router: Router,
    private routerActive: ActivatedRoute,
    private formService: FormService
  ) {
    // Get id
    const id = this.routerActive.snapshot.paramMap.get('id');
    this.id = id ? parseInt(id) : 0;
    // Get State
    const currentNavigation: Navigation = this.router.getCurrentNavigation();
    if (currentNavigation && currentNavigation.extras && currentNavigation.extras.state) {
      this.state = currentNavigation.extras.state;
      this.replicar = this.state.replicar;
    }
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.buildForm();
    this.getCursos();
    this.getunidades();
    this.loadData();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      presencial: [null, [Validators.required]],
      ano: [null, [Validators.required]],
      semestre: [null, [Validators.required]],
      periodo: [null, [Validators.required]],
      horarioInicio: [null, [Validators.required]],
      horarioTermino: [null, [Validators.required]],
      sala: [null, [Validators.required]],
      disponivel: [null, [Validators.required]],
      inicioTurma: [null, [Validators.required]],
      terminoTurma: [null, [Validators.required]],
      segunda: [false],
      terca: [false],
      quarta: [false],
      quinta: [false],
      sexta: [false],
      sabado: [false],
      domingo: [false],
      curso: [[], [Validators.required]],
      quantidadeVagas: [null, [Validators.required, Validators.pattern(this.onlyNumbers)]],
      unidadeSelect: [null]
    });

    this.form.setValidators([
      CompareInitialEndHour('horarioInicio', 'horarioTermino')
    ])

    this.filterUnidades = this.form.get('unidadeSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
      );

    this.form.get('presencial').valueChanges.subscribe(val => {
      const itensForms = [ 'periodo', 'horarioInicio', 'horarioTermino', 'sala', 'quantidadeVagas', 'inicioTurma', 'terminoTurma', 'horarioTermino']; 
      const semanas = ['segunda', 'terca', 'quarta', 'quinta', 'sexta', 'sabado', 'domingo' ];

      // Change validations
      itensForms.forEach(elem => {
        if (val) this.formService.enableField(this.form.get(elem));
        else this.formService.disableField(this.form.get(elem));
      });

      semanas.forEach(elem => {
        if (val) {
          this.formService.enableField(this.form.get(elem));
          this.form.get(elem).setValue(false);
        }
        else this.formService.disableField(this.form.get(elem));
      });
    });

    if (!this.replicar) this.form.get('id').setValue(this.id);
  }

  loadData(): void {
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.turmaService.getPorId(this.id).subscribe(val => {
        const { curso, unidade } = val;
        this.form.patchValue(val)

        // Add 'curso'
        let temp = [];
        curso.forEach(elem => temp.push(elem.id));
        this.form.get('curso').setValue(temp);

        // Add 'unidade'
        unidade.forEach(elem => this.unidades.push({ id: elem.id, nome: elem.nome }));
        this.dataSource.data = this.unidades;

        // Set id
        if (this.replicar) this.form.get('id').setValue(0);

        this.isLoadingResults = false;
      })
    }
  }

  getCursos(): void {
    this.cursoService.getCursos().subscribe(val => {
      if (val['status'] === 'error') this.error = true;
      else this.cursos = val;
    })
  }

  getunidades(): void {
    this.unidadeService.getAll()
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else this.unidadesDefault = val;

        this.form.get('unidadeSelect').reset()
      });
  }

  addUnidade(): void {
    // Get 'unidade' select
    const { unidadeSelect } = this.form.value;
    if (!unidadeSelect) return;
    // Check if 'unidade' exists
    const unidadeId = this.unidadesDefault?.length > 0 ? this.unidadesDefault.find(elem => elem.nome == unidadeSelect) : null;
    if (!unidadeId || unidadeId.length == 0) return;
    // Check if isn't alredy selected
    const alredySelected = this.unidades.find(elem => elem.id == unidadeId.id);
    if (alredySelected) {
      this.animationsService.showErrorSnackBar('Unidade já selecionada');
      return;
    }
    // Add 'unidade'
    this.unidades.push({ id: unidadeId.id, nome: unidadeId.nome });
    this.dataSource.data = this.unidades;

    this.form.get('unidadeSelect').reset();
  }

  _filterUnidades(value: string): any[] {
    const filterValue = value.toLowerCase();

    return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  voltar(): void {
    this.router.navigateByUrl(`gerenciador/curso-turmas`, { state: { tabPage: 1 } })
  }

  salvarData(): void {
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    // Setting 'curso'
    let cursoForm = this.form.get('curso').value;
    let cursoFormTemp = [];
    cursoForm.forEach(elem => cursoFormTemp.push({ id: elem }));

    const presencial = this.form.get('presencial').value;
    if (presencial) {
      const { segunda, terca, quarta, quinta, sexta, sabado, domingo } = this.form.value;
      if (!segunda && !terca && !quarta && !quinta && !sexta && !sabado && !domingo) {
        this.animationsService.showErrorSnackBar('Selecione pelo menos um dia da semana');
        return;
      }
    }

    const formValue = this.form.getRawValue();
    // Setting 'curso'
    formValue.curso = cursoFormTemp;
    const semanas = ['segunda', 'terca', 'quarta', 'quinta', 'sexta', 'sabado', 'domingo' ];
    semanas.forEach(dia => formValue[dia] ? formValue[dia] = true : formValue[dia] = false);
    // Removing useless data
    delete formValue.unidadeSelect;
    // Adding 'unidades'
    let unidade = [];
    this.unidades.forEach(elem => unidade.push({ id: elem.id }));
    // Unifying data
    const data = {...formValue, unidade };
    // Make request
    this.turmaService.cadastrar(data).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        const { id } = val;
        this.id = id;
        this.form.get('id').setValue(id);
      }
    })
  }

  removeUnidade(index: number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      let data = this.dataSource.data;
      data.splice(index, 1);
      this.dataSource.data = data;
    });
  }

}
