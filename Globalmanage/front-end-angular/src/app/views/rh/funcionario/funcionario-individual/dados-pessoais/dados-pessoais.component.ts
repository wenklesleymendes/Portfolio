import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { CPFMask, TelMask, CelMask } from 'src/app/utils/mask/mask';
import { Estados } from 'src/app/utils/variables/locations';
import { DataProvider } from '../data-provider';
import { LocationService } from 'src/app/services/location.service';
import { debounceTime } from 'rxjs/operators';
import { FormService } from 'src/app/services/form.service';
import { validarCPF } from 'src/app/utils/form-validation/cpf.validation';

@Component({
  selector: 'app-dados-pessoais',
  templateUrl: './dados-pessoais.component.html',
  styleUrls: ['./dados-pessoais.component.scss']
})
export class DadosPessoaisComponent implements OnInit {
  @Input() dadosPessoaisInput: any;
  onlyNumbers = /^-?(0|[1-9]\d*)?$/;

  form: FormGroup = new FormGroup({
    nome: new FormControl(null, [Validators.required]),
    rg: new FormControl (null, [Validators.required]),
    cpf: new FormControl(null, [Validators.required, validarCPF]),
    dataNascimento: new FormControl(null, [Validators.required]),
    isActive: new FormControl(false),
    endereco: new FormGroup({
      id:  new FormControl(0),
      cep: new FormControl(null, [Validators.required]),
      rua: new FormControl(null, [Validators.required]),
      numero: new FormControl(null, [Validators.required, Validators.pattern(this.onlyNumbers)]),
      complemento: new FormControl(null),
      bairro: new FormControl(null, [Validators.required]),
      cidade: new FormControl(null, [Validators.required]),
      estado: new FormControl(null, [Validators.required])
    }),
    contato: new FormGroup({
      id: new FormControl(0),
      telefoneFixo: new FormControl(null, [Validators.required]),
      celular: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required, Validators.email])
    })   
  });

  cpfkMask = CPFMask;  
  estados: string[] = Estados;
  maskTelefoneFixoPrincipal = TelMask;
  maskCelular = TelMask;

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
    this.buildForm();
    this.loadFormData(this.dadosPessoaisInput);
    this.dataProvider.getValidate().subscribe(res => {
      if (res) this.formService.validateAllFields(this.form)
    });
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {

    // All form
    this.form.valueChanges
      .pipe(debounceTime(500))
      .subscribe(val => {
        let data = null;
        if (this.form.valid) data = val;        
        this.dataProvider.dadosPessoaisNext(data);
      })

    // Cep
    this.form.get('endereco').get('cep').valueChanges
      .pipe(debounceTime(500))
      .subscribe(val => this.getLocation(val));

    // Change mask of all contact numbers
    this.form.get('contato').get('telefoneFixo').valueChanges
      .subscribe(val => this.maskTelefoneFixoPrincipal = (val && val.length > 10) ? CelMask : TelMask);
    this.form.get('contato').get('celular').valueChanges
    .subscribe(val => this.maskCelular = (val && val.length > 10) ? CelMask : TelMask);
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
    this.form.patchValue(data)
    this.dataProvider.dadosPessoaisNext(this.form.value);
  }
}
