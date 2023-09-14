import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { DadosEditadosComponent } from '../pesquisar/resultados/editar-resultados/dados-editados/dados-editados.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { TelMask, CelMask } from 'src/app/utils/mask/mask';
import { OutboundService } from 'src/app/services/Atendimento/OutboundService.service';
import { HistoricoTentativasComponent } from './historico-tentativas/historico-tentativas.component';
import { Observable } from 'rxjs';
import { UsuarioService } from 'src/app/services/portal-adm/usuario.service';
import { AuthService } from 'src/app/security/auth.service';

@Component({
  selector: 'app-outbound',
  templateUrl: './outbound.component.html',
  styleUrls: ['./outbound.component.scss']
})

export class OutboundComponent implements OnInit {
  formTentativa: FormGroup;
  formDadosAtendimento: FormGroup;
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = true;
  maskTelefoneFixoPrincipal = TelMask;
  maskCelular = TelMask;
  agendamentoSim: number = 0;
  motivoAgendamentoSim: number = 0;
  usuariosUnidade: Observable<any[]>;
  idUnidade: 0;
  nomeUnidade: string;
  showUsuario: boolean;
  tentativaNumero: number;
  horariodoContato: string;
  idUsuarioLogado = 0;
  usuarioDefault = null;
  scoreInicial = 0;
  scoreAplicado = 0;
  verificacaoform: any;
  acionarProximo: boolean;
  acionarSair: boolean;
  historicoTentativa: any;

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private routerActive: ActivatedRoute,
    private formService: FormService,
    private dialog: MatDialog,
    private outboundService: OutboundService,
    private router: Router,
    private usuarioService: UsuarioService,
    private authService: AuthService,
  ) {
    // Get id
    const id = this.routerActive.snapshot.paramMap.get('id');
    this.id = id ? parseInt(id) : 0;
  }

  ngOnInit(): void {

    this.buildFormDadosAtendimento();
    this.buildFormTentativa();

    const token = this.authService.getToken();
    if (token != null) {
      this.nomeUnidade = token.user.unidade.nome;
      this.idUnidade = token.user.unidade.id;
      this.idUsuarioLogado = token.user.id;
    }

    Promise.all([this.getFiltarUsuarioUnidade()])
      .then(() => {
        this.isLoadingResults = false;
        this.loadData();
      })
      .catch(() => this.isLoadingResults = false);
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildFormDadosAtendimento(): void {
    this.formDadosAtendimento = this.fb.group({
      canaldeAtendimento: [null, [Validators.required]],
      celular: [null, [Validators.required]],
      comonosConheceu: [null, [Validators.required]],
      cursodeInteresse: [null, [Validators.required]],
      dataeHoradoAtendimento: [null, [Validators.required]],
      email: [null, [Validators.required]],
      id: [0],
      motivodeInteressenoCurso: [null, [Validators.required]],
      nomedoCliente: [null, [Validators.required]],
      nomeUsuarioCadastroAtendimento: [null, [Validators.required]],
      periodo: [null, [Validators.required]],
      score: [null, [Validators.required]],
      telefoneFixo: [null, [Validators.required]],
      usuarioCadastro: [null, [Validators.required]],
      usuarioLogado: [null, [Validators.required]],
      agendamentodaMatricula: [null, [Validators.required]],
      motivodoNaoAgendamento: [null, [Validators.required]],
      diadoAgendamento: [null, [Validators.required]],
      horadoAgendamento: [null, [Validators.required]],
      status: [null, [Validators.required]],
    })

    // Change mask of all contact numbers
    this.formDadosAtendimento.get('telefoneFixo').valueChanges
      .subscribe(val => this.maskTelefoneFixoPrincipal = (val && val.length > 10) ? CelMask : TelMask);

    this.formDadosAtendimento.get('celular').valueChanges
      .subscribe(val => this.maskCelular = (val && val.length > 10) ? CelMask : TelMask);
  }

  buildFormTentativa(): void {
    this.formTentativa = this.fb.group({
      id: [0],
      atendimentoId: [null, [Validators.required]],
      existeAgendamento: [null, [Validators.required]],
      observacoes: [null, [Validators.required]],
      selecioneSeuLogin: [null, [Validators.required]],
      numeroTentativa: [null, [Validators.required]],
      scoreInicial: [null, [Validators.required]],
      scoreAplicado: [null, [Validators.required]],
      usuarioCadastrado: [null, [Validators.required]],
      usuarioLogado: [null, [Validators.required]],
      diadoAgendamento: [null, [Validators.required]],
      horadoAgendamento: [null, [Validators.required]],
      dataeHoradoContato: [null, [Validators.required]],
      motivodoNaoAgendamento: [null, [Validators.required]],
      agendamentodaMatricula: [null, [Validators.required]],

    })

    var dataAtual = new Date();
    this.formTentativa.get('dataeHoradoContato').setValue(dataAtual.toLocaleString());
  }

  loadData(): void {
    //debugger
    this.isLoadingResults = true;

    this.outboundService.getOutbound(this.idUnidade).subscribe(val => {

      if (!val) {
        return;
      }

      if (val?.status === 'error') {
        return this.error = true;
      }

      const patch: any = {};
      for (let key in val) {
        if (val[key]) patch[key] = val[key];
      }

      this.formDadosAtendimento.patchValue(patch);
      this.isLoadingResults = false;
      this.getFiltrarUsuarioPorId();
      this.getHistoricoTentativas();

    });

    this.formTentativa.get('SelecioneSeuLogin').valueChanges.subscribe(val => {
      // debugger

      const usuarioId = this.usuarioDefault?.length >= 0 ? this.usuarioDefault.find(e => e.nome == val) : null;

      if (usuarioId && usuarioId.id) {
        this.formTentativa.get('usuarioId').setValue(usuarioId.id)
      }
      else {
        this.formTentativa.get('usuarioId').setValue(null);
      }

    });
  }

  getHistoricoTentativas() {
    //debugger

    const id = this.formDadosAtendimento.value.id

    this.outboundService.getHistoricoTentativa(id)
      .subscribe(val => {

        if (val['status'] === 'error') {
          this.error = true;
        }
        else {

          debugger

          if (val.length == 0) {
            this.tentativaNumero = 1;
            this.scoreInicial = 1000,
              this.scoreAplicado = 990
          }
          else {
            this.tentativaNumero = val.length + 1;

            const indice = val.length - 1;
            this.scoreInicial = val[indice].scoreInicial
            this.scoreAplicado = val[indice].scoreAplicado

            this.historicoTentativa = val;
          }
        }
      });
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  voltar(): void {
    this.location.back();
  }

  registrarTentativasOutbound(): void {
    // debugger

    this.formService.validateAllFields(this.formTentativa);
    this.formTentativa.get('atendimentoId').setValue(this.formDadosAtendimento.value.id);
    this.formTentativa.get('numeroTentativa').setValue(this.tentativaNumero);
    this.formTentativa.get('usuarioLogado').setValue(this.idUsuarioLogado);
    this.formTentativa.get('usuarioCadastrado').setValue(this.formTentativa.value.selecioneSeuLogin);
    this.formTentativa.get('scoreAplicado').setValue(this.scoreAplicado);
    this.formTentativa.get('scoreInicial').setValue(this.scoreInicial);
    this.formTentativa.get('agendamentodaMatricula').setValue(this.agendamentoSim);

    var tentativaDados = this.formTentativa.value;

    if (tentativaDados) {
      this.animationsService.showProgressBar(true);
      this.outboundService.cadastrar(tentativaDados).subscribe(val => {
        if (val.length != 0) {
          const message = 'Tentativa Registrada com sucesso';
          if (val['status'] == 'error') {
            return;
          }

          this.animationsService.showProgressBar(false);
          this.animationsService.showSuccessSnackBar(message);

          if (this.acionarProximo) {
            this.formTentativa.reset();
            this.formDadosAtendimento.reset();
            location.reload()
          }

          if (this.acionarSair) {
            this.formTentativa.reset();
            this.goToHome();
          }

          this.verificacaoform = 'DadosCorretos'
        }

      });
    } else {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
    }
  }

  async salvarDataFormDadosAtendimento(): Promise<void> {
    // debugger
    this.formService.validateAllFields(this.formDadosAtendimento);
    if (!this.formDadosAtendimento.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }
  }

  goToHome(): void {
    this.router.navigate(['menu-atendimento/m-a-home']);
  }

  goToNovoAtendimento(): void {
    this.router.navigate(['menu-atendimento/m-a-home/novo-atendimento']);
  }

  goToPesquisar(): void {
    this.router.navigate(['menu-atendimento/m-a-home/pesquisar']);
  }

  goToOutbound(): void {
    this.router.navigate(['menu-atendimento/m-a-home/outbound']);
  }

  goToAgendados(): void {
    this.router.navigate(['menu-atendimento/m-a-home/agendados']);
  }

  goToContatosPrioritarios(): void {
    this.router.navigate(['menu-atendimento/m-a-home/contatos-prioritarios']);
  }

  editarDados(): void {
    const isMobileResolution = window.innerWidth < 768 ? true : false;
    const dialogRefMsg = this.dialog.open(DadosEditadosComponent, {
      width: isMobileResolution ? '98vw' : '50vw',
      data: {},
      autoFocus: true
    });
    dialogRefMsg.afterClosed().subscribe(() => {

    });
  }

  agendamentoSimNao(): void {

    if (this.formTentativa.get('existeAgendamento').value == 1)
      this.agendamentoSim = 1;
    else if (this.formTentativa.get('existeAgendamento').value == 2)
      this.agendamentoSim = 2;
    else
      this.agendamentoSim = 3;
  }

  motivoAgendamentoSimNao(): void {
    // debugger
    if (this.formTentativa.get('motivodoNaoAgendamento').value == 14)
      this.motivoAgendamentoSim = 1;
    else
      this.motivoAgendamentoSim = 2;
  }

  abrirHistoricoCompleto(): void {
    const dadosTentativas = this.historicoTentativa;
    const isMobileResolution = window.innerWidth < 768 ? true : false;
    const dialogRefMsg = this.dialog.open(HistoricoTentativasComponent, {
      width: isMobileResolution ? '98vw' : '90vw',
      data: { dadosTentativas },
      autoFocus: true
    });
    dialogRefMsg.afterClosed().subscribe(() => {

    });
  }

  getFiltarUsuarioUnidade(): void {
    //debugger

    this.usuarioService.getFiltrar({ unidadeId: this.idUnidade, ehAtendimento: true })
      .subscribe(val => {
        if (val['status'] === 'error') {
          this.error = true;
        }
        else {
          const usuario = this.authService.getToken();
          const data = val.filter(e => e?.id !== usuario?.user?.id);
          this.usuariosUnidade = data;
          this.showUsuario = true;
        }
      });
  }

  getFiltrarUsuarioPorId(): void {
    // debugger

    const id = this.formDadosAtendimento.value.usuarioCadastro;

    this.usuarioService.getPorId(id)
      .subscribe(val => {
        if (val['status'] === 'error') {
          this.error = true;
        }
        else {

          const nome = val.userName;
          this.formDadosAtendimento.get('nomeUsuarioCadastroAtendimento').setValue(nome);
        }
      });
  }

  enviarSair(): void {
    // debugger
    this.acionarSair = true;
    this.acionarProximo = false;
    this.registrarTentativasOutbound();

  }

  enviarProximo(): void {
    // debugger

    this.acionarProximo = true;
    this.acionarSair = false;
    this.registrarTentativasOutbound();
  }
}
