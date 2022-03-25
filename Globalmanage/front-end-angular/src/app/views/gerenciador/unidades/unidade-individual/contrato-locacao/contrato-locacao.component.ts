import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { CelMask, TelMask } from 'src/app/utils/mask/mask';
import { CompareInitialEndDate } from 'src/app/utils/form-validation/initial-end-dates.validation';
import { combineLatest } from 'rxjs';
import { DataProvider } from '../data-provider';
import { debounceTime } from 'rxjs/operators';
import { DeleteService } from 'src/app/services/delete.service';
import { FormService } from 'src/app/services/form.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-contrato-locacao',
  templateUrl: './contrato-locacao.component.html',
  styleUrls: ['./contrato-locacao.component.scss']
})
export class ContratoLocacaoComponent implements OnInit, OnChanges {
  @Input() contratoLocacaoInput: any;
  form: FormGroup;
  despesasForm: FormGroup;
  displayedColumns: string[] = ['descricao','valor', 'options'];
  dataSource = new MatTableDataSource([]);
  btnAddDespesaHidden: boolean = true;

  maskTelefoneProprietario = TelMask;
  masktelefoneFixo = TelMask;
  maskCelular = TelMask;
  selection = new SelectionModel<any>(true, []);

  constructor(
    private fb: FormBuilder,
    private dataProvider: DataProvider,
    private deleteService: DeleteService,
    private formService: FormService
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.form = this.fb.group({
      id: [0],
      nomeProprietario:     [null, [Validators.required]],
      telefoneProprietario: [null],
      nomeImobiliaria:      [null, [Validators.required]],
      telefoneFixo:         [null, [Validators.required]],
      celular:              [null],
      email:                [null, [Validators.required, Validators.email]],
      vigenciaInicio:       [null, [Validators.required]],
      vigenciaTermino:      [null, [Validators.required]],
      valorAluguel:         [null, [Validators.required]],
      valorCondominio:      [null],
      valorIPTU:            [null, [Validators.required]],
      unidadeDespesas:      [null]
    })

    this.despesasForm = this.fb.group({
      id: [''],
      descricao: [''],
      valor: [null],
      unidadeId:[''],
    })

    this.form.setValidators([
      CompareInitialEndDate('vigenciaInicio', 'vigenciaTermino')
    ]);

    this.onFormsChanges();
    this.loadFormData(this.contratoLocacaoInput);
    this.dataProvider.getValidate().subscribe(res => {
      if (res) this.formService.validateAllFields(this.form)
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (!changes && !changes.contratoLocacaoInput || changes.contratoLocacaoInput.firstChange) return;
    const { currentValue } = changes.contratoLocacaoInput;
    this.loadFormData(currentValue);
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  onFormsChanges(): void {
    // All form
    this.form.valueChanges
      .pipe(debounceTime(500))
      .subscribe(val => {
        let data = null;
        if (this.form.valid) data = val;
        this.contratoLocacaoNext(data);
      })
    
    // Despesa ou Valor
    combineLatest(this.despesasForm.get('descricao').valueChanges, this.despesasForm.get('valor').valueChanges).subscribe(val => {
      this.btnAddDespesaHidden = (val[0] === '' || val[1] === '') ? true : false;
    })

    // Change mask of all contact numbers
    this.form.get('telefoneProprietario').valueChanges
      .subscribe(val => this.maskTelefoneProprietario = (val && val.length > 10) ? CelMask : TelMask);
    this.form.get('telefoneFixo').valueChanges
      .subscribe(val => this.masktelefoneFixo = (val && val.length > 10) ? CelMask : TelMask);
    this.form.get('celular').valueChanges
      .subscribe(val => this.maskCelular = (val && val.length > 10) ? CelMask : TelMask);
  }

  contratoLocacaoNext(dataForm: any): void {
    if (!dataForm) {
      this.dataProvider.contratoLocacaoNext(null);
      return; 
    }
    const { id, nomeProprietario, telefoneProprietario, nomeImobiliaria, telefoneFixo, celular, email, vigenciaInicio, vigenciaTermino, valorAluguel, valorCondominio, valorIPTU, unidadeDespesas} = dataForm;
    const data = {
      id,
      nomeProprietario,
      telefoneProprietario,
      nomeImobiliaria,
      telefoneFixo,
      celular,
      email,
      vigenciaInicio,
      vigenciaTermino,
      valorAluguel: valorAluguel ? parseFloat(valorAluguel) : null,
      valorCondominio: valorCondominio ? parseFloat(valorCondominio) : null,
      valorIPTU: valorIPTU ? parseFloat(valorIPTU) : null,
      unidadeDespesas,
    }
    this.dataProvider.contratoLocacaoNext(data);
  }

  loadFormData(data) {
    if (!data) return;
    const { id, nomeProprietario, telefoneProprietario, nomeImobiliaria, telefoneFixo, celular, email, vigenciaInicio, vigenciaTermino, valorAluguel, valorCondominio, valorIPTU, unidadeDespesas } = data;
    this.form.patchValue({
      id, 
      nomeProprietario,
      telefoneProprietario,
      nomeImobiliaria,
      telefoneFixo,
      celular,
      email,
      vigenciaInicio,
      vigenciaTermino,
      valorAluguel,
      valorCondominio,
      valorIPTU,
      unidadeDespesas
    })

    this.dataProvider.contratoLocacaoNext(this.form.value);

    this.dataSource.data = unidadeDespesas;
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  addDespesa(): void {
    const { descricao, valor} = this.despesasForm.value;
    if (!descricao || !valor) return;
    let data = this.dataSource.data;
    data.push({ descricao, valor: parseFloat(valor) });
    this.dataSource.data = data;
    this.form.get('unidadeDespesas').setValue(data);
    this.despesasForm.reset();
  }

  removeDespesa(index: number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      let data = this.dataSource.data;
      data.splice(index, 1);
      this.dataSource.data = data;
      this.form.get('unidadeDespesas').setValue(data);
    })
  }
}
