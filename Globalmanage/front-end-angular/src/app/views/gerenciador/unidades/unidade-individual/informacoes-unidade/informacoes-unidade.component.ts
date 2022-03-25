import { Component, OnInit, Input, SimpleChanges, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HourMinuteMask, CNPJMask, CelMask, TelMask } from 'src/app/utils/mask/mask';
import { DataProvider } from '../data-provider';
import { debounceTime } from 'rxjs/operators';
import { LocationService } from 'src/app/services/location.service';
import { CompareInitialEndDate } from 'src/app/utils/form-validation/initial-end-dates.validation';
import { CompareInitialEndHour } from 'src/app/utils/form-validation/initial-end-hour.validation';
import { Estados } from 'src/app/utils/variables/locations';
import { FormService } from 'src/app/services/form.service';
import { validarCNPJ } from 'src/app/utils/form-validation/cnpj.validation';

@Component({
  selector: 'app-informacoes-unidade',
  templateUrl: './informacoes-unidade.component.html',
  styleUrls: ['./informacoes-unidade.component.scss']
})
export class InformacoesUnidadeComponent implements OnInit, OnChanges {
  @Input() infomacoesInput: any;

  form: FormGroup;
  hourMinute = HourMinuteMask;
  cnpjkMask = CNPJMask;
  onlyNumbers = /^-?(0|[1-9]\d*)?$/;
  estados: string[] = Estados;

  maskTelefoneFixoPrincipal = TelMask;
  maskTelefoneFixo2 = TelMask;
  maskTelefoneFixo3 = TelMask;
  maskTelefoneFixo4 = TelMask;
  maskTelefoneFixo5 = TelMask;
  maskCelular = TelMask;
  maskWhatsApp = TelMask;
  maskSigla = 'SS'

  constructor(
    private fb: FormBuilder,
    private dataProvider: DataProvider,
    private locationService: LocationService,
    private formService: FormService
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit() {
    this.buildForn();
    this.loadFormData(this.infomacoesInput);
    this.dataProvider.getValidate().subscribe(res => {
      if (res) this.formService.validateAllFields(this.form)
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (!changes && !changes.infomacoesInput || changes.infomacoesInput.firstChange) return;
    const { currentValue } = changes.infomacoesInput;
    if (currentValue.contato.id > 0 ) {
      this.loadFormData(currentValue);
    }
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForn(): void {
    this.form = this.fb.group({
      nome: [null, [Validators.required]],
      cnpj: [null, [Validators.required, validarCNPJ]],
      razaoSocial: [null, [Validators.required]],
      nomeFantasia: [null, [Validators.required]],
      sigla: [null, [Validators.required]],
      vigenciaInicioAVCB: [null],
      vigenciaTerminoAVCB: [null],
      vigenciaInicioAlvara: [null],
      vigenciaTerminoAlvara: [null],
      endereco: this.fb.group({
        id:[0],
        cep: [null, [Validators.required]],
        rua: [null, [Validators.required]],
        numero: [null, [Validators.required, Validators.pattern(this.onlyNumbers)]],
        complemento: [null],
        bairro: [null, [Validators.required]],
        cidade: [null, [Validators.required]],
        estado: [null, [Validators.required]]
      }),
      contato: this.fb.group({
        id:[0],
        telefoneFixoPrincipal: [null, [Validators.required]],
        telefoneFixo2: [null],
        telefoneFixo3: [null],
        telefoneFixo4: [null],
        telefoneFixo5: [null],
        celular: [null, [Validators.required]],
        email: [null, [Validators.required, Validators.email]],
        site: [null, [Validators.required]],
        whatsApp: [null, [Validators.required]],
        instagram: [null, [Validators.required]],
        faceBook: [null, [Validators.required]]
      }),
      horarioFuncionamento: this.fb.group({
        comAulaSemanaInicio: [null, [Validators.required]],
        comAulaSemanaTermino: [null, [Validators.required]],
        comAulaSabadoInicio: [null, [Validators.required]],
        comAulaSabadoTermino: [null, [Validators.required]],
        semAulaSemanaInicio: [null, [Validators.required]],
        semAulaSemanaTermino: [null, [Validators.required]],
        semAulaSabadoInicio: [null, [Validators.required]],
        semAulaSabadoTermino: [null, [Validators.required]],
      })
    })

    this.form.setValidators([
      CompareInitialEndDate('vigenciaInicioAVCB', 'vigenciaTerminoAVCB'),
      CompareInitialEndDate('vigenciaInicioAlvara', 'vigenciaTerminoAlvara')
    ]);

    this.form.get('horarioFuncionamento').setValidators([
      CompareInitialEndHour('comAulaSemanaInicio', 'comAulaSemanaTermino'),
      CompareInitialEndHour('comAulaSabadoInicio', 'comAulaSabadoTermino'),
      CompareInitialEndHour('semAulaSemanaInicio', 'semAulaSemanaTermino'),
      CompareInitialEndHour('semAulaSabadoInicio', 'semAulaSabadoTermino'),
    ]);
    
    // All form
    this.form.valueChanges
      .pipe(debounceTime(500))
      .subscribe(val => {
        let data = null;
        if (this.form.valid) data = val;
        
        this.dataProvider.infomacoesNext(data);
      })

    // Cep
    this.form.get('endereco').get('cep').valueChanges
      .pipe(debounceTime(500))
      .subscribe(val => this.getLocation(val));

    // Change mask of all contact numbers
    this.form.get('contato').get('telefoneFixoPrincipal').valueChanges
      .subscribe(val => this.maskTelefoneFixoPrincipal = (val && val.length > 10) ? CelMask : TelMask);
    this.form.get('contato').get('telefoneFixo2').valueChanges
      .subscribe(val => this.maskTelefoneFixo2 = (val && val.length > 10) ? CelMask : TelMask);
    this.form.get('contato').get('telefoneFixo3').valueChanges
      .subscribe(val => this.maskTelefoneFixo3 = (val && val.length > 10) ? CelMask : TelMask);
    this.form.get('contato').get('telefoneFixo4').valueChanges
      .subscribe(val => this.maskTelefoneFixo4 = (val && val.length > 10) ? CelMask : TelMask);
    this.form.get('contato').get('telefoneFixo5').valueChanges
      .subscribe(val => this.maskTelefoneFixo5 = (val && val.length > 10) ? CelMask : TelMask);
    this.form.get('contato').get('celular').valueChanges
    .subscribe(val => this.maskCelular = (val && val.length > 10) ? CelMask : TelMask);
    this.form.get('contato').get('whatsApp').valueChanges
      .subscribe(val => this.maskWhatsApp = (val && val.length > 10) ? CelMask : TelMask);
  }

  /** 
   * @description Update the location data
   * @param {string} cep CEP
  */
  getLocation(cep: string): void { 
    this.locationService.getLocation(cep).subscribe(val => {
      const { bairro, localidade, logradouro, uf } = val;

      if (bairro) this.form.get('endereco').get('bairro').setValue(bairro);
      if (logradouro) this.form.get('endereco').get('rua').setValue(logradouro);
      if (localidade) this.form.get('endereco').get('cidade').setValue(localidade);
      if (uf) this.form.get('endereco').get('estado').setValue(uf);
    })
  }

  loadFormData(data) {
    if (!data) return;
    const { nome, cnpj, razaoSocial, nomeFantasia, vigenciaInicioAVCB, vigenciaTerminoAVCB, vigenciaInicioAlvara, vigenciaTerminoAlvara, endereco, contato, horarioFuncionamento, sigla } = data;
    this.form.patchValue({
      nome,
      cnpj,
      razaoSocial,
      nomeFantasia,
      vigenciaInicioAVCB,
      vigenciaTerminoAVCB,
      vigenciaInicioAlvara,
      vigenciaTerminoAlvara,
      endereco,
      contato,
      sigla,
      horarioFuncionamento: {
        comAulaSemanaInicio:  horarioFuncionamento[0] ? horarioFuncionamento[0]['semanaInicio'] : null,
        comAulaSemanaTermino: horarioFuncionamento[0] ? horarioFuncionamento[0]['semanaTermino'] : null,
        comAulaSabadoInicio:  horarioFuncionamento[0] ? horarioFuncionamento[0]['sabadoInicio'] : null,
        comAulaSabadoTermino: horarioFuncionamento[0] ? horarioFuncionamento[0]['sabadoTermino'] : null,
        semAulaSemanaInicio:  horarioFuncionamento[1] ? horarioFuncionamento[1]['semanaInicio'] : null,
        semAulaSemanaTermino: horarioFuncionamento[1] ? horarioFuncionamento[1]['semanaTermino'] : null,
        semAulaSabadoInicio:  horarioFuncionamento[1] ? horarioFuncionamento[1]['sabadoInicio'] : null,
        semAulaSabadoTermino: horarioFuncionamento[1] ? horarioFuncionamento[1]['sabadoTermino'] : null
      }
    })

    this.dataProvider.infomacoesNext(this.form.value);
  }
}
