import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { MatTableDataSource } from '@angular/material/table';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DeleteService } from 'src/app/services/delete.service';
import { ActivatedRoute } from '@angular/router';
import { CursoService } from 'src/app/services/gerenciador/curso.service';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { AnimationsService } from 'src/app/services/animations.service';
import { CampanhaService } from 'src/app/services/gerenciador/campanha.service';
import { CompareInitialEndDate } from 'src/app/utils/form-validation/initial-end-dates.validation';
import { FormService } from 'src/app/services/form.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-promocoes-bolsas-convenio-individual',
  templateUrl: './promocoes-bolsas-convenio-individual.component.html',
  styleUrls: ['./promocoes-bolsas-convenio-individual.component.scss']
})
export class PromocoesBolsasConvenioIndividualComponent implements OnInit {
  form: FormGroup;
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;  
  parcelas = Array(12).fill('').map((x,i) => i+1);
  cursos: any = [];
  displayedColumns: string[] = ['descricao', 'options'];
  dataUnidadeSource = new MatTableDataSource();
  filterUnidades: Observable<any[]>;
  unidadesDefault = [];
  unidades = [];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private deleteService: DeleteService,
    private unidadeService: UnidadeService,
    private cursoService: CursoService,
    private animationsService: AnimationsService,
    private campanhaService: CampanhaService,
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
      nomeCampanha: [null, [Validators.required]],
      parcela: [null, [Validators.required]],
      codigoPromocao: [null],
      exigeComprovante: [null, [Validators.required]],
      descontoPlanoPagamento: [null],
      descontoTaxaMatricula: [null],
      descontoTaxaMateriaDidatico: [null],
      descontoTaxaInscricaoProvas: [null],
      cursoId: [null, [Validators.required]],
      isActive: [false],
      inicioCampanha: [null, [Validators.required]],
      terminoCampanha: [null, [Validators.required]],
      credito: [false],
      debito: [false],
      boleto: [false],
      unidadeSelected: [null]
    });

    this.form.setValidators([
      CompareInitialEndDate('inicioCampanha', 'terminoCampanha')
    ])

    this.filterUnidades = this.form.get('unidadeSelected').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
      );

    this.form.get('id').setValue(this.id);
  }

  loadData(): void {
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.campanhaService.getPorId(this.id).subscribe(val => {
        if (!val) return;
        if (val['status'] === 'error') this.error = true;
        const { campanhaUnidade, campanhaTipoPagamento } = val;
        this.form.patchValue(val);

        // Add 'unidade'
        campanhaUnidade.forEach(elem => this.unidades.push({ id: elem.unidadeId, nome: elem.nomeUnidade }));
        this.dataUnidadeSource.data = this.unidades;

        // Set 'tipo de pagamento'
        campanhaTipoPagamento.forEach(elem => {
          if (elem.tipoPagamento == 1) this.form.get('credito').setValue(true);
          if (elem.tipoPagamento == 2) this.form.get('debito').setValue(true);
          if (elem.tipoPagamento == 3) this.form.get('boleto').setValue(true);
        })

        this.isLoadingResults = false;
      })
    }
  }

  getCursos(): void {
    this.cursoService.getCursos().subscribe(val => {
      if (!val) return;
      if (val['status'] === 'error') this.error = true;
      this.cursos = val;
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
    const alredySelected = this.unidades.find(elem => elem.id == unidadeId.id);
    if (alredySelected) {
      this.animationsService.showErrorSnackBar('Unidade já selecionada');
      return;
    }
    // Add 'unidade'
    this.unidades.push({ id: unidadeId.id, nome: unidadeId.nome });
    this.dataUnidadeSource.data = this.unidades;

    this.form.get('unidadeSelected').reset();
  }

  _filterUnidades(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  voltar(): void {
    this.location.back();
  }

  salvarData(): void {
    // Validating form
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }
    // Get form data
    const formValue = this.form.value;
    // Casting
    formValue['descontoPlanoPagamento']      =  formValue['descontoPlanoPagamento']      ? formValue['descontoPlanoPagamento']      : 0;
    formValue['descontoTaxaMatricula']       =  formValue['descontoTaxaMatricula']       ? formValue['descontoTaxaMatricula']       : 0;
    formValue['descontoTaxaMateriaDidatico'] =  formValue['descontoTaxaMateriaDidatico'] ? formValue['descontoTaxaMateriaDidatico'] : 0;
    formValue['descontoTaxaInscricaoProvas'] =  formValue['descontoTaxaInscricaoProvas'] ? formValue['descontoTaxaInscricaoProvas'] : 0;
    // Adding 'unidades'
    let campanhaUnidade = [];
    this.unidades.forEach(elem => campanhaUnidade.push({ unidadeId: elem.id, campanhaId: this.id,  }));
    // Adding 'campanhaTipoPagamento'
    let campanhaTipoPagamento = [];
    if (formValue.credito) campanhaTipoPagamento.push({ tipoPagamento: 1, campanhaId: this.id });
    if (formValue.debito)  campanhaTipoPagamento.push({ tipoPagamento: 2, campanhaId: this.id });
    if (formValue.boleto)  campanhaTipoPagamento.push({ tipoPagamento: 3, campanhaId: this.id });
    if (campanhaTipoPagamento.length == 0) {
      this.animationsService.showErrorSnackBar('Escolha pelo menos um tipo de pagamento');
      return;
    }
    // Removing useless data
    delete formValue.unidadeSelected;
    delete formValue.cursoSelected;
    delete formValue.credito;
    delete formValue.debito;
    delete formValue.boleto;
    // Unifying data
    const data = {...formValue, campanhaUnidade, campanhaTipoPagamento };
    // Make request
    this.campanhaService.cadastrar(data).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        this.id = val.id;
        this.form.get('id').setValue(val.id);
      }
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

}
