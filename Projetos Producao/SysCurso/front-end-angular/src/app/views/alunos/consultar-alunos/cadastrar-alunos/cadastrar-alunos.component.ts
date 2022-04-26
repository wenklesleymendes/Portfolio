import { DirectiveModule } from 'src/app/utils/directive/directive.module';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { TelMask, CelMask, CPFMask, CepMask } from 'src/app/utils/mask/mask';
import { Estados } from 'src/app/utils/variables/locations';
import { LocationService } from 'src/app/services/location.service';
import { debounceTime, distinctUntilChanged, pairwise } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { AlunoService } from 'src/app/services/aluno/aluno.service';
import * as moment from 'moment';
import { validarCPF } from 'src/app/utils/form-validation/cpf.validation';
import { NacionalidadeService } from 'src/app/services/aluno/nacionalidade.service';
import { DeleteService } from 'src/app/services/delete.service';
import { NaturalidadeService } from 'src/app/services/aluno/naturalidade.service';
import { AlunoStoreActions, AlunoStoreState } from 'src/app/_store/aluno-store';
import { Store } from '@ngrx/store';
import { MatriculaAlunoService } from 'src/app/services/aluno/matricula-aluno.service';
import { ConfirmarDadosComponent } from './confirmar-dados/confirmar-dados.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-cadastrar-alunos',
  templateUrl: './cadastrar-alunos.component.html',
  styleUrls: ['./cadastrar-alunos.component.scss']
})
export class CadastrarAlunosComponent implements OnInit {
  imgProfileSrc: string = null;
  form: FormGroup;
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = true;
  onlyNumbers = /^-?(0|[1-9]\d*)?$/;
  cpfMask = CPFMask;
  maskTelefoneFixoPrincipal = TelMask;
  maskCelular = TelMask;
  maskCep = CepMask;
  bancos: any = null;
  estados: string[] = Estados;
  filterUnidades: Observable<any[]>;
  unidadesDefault = null;
  today: Date = new Date();
  file: any;
  updateImg: boolean = false;
  filterNacionalidade: Observable<any[]> = null;
  nacionalidadeDefault = null;
  filterNaturalidade: Observable<any[]> = null;
  naturalidadeDefault = null;
  alunoJaCadastrado: boolean = false;
  CPFAlunoLocalStorage: string;

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private alunoService: AlunoService,
    private deleteService: DeleteService,
    private nacionalidadeService: NacionalidadeService,
    private naturalidadeService: NaturalidadeService,
    private unidadeService: UnidadeService,
    private locationService: LocationService,
    private routerActive: ActivatedRoute,
    private route: Router,
    private formService: FormService,
    private store: Store<AlunoStoreState.Aluno>,
    private matriculaAlunoService: MatriculaAlunoService,
    private dialog: MatDialog,
  ) {
    // Get id
    const id = this.routerActive.snapshot.paramMap.get('id');
    this.id = id ? parseInt(id) : 0;
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    var CliqueVerMatriculaAlunoLocalStorage = window.localStorage.getItem('CliqueVerMatriculaAlunoLocalStorage');
    if (CliqueVerMatriculaAlunoLocalStorage == '1')
    {
      setTimeout(() => {
        this.CPFAlunoLocalStorage = window.localStorage.getItem('CPFAlunoLocalStorage');
        if (this.CPFAlunoLocalStorage != null && this.CPFAlunoLocalStorage != undefined && this.CPFAlunoLocalStorage != '')
        {
          this.form.get('cpf').setValue(this.CPFAlunoLocalStorage);
          this.pesquisandoCPFAluno(this.CPFAlunoLocalStorage);

          var UnidadeNomeMatriculaAlunoLocalStorage = window.localStorage.getItem('UnidadeNomeMatriculaAlunoLocalStorage');
          this.form.get('unidadeSelect').setValue(UnidadeNomeMatriculaAlunoLocalStorage);

          var UnidadeIDMatriculaAlunoLocalStorage = window.localStorage.getItem('UnidadeIDMatriculaAlunoLocalStorage');
          this.form.get('unidadeId').setValue(UnidadeIDMatriculaAlunoLocalStorage);
        }
      }, 1000);
    }

    this.buildForm();

    Promise.all([
      this.getunidades(),
      this.getNacionalidade(),
      this.getNaturalidade(),
      this.getImg(),
    ])
    .then(() => {
      this.isLoadingResults = false;
      this.loadData();
    })
    .catch(() => this.isLoadingResults = false);
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      unidadeId: [null, [Validators.required]],
      nome: [null, [Validators.required]],
      dataNascimento: [null, [Validators.required]],
      nomeMae: [null, [Validators.required]],
      sexo: [null, [Validators.required]],
      cpf: [null, [Validators.required, validarCPF]],
      rg: [null, [Validators.required]],
      orgaoExpedicao: [null, [Validators.required]],
      estadoCivil: [null, [Validators.required]],
      nacionalidade: [null, [Validators.required]],
      naturalidade: [null, [Validators.required]],
      tituloEleitoral: [null],
      zona: [null],
      secao: [null],
      nomeResponsavel: [null, [Validators.required]],
      rgResponsavel: [null, [Validators.required]],
      cpfResponsavel: [null, [Validators.required]],
      contato: this.fb.group({
        id: [0],
        telefoneFixo: [null],
        email: [null, [Validators.required]],
        celular: [null, [Validators.required]],
        faceBook: [null, [Validators.required]],
        instagram: [null, [Validators.required]],
        recebeSMS: [true],
        receberWhatsApp: [true],
        receberEmail: [true],
        receberFacebook: [true],
        receberInstagram: [true],
        comoConheceuEnum: [null, [Validators.required]],
      }),
      endereco: this.fb.group({
        id: [0],
        cep: [null, [Validators.required]],
        rua: [null, [Validators.required]],
        numero: [null, [Validators.required]],
        complemento: [null],
        bairro: [null, [Validators.required]],
        cidade: [null, [Validators.required]],
        estado: [null, [Validators.required]]
      }),

      unidadeSelect: [null, [Validators.required]],
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

    this.filterUnidades = this.form.get('unidadeSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
      );

    this.form.get('unidadeSelect').valueChanges.subscribe(val => {
      const unidadeId = this.unidadesDefault?.length >= 0 ? this.unidadesDefault.find(elem => elem.nome == val) : null;
      if (unidadeId && unidadeId.id) this.form.get('unidadeId').setValue(unidadeId.id);
      else this.form.get('unidadeId').setValue(null);
    })

    this.filterNacionalidade = this.form.get('nacionalidade').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterNacionalidade(elem) : this.nacionalidadeDefault.slice())
      );

    this.form.get('nacionalidade').valueChanges
    .subscribe(pais => {
      if (pais != 'brasileiro(a)') {
        this.form.get('naturalidade').reset();
        this.form.get('naturalidade').disable();
        this.formService.notMandatoryFields(this.form.get('naturalidade'));
      }
      else {
        this.formService.mandatoryFields(this.form.get('naturalidade'));
        this.form.get('naturalidade').enable();
    }
    });

    this.filterNaturalidade = this.form.get('naturalidade').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterNaturalidade(elem) : this.naturalidadeDefault.slice())
      );

    this.form.get('cpf').valueChanges.pipe(debounceTime(250), distinctUntilChanged(),pairwise()).subscribe(([oldValue, cpf]) => {
      if(this.form.get('cpf').hasError('cpfInvalido')) return;
      this.alunoJaCadastrado = false;
      this.alunoService.getPorCpf(cpf).subscribe(res => {
        if(res?.id) {

          const { unidade, nacionalidade, naturalidade } = res;

          const patch: any = {};
          for(let key in res) {
            if (res[key]) patch[key] = res[key];
          }

          const token = JSON.parse(window.localStorage.getItem('accessToken'));
          const unidadeUsuarioLogado = token?.user?.unidade;

          this.form.patchValue(patch);

          if(!token?.user.perfilUsuario.verTodasUnidades)
          {
            this.form.get('unidadeSelect').setValue(unidadeUsuarioLogado?.nome ? unidadeUsuarioLogado.nome : null);
            this.form.get('unidadeId').setValue(unidadeUsuarioLogado?.id ? unidadeUsuarioLogado.id : null);
          }

          this.form.get('nacionalidade').setValue(nacionalidade?.descricao ? nacionalidade.descricao : null);
          this.form.get('naturalidade').setValue(naturalidade?.descricao ? naturalidade.descricao : null);
          this.alunoJaCadastrado = true;

        }
      })
    })
    if(this.id === 0) this.form.get('cpf').setValue(null);

    this.form.get('contato').get('receberFacebook').valueChanges.subscribe(val => {
      if(val) {
        this.formService.mandatoryFields(this.form.get('contato').get('faceBook'));
        this.formService.enableField(this.form.get('contato').get('faceBook'));
      }
      else {
        this.formService.notMandatoryFields(this.form.get('contato').get('faceBook'));
        this.formService.disableField(this.form.get('contato').get('faceBook'));
      }
    });

    this.form.get('contato').get('receberInstagram').valueChanges.subscribe(val => {
      if(val) {
        this.formService.mandatoryFields(this.form.get('contato').get('instagram'));
        this.formService.enableField(this.form.get('contato').get('instagram'));
      }
      else {
        this.formService.notMandatoryFields(this.form.get('contato').get('instagram'));
        this.formService.disableField(this.form.get('contato').get('instagram'));
      }
    });

    this.form.get('dataNascimento').valueChanges.subscribe((val: moment.Moment) => {
      const today: moment.Moment = moment();
      const diff = today.diff(val, 'years');

      if (diff >= 18) {
        this.formService.disableField(this.form.get('nomeResponsavel'));
        this.formService.disableField(this.form.get('rgResponsavel'));
        this.formService.disableField(this.form.get('cpfResponsavel'));
      } else {
        this.formService.enableField(this.form.get('nomeResponsavel'));
        this.formService.enableField(this.form.get('rgResponsavel'));
        this.formService.enableField(this.form.get('cpfResponsavel'));
      }
    })
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
          //this.unidadesDefault?.length === 1 ? this.form.get('unidadeSelect').setValue(this.unidadesDefault[0].nome) : this.form.get('unidadeSelect').setValue('');
          res(val);
        });
    });
  }

  getNacionalidade(): Promise<any> {
    return new Promise((res, rej) => {
      this.nacionalidadeDefault = [];
      this.form.get('nacionalidade').setValue('');

      this.nacionalidadeService.getAll()
        .subscribe(val => {
          if (val['status'] === 'error') {
            this.error = true;
            rej();
          }
          else this.nacionalidadeDefault = val;
          this.form.get('nacionalidade').setValue('');
          res(val);
        });

    });
  }

  getNaturalidade(): Promise<any> {
    return new Promise((res, rej) => {
      this.nacionalidadeDefault = [];
      this.form.get('naturalidade').setValue('');

      this.naturalidadeService.getAll()
        .subscribe(val => {
          if (val['status'] === 'error') {
            this.error = true;
            rej();
          }
          else this.naturalidadeDefault = val;
          this.form.get('naturalidade').setValue('');
          res(val);
        });

    });
  }

  getImg(): Promise<any> {
    return new Promise((res, rej) => {
      if(this.id === 0) return rej();

      this.alunoService.getImg(this.id)
        .subscribe(val => {
          if (val?.status === 'error') {
            this.error = true;
            return rej();
          }
          this.imgProfileSrc = val;
          return res(val);
        });
    });
  }

  loadData(): void {
    if (this.id != 0) {
      
      this.isLoadingResults = true;
      
      this.alunoService.getPorId(this.id).subscribe(val => {
        if (!val) return;
        if (val?.status === 'error') return this.error = true;

        const { unidade, nacionalidade, naturalidade } = val;

        const patch: any = {};
        for(let key in val) {
          if (val[key]) patch[key] = val[key];
        }

        this.form.patchValue(patch);
        this.form.get('unidadeSelect').setValue(unidade?.nome ? unidade.nome : null);
        this.form.get('unidadeId').setValue(unidade?.id ? unidade.id : null);
        this.form.get('nacionalidade').setValue(nacionalidade?.descricao ? nacionalidade.descricao : null);
        this.form.get('naturalidade').setValue(naturalidade?.descricao ? naturalidade.descricao : null);

        setTimeout(() => {
          this.form.get('contato').get('email').setValue(val?.contato?.email);
        }, 100);

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

  _filterUnidades(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  _filterNacionalidade(value): any[] {
    if(!value) return;
    let filterValue: string = null;
    if(value?.descricao) filterValue = value.descricao.toLowerCase();
    else filterValue = value.toLowerCase();
    return this.nacionalidadeDefault.filter(elem => elem.descricao.toLowerCase().indexOf(filterValue) === 0);
  }

  _filterNaturalidade(value): any[] {
    if(!value) return;
    let filterValue: string = null;
    if(value?.descricao) filterValue = value.descricao.toLowerCase();
    else filterValue = value.toLowerCase();
    return this.naturalidadeDefault.filter(elem => elem.descricao.toLowerCase().indexOf(filterValue) === 0);
  }

  addToSate(data: any): void {
    this.store.dispatch(AlunoStoreActions.updateCadastroAluno({ payload: data }));
    if(!this.updateImg) return;
    const extensao = this.imgProfileSrc.split(';base64,')[0].replace('data:', '');
    const foto = this.imgProfileSrc.split(';base64,')[1];
    this.store.dispatch(AlunoStoreActions.updateImgAluno({ payload: { foto, extensao } }));
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  voltar(): void {
    this.location.back();
  }

  async salvarData(): Promise<void> {

    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    // Validating form
    const formValue: any = this.form.value;

    // Validating nacionalidade
    const nacionalidade = this.nacionalidadeDefault.length > 0 ? this.nacionalidadeDefault.find(elem => elem.descricao.toLowerCase() === formValue.nacionalidade.toLowerCase().trim()) : false;
    if (!nacionalidade) {
      this.animationsService.showErrorSnackBar('Adicione a nacionalidade');
      return;
    }

    // Validating naturalidade
    if (this.form.get('naturalidade').enabled){
      const naturalidade = this.naturalidadeDefault.length > 0 ? this.naturalidadeDefault.find(elem => elem.descricao.toLowerCase() === formValue.naturalidade.toLowerCase().trim()) : false;
      if (!naturalidade) {
        this.animationsService.showErrorSnackBar('Adicione a naturalidade');
        return;
      }
      formValue['naturalidadeId'] = naturalidade.id;
    }
    else{
      formValue['naturalidadeId'] = null;
    }

    formValue['nacionalidadeId'] = nacionalidade.id;
    delete formValue.nacionalidade;
    delete formValue.naturalidade;
    delete formValue.unidadeSelect;

    const jaCadastrado = await this.jaCadastrado(this.alunoJaCadastrado);
    if(jaCadastrado) {
      if(this.alunoJaCadastrado === true) {
        this.animationsService.showErrorSnackBar('Aluno já cadastrado');
        return;
      }
    }

   if(this.id === 0) {
      this.addToSate(formValue);
      this.route.navigate(['/alunos/matricula-aluno']);
    } else {
      this.alunoService.cadastrar(formValue).subscribe(val => {
        if (val && !val['status']) {
          this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
          const { id } = val;
          this.id = id;
          this.form.get('id').setValue(id);
        }
      })
    }
  }

  jaCadastrado(alunoJaCadastrado): Promise<any> {
    return new Promise((res,rej) => {
      if(!alunoJaCadastrado) res(false);
      const token = JSON.parse(window.localStorage.getItem('accessToken'));
      const usuarioLogadoId = token?.user.id;
      const id = this.form.get('id').value;
      this.matriculaAlunoService.jaExistenteMatricula(id, usuarioLogadoId).subscribe(val => res(val));
    });
  }

  // loadImgProfile(event): void {
  //   let reader = new FileReader();
  //   // Select file
  //   let file = event.target.files[0];
  //   this.file = event.target.files[0];
  //   // Render file
  //   reader.readAsDataURL(file);

  //   reader.onloadend = e => {
  //     this.imgProfileSrc = reader.result as string;
  //     this.updateImg = true;
  //   };
  // }
  abrirConfirmarDados(): void{
    const isMobileResolution = window.innerWidth < 768 ? true : false;

    var dadosAlunoForm = this.form.value;

    const dialogRef = this.dialog.open(ConfirmarDadosComponent, {
      width: isMobileResolution ? '95vw' : '90vw',
      autoFocus: false,
      data: { dadosAlunoForm }
    });
    dialogRef.afterClosed().subscribe(result => {
    });
  }

  onBlurMethod(): void {
    var email = this.form.get('contato').get('email').value;
    var celular = this.form.get('contato').get('celular').value;

    if(email.trim() && celular.trim())
    {
      this.abrirConfirmarDados();
    }
  }

  pesquisandoCPFAluno(cpf): void {
    if(this.form.get('cpf').hasError('cpfInvalido')) return;
      this.alunoJaCadastrado = false;
      this.alunoService.getPorCpf(cpf).subscribe(res => {
        if(res?.id) {

          const { unidade, nacionalidade, naturalidade } = res;

          const patch: any = {};
          for(let key in res) {
            if (res[key]) patch[key] = res[key];
          }

          const token = JSON.parse(window.localStorage.getItem('accessToken'));
          const unidadeUsuarioLogado = token?.user?.unidade;

          this.form.patchValue(patch);

          if(!token?.user.perfilUsuario.verTodasUnidades)
          {
            this.form.get('unidadeSelect').setValue(unidadeUsuarioLogado?.nome ? unidadeUsuarioLogado.nome : null);
            this.form.get('unidadeId').setValue(unidadeUsuarioLogado?.id ? unidadeUsuarioLogado.id : null);
          }

          this.form.get('nacionalidade').setValue(nacionalidade?.descricao ? nacionalidade.descricao : null);
          this.form.get('naturalidade').setValue(naturalidade?.descricao ? naturalidade.descricao : null);
          this.alunoJaCadastrado = true;

        }
    })
  }
}
