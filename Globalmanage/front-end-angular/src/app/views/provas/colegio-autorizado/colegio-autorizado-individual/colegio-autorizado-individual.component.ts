import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { CelMask, TelMask, CNPJMask } from 'src/app/utils/mask/mask';
import { Estados } from 'src/app/utils/variables/locations';
import { debounceTime } from 'rxjs/operators';
import { LocationService } from 'src/app/services/location.service';
import { ColegioAutorizadoService } from 'src/app/services/provas/colegio-autorizado.service';
import { validarCNPJ } from 'src/app/utils/form-validation/cnpj.validation';

@Component({
  selector: 'app-colegio-autorizado-individual',
  templateUrl: './colegio-autorizado-individual.component.html',
  styleUrls: ['./colegio-autorizado-individual.component.scss']
})
export class ColegioAutorizadoIndividualComponent implements OnInit {
  id = 0;
  form: FormGroup;
  error: boolean = false;
  isLoadingResults: boolean = false;
  onlyNumbers = /^-?(0|[1-9]\d*)?$/;
  maskPrimeiroContato = TelMask;
  maskSegundoContato = TelMask;
  cnpjMask = CNPJMask;
  estados = Estados;

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private colegioAutorizadoService: ColegioAutorizadoService,
    private locationService: LocationService,
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
    this.loadData();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      nomeColegioAutorizado: [null],
      primeiroContatoNome: [null, [Validators.required]],
      primeiroContatoTelefone: [null, [Validators.required]],
      primeiroContatoEmail: [null, [Validators.required, Validators.email]],
      segundoContatoNome: [null],
      segundoContatoTelefone: [null],
      segundoContatoEmail: [null],
      cnpj: [null, [Validators.required, validarCNPJ]],
      razaoSocial: [null, [Validators.required]],
      site: [null],
      endereco: this.fb.group({
        id: [0],
        cep: [null, [Validators.required]],
        rua: [null, [Validators.required]],
        numero: [null, [Validators.required, Validators.pattern(this.onlyNumbers)]],
        complemento: [null],
        bairro: [null, [Validators.required]],
        cidade: [null, [Validators.required]],
        estado: [null, [Validators.required]]
      }),
    });

    // Cep
    this.form.get('endereco').get('cep').valueChanges
      .pipe(debounceTime(500))
      .subscribe(val => this.getLocation(val));

    this.form.get('primeiroContatoTelefone').valueChanges
      .subscribe(val => this.maskPrimeiroContato = (val && val.length > 10) ? CelMask : TelMask);
    this.form.get('segundoContatoTelefone').valueChanges
      .subscribe(val => this.maskSegundoContato = (val && val.length > 10) ? CelMask : TelMask);
  }

  loadData(): void {
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.colegioAutorizadoService.getPorId(this.id)
      .subscribe(val => {
        if (!val || val['status'] === 'error') return this.error = true;

        const patch: any = {};
        for(let key in val) {
          if (val[key]) patch[key] = val[key];
        }
        this.form.patchValue(patch);

      })
    }
    this.isLoadingResults = false;
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

    const formValue: any = this.form.value;
    delete formValue.unidadeId;
    delete formValue.unidadeSelect;

    // Make request
    this.colegioAutorizadoService.cadastrar(formValue).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        const { id } = val;
        this.id = id;
        this.form.get('id').setValue(id);
      }
    })
  }
}
