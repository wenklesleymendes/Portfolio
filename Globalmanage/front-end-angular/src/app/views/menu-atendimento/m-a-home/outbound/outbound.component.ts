import { Component, Input, OnInit, ViewChild } from '@angular/core';
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
import { FormClienteComponent } from '../componentes-atendimento/form-cliente/form-cliente.component';

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
  outboundNumero: number;
  horariodoContato: string;
  idUsuarioLogado = 0;
  usuarioDefault = null;
  scoreInicial = 0;
  scoreAplicado = 0;
  verificacaoform: any;
  acionarProximo: boolean;
  acionarSair: boolean;
  historicoTentativa: any;
  outbound: any;
  minDate: Date;
  //valor: any;

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

  @ViewChild(FormClienteComponent, { static: false })
  formAtendimento: FormClienteComponent

  ngOnInit(): void {

    const token = this.authService.getToken();

    if (token != null) {

      this.nomeUnidade = token.user.unidade.nome;
      this.idUnidade = token.user.unidade.id;
      this.idUsuarioLogado = token.user.id;
    }

    this.buildFormTentativa();

    Promise.all([this.getFiltarUsuarioUnidade()])
      .then(() => {

        this.isLoadingResults = false;
        this.loadData();
      })
      .catch(() => this.isLoadingResults = false);

    this.minDate = new Date(Date.now());
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------

  buildFormTentativa(): void {
    this.formTentativa = this.fb.group({
      id: [0],
      atendimentoId: [null, [Validators.required]],
      existeAgendamento: [null, [Validators.required]],
      observacao: [null],
      selecioneSeuLogin: [null, [Validators.required]],
      numeroOutbound: [null, [Validators.required]],
      scoreInicial: [null],
      scoreAplicado: [null],
      usuarioCadastro: [null, [Validators.required]],
      usuarioLogado: [null, [Validators.required]],
      diadoAgendamento: [null, [Validators.required]],
      horadoAgendamento: [null, [Validators.required]],
      dataeHoradoContato: [null, [Validators.required]],
      motivodoNaoAgendamento: [null],
      agendamentodaMatricula: [null, [Validators.required]],

    })

    var dataAtual = new Date();
    this.formTentativa.get('dataeHoradoContato').setValue(dataAtual.toLocaleString());
  }

  loadData(): void {
    debugger
    this.isLoadingResults = true;
    this.outboundService.getOutbound(this.idUnidade).subscribe(val => {

      if (!val) {
        return;
      }

      if (val?.status === 'error') {
        return this.error = true;
      }

      // const patch: any = {};
      // for (let key in val) {
      //   if (val[key]) patch[key] = val[key];
      // }

      this.isLoadingResults = false;
      this.outbound = val;
      //this.UpdateStatusEmExecucao(outbouns.id);
      //this.valor = outbound;
      this.carregaFormDadosAtendimento()
      this.getFiltrarUsuarioPorId();
      this.getHistoricoTentativas();
    });

    this.formTentativa.get('SelecioneSeuLogin').valueChanges.subscribe(val => {
      //

      const usuarioId = this.usuarioDefault?.length >= 0 ? this.usuarioDefault.find(e => e.nome == val) : null;

      if (usuarioId && usuarioId.id) {
        this.formTentativa.get('usuarioId').setValue(usuarioId.id)
      }
      else {
        this.formTentativa.get('usuarioId').setValue(null);
      }
    });
  }

  carregaFormDadosAtendimento() {
    this.formAtendimento.formDadosAtendimento.get('id').setValue(this.outbound.id);
    this.formAtendimento.formDadosAtendimento.get('dataeHoradoAtendimento').setValue(this.outbound.dataeHoradoAtendimento);
    this.formAtendimento.formDadosAtendimento.get('usuarioLogado').setValue(this.outbound.usuarioLogado);
    this.formAtendimento.formDadosAtendimento.get('canaldeAtendimento').setValue(this.outbound.canaldeAtendimento);
    this.formAtendimento.formDadosAtendimento.get('nomedoCliente').setValue(this.outbound.nomedoCliente);
    this.formAtendimento.formDadosAtendimento.get('cursodeInteresse').setValue(this.outbound.cursodeInteresse);
    this.formAtendimento.formDadosAtendimento.get('celular').setValue(this.outbound.celular);
    this.formAtendimento.formDadosAtendimento.get('telefoneFixo').setValue(this.outbound.telefoneFixo);
    this.formAtendimento.formDadosAtendimento.get('periodo').setValue(this.outbound.periodo);
    this.formAtendimento.formDadosAtendimento.get('comonosConheceu').setValue(this.outbound.comonosConheceu);
    this.formAtendimento.formDadosAtendimento.get('agendamentodaMatricula').setValue(this.outbound.agendamentodaMatricula);
    this.formAtendimento.formDadosAtendimento.get('email').setValue(this.outbound.email);
    this.formAtendimento.formDadosAtendimento.get('motivodeInteressenoCurso').setValue(this.outbound.motivodeInteressenoCurso);
    this.formAtendimento.formDadosAtendimento.get('diadoAgendamento').setValue(this.outbound.diadoAgendamento);
    this.formAtendimento.formDadosAtendimento.get('horadoAgendamento').setValue(this.outbound.horadoAgendamento);
    this.formAtendimento.formDadosAtendimento.get('motivodoNaoAgendamento').setValue(this.outbound.motivodoNaoAgendamento);
    this.formAtendimento.formDadosAtendimento.get('usuarioCadastro').setValue(this.outbound.usuarioCadastro);
    this.formAtendimento.formDadosAtendimento.get('unidadeCadastro').setValue(this.outbound.unidadeCadastro);
    this.formAtendimento.formDadosAtendimento.get('observacoes').setValue(this.outbound.observacoes);
    this.formAtendimento.getHistoricoTentativas();
    this.formAtendimento.agendamentoSimNao();
    this.formAtendimento.informaData();
  }

  // UpdateStatusEmExecucao(Id: any) {
  //   debugger
  //   this.outboundService.updateStatusAtendimento(Id)
  //     .subscribe(val => {

  //       this.outbouns.splice(this.outbouns
  //         .indexOf(this
  //           .outbouns.shift()), 1);

  //     });
  // }

  getHistoricoTentativas() {

    const id = this.outbound.id

    this.outboundService.getHistoricoTentativa(id)
      .subscribe(val => {
        if (val['status'] === 'error') {
          this.error = true;
        }
        else {

          if (val.length === 0) {

            this.outboundNumero = 1;

            this.scoreInicial = 1000;

            this.scoreAplicado = this.outbound.score > 2000 ? this.outbound.score : 990;

          }
          else {
            this.outboundNumero = val.length + 1;

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

  CadastrarOutbound(): void {

    this.formService.validateAllFields(this.formTentativa);
    this.formTentativa.get('atendimentoId').setValue(this.outbound.id);
    this.formTentativa.get('numeroOutbound').setValue(this.outboundNumero);
    this.formTentativa.get('usuarioLogado').setValue(this.idUsuarioLogado);
    this.formTentativa.get('usuarioCadastro').setValue(this.formTentativa.value.selecioneSeuLogin);
    this.formTentativa.get('scoreAplicado').setValue(this.scoreAplicado);
    this.formTentativa.get('scoreInicial').setValue(this.scoreInicial);
    this.formTentativa.get('agendamentodaMatricula').setValue(this.agendamentoSim);

    var tentativaDados = this.formTentativa.value;

    if (!this.validaDados(tentativaDados)) {
      this.animationsService.showErrorSnackBar('Dados Incorretos, ou campos obrigatórios não preenchidos');
      return;
    }

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
            this.formAtendimento.formDadosAtendimento.reset();
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

    var dadosCliente = this.formDadosAtendimento.value
    const isMobileResolution = window.innerWidth < 768 ? true : false;
    const dialogRefMsg = this.dialog.open(DadosEditadosComponent, {
      width: isMobileResolution ? '98vw' : '50vw',
      data: { dadosCliente },
      autoFocus: true
    });
    dialogRefMsg.afterClosed().subscribe(result => {
      if (result) this.loadData();
    });
  }

  agendamentoSimNao(): void {

    if (this.formTentativa.get('existeAgendamento').value == 1)
      this.agendamentoSim = 1;
    else if (this.formTentativa.get('existeAgendamento').value == 2)
      this.agendamentoSim = 2;
    else {
      this.agendamentoSim = 3;
      this.formTentativa.get('diadoAgendamento').reset();
      this.formTentativa.get('horadoAgendamento').reset();
    }

  }

  motivoAgendamentoSimNao(): void {

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

    this.usuarioService.getBusarUsuarioPorUnidade({ unidadeId: this.idUnidade, ehAtendimento: true })
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

  enviarSair(): void {

    this.acionarSair = true;
    this.acionarProximo = false;
    this.CadastrarOutbound();

  }

  enviarProximo(): void {

    this.acionarProximo = true;
    this.acionarSair = false;
    this.CadastrarOutbound();
  }

  validaDados(dados: any): boolean {

    if (dados.agendamentodaMatricula !== 3) {
      if (dados.diadoAgendamento === null) {
        return false
      }
      if (dados.horadoAgendamento === null) {
        return false
      }
    }

    return true
  }

  getFiltrarUsuarioPorId(): void {

    const id = this.outbound.usuarioCadastro;

    this.usuarioService.getPorId(id)
      .subscribe(val => {
        if (val['status'] === 'error') {
          this.error = true;
        }
        else {

          const nome = val.userName;
          this.formAtendimento.formDadosAtendimento.get('nomeUsuarioCadastroAtendimento').setValue(nome);
        }
      });
  }
}
