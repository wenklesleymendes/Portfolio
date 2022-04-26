import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators, FormArray, AbstractControl, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { startWith, map, distinctUntilChanged } from 'rxjs/operators';
import { FornecedorService } from 'src/app/services/financeiro/fornecedor.service';
import { Observable, BehaviorSubject, forkJoin, combineLatest } from 'rxjs';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { CategoriaService } from 'src/app/services/financeiro/categoria.service';
import * as moment from 'moment';
import { ContasPagarService } from 'src/app/services/financeiro/contas-pagar.service';
import { InstituicaoBancariaService } from 'src/app/services/instituicao-bancaria.service';
import { MatTableDataSource } from '@angular/material/table';
import { DeleteService } from 'src/app/services/delete.service';
import { Anexos } from 'src/app/utils/variables/anexos';
import { AnexoService } from 'src/app/services/anexo.service';
import { MatDialog } from '@angular/material/dialog';
import { CancelarPagamentoComponent } from './cancelar-pagamento/cancelar-pagamento.component';
import { CPFCNPJMask, CNPJMask } from 'src/app/utils/mask/mask';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-contas-pagar-individual',
  templateUrl: './contas-pagar-individual.component.html',
  styleUrls: ['./contas-pagar-individual.component.scss']
})
export class ContasPagarIndividualComponent implements OnInit {
  form: FormGroup;
  despesaParcela: FormArray;
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;  
  filterUnidades: Observable<any[]>;
  unidadesDefault: any[] = null;
  filterFornecedor: Observable<any[]>;
  fornecedorDefault: any[] = null;
  filterCategorias: Observable<any[]> = null;
  categoriasDefault = null;
  departamentos: any[] = [];
  displayedColumns: string[] = ['data', 'valor', 'codigo', 'formaPagamento', 'manual', 'options'];
  dataSource = new BehaviorSubject<FormArray | AbstractControl>(null);
  anexoColumns: string[] = ['descricao', 'tipo', 'dataAnexo', 'options'];
  anexoSource = new MatTableDataSource([]);
  bancos: any[] = [];
  anexos = Anexos;
  cpfCnpjMask = CPFCNPJMask;
  identificadorMask = CPFCNPJMask;
  selectionAnexo = new SelectionModel<any>(true, []);
  selectionParcelas = new SelectionModel<any>(true, []);

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private fornecedorService: FornecedorService,
    private contasPagarService: ContasPagarService,
    private unidadeService: UnidadeService,
    private anexoService: AnexoService,
    private categoriaService: CategoriaService,
    private instituicaoBancariaService: InstituicaoBancariaService,
    private dialog: MatDialog,
    private animationService: AnimationsService,
    private deleteService: DeleteService,
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
    Promise.all([
      this.getCategoria(),
      this.getunidades(),
      this.getBancos(),
      this.getFornecedor(),
      this.getAnexo()
    ])
    .then(() => this.loadData())
    .catch(() => this.isLoadingResults = false)
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      nomeDespesa: [null, [Validators.required]],
      unidadeId: [null, [Validators.required]],
      unidadeSelect: [null, [Validators.required]],
      fornecedorId: [null, [Validators.required]],
      fornecedorSelect: [null, [Validators.required]],
      centroCustoId: [{ disabled: true, value: null}, [Validators.required]],
      categoria: [null, [Validators.required]],
      tipoDespesa: [null, [Validators.required]],
      dataVencimento: [null, [Validators.required]],
      valorTotalDespesa: [null, [Validators.required]],
      tipoPessoa: [1, [Validators.required]],
      dataEmissao: [null, [Validators.required]],
      observacao: [null],
      codigoBanco: [null, [Validators.required]],
      tipoContaBancaria: [null, [Validators.required]],
      numeroAgencia: [null, [Validators.required]],
      numeroConta: [null, [Validators.required]],
      baixaManual: [false, [Validators.required]],
      despesaParcela: this.fb.array([]),      
      despesaDARF: this.fb.group({
        id: [0],
        nomeContribuinte: [null],
        periodoApuracao: [null],
        cnpjCpf: [null],
        codigoReceita: [null],
        numeroReferencia: [null],
        dataVencimento: [null],
        valorPrincipal: [null],
        valorMulta: [null],
        valorJurosEncargos: [null],
        valorTotal: [null],
        dataPagamento: [null],
        referenciaEmpresa: [null],
        identificacaoComprovante: [null]
      }),
      despesaGPS: this.fb.group({
        id: [0],
        nomeContribuinte: [null],
        codigoPagamento: [null],
        competencia: [null],
        identificador: [null],
        valorINSS: [null],
        valorOutrasEntidades: [null],
        atualizacaoMonetariaJuroMora: [null],
        valorTotalRecolher: [null],
        dataPagamento: [null],
        identificaçãoComprovante: [null],
        referenciaEmpresa: [null]
      }),

      numeroDocumento: [null],
      tipoParcela: [null],
      quantidadeParcela: [null],
      descricao: [null],
      tipoAnexo: [null],
    })

    this.filterUnidades = this.form.get('unidadeSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
      );

    this.form.get('unidadeSelect').valueChanges.subscribe(val => {
      const unidadeId = this.unidadesDefault?.length >= 0 ? this.unidadesDefault.find(elem => elem.nome == val) : null;
      if (unidadeId && unidadeId.id) {
        this.form.get('unidadeId').setValue(unidadeId.id);
        let departamentos = this.unidadesDefault.find(elem => elem.id == unidadeId.id);
        this.departamentos = departamentos?.centroCusto ? departamentos.centroCusto : [];
        this.formService.enableField(this.form.get('centroCustoId'));
        this.formService.mandatoryFields(this.form.get('centroCustoId'));
      }
      else {
        this.form.get('unidadeId').setValue(null);
        this.formService.disableField(this.form.get('centroCustoId'));
      }
    });

    this.filterFornecedor = this.form.get('fornecedorSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterFornecedor(elem) : this.fornecedorDefault.slice())
      );

    this.form.get('fornecedorSelect').valueChanges.subscribe(val => {
      const fornecedorId = this.fornecedorDefault?.length >= 0 ? this.fornecedorDefault.find(elem => elem.nomeFantasia == val) : null;
      if (fornecedorId && fornecedorId.id) this.form.get('fornecedorId').setValue(fornecedorId.id)
      else this.form.get('fornecedorId').setValue(null)
    });

    this.filterCategorias = this.form.get('categoria').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterCategoria(elem) : this.categoriasDefault.slice())
      );

    this.form.get('tipoParcela').valueChanges.subscribe(val => {
      if (this.id != 0 || val === 1 || val === 3) this.formService.disableField(this.form.get('quantidadeParcela'))
      else this.formService.enableField(this.form.get('quantidadeParcela'))

      const { valorTotalDespesa, numeroDocumento, dataVencimento, tipoParcela } = this.form.value;
      if (valorTotalDespesa && numeroDocumento && tipoParcela && dataVencimento) {
        this.gerar();
      }
    });

    this.form.get('fornecedorId').valueChanges.subscribe(val => {
        if (this.form.get('tipoDespesa').value !== 2) return;
        if (!val) return;
        const fornecedor = this.fornecedorDefault.find(elem => elem.id === val);
        if (!fornecedor) return
        const { codigoBanco, tipoContaBancaria, numeroAgencia, numeroConta } = fornecedor.dadosBancario;

        this.form.get('codigoBanco').setValue(codigoBanco);
        this.form.get('tipoContaBancaria').setValue(tipoContaBancaria);
        this.form.get('numeroAgencia').setValue(numeroAgencia);
        this.form.get('numeroAgencia').setValue(numeroAgencia);
        this.form.get('numeroConta').setValue(numeroConta);
      });

    this.form.get('tipoDespesa').valueChanges.subscribe(val => {
      this.despesaParcela.clear();
      this.dataSource.next(null);
      this.dataSource.next(this.despesaParcela);
      // BOLETO
      if (val === 1) {
        this.formService.disableAllFields(this.form.get('despesaGPS') as FormGroup);
        this.formService.disableAllFields(this.form.get('despesaDARF') as FormGroup);
        this.formService.enableField(this.form.get('tipoPessoa'));
        this.formService.enableField(this.form.get('fornecedorSelect'));
        this.formService.enableField(this.form.get('fornecedorId'));
        this.form.get('fornecedorSelect').setValue('');
        this.formService.disableField(this.form.get('codigoBanco'));
        this.formService.disableField(this.form.get('tipoContaBancaria'));
        this.formService.disableField(this.form.get('numeroAgencia'));
        this.formService.disableField(this.form.get('numeroConta'));
        this.formService.enableField(this.form.get('dataEmissao'));
        this.formService.enableField(this.form.get('dataVencimento'));
        this.formService.enableField(this.form.get('valorTotalDespesa'));
      }
      // TRANSFERENCIA
      else if (val === 2) {
        this.formService.disableAllFields(this.form.get('despesaGPS') as FormGroup);
        this.formService.disableAllFields(this.form.get('despesaDARF') as FormGroup);
        this.formService.enableField(this.form.get('tipoPessoa'));
        this.formService.enableField(this.form.get('fornecedorSelect'));
        this.formService.enableField(this.form.get('fornecedorId'));
        this.form.get('fornecedorSelect').setValue('');
        this.formService.enableField(this.form.get('codigoBanco'));
        this.formService.enableField(this.form.get('tipoContaBancaria'));
        this.formService.enableField(this.form.get('numeroAgencia'));
        this.formService.enableField(this.form.get('numeroConta'));
        this.formService.enableField(this.form.get('dataEmissao'));
        this.formService.enableField(this.form.get('dataVencimento'));
        this.formService.enableField(this.form.get('valorTotalDespesa'));
      }
      // DARF
      else if (val === 3) {
        this.formService.disableAllFields(this.form.get('despesaGPS') as FormGroup);
        this.formService.enableAllFields(this.form.get('despesaDARF') as FormGroup);
        this.form.get('despesaDARF').get('valorTotal').disable({emitEvent: false});
        this.formService.notMandatoryFields(this.form.get('despesaDARF').get('numeroReferencia'));
        this.formService.notMandatoryFields(this.form.get('despesaDARF').get('valorMulta'));
        this.formService.notMandatoryFields(this.form.get('despesaDARF').get('valorJurosEncargos'));
        this.formService.notMandatoryFields(this.form.get('despesaDARF').get('nomeContribuinte'));
        this.formService.notMandatoryFields(this.form.get('despesaDARF').get('referenciaEmpresa'));
        this.formService.notMandatoryFields(this.form.get('despesaDARF').get('identificacaoComprovante'));
        this.formService.disableField(this.form.get('tipoPessoa'));
        this.formService.disableField(this.form.get('fornecedorSelect'));
        this.formService.disableField(this.form.get('fornecedorId'));
        this.formService.disableField(this.form.get('codigoBanco'));
        this.formService.disableField(this.form.get('tipoContaBancaria'));
        this.formService.disableField(this.form.get('numeroAgencia'));
        this.formService.disableField(this.form.get('numeroConta'));
        this.formService.disableField(this.form.get('dataEmissao'));
        this.formService.disableField(this.form.get('dataVencimento'));
        this.formService.disableField(this.form.get('valorTotalDespesa'));
      } 
      // GPS
      else if (val === 4) {
        this.formService.enableAllFields(this.form.get('despesaGPS') as FormGroup);
        this.formService.disableAllFields(this.form.get('despesaDARF') as FormGroup);
        this.form.get('despesaGPS').get('valorTotalRecolher').disable({emitEvent: false});
        this.formService.notMandatoryFields(this.form.get('despesaGPS').get('valorOutrasEntidades'));
        this.formService.notMandatoryFields(this.form.get('despesaGPS').get('atualizacaoMonetariaJuroMora'));
        this.formService.notMandatoryFields(this.form.get('despesaGPS').get('nomeContribuinte'));
        this.formService.notMandatoryFields(this.form.get('despesaGPS').get('identificaçãoComprovante'));
        this.formService.notMandatoryFields(this.form.get('despesaGPS').get('referenciaEmpresa'));
        this.formService.disableField(this.form.get('tipoPessoa'));
        this.formService.disableField(this.form.get('fornecedorSelect'));
        this.formService.disableField(this.form.get('fornecedorId'));
        this.formService.disableField(this.form.get('codigoBanco'));
        this.formService.disableField(this.form.get('tipoContaBancaria'));
        this.formService.disableField(this.form.get('numeroAgencia'));
        this.formService.disableField(this.form.get('numeroConta'));
        this.formService.disableField(this.form.get('dataEmissao'));
        this.formService.disableField(this.form.get('dataVencimento'));
        this.formService.disableField(this.form.get('valorTotalDespesa'));
      }
    });

    this.despesaParcela = this.form.get('despesaParcela') as FormArray;

    this.despesaParcela.valueChanges
      .pipe(distinctUntilChanged())
      .subscribe((val: any[]) => {
      val.forEach((control, index) => {
        if(control?.statusPagamento === 2 || control?.statusPagamento === 3) {
          this.despesaParcela.at(index).get('dataVencimento').disable({emitEvent: false});
          this.despesaParcela.at(index).get('valorParcela').disable({emitEvent: false});
          this.despesaParcela.at(index).get('codigoBarras').disable({emitEvent: false});
          this.despesaParcela.at(index).get('tipoPagamento').disable({emitEvent: false});
          this.despesaParcela.at(index).get('lancamentoManual').disable({emitEvent: false});
        }
      })
    })

    this.form.get('id').valueChanges.subscribe(val => {
      if (val === 0) return;
      this.form.get('numeroDocumento').disable({emitEvent: false});
      this.form.get('tipoParcela').disable({emitEvent: false});
      this.form.get('quantidadeParcela').disable({emitEvent: false});
      this.form.get('dataVencimento').disable({emitEvent: false});
      this.form.get('valorTotalDespesa').disable({emitEvent: false});
      this.form.get('baixaManual').disable({emitEvent: false});      
    });

    this.form.get('despesaDARF').get('cnpjCpf').valueChanges.subscribe((val: string) => {
      if(!val) return;
      this.cpfCnpjMask = val.length > 11 ? CNPJMask : CPFCNPJMask;
    })

    this.form.get('despesaGPS').get('identificador').valueChanges.subscribe((val: string) => {
      if(!val) return;
      this.identificadorMask = val.length > 11 ? CNPJMask : CPFCNPJMask;
    })

    // Soma valores DARF
    combineLatest(
      this.form.get('despesaDARF').get('valorPrincipal').valueChanges,
      this.form.get('despesaDARF').get('valorMulta').valueChanges,
      this.form.get('despesaDARF').get('valorJurosEncargos').valueChanges,
    ).subscribe((val: number[]) => {
      let soma: number = 0;
      val.forEach(elem => {
        if(elem) soma+= elem;
      });
      this.form.get('despesaDARF').get('valorTotal').setValue(soma);
    })

    // Soma valores GPS
    combineLatest(
      this.form.get('despesaGPS').get('valorINSS').valueChanges,
      this.form.get('despesaGPS').get('valorOutrasEntidades').valueChanges,
      this.form.get('despesaGPS').get('atualizacaoMonetariaJuroMora').valueChanges,
    ).subscribe((val: number[]) => {
      let soma: number = 0;
      val.forEach(elem => {
        if(elem) soma+= elem;
      });
      this.form.get('despesaGPS').get('valorTotalRecolher').setValue(soma);
    })
  }

  loadData(): void {
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.contasPagarService.getPorId(this.id)
      .subscribe(val => {
        if (!val || val['status'] === 'error') return this.error = true;
        const { categoria, unidade, centroCusto, fornecedor, codigoBanco, tipoContaBancaria, numeroAgencia, numeroConta, despesaParcela } = val;

        const patch: any = {};
        for(let key in val) {
          if (val[key]) patch[key] = val[key];
        }

        this.form.patchValue(patch);

        this.form.get('unidadeSelect').setValue(unidade?.nome ? unidade.nome : null);
        this.form.get('centroCustoId').setValue(centroCusto?.id ? centroCusto.id : null);
        this.form.get('fornecedorSelect').setValue(fornecedor?.nomeFantasia ? fornecedor.nomeFantasia : null);
        this.form.get('codigoBanco').setValue(codigoBanco);
        this.form.get('tipoContaBancaria').setValue(tipoContaBancaria);
        this.form.get('numeroAgencia').setValue(numeroAgencia);
        this.form.get('numeroAgencia').setValue(numeroAgencia);
        this.form.get('numeroConta').setValue(numeroConta);
        this.form.get('categoria').setValue(categoria?.descricao ? categoria.descricao : null);
        this.despesaParcela.clear();

        if(despesaParcela) {
          despesaParcela.forEach(elem => {
            const { id, despesaId, dataVencimento, valorParcela, codigoBarras, tipoPagamento, statusPagamento, lancamentoManual } = elem;
            this.despesaParcela.push(
              this.fb.group({
                despesaId: [despesaId],
                id: [id],
                dataVencimento: [dataVencimento],
                valorParcela: [valorParcela, [Validators.required]],
                codigoBarras: [codigoBarras],
                tipoPagamento: [tipoPagamento, [Validators.required]],
                statusPagamento: [statusPagamento],
                lancamentoManual: [lancamentoManual]
              })
            );
          });

          this.dataSource.next(this.despesaParcela);
        }

        this.isLoadingResults = false;
      })
    }
  }

  getunidades(): Promise<any> {
    return new Promise((res, rej) => {
      this.unidadeService.getAll()
        .subscribe(val => {
          if (val['status'] === 'error') {
            this.error = true;
            rej();
          }
          else this.unidadesDefault = val;
          this.form.get('unidadeSelect').setValue('');
          res();
        });
    });
  }

  getCategoria(): Promise<any> {
    return new Promise((res, rej) => {
      this.categoriasDefault = [];
      this.form.get('categoria').setValue('');
  
      this.categoriaService.getAll()
        .subscribe(val => {
          if (val['status'] === 'error') {
            this.error = true;
            rej();
          }
          else this.categoriasDefault = val;
          this.form.get('categoria').setValue('');
          res();
        });

    });
  }

  getBancos(): Promise<any> {
    return new Promise((res, rej) => {
      this.instituicaoBancariaService.getAll()
        .subscribe(val => {
          if (val['status'] === 'error') {
            this.error = true;
            rej();
          }
          else this.bancos = val;
          res();
        });
    });
  }

  getFornecedor(): Promise<any> {
    return new Promise((res, rej) => {
      this.fornecedorService.getAll()
        .subscribe(val => {
          if (val['status'] === 'error') {
            this.error = true;
            rej();
          }
          else this.fornecedorDefault = val;
          this.form.get('fornecedorSelect').setValue('');
          res();
        });
    });
  }

  getAnexo(): Promise<any> {
    return new Promise((res, rej) => {
      this.isLoadingResults = true;
      this.contasPagarService.getDocumentosDespesa(this.id, true).subscribe(val => {
        if (val['status'] === 'error') {
          this.error = true;
          rej();
        }
        this.isLoadingResults = false;
        this.anexoSource.data = val;
        res();
      })
    });
  }

  _filterUnidades(value: string): any[] {
    if(!value) return;
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  _filterCategoria(value): any[] {
    if(!value) return;
    const filterValue: string = null;
    if(value?.descricao) value.descricao.toLowerCase();
    else value.toLowerCase();
    return this.categoriasDefault.filter(elem => elem.descricao.toLowerCase().indexOf(filterValue) === 0);
  }

  _filterFornecedor(value: string): any[] {
    if(!value) return;
    const filterValue = value.toLowerCase();
    return this.fornecedorDefault.filter(elem => elem.nomeFantasia.toLowerCase().indexOf(filterValue) === 0);
  }

  labelAnexo(id): string {
    let label = this.anexos.find(anexo => anexo.value == id ).label;
    return label ? label : '-';
  }

  closeDatePicker(dateString: string, dp: any, control: FormControl | AbstractControl) {
    const date: moment.Moment = moment(dateString);
    date.endOf('month');
    control.setValue(date.format());
    dp.close();
  }

  corLinhaParcela(index: number): string {
    const data = this.despesaParcela.at(index).value;
    if(data?.statusPagamento === 2) return 'bg-light-green ';
    else if(data?.statusPagamento === 3) return 'bg-light-red ';
    else return '';
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

    // Validating 'Categoria'
    const formValue: any = this.form.value;
    const categoria = this.categoriasDefault.length > 0 ? this.categoriasDefault.find(elem => elem.descricao.toLowerCase() === formValue.categoria.toLowerCase().trim()) : false;
    if (!categoria) {
      this.animationsService.showErrorSnackBar('Adicione a categoria');
      return;
    }
    // Validating 'Despesa'
    if(!this.despesaParcela?.length || this.despesaParcela?.length === 0) {
      this.animationsService.showErrorSnackBar('Gere despesas');
      return;
    }

    formValue['categoriaId'] = categoria.id;
    delete formValue.categoria;
    delete formValue.unidadeSelect;
    delete formValue.fornecedorSelect;
    delete formValue.descricao;
    delete formValue.tipoAnexo;

    const rawData: any = this.form.getRawValue();
    const { numeroDocumento, tipoParcela, quantidadeParcela, dataVencimento, valorTotalDespesa, despesaParcela, baixaManual } = rawData;
    formValue['numeroDocumento'] = numeroDocumento;
    formValue['tipoParcela'] = tipoParcela;
    formValue['quantidadeParcela'] = quantidadeParcela;
    formValue['dataVencimento'] = dataVencimento;
    formValue['valorTotalDespesa'] = valorTotalDespesa;
    formValue['despesaParcela'] = despesaParcela;
    formValue['baixaManual'] = baixaManual;

    const anexos: any[] = this.anexoSource.data;

    if (!anexos || anexos.length === 0) {
      this.animationsService.showErrorSnackBar('Adicione o documento');
      return;
    }

    const documentos = anexos.filter(elem => elem.id === 0);
    const data =  { ...formValue, documentos };

    // Make request
    this.contasPagarService.cadastrar(data).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        const { id } = val;
        this.id = id;
        this.form.get('id').setValue(id);

        this.loadData();
        this.getAnexo();
      }
    })
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

  gerar(): void {
    const { tipoDespesa, numeroDocumento, tipoParcela, despesaDARF, despesaGPS } = this.form.getRawValue();
    let { quantidadeParcela } = this.form.value;

    let soma: number = 0;
    let dataCalc: Date | string = null;

    // BOLETO OU TRANSFERENCIA
    if(tipoDespesa === 1 || tipoDespesa === 2) {
      const { valorTotalDespesa, dataVencimento } = this.form.value;
      if (!valorTotalDespesa || !numeroDocumento || !tipoParcela || !dataVencimento) {
        this.animationsService.showErrorSnackBar('Preencha todos os campos necessários');
        return;
      }
      soma = valorTotalDespesa;
      dataCalc = dataVencimento;
    }
    // DARF
    else if (tipoDespesa === 3) {
      const { valorTotal, dataVencimento } = despesaDARF;
      if (!valorTotal || !numeroDocumento || !tipoParcela || !dataVencimento) {
        this.animationsService.showErrorSnackBar('Preencha todos os campos necessários');
        return;
      }
      soma = valorTotal;
      dataCalc = dataVencimento;
    }
    // GPS
    else if (tipoDespesa === 4) {
      const { valorTotalRecolher, dataPagamento } = despesaGPS;
      if (!valorTotalRecolher || !numeroDocumento || !tipoParcela || !dataPagamento) {
        this.animationsService.showErrorSnackBar('Preencha todos os campos necessários');
        return;
      }
      soma = valorTotalRecolher;
      dataCalc = dataPagamento;
    } else {
      return;
    }

    if (!quantidadeParcela) {
      this.form.get('quantidadeParcela').setValue(1);
      quantidadeParcela = 1;
    }

    if (quantidadeParcela > soma) {
      this.animationsService.showErrorSnackBar('Quantidade de tipoParcela inválida');
      return;
    }

    this.despesaParcela.clear();
    const valorParcela = soma / quantidadeParcela;
    const date: moment.Moment = moment(dataCalc);

    for(let i = 0; i < quantidadeParcela; i++) {
      this.despesaParcela.push(
        this.fb.group({
          despesaId: [this.id],
          id: [0],
          dataVencimento: [date.format()],
          valorParcela: [valorParcela, [Validators.required]],
          codigoBarras: [null],
          tipoPagamento: [null, [Validators.required]],
          statusPagamento: [null],
          lancamentoManual: [false],
        })
      );
      date.add(1, 'month');
    }

    this.dataSource.next(null);
    this.dataSource.next(this.despesaParcela);
  }

  removeAnexo(id, index: number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      const dataOriginal = this.anexoSource.data;
      let data = this.anexoSource.data;
      data.splice(index, 1);
      this.anexoSource.data = data;

      if(id === 0) return;
  
      this.anexoService.deletarAnexo(id).subscribe(val => {
        if (!val) this.anexoSource.data = dataOriginal;
      })
    })
  }

  excluirParcela(despesaParcela: FormArray | AbstractControl, index: number): void {
    const dataOriginal: any[] = despesaParcela.value;
    const item = dataOriginal[index];
    if(item?.id === 0) {
      let formData = despesaParcela.value;
      let data = despesaParcela.value;
      data.splice(index, 1);
      formData = data;
      return;
    };
    const isMobileResolution = window.innerWidth < 768 ? true : false;
    const dialogRef = this.dialog.open(CancelarPagamentoComponent, {
      width: isMobileResolution ? '98vw' : '30vw',
      data: { id: item.id  },
      autoFocus: false
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.remove) {
        this.contasPagarService.deletarParcela(result.data).subscribe(val => {
          if (!val) this.dataSource.next(despesaParcela);
          else {
            this.despesaParcela.at(index).get('dataVencimento').disable({emitEvent: false});
            this.despesaParcela.at(index).get('valorParcela').disable({emitEvent: false});
            this.despesaParcela.at(index).get('codigoBarras').disable({emitEvent: false});
            this.despesaParcela.at(index).get('tipoPagamento').disable({emitEvent: false});
            this.despesaParcela.at(index).get('lancamentoManual').disable({emitEvent: false});
            this.despesaParcela.at(index).get('statusPagamento').setValue(3);
          }
        })
      }
    });
  }

  habilitarDelPagamento(despesaParcela: FormArray | AbstractControl, index: number): number {
    let formData = despesaParcela.value;
    const item = formData[index];
    return item?.statusPagamento;
  }

  loadFile(event): void {
    const { descricao, tipoAnexo } = this.form.value;

    if (!descricao || !tipoAnexo) {
      this.animationService.showErrorSnackBar('Insira uma descrição e tipo do anexo');
      return;
    }

    let reader = new FileReader();
    // Select file
    let file = event.target.files[0];
    // Render file
    reader.readAsDataURL(file);

    reader.onloadend = () => {
      const result: string = reader.result as string;

      const extensao = result.split(';base64,')[0].replace('data:', '');
      const arquivo = result.split(';base64,')[1];
      const arquivoString = file.name;

      const data = this.anexoSource.data;
      data.push({
        id: 0,
        despesaId: this.id,
        arquivo,
        descricao,
        extensao,
        tipoAnexo,
        arquivoString,
        dataAnexo: new Date()
      });

      this.anexoSource.data = data;
    };
  }

  download(id, arquivoString, extensao): void {
    this.anexoService.download(id, arquivoString, extensao);
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
