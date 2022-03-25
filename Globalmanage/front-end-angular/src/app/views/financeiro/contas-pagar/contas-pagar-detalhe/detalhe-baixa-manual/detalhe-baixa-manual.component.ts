import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { startWith, map } from 'rxjs/operators';
import { Observable, combineLatest } from 'rxjs';
import { ContasPagarService } from 'src/app/services/financeiro/contas-pagar.service';
import { InstituicaoBancariaService } from 'src/app/services/instituicao-bancaria.service';

@Component({
  selector: 'app-detalhe-baixa-manual',
  templateUrl: './detalhe-baixa-manual.component.html',
  styleUrls: ['./detalhe-baixa-manual.component.scss']
})
export class DetalheBaixaManualComponent implements OnInit {
  @Input() id: number;
  @Input() quitado: boolean;
  @Output() onLiquidado: EventEmitter<any> = new EventEmitter();
  form: FormGroup;
  error: boolean = false;
  isLoadingResults: boolean = false;
  filterUnidades: Observable<any[]>;
  unidadesDefault: any[] = null;
  bancos: any[] = null;

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private contasPagarService: ContasPagarService,
    private unidadeService: UnidadeService,
    private instituicaoBancariaService: InstituicaoBancariaService,
    private formService: FormService
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.buildForm();
    this.getunidades();
    this.getBancos();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      idDespesa: [this.id],
      dataPagamento: [null, [Validators.required]],
      tipoPagamento: [null, [Validators.required]],
      valorPago: [null, [Validators.required]],
      descontoTaxa: [null],
      juros: [null],
      multa: [null],
      observacao: [null, [Validators.required]],
      unidadeId: [null, [Validators.required]],
      unidadeSelect: [null, [Validators.required]],
      valorTotalPagar: [{disabled: true, value: null}, [Validators.required]],
      codigoBanco: [null],
      tipoContaBancaria: [null],
      numeroAgencia: [null],
      numeroConta: [null]
    })

    this.filterUnidades = this.form.get('unidadeSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
      );

    this.form.get('unidadeSelect').valueChanges.subscribe(val => {
      const unidadeId = this.unidadesDefault?.length >= 0 ? this.unidadesDefault.find(elem => elem.nome == val) : null;
      if (unidadeId && unidadeId.id) this.form.get('unidadeId').setValue(unidadeId.id)
      else this.form.get('unidadeId').setValue(null)
    })

    combineLatest(
      this.form.get('valorPago').valueChanges,
      this.form.get('juros').valueChanges,
      this.form.get('multa').valueChanges,
      this.form.get('descontoTaxa').valueChanges
    ).subscribe(val => {
      let soma: number = 0;
      soma += val[0];
      soma += val[1];
      soma += val[2];
      soma -= val[3];
      this.form.get('valorTotalPagar').setValue(soma);
    });

    this.form.reset();
    this.form.get('idDespesa').setValue(this.id)

    if (this.quitado) {
      this.formService.disableAllFields(this.form);
    }
  }

  getunidades(): void {
    this.unidadeService.getAll()
      .subscribe(val => {
        if (val['status'] === 'error') this.error = true;
        else this.unidadesDefault = val;
        this.form.get('unidadeSelect').setValue('');
      });
  }

  getBancos(): void {
    this.instituicaoBancariaService.getAll()
      .subscribe(val => {
        if (val['status'] === 'error') this.error = true;
        else this.bancos = val;
      });
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

  liquidar(): void {
    // Validating form
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }
    const formValue: any = this.form.getRawValue();
    delete formValue.unidadeSelect;

    const data = { ...formValue };
    
    // Make request
    this.contasPagarService.liquidar(data).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar('Liquidado com sucesso');
      }
      this.onLiquidado.emit();
    })
  }
}
