import { Component, OnInit, Input, SimpleChanges, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { debounceTime } from 'rxjs/operators';
import { DataProvider } from '../data-provider';
import { InstituicaoBancariaService } from 'src/app/services/instituicao-bancaria.service';
import { FormService } from 'src/app/services/form.service';

@Component({
  selector: 'app-dados-bancarios',
  templateUrl: './dados-bancarios.component.html',
  styleUrls: ['./dados-bancarios.component.scss']
})
export class DadosBancariosComponent implements OnInit, OnChanges {
  @Input() dadosBancariosInput: any;
  form: FormGroup;
  bancos: any = [];

  constructor(
    private fb: FormBuilder,
    private dataProvider: DataProvider,
    private instituicaoBancariaService: InstituicaoBancariaService,
    private formService: FormService
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit() {
    this.form = this.fb.group({
      id: [0],
      codigoBanco: [null, [Validators.required]],
      tipoContaBancaria: [null, [Validators.required]],
      numeroAgencia: [null, [Validators.required]],
      numeroConta: [null, [Validators.required]]
    })

    this.onFormChanges();
    this.getBancos();
    this.loadFormData(this.dadosBancariosInput);
    this.dataProvider.getValidate().subscribe(res => {
      if (res) this.formService.validateAllFields(this.form)
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (!changes && !changes.dadosBancariosInput || changes.dadosBancariosInput.firstChange) return;
    const { currentValue } = changes.dadosBancariosInput;
    this.loadFormData(currentValue);
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  onFormChanges(): void {
    // All form
    this.form.valueChanges
      .pipe(debounceTime(500))
      .subscribe(val => {
        let data = null;
        if (this.form.valid) {
          data = val;
          data['tipoContaBancaria'] = parseInt(data['tipoContaBancaria']);
        }
        this.dataProvider.dadosBancariosNext(data);
      })
  }

  getBancos(): void {
    this.instituicaoBancariaService.getAll().subscribe(val => this.bancos = val);
  }

  loadFormData(data) {
    if (!data) return;
    let { id, codigoBanco, tipoContaBancaria, numeroAgencia, numeroConta } = data;
    tipoContaBancaria = tipoContaBancaria ? tipoContaBancaria.toString() : null;

    this.form.patchValue({
      id,
      codigoBanco,
      tipoContaBancaria,
      numeroAgencia,
      numeroConta
    });

    this.dataProvider.dadosBancariosNext(this.form.value);
  }
}
