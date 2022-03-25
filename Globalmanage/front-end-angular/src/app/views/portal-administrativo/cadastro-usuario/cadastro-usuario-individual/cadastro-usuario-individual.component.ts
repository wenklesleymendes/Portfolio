import { Component, OnInit, OnDestroy } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { HourMinuteMask, CPFMask } from 'src/app/utils/mask/mask';
import { PerfilUsuarioService } from 'src/app/services/portal-adm/perfil-usuario.service';
import { CompareFields } from 'src/app/utils/form-validation/compare-fields.validation';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { FuncionarioService } from 'src/app/services/rh/funcionario.service';
import { DatePtBrPipe } from 'src/app/utils/pipes/date-pt-br.pipe';
import { UsuarioService } from 'src/app/services/portal-adm/usuario.service';
import { BehaviorSubject } from 'rxjs';
import { validarCPF } from 'src/app/utils/form-validation/cpf.validation';
import { Perfis } from 'src/app/utils/variables/perfis';

@Component({
  selector: 'app-cadastro-usuario-individual',
  templateUrl: './cadastro-usuario-individual.component.html',
  styleUrls: ['./cadastro-usuario-individual.component.scss']
})
export class CadastroUsuarioIndividualComponent implements OnInit, OnDestroy {
  form: FormGroup;
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;  
  hourMinute = HourMinuteMask;
  onlyNumbers = /^-?(0|[1-9]\d*)?$/;
  cpfMask = CPFMask;
  pwdhide: boolean = true;
  confirmPwdhide: boolean = true;
  perfis = null;
  perfilEnum = Perfis;
  unidades: any[] = null;
  departamentos: any[] = [];
  infoFilter: any[] = [];
  unidadeSubject: BehaviorSubject<any> = null;

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private datePtBrPipe: DatePtBrPipe,
    private animationsService: AnimationsService,
    private usuarioService: UsuarioService,
    private perfilUsuarioService: PerfilUsuarioService,
    private funcionarioService: FuncionarioService,
    private routerActive: ActivatedRoute,
    private unidadeService: UnidadeService,
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
    
    this.isLoadingResults = true;
    Promise.all([
      this.getunidades(),
      this.getPerfis()
    ])
    .then(() => this.loadData())
    .catch(() => this.isLoadingResults = false);


    this.unidadeSubject = new BehaviorSubject(null);
    this.unidadeSubject.subscribe(val => this.ajustarDepartamento());
  }

  ngOnDestroy(): void {
    this.unidadeSubject.complete();
    this.unidadeSubject.unsubscribe();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      cpf: [null, [Validators.required, validarCPF]],
      funcionarioId: [null],
      perfilUsuarioId: [null, [Validators.required]],
      unidadeId: [null, [Validators.required]],
      departamentoId: [{ disabled: true, value: null}, [Validators.required]],
      password: [null, [Validators.required]],
      confirmPassword: [null, [Validators.required]],
      isActive : [false, [Validators.required]],
    })

    this.form.setValidators([CompareFields('password', 'confirmPassword')]);
    this.form.get('unidadeId').valueChanges.subscribe(val => {
      if (!val || val == '') {
        this.formService.disableField(this.form.get('departamentoId'));
        return;
      }
      this.ajustarDepartamento();
    });
  }

  getFuncionario(): void {
    this.funcionarioService.getPorCpf(this.form.get('cpf').value).subscribe(val => {
      const { id, nome, rg, dataNascimento, isActive } = val;
      this.form.get('funcionarioId').setValue(id);
      this.infoFilter = [
        { label: 'Nome',  value: nome },
        { label: 'RG',    value: rg },
        { label: 'Data de Nascimento', value: this.datePtBrPipe.transform(dataNascimento) },
        { label: 'Funcionário Ativado', value: isActive ? 'SIM' : 'NÃO' }
      ];
    })
  }

  ajustarDepartamento(): void {
    const id = this.form.get('unidadeId').value;
    if (!id || this.unidades?.length <= 0) return;
    let departamentos = this.unidades.find(elem => elem.id == id);
    this.departamentos = departamentos?.centroCusto ? departamentos.centroCusto : [];
    this.formService.enableField(this.form.get('departamentoId'));
  }

  getunidades(): Promise<any> {
    return new Promise((res, rej) => {
      this.unidadeService.getAll()
        .subscribe(val => {
          if (val['status'] === 'error') {
            this.error = true;
            rej();
          }
          else {
            this.unidades = val;
            this.unidadeSubject.next(null);
            res();
          }
        });
    });
  }

  getPerfis(): Promise<any> {
    return new Promise((res, rej) => {
      this.perfilUsuarioService.getAllAtivos()
        .subscribe(val => {
          if (val['status'] === 'error') {
            this.error = true;
            rej();
          }
          else {
            this.perfis = val;
            res();
          }
        });
    });
  }

  loadData(): void {
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.usuarioService.getPorId(this.id)
      // .pipe(delay(1000))
      .subscribe(val => {
        if (!val) return;
        if (val['status'] === 'error') return this.error = true;
        const { perfilUsuario, unidade, departamento, funcionario } = val;
        this.form.patchValue(val);
        this.form.get('unidadeId').setValue(unidade.id);
        this.form.get('funcionarioId').setValue(funcionario.id);
        this.form.get('cpf').setValue(funcionario.cpf);
        this.form.get('perfilUsuarioId').setValue(perfilUsuario.perfilSistemaEnum);
        this.form.get('password').setValue(null);
        this.form.get('confirmPassword').setValue(null);
        this.form.get('departamentoId').setValue(departamento?.id);
        
        
        // this.formService.notMandatoryFields(this.form.get('password'));
        // this.formService.notMandatoryFields(this.form.get('confirmPassword'));
        this.unidadeSubject.next(null);
        this.isLoadingResults = false;
        
        this.getFuncionario();
      })
    } else {
      this.isLoadingResults = false;
    }

    this.isLoadingResults = false;
  }

  labelDescricao(perfilSistemaEnum: number): string {
    if(!perfilSistemaEnum) return ' - ';
    const label = this.perfilEnum.find(elem => elem.value === perfilSistemaEnum);
    return label ? label.label : ' - ';
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
    delete formValue.confirmPassword;
    // Make request
    this.usuarioService.cadastrar(formValue).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        const { id } = val?.user;
        this.id = id;
        this.form.get('id').setValue(id);
      }
    })
  }


}
