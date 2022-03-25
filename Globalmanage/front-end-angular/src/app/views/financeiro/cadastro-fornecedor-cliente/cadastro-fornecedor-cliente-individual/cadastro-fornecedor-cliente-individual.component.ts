import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { CNPJMask, TelMask, CelMask, CPFMask } from 'src/app/utils/mask/mask';
import { Estados } from 'src/app/utils/variables/locations';
import { InstituicaoBancariaService } from 'src/app/services/instituicao-bancaria.service';
import { LocationService } from 'src/app/services/location.service';
import { debounceTime } from 'rxjs/operators';
import { FornecedorService } from 'src/app/services/financeiro/fornecedor.service';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { CategoriaService } from 'src/app/services/financeiro/categoria.service';
import { MatAutocomplete, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { DeleteService } from 'src/app/services/delete.service';

@Component({
  selector: 'app-cadastro-fornecedor-cliente-individual',
  templateUrl: './cadastro-fornecedor-cliente-individual.component.html',
  styleUrls: ['./cadastro-fornecedor-cliente-individual.component.scss']
})
export class CadastroFornecedorClienteIndividualComponent implements OnInit {
  form: FormGroup;
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;
  onlyNumbers = /^-?(0|[1-9]\d*)?$/;
  cpfCnpjMask = CNPJMask;
  maskTelefoneFixoPrincipal = TelMask;
  maskCelular = TelMask;
  bancos: any = null;
  estados: string[] = Estados;
  filterCategorias: Observable<any[]> = null;
  categoriasDefault = null;

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private fornecedorService: FornecedorService,
    private categoriaService: CategoriaService,
    private deleteService: DeleteService,
    private instituicaoBancariaService: InstituicaoBancariaService,
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
    this.getBancos();
    this.getCategoria();
    this.loadData();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      tipoPessoa: [1, [Validators.required]],
      categoria: [null, [Validators.required]],
      isActive: [false, [Validators.required]],
      nomeFantasia: [null, [Validators.required]],
      razaoSocial: [null, [Validators.required]],
      dataNascimento: [null, [Validators.required]],
      cpfCnpj: [null, [Validators.required]],
      inscricaoMunicipal: [null, [Validators.required]],
      inscricaoEstadual: [null, [Validators.required]],
      isento: [false, [Validators.required]],
      observacao: [null],
      contato: this.fb.group({
        id: [0],
        telefoneFixo: [null, [Validators.required]],
        celular: [null],
        ramal: [null, [Validators.required]],
        fax: [null],
        site: [null],
        email: [null, [Validators.required, Validators.email]]
      }),
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
      dadosBancario: this.fb.group({
        id: [0],
        codigoBanco: [null],
        tipoContaBancaria: [null],
        numeroAgencia: [null],
        numeroConta: [null],
      })
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

    this.form.get('tipoPessoa').valueChanges.subscribe(val => {
      if (val === 1) {
        this.formService.disableField(this.form.get('dataNascimento'));
        this.formService.enableField(this.form.get('razaoSocial'));
        this.formService.enableField(this.form.get('inscricaoMunicipal'));
        this.formService.enableField(this.form.get('inscricaoEstadual'));
        this.formService.enableField(this.form.get('isento'));
        this.cpfCnpjMask = CNPJMask;
      } else {
        this.formService.enableField(this.form.get('dataNascimento'));
        this.formService.disableField(this.form.get('razaoSocial'));
        this.formService.disableField(this.form.get('inscricaoMunicipal'));
        this.formService.disableField(this.form.get('inscricaoEstadual'));
        this.formService.disableField(this.form.get('isento'));
        this.cpfCnpjMask = CPFMask;
      }
    })

    this.form.get('isento').valueChanges.subscribe(val => {
      if (val) {
        this.formService.disableField(this.form.get('inscricaoMunicipal'));
        this.formService.disableField(this.form.get('inscricaoEstadual'));
      } else {
        this.formService.enableField(this.form.get('inscricaoMunicipal'));
        this.formService.enableField(this.form.get('inscricaoEstadual'));
      }
    })

    this.filterCategorias = this.form.get('categoria').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterCategoria(elem) : this.categoriasDefault.slice())
      );

    this.form.get('tipoPessoa').setValue(1);
  }

  getBancos(): void {
    this.instituicaoBancariaService.getAll()
      .subscribe(val => {
        if (val['status'] === 'error') this.error = true;
        else this.bancos = val;
      });
  }

  getCategoria(): void {
    ////debugger
    this.categoriasDefault = [];
    this.form.get('categoria').setValue('');

    this.categoriaService.getAll()
      .subscribe(val => {
        if (val['status'] === 'error') this.error = true;
        else this.categoriasDefault = val;
        this.form.get('categoria').setValue('');
      });
  }

  loadData(): void {
    ////debugger
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.fornecedorService.getPorId(this.id)
        .subscribe(val => {
          if (!val) return;
          if (val['status'] === 'error') return this.error = true;
          const { categoria } = val;
          this.form.patchValue(val);
          this.form.get('categoria').setValue(categoria?.descricao ? categoria.descricao : null);
          this.isLoadingResults = false;
        })
    }
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

  _filterCategoria(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.categoriasDefault.filter(elem => elem.descricao.toLowerCase().indexOf(filterValue) === 0);
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  voltar(): void {
    this.location.back();
  }

  addCategoria(): void {
    const descricao: string = this.form.get('categoria').value;
    if (!descricao || descricao == '') {
      this.animationsService.showErrorSnackBar('Informe uma categoria');
      return;
    }
    const existe = this.categoriasDefault.length > 0 ? this.categoriasDefault.find(elem => elem.descricao.toLowerCase() === descricao.toLowerCase().trim()) : false;
    if (existe) {
      this.animationsService.showErrorSnackBar('Categoria já cadastrada');
      return;
    }
    const data = { id: 0,  descricao }
    this.categoriaService.cadastrar(data).subscribe(val => {
      if (val['status'] === 'error') this.error = true;
      else {
        this.animationsService.showSuccessSnackBar('Categoria adicionada');
        this.getCategoria();
      }
    })
  }

  salvarData(): void {
    // Validating form
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }
    const formValue: any = this.form.value;

    const categoria = this.categoriasDefault.length > 0 ? this.categoriasDefault.find(elem => elem.descricao.toLowerCase() === formValue.categoria.toLowerCase().trim()) : false;
    if (!categoria) {
      this.animationsService.showErrorSnackBar('Adicione a categoria');
      return;
    }
    formValue['categoriaId'] = categoria.id;
    delete formValue.categoria;
    // Make request
    this.fornecedorService.cadastrar(formValue).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        const { id } = val;
        this.id = id;
        this.form.get('id').setValue(id);
      }
    })
  }

  deleteCategoria(): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      const categoria = this.form.get('categoria').value;
      const categoriaSelected = this.categoriasDefault.length > 0 ? this.categoriasDefault.find(elem => elem.descricao.toLowerCase() === categoria.toLowerCase().trim()) : false;

      if(!categoriaSelected?.id) return;

      this.categoriaService.deletarPorId(categoriaSelected.id).subscribe(val => {
        this.form.get('categoria').setValue(null);
        this.getCategoria();
      })
    })
  }
}
