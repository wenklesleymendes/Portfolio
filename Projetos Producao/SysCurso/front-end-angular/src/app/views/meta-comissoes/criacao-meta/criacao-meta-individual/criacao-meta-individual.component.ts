import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { ActivatedRoute, Navigation, Router } from '@angular/router';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { CPFMask } from 'src/app/utils/mask/mask';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { CompareInitialEndDate } from 'src/app/utils/form-validation/initial-end-dates.validation';
import { MetasService } from 'src/app/services/metas-comissoes/metas.service';
import { Meses } from 'src/app/utils/variables/meses';
import { combineLatest } from 'rxjs';
import * as moment from 'moment';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-criacao-meta-individual',
  templateUrl: './criacao-meta-individual.component.html',
  styleUrls: ['./criacao-meta-individual.component.scss']
})
export class CriacaoMetaIndividualComponent implements OnInit {
  form: FormGroup;
  detalhamentoMeta: FormArray;
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;
  onlyNumbers = /^-?(0|[1-9]\d*)?$/;
  cpfMask = CPFMask;
  unidades: any[] = null;
  replicar: any;
  totalRestante: number = 0;
  detalhamento: any[] = [];

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private netasService: MetasService,
    private router: Router,
    private routerActive: ActivatedRoute,
    private unidadeService: UnidadeService,
    private formService: FormService
  ) {
    // Get id
    const id = this.routerActive.snapshot.paramMap.get('id');
    this.id = id ? parseInt(id) : 0;
    // Get State
    const currentNavigation: Navigation = this.router.getCurrentNavigation();
    if (currentNavigation && currentNavigation.extras && currentNavigation.extras.state) {
      const state = currentNavigation.extras.state;
      this.replicar = state.replicar;
    }
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.buildForm();
    this.getunidades();
    this.loadData();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      descricao: [null, [Validators.required]],
      unidadeId: [null, [Validators.required]],
      inicioMeta: [null, [Validators.required]],
      terminoMeta: [null, [Validators.required]],
      quantidade: [null, [Validators.required, Validators.pattern(this.onlyNumbers)]],
      bonusPeriodo: [null, [Validators.required]],
      detalhamentoMeta: this.fb.array([])
    })
    this.detalhamentoMeta = this.form.get('detalhamentoMeta') as FormArray;

    this.form.setValidators([CompareInitialEndDate('inicioMeta', 'terminoMeta')]);

    combineLatest(this.form.get('inicioMeta').valueChanges, this.form.get('terminoMeta').valueChanges)
      .subscribe(val => this.addDetalhamento([...val, this.detalhamento]));

    combineLatest(this.form.get('quantidade').valueChanges, this.form.get('detalhamentoMeta').valueChanges)
      .pipe(debounceTime(500))
      .subscribe(val => this.calcularTotalRestante(val));

    if (!this.replicar) this.form.get('id').setValue(this.id);
  }

  getunidades(): void {
    this.unidadeService.getAll()
      .subscribe(val => {
        if (val['status'] === 'error') this.error = true;
        else {
          this.unidades = val;
        }
      });
  }

  calcularTotalRestante(val: any[]): void {
    const quantidade: number = val[0];
    const detalhamentoMeta: any[] = val[1];
    this.detalhamento = val[1];

    if (!detalhamentoMeta || detalhamentoMeta.length == 0) {
      this.totalRestante = quantidade;
      return;
    } 
    let totalDetalhe: number = 0;
    detalhamentoMeta.forEach(elem => totalDetalhe += elem.quantidade);
    this.totalRestante = quantidade - totalDetalhe;
  }

  loadData(): void {
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.netasService.getPorId(this.id)
      .subscribe(val => {
        if (!val) return;
        if (val['status'] === 'error') return this.error = true;
        const { inicioMeta, terminoMeta, detalhamentoMeta } = val;
        delete val.detalhamentoMeta;
        this.form.patchValue(val);
        this.isLoadingResults = false;
        
        // Set id
        if (this.replicar) this.form.get('id').setValue(0);
        // Add Detalhamento
        this.detalhamento = detalhamentoMeta;
        this.addDetalhamento([inicioMeta, terminoMeta, detalhamentoMeta]);
      })
    }
  }

  addDetalhamento(val: any[]): void {
    const inicioMeta: Date | string = val[0];
    const terminoMeta: Date | string = val[1];
    this.detalhamento = val[2];
    if (!inicioMeta || !terminoMeta) return;

    const startDate: moment.Moment = moment(inicioMeta).set('date', 1).set('hour', 0);
    const endDate: moment.Moment = moment(terminoMeta).set('date', 1).set('hour', 0);
    const dates: string[] = [];

    const month = moment(startDate);
    while( month <= endDate ) {
      dates.push(month.format('YYYY-MM-DD'));
      month.add(1, 'month');
    }

    this.detalhamentoMeta.clear();
    if (dates.length == 0) return;
    dates.forEach(date => {
      this.detalhamentoMeta.push(
        this.fb.group({
          id: [0],
          dataPeriodo: [date],
          quantidade: [val[2] ? this.ajustarQtDDetalhamento(val[2], date) : null, [Validators.required]]
        })
      )
    })
  }

  ajustarDetalhamento(form: any, index: number): string {
    const dataPeriodo = form[index]?.dataPeriodo;
    if (!dataPeriodo) return ' - ';

    const date = new Date(dataPeriodo);
    const month = Meses.find(elem => elem.value == date.getUTCMonth()).label;
    const year = date.getUTCFullYear();

    return `${month} / ${year}`;
  }

  ajustarQtDDetalhamento(val: any[],  date: any): number {
    if (!val || val.length == 0) return null;
    const teste = val.find(elem => elem.dataPeriodo.match(date));
    return teste?.quantidade ? teste.quantidade : null
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
    if (this.totalRestante != 0) {
      this.animationsService.showErrorSnackBar('Distribua corretamente as metas');
      return;
    }
    const formValue: any = this.form.value;
    delete formValue.confirmPassword;
    // Make request
    this.netasService.cadastrar(formValue).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        const { id } = val;
        this.id = id;
        this.form.get('id').setValue(id);
      }
    })
  }
}
