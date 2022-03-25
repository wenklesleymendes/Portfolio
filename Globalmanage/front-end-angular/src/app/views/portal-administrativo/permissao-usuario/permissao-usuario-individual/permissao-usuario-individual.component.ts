import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { HourMinuteMask } from 'src/app/utils/mask/mask';
import { PerfilUsuarioService } from 'src/app/services/portal-adm/perfil-usuario.service';
import { Perfis } from 'src/app/utils/variables/perfis';

@Component({
  selector: 'app-permissao-usuario-individual',
  templateUrl: './permissao-usuario-individual.component.html',
  styleUrls: ['./permissao-usuario-individual.component.scss']
})
export class PermissaoUsuarioIndividualComponent implements OnInit {
  form: FormGroup;
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;
  perfis = Perfis;
  hourMinute = HourMinuteMask;
  AllAcess: Array<{ label: string, value: string[] }> = [
    { label: 'aluno', value: ['consultarAluno', 'isentarMultaCancelamento'], },
    { label: 'relatorio', value: ['relatorios'] },
    { label: 'portalAdm', value: ['criacaoUsuario', 'criacaoPerfil', 'configuradorParametros'] },
    { label: 'ticket', value: ['ticketAdministracao', 'ticketPainel'] },
    { label: 'comunicacao', value: ['comunicacao'] },
    { label: 'rh', value: ['cadastroFuncionario', 'controlePonto', 'escalaServico', 'uploadPonto'] },
    { label: 'financeiro', value: ['cadastroFornecedorCliente', 'contasAPagar', 'estoque', 'folhaPagamento'] },
    { label: 'provas', value: ['criarColegioAutorizado', 'criarAgendaProva', 'listaPassageiros', 'historicoViagem', 'historicoProva'] },
    { label: 'gerenciador', value: ['cursoTurma', 'unidade', 'planoPagamento', 'promocoesBolsaConvenio', 'solicitacao', 'conteudoDigital'] },
    { label: 'metasComissoes', value: ['criarMeta', 'metaPainel', 'criarComissoes'] },
    { label: 'aulaOnline', value: ['criarAulaOnline', 'minhasAulas'] },
    { label: 'menuAtendimento', value: ['acessoMenuAtendimento'] }
  ];

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private perfilUsuarioService: PerfilUsuarioService,
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
      perfilSistemaEnum: [null, Validators.required],
      verTodasUnidades: [false],
      isActive: [false],
      aluno: [[]],
      rh: [[]],
      comunicacao: [[]],
      relatorio: [[]],
      portalAdm: [[]],
      ticket: [[]],
      financeiro: [[]],
      provas: [[]],
      gerenciador: [[]],
      metasComissoes: [[]],
      aulaOnline: [[]],
      menuAtendimento: [[]]
    });
  }

  loadData(): void {
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.perfilUsuarioService.getPorId(this.id).subscribe(val => {
        if (!val) return;
        if (val['status'] === 'error') return this.error = true;

        this.form.patchValue(val);

        for (let key in val) {
          // //debugger
          if (val[key] === true) {
            this.AllAcess.forEach(elem => {
              if (elem.value.find(item => item == key)) {
                let formValue = this.form.get(elem.label).value as String[];
                if (!(formValue?.length > 0)) formValue = [];
                formValue.push(key);
                this.form.get(elem.label).setValue(formValue);
              }
            })
          }
        }

        this.isLoadingResults = false;
      })
    }
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  voltar(): void {
    this.location.back();
  }

  findField(array: any[], text: string): boolean {
    if (!array || !text) return false;
    return array.find(elem => elem == text) ? true : false;
  }

  salvarData(): void {
    // //debugger
    // Validating form
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }
    const formValue = this.form.value;

    const {
      id,
      perfilSistemaEnum,
      nivelAcesso,
      isActive,
      verTodasUnidades
    } = this.form.value;

    // Unifying data
    const data = {
      id,
      perfilSistemaEnum,
      nivelAcesso,
      isActive,
      verTodasUnidades,
      cadastrarAluno: this.findField(formValue.aluno, 'cadastrarAluno'),
      consultarAluno: this.findField(formValue.aluno, 'consultarAluno'),
      isentarMultaCancelamento: this.findField(formValue.aluno, 'isentarMultaCancelamento'),
      relatorios: this.findField(formValue.relatorio, 'relatorios'),
      criacaoUsuario: this.findField(formValue.portalAdm, 'criacaoUsuario'),
      criacaoPerfil: this.findField(formValue.portalAdm, 'criacaoPerfil'),
      configuradorParametros: this.findField(formValue.portalAdm, 'configuradorParametros'),
      cadastroFuncionario: this.findField(formValue.rh, 'cadastroFuncionario'),
      controlePonto: this.findField(formValue.rh, 'controlePonto'),
      escalaServico: this.findField(formValue.rh, 'escalaServico'),
      uploadPonto: this.findField(formValue.rh, 'uploadPonto'),
      ticketAdministracao: this.findField(formValue.ticket, 'ticketAdministracao'),
      ticketPainel: this.findField(formValue.ticket, 'ticketPainel'),
      comunicacao: this.findField(formValue.comunicacao, 'comunicacao'),
      cadastroFornecedorCliente: this.findField(formValue.financeiro, 'cadastroFornecedorCliente'),
      contasAPagar: this.findField(formValue.financeiro, 'contasAPagar'),
      estoque: this.findField(formValue.financeiro, 'estoque'),
      folhaPagamento: this.findField(formValue.financeiro, 'folhaPagamento'),
      criarAgendaProva: this.findField(formValue.provas, 'criarAgendaProva'),
      listaPassageiros: this.findField(formValue.provas, 'listaPassageiros'),
      historicoViagem: this.findField(formValue.provas, 'historicoViagem'),
      historicoProva: this.findField(formValue.provas, 'historicoProva'),
      criarColegioAutorizado: this.findField(formValue.provas, 'criarColegioAutorizado'),
      cursoTurma: this.findField(formValue.gerenciador, 'cursoTurma'),
      unidade: this.findField(formValue.gerenciador, 'unidade'),
      planoPagamento: this.findField(formValue.gerenciador, 'planoPagamento'),
      conteudoDigital: this.findField(formValue.gerenciador, 'conteudoDigital'),
      promocoesBolsaConvenio: this.findField(formValue.gerenciador, 'promocoesBolsaConvenio'),
      solicitacao: this.findField(formValue.gerenciador, 'solicitacao'),
      criarMeta: this.findField(formValue.metasComissoes, 'criarMeta'),
      metaPainel: this.findField(formValue.metasComissoes, 'metaPainel'),
      criarComissoes: this.findField(formValue.metasComissoes, 'criarComissoes'),
      criarAulaOnline: this.findField(formValue.aulaOnline, 'criarAulaOnline'),
      minhasAulas: this.findField(formValue.aulaOnline, 'minhasAulas'),
      acessoMenuAtendimento: this.findField(formValue.menuAtendimento, 'acessoMenuAtendimento')
    };

    // Make request
    this.perfilUsuarioService.cadastrar(data).subscribe(val => {
      if (!val && !val['status']) {
        this.animationsService.showErrorSnackBar('Perfil de usuário já cadastrado');
      }
      else {
        if (val && !val['status']) {
          this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
          const { id } = val;
          this.id = id;
          this.form.get('id').setValue(id);
        }
      }
    })
  }
}
