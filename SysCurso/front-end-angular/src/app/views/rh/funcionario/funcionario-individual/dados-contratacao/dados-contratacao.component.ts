import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, FormArray } from '@angular/forms';
import { HourMinuteMask, TelMask, CelMask, WeekMask } from 'src/app/utils/mask/mask';
import { Estados } from 'src/app/utils/variables/locations';
import { DataProvider } from '../data-provider';
import { debounceTime, startWith, map, distinctUntilChanged } from 'rxjs/operators';
import { InstituicaoBancariaService } from 'src/app/services/instituicao-bancaria.service';
import { CompareInitialEndHour } from 'src/app/utils/form-validation/initial-end-hour.validation';
import { MatTableDataSource } from '@angular/material/table';
import { Observable, combineLatest } from 'rxjs';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { DeleteService } from 'src/app/services/delete.service';
import { FormService } from 'src/app/services/form.service';
import { CursoService } from 'src/app/services/gerenciador/curso.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-dados-contratacao',
  templateUrl: './dados-contratacao.component.html',
  styleUrls: ['./dados-contratacao.component.scss']
})
export class DadosContratacaoComponent implements OnInit {
  @Input() dadosContratacaoAllInput: any;
  funcionarioId: number = 0;
  error: boolean = false;
  form: FormGroup;
  salarioUnidadeForm: FormGroup;
  agenteIntegracaoForm: FormGroup;
  jornadaTrabalhoForm: FormGroup;
  materiasProfessorForm: FormGroup;
  cursoProfessor: FormArray;
  bancos: any = null;
  estados: string[] = Estados;
  hourMinute = HourMinuteMask;
  WeekMask = WeekMask;
  maskTelefoneFixoPrincipal = TelMask;
  maskCelular = TelMask;
  unidadeDisplayedColumns: string[] = ['descricao', 'descricaoCargo', 'valor', 'options'];
  unidadeDataSource = new MatTableDataSource([]);
  UnidadeBtnAddHidden: boolean = true;
  unidadesDefault: any[] = [];
  unidades: any[] = [];
  cursosDefault: any[] = [];
  cursos: any[] = [];
  filterUnidades: Observable<any[]>;
  filterCursos: Observable<any[]>;
  onlyNumbers = /^-?(0|[1-9]\d*)?$/;
  selection = new SelectionModel<any>(true, []);

  constructor(
    private fb: FormBuilder,
    private dataProvider: DataProvider,
    private instituicaoBancariaService: InstituicaoBancariaService,
    private unidadeService: UnidadeService,
    private animationsService: AnimationsService,
    private deleteService: DeleteService,
    private cursoService: CursoService,
    private formService: FormService,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit() {
    this.buildForm();
    this.getBancos();
    this.getCursos();
    this.getunidades();
    this.loadFormData(this.dadosContratacaoAllInput);

    this.dataProvider.getValidate().subscribe(res => {
      if (res) {
        this.formService.validateAllFields(this.form);
        this.formService.validateAllFields(this.salarioUnidadeForm);
        this.formService.validateAllFields(this.agenteIntegracaoForm);
        this.formService.validateAllFields(this.jornadaTrabalhoForm);
        this.formService.validateAllFields(this.materiasProfessorForm);
      }
    });
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    // ************************************************************************
    // Dados contratação
    this.form = this.fb.group({
      id: [0],
      tipoRegimeContratacao: [null, [Validators.required]],
      matricula: [null],
      dataAtestadoAdmissao: [null, [Validators.required]],
      dataAtestadoDemissao: [null],
      dataAlteracaoRegime: [null],
      dataRecisao: [null],
      tempoAlmoco: [null],
      valeTransporte: [null],
      valeAlimentacao: [null],
      numeroCT: [null],
      dataEmissaoCT: [null],
      serieCT: [null],
      cargaHorarioSemanalCT: [null],
      numeroPIS: [null, [Validators.required, Validators.minLength(11), Validators.maxLength(11), Validators.pattern(this.onlyNumbers)]],
      numeroTituloEleitor: [null],
      zonaTituloEleitor: [null],
      secaoTituloEleitor: [null],
      dadosBancario: this.fb.group({
        id:[0],
        codigoBanco: [null, [Validators.required]],
        tipoContaBancaria: [null, [Validators.required]],
        numeroAgencia: [null, [Validators.required]],
        numeroConta: [null, [Validators.required]],
      })
    })

    // ************************************************************************
    // Salario / Unidade
    this.salarioUnidadeForm = this.fb.group({
      id: [0],
      descricaoCargo: [null],
      valorSalario: [null],
      funcionarioId:[0],
      unidadeId:[0],
      unidadeSelect: [null]
    })

    this.filterUnidades = this.salarioUnidadeForm.get('unidadeSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
      );

    combineLatest([this.salarioUnidadeForm.get('unidadeSelect').valueChanges, this.salarioUnidadeForm.get('valorSalario').valueChanges])
      .pipe(debounceTime(250))
      .subscribe(val => this.UnidadeBtnAddHidden = (val[0] === '' || !val[0] || val[1] === '' || !val[1]) ? true : false)

    // ************************************************************************
    // Agente de contratação
    this.agenteIntegracaoForm = this.fb.group({
      id: [0],
      nome: [null],
      telefone: [null],
      email: [null],
      site: [null],
      pessoaContato: [null]
    })

    // Change mask of all contact numbers
    this.agenteIntegracaoForm.get('telefone').valueChanges
      .subscribe(val => this.maskTelefoneFixoPrincipal = (val && val.length > 10) ? CelMask : TelMask);

    // ************************************************************************
    // Jornada de trabalho
    this.jornadaTrabalhoForm = this.fb.group({
      id: [0],
      isActive: [false],
      segundaFeiraInicio: [null],
      segundaFeiraTermino: [null],
      tercaFeiraInicio: [null],
      tercaFeiraTermino: [null],
      quartaFeiraInicio: [null],
      quartaFeiraTermino: [null],
      quintaFeiraInicio: [null],
      quintaFeiraTermino: [null],
      sextaFeiraInicio: [null],
      sextaFeiraTermino: [null],
      sabadoInicio: [null],
      sabadoTermino: [null],
      domingoInicio: [null],
      domingoTermino: [null]
    })

    this.jornadaTrabalhoForm.setValidators([
      CompareInitialEndHour('segundaFeiraInicio'  ,'segundaFeiraTermino'),
      CompareInitialEndHour('tercaFeiraInicio'    ,'tercaFeiraTermino'  ),
      CompareInitialEndHour('quartaFeiraInicio'   ,'quartaFeiraTermino' ),
      CompareInitialEndHour('quintaFeiraInicio'   ,'quintaFeiraTermino' ),
      CompareInitialEndHour('sextaFeiraInicio'    ,'sextaFeiraTermino'  ),
      CompareInitialEndHour('sabadoInicio'        ,'sabadoTermino'      ),
      CompareInitialEndHour('domingoInicio'       ,'domingoTermino'     ),
    ]);

    // ************************************************************************
    // Materiais professor
    this.materiasProfessorForm = this.fb.group({
      cursoSelect: [null],
      cursoProfessor: this.fb.array([])
    })
    this.cursoProfessor = this.materiasProfessorForm.get('cursoProfessor') as FormArray;

    this.filterCursos = this.materiasProfessorForm.get('cursoSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterCursos(elem) : this.cursosDefault.slice())
      );

    // ************************************************************************
    // 'tipoRegimeContratacao' validation
    this.form.get('tipoRegimeContratacao').valueChanges.pipe(distinctUntilChanged()).subscribe(val => {
      val = parseInt(val);

      switch(val) {
        // CLT Seg a Sex
        case 1:
          this.formService.disableAllFields(this.materiasProfessorForm);
          this.formService.disableAllFields(this.agenteIntegracaoForm);
          this.formService.enableAllFields(this.jornadaTrabalhoForm);
          this.formService.notMandatoryAllFields(this.jornadaTrabalhoForm);
          this.formService.disableField(this.form.get('dataRecisao') as FormControl);
          this.formService.enableField(this.form.get('numeroCT') as FormControl);
          this.formService.enableField(this.form.get('dataEmissaoCT') as FormControl);
          this.formService.enableField(this.form.get('serieCT') as FormControl);
          this.formService.enableField(this.form.get('cargaHorarioSemanalCT') as FormControl);
          break;
        // Estagiário Seg a Sex
        case 2:
        case 10:
          this.formService.disableAllFields(this.materiasProfessorForm);
          this.formService.enableAllFields(this.agenteIntegracaoForm);
          this.formService.enableAllFields(this.jornadaTrabalhoForm);
          this.formService.notMandatoryAllFields(this.jornadaTrabalhoForm);
          this.formService.enableField(this.form.get('dataRecisao') as FormControl);
          this.formService.notMandatoryFields(this.form.get('dataRecisao') as FormControl);
          this.formService.disableField(this.form.get('numeroCT') as FormControl);
          this.formService.disableField(this.form.get('dataEmissaoCT') as FormControl);
          this.formService.disableField(this.form.get('serieCT') as FormControl);
          this.formService.disableField(this.form.get('cargaHorarioSemanalCT') as FormControl);
          break;
        // Professor Autonomo
        case 3:
          this.formService.enableAllFields(this.materiasProfessorForm);
          this.formService.notMandatoryAllFields(this.materiasProfessorForm);
          this.formService.disableAllFields(this.agenteIntegracaoForm);
          this.formService.disableAllFields(this.jornadaTrabalhoForm);
          this.formService.enableField(this.form.get('dataRecisao') as FormControl);
          this.formService.notMandatoryFields(this.form.get('dataRecisao') as FormControl);
          this.formService.disableField(this.form.get('numeroCT') as FormControl);
          this.formService.disableField(this.form.get('dataEmissaoCT') as FormControl);
          this.formService.disableField(this.form.get('serieCT') as FormControl);
          this.formService.disableField(this.form.get('cargaHorarioSemanalCT') as FormControl);
          break;
        // Professor CLT
        case 4:
          this.formService.enableAllFields(this.materiasProfessorForm);
          this.formService.notMandatoryAllFields(this.materiasProfessorForm);
          this.formService.disableAllFields(this.agenteIntegracaoForm);
          this.formService.disableAllFields(this.jornadaTrabalhoForm);
          this.formService.disableField(this.form.get('dataRecisao') as FormControl);
          this.formService.enableField(this.form.get('numeroCT') as FormControl);
          this.formService.enableField(this.form.get('dataEmissaoCT') as FormControl);
          this.formService.enableField(this.form.get('serieCT') as FormControl);
          this.formService.enableField(this.form.get('cargaHorarioSemanalCT') as FormControl);
          break;
        // Profissional Autonomo
        case 5:
          this.formService.disableAllFields(this.materiasProfessorForm);
          this.formService.disableAllFields(this.agenteIntegracaoForm);
          this.formService.disableAllFields(this.jornadaTrabalhoForm);
          this.formService.enableField(this.form.get('dataRecisao') as FormControl);
          this.formService.notMandatoryFields(this.form.get('dataRecisao') as FormControl);
          this.formService.disableField(this.form.get('numeroCT') as FormControl);
          this.formService.disableField(this.form.get('dataEmissaoCT') as FormControl);
          this.formService.disableField(this.form.get('serieCT') as FormControl);
          this.formService.disableField(this.form.get('cargaHorarioSemanalCT') as FormControl);
          break;
        // CLT Seg a Sab
        case 6:
          this.formService.disableAllFields(this.materiasProfessorForm);
          this.formService.disableAllFields(this.agenteIntegracaoForm);
          this.formService.enableAllFields(this.jornadaTrabalhoForm);
          this.formService.notMandatoryAllFields(this.jornadaTrabalhoForm);
          this.formService.disableField(this.form.get('dataRecisao') as FormControl);
          this.formService.enableField(this.form.get('numeroCT') as FormControl);
          this.formService.enableField(this.form.get('dataEmissaoCT') as FormControl);
          this.formService.enableField(this.form.get('serieCT') as FormControl);
          this.formService.enableField(this.form.get('cargaHorarioSemanalCT') as FormControl);
          break;
        // Estagiário Seg a Sab
        case 7:
        case 11:
          this.formService.disableAllFields(this.materiasProfessorForm);
          this.formService.enableAllFields(this.agenteIntegracaoForm);
          this.formService.enableAllFields(this.jornadaTrabalhoForm);
          this.formService.notMandatoryAllFields(this.jornadaTrabalhoForm);
          this.formService.enableField(this.form.get('dataRecisao') as FormControl);
          this.formService.notMandatoryFields(this.form.get('dataRecisao') as FormControl);
          this.formService.disableField(this.form.get('numeroCT') as FormControl);
          this.formService.disableField(this.form.get('dataEmissaoCT') as FormControl);
          this.formService.disableField(this.form.get('serieCT') as FormControl);
          this.formService.disableField(this.form.get('cargaHorarioSemanalCT') as FormControl);
          break;
        // Autônomo Pré CLT Seg a Sex
        case 8:
          this.formService.disableAllFields(this.materiasProfessorForm);
          this.formService.disableAllFields(this.agenteIntegracaoForm);
          this.formService.enableAllFields(this.jornadaTrabalhoForm);
          this.formService.notMandatoryAllFields(this.jornadaTrabalhoForm);
          this.formService.enableField(this.form.get('dataRecisao') as FormControl);
          this.formService.notMandatoryFields(this.form.get('dataRecisao') as FormControl);
          this.formService.enableField(this.form.get('numeroCT') as FormControl);
          this.formService.enableField(this.form.get('dataEmissaoCT') as FormControl);
          this.formService.enableField(this.form.get('serieCT') as FormControl);
          this.formService.enableField(this.form.get('cargaHorarioSemanalCT') as FormControl);
          break;
        // Autônomo Pré CLT Seg a Sab
        case 9:
          this.formService.disableAllFields(this.materiasProfessorForm);
          this.formService.disableAllFields(this.agenteIntegracaoForm);
          this.formService.enableAllFields(this.jornadaTrabalhoForm);
          this.formService.notMandatoryAllFields(this.jornadaTrabalhoForm);
          this.formService.enableField(this.form.get('dataRecisao') as FormControl);
          this.formService.notMandatoryFields(this.form.get('dataRecisao') as FormControl);
          this.formService.enableField(this.form.get('numeroCT') as FormControl);
          this.formService.enableField(this.form.get('dataEmissaoCT') as FormControl);
          this.formService.enableField(this.form.get('serieCT') as FormControl);
          this.formService.enableField(this.form.get('cargaHorarioSemanalCT') as FormControl);
          break;
      }
    });

    // ************************************************************************
    // All form
    combineLatest([
        this.form.valueChanges.pipe(startWith(null)),
        this.agenteIntegracaoForm.valueChanges.pipe(startWith(null)),
        this.jornadaTrabalhoForm.valueChanges.pipe(startWith(null)),
        this.materiasProfessorForm.valueChanges.pipe(startWith(null))
    ])
      .pipe(debounceTime(500))
      .subscribe(val => {
        // Set data
        let data = null;
        if (
          this.form.valid
          && (this.agenteIntegracaoForm.valid || this.agenteIntegracaoForm.disabled)
          && (this.jornadaTrabalhoForm.valid || this.jornadaTrabalhoForm.disabled)
          && (this.materiasProfessorForm.valid || this.materiasProfessorForm.disabled)
          ){
          const { dadosBancario, ...dadosContratacao  } = val[0];
          const agenteIntegracao = this.agenteIntegracaoForm.disabled   ? null : val[1];
          const jornadaTrabalho  = this.jornadaTrabalhoForm.disabled    ? null : val[2];
          const materiaProfessor = this.materiasProfessorForm.disabled  ? null : val[3].cursoProfessor;
          // Casting form values
          dadosBancario['tipoContaBancaria'] = parseInt(dadosBancario['tipoContaBancaria']);

          // Set 'cursoProfessor'
          let cursoProfessor = null;
          let auxCurso = [];
          if (materiaProfessor) {
            cursoProfessor = [];
            materiaProfessor.forEach(elem => {
              auxCurso = [];
              if (elem?.materia?.length > 0) elem.materia.forEach(curso => auxCurso.push({ idMateria: curso }));
              cursoProfessor.push({ idCurso: elem.id, materiaCursoProfessor: auxCurso });
            });
          }

          data = {
            dadosContratacao,
            dadosBancario,
            agenteIntegracao,
            cursoProfessor,
            jornadaTrabalho
          };
        }
        this.dataProvider.dadosContratacaoNext(data);
      });


    // Trigger validations
    this.form.get('tipoRegimeContratacao').setValue('1');
  }

  getBancos(): void {
    this.instituicaoBancariaService.getAll().subscribe(val => this.bancos = val);
  }

  getCursos(): void {
    this.cursoService.getCursoComMateria()
      .subscribe(val => {
        if (val['status'] === 'error') this.error = true;
        else this.cursosDefault = val;

        this.materiasProfessorForm.get('cursoSelect').setValue(null);
      });
  }

  getunidades(): void {
    this.unidadeService.getAll()
      .subscribe(val => {
        if (val['status'] === 'error') this.error = true;
        else this.unidadesDefault = val;

        this.salarioUnidadeForm.get('unidadeSelect').reset()
      });
  }

  _filterUnidades(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  _filterCursos(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.cursosDefault.filter(elem => elem.descricao.toLowerCase().indexOf(filterValue) === 0);
  }

  labelAdmissao(): string {
    const tipoRegimeContratacao = this.form.get('tipoRegimeContratacao').value;
    if (tipoRegimeContratacao == 1 || tipoRegimeContratacao == 6) return 'Data de Admissão';
    if (tipoRegimeContratacao == 2 || tipoRegimeContratacao == 7) return 'Data de Início do Termo de Estágio';
    if (tipoRegimeContratacao == 3 || tipoRegimeContratacao == 8 || tipoRegimeContratacao == 9 || tipoRegimeContratacao == 10 || tipoRegimeContratacao == 11) return 'Data de Início de Contrato';
    if (tipoRegimeContratacao == 4) return 'Data de Admissão';
    if (tipoRegimeContratacao == 5) return 'Data de Início de Contrato';
    return 'Data de Admissão';
  }

  labelDemissao(): string {
    const tipoRegimeContratacao = this.form.get('tipoRegimeContratacao').value;
    if (tipoRegimeContratacao == 1 || tipoRegimeContratacao == 6) return 'Data de Rescisão';
    if (tipoRegimeContratacao == 2 || tipoRegimeContratacao == 7) return 'Data de Término do Termo de Estágio';
    if (tipoRegimeContratacao == 3 || tipoRegimeContratacao == 8 || tipoRegimeContratacao == 9 || tipoRegimeContratacao == 10 || tipoRegimeContratacao == 11) return 'Data de Término de Contrato';
    if (tipoRegimeContratacao == 4) return 'Data de Rescisão';
    if (tipoRegimeContratacao == 5) return 'Data de Término de Contrato';
    return 'Data de Rescisão';
  }

  labelSalario(): string {
    const tipoRegimeContratacao = this.form.get('tipoRegimeContratacao').value;
    if (tipoRegimeContratacao == 1 || tipoRegimeContratacao == 6 || tipoRegimeContratacao == 4 || tipoRegimeContratacao == 8 || tipoRegimeContratacao == 9) return 'Salário bruto';
    if (tipoRegimeContratacao == 2 || tipoRegimeContratacao == 7) return 'Bolsa auxílio';
    if (tipoRegimeContratacao == 3) return 'Valor aula';
    if (tipoRegimeContratacao == 5) return 'Valor remuneração por dia';
    if (tipoRegimeContratacao == 10 || tipoRegimeContratacao == 11) return 'Remuneração';
    return 'Valor';
  }

  labelRecisao(): string {
    const tipoRegimeContratacao = this.form.get('tipoRegimeContratacao').value;
    if (tipoRegimeContratacao == 3 || tipoRegimeContratacao == 5) return 'Data de Rescisão de Contrato';
    return 'Data de Rescisão'
  }

  validationPIS(): void {
    if (
      this.form.get('tipoRegimeContratacao').value == 1
      || this.form.get('tipoRegimeContratacao').value == 8
      || this.form.get('tipoRegimeContratacao').value == 9
      || this.form.get('tipoRegimeContratacao').value == 6
    ) {
      this.form.get('numeroPIS').setValidators([Validators.pattern(this.onlyNumbers), Validators.required, Validators.minLength(11)]);
      this.form.get('numeroPIS').updateValueAndValidity();
    } else{
      this.form.get('numeroPIS').setValidators([Validators.pattern(this.onlyNumbers), Validators.minLength(12)]);
      this.form.get('numeroPIS').updateValueAndValidity();
    }
  }

  loadFormData(data) {
    if (!data) return;
    const { dadosContratacao, dadosBancario, salarioUnidade, agenteIntegracao, jornadaTrabalho, cursoProfessor, funcionarioId } = data;

    if (dadosContratacao) this.form.patchValue(dadosContratacao);
    if (dadosBancario) this.form.get('dadosBancario').patchValue(dadosBancario);
    if (agenteIntegracao) this.agenteIntegracaoForm.patchValue(agenteIntegracao);
    if (jornadaTrabalho) this.jornadaTrabalhoForm.patchValue(jornadaTrabalho);
    if (cursoProfessor) this.loadCursoProfessor(cursoProfessor);
    this.unidades = salarioUnidade;
    this.unidadeDataSource.data = salarioUnidade;
    this.dataProvider.salarioUnidadeNext(salarioUnidade);
    this.funcionarioId = funcionarioId ? funcionarioId : 0;

    // Casting form values
    const regime = dadosContratacao?.tipoRegimeContratacao ? dadosContratacao.tipoRegimeContratacao.toString() : null;
    this.form.get('tipoRegimeContratacao').setValue(regime);
    const tipoContaBancaria = dadosBancario?.tipoContaBancaria ? dadosBancario.tipoContaBancaria.toString() : null;
    this.form.get('dadosBancario').get('tipoContaBancaria').setValue(tipoContaBancaria);
  }

  loadCursoOptions(id: number): any[] {
    const curso = this.cursosDefault.find(elem => elem.id == id);
    if (!curso) return null;
    if (curso && curso.materia) return curso.materia;
    return null;
  }

  loadCursoProfessor(cursoProfessor: any[]): void {
    let auxDiciplina: number[] = [];
    cursoProfessor.forEach(materia => {
      auxDiciplina = [];
      if (materia?.materiaCursoProfessor?.length > 0) {
        materia.materiaCursoProfessor.forEach(elem => auxDiciplina.push(elem.idMateria));
      }
      this.cursoProfessor.push(this.fb.group({
        id: [materia.idCurso],
        descricao: [materia.nomeCurso],
        materia: [auxDiciplina]
      }))
    })
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  add(): void {
    // Get 'unidade' select
    const { unidadeSelect, valorSalario, descricaoCargo } = this.salarioUnidadeForm.value;
    if (!unidadeSelect || !valorSalario) return;
    // Check if 'unidade' exists
    const unidadeId = this.unidadesDefault?.length >= 0 ? this.unidadesDefault.find(elem => elem.nome == unidadeSelect) : null;
    if (!unidadeId || unidadeId.length == 0) return;
    // Check if isn't alredy selected
    const alredySelected = this.unidades.find(elem => elem.unidadeId == unidadeId.id);
    if (alredySelected) {
      this.animationsService.showErrorSnackBar('Unidade já selecionada');
      return;
    }
    // Check 'tipoRegimeContratacao' and 'unidades' length
    const tipoRegimeContratacao = this.form.get('tipoRegimeContratacao').value;
    if (tipoRegimeContratacao != 3 && this.unidades.length >= 1) {
      this.animationsService.showErrorSnackBar('Apenas uma unidade permitida');
      return;
    }
    // Add 'unidade'
    this.unidades.push({ unidadeId: unidadeId.id, nomeUnidade: unidadeId.nome, valorSalario, descricaoCargo, funcionarioId: this.funcionarioId });
    this.unidadeDataSource.data = this.unidades;

    // Reset form
    this.salarioUnidadeForm.reset();
    // Next observable
    this.dataProvider.salarioUnidadeNext(this.unidades);
    this
  }

  addCurso(): void {
    const { cursoSelect } = this.materiasProfessorForm.value;
    if (!cursoSelect) return;
    // Check if 'curso' exists
    const curso = this.cursosDefault?.length >= 0 ?  this.cursosDefault.find(elem => elem.descricao == cursoSelect) : null;
    if (!curso) return;
    // Check if isn't alredy selected
    const cursoSelected = this.materiasProfessorForm.get('cursoProfessor').value as any[];
    const alredySelected = cursoSelected.find(elem => elem.descricao == cursoSelect);
    if (alredySelected) {
      this.animationsService.showErrorSnackBar('Curso já selecionado');
      return;
    }
    this.cursoProfessor.push(
      this.fb.group({
        id: [curso.id],
        descricao: [curso.descricao],
        materia: [null]
      })
    );

    this.materiasProfessorForm.get('cursoSelect').setValue(null);
  }

  remove(index: number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      let data = this.unidadeDataSource.data;
      data.splice(index, 1);
      this.unidadeDataSource.data = data;
      this.dataProvider.salarioUnidadeNext(data);
    })
  }

  removeDoFormArray(controls: any, index: number) {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      controls as FormArray;
      controls.removeAt(index);
    });
  }
}
