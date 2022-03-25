import { Component, OnInit, ViewChild } from '@angular/core';
import { Navigation, Router } from '@angular/router';
import { Location } from '@angular/common';
import { DadosEditadosComponent } from '../../pesquisar/resultados/editar-resultados/dados-editados/dados-editados.component';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'src/app/services/form.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { TelMask } from 'src/app/utils/mask/mask';
import { HistoricoTentativasComponent } from '../../outbound/historico-tentativas/historico-tentativas.component';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/security/auth.service';
import { OutboundService } from 'src/app/services/Atendimento/OutboundService.service';
import { Observable } from 'rxjs';
import { AtendimentoService } from 'src/app/services/Atendimento/Atendimento.service';
import { UsuarioService } from 'src/app/services/portal-adm/usuario.service';
import { FormClienteComponent } from '../../componentes-atendimento/form-cliente/form-cliente.component';
import { Outbound } from 'src/app/interfaces/outbound.interface';

@Component({
  selector: 'app-registro-completo',
  templateUrl: './registro-completo.component.html',
  styleUrls: ['./registro-completo.component.scss']
})

export class RegistroCompletoComponent implements OnInit {

  isLoadingResults: boolean = true;
  formConfirmacaoAgendamento: FormGroup;
  formDadosCliente: FormGroup;
  id = 0;
  error: boolean = false;
  agendamentoSim: number = 0;
  reagendouSim: number = 0;
  maskTelefoneFixoPrincipal = TelMask;
  maskCelular = TelMask;
  usuariosUnidade: Observable<any[]>;
  idUnidade: 0;
  nomeUnidade: string;
  showUsuario: boolean;
  idUsuarioLogado = 0;
  usuarioDefault = null;
  historicoTentativa: any;
  minDate: Date;
  valor: any;
  agendamento: any;

  constructor(
    private router: Router,
    private location: Location,
    private dialog: MatDialog,
    private fb: FormBuilder,
    private atendimentoService: AtendimentoService,
    private usuarioService: UsuarioService,
    private formService: FormService,
    private animationsService: AnimationsService,
    private authService: AuthService,
    private routerActive: ActivatedRoute,
    private outboundService: OutboundService,
  ) {
    const currentNavigation: Navigation = this.router.getCurrentNavigation();
    if (currentNavigation && currentNavigation.extras && currentNavigation.extras.state) {
      const state = currentNavigation.extras.state;
      this.id = state.id;
    }
  }

  @ViewChild(FormClienteComponent, { static: false })
  formAtendimento: FormClienteComponent

  ngOnInit(): void {

    this.buildFormConfirmacaoAgendamento();
    this.getHistoricoTentativas();

    const token = this.authService.getToken();
    if (token != null) {
      this.nomeUnidade = token.user.unidade.nome;
      this.idUnidade = token.user.unidade.id;
      this.idUsuarioLogado = token.user.id;
    }

    this.minDate = new Date(Date.now());

    Promise.all([this.getFiltarUsuarioUnidade()])
      .then(() => {
        this.isLoadingResults = false;
        this.loadData();
      })
      .catch(() => this.isLoadingResults = false);
  }

  buildFormConfirmacaoAgendamento(): void {
    this.formConfirmacaoAgendamento = this.fb.group({
      id: [0],
      idAtendimento: [0],
      dataeHoradoUltimoContato: [null, [Validators.required]],
      tipoAgendamento: [null, [Validators.required]],
      celular: [null, [Validators.required]],
      horaAgendamento: [null, [Validators.required]],
      dataAgendamento: [null, [Validators.required]],
      diadoReagendamento: [null, [Validators.required]],
      horadoReagendamento: [null, [Validators.required]],
      situacao: [null],
      observacoes: [null],
      usuarioCadastro: [null],
      AgendamentoConfirmado: [null],
      MotivodaNaoConfirmacao: [null, [Validators.required]],
    });

    var dataAtual = new Date();
    this.formConfirmacaoAgendamento.get('dataeHoradoUltimoContato').setValue(dataAtual.toLocaleString());
  }

  loadData(): void {

    if (this.id != 0) {

      this.atendimentoService.getPorId(this.id).subscribe(val => {
        if (!val) return;
        if (val?.status === 'error') return this.error = true;

        const patch: any = {};

        for (let key in val) {
          if (val[key]) patch[key] = val[key];
        }

        this.isLoadingResults = false;
        this.valor = patch;
        this.carregaFormDadosAtendimento();
        this.getFiltrarUsuarioPorId();
      });

      this.atendimentoService.getAgendamentoIdAtendimento(this.id).subscribe(val => {
        if (!val) return;
        if (val?.status === 'error') return this.error = true;

        this.isLoadingResults = false;

        this.agendamento = val;
      });
    }
  }

  carregaFormDadosAtendimento() {
    this.formAtendimento.formDadosAtendimento.get('id').setValue(this.valor.id);
    this.formAtendimento.formDadosAtendimento.get('dataeHoradoAtendimento').setValue(this.valor.dataeHoradoAtendimento);
    this.formAtendimento.formDadosAtendimento.get('usuarioLogado').setValue(this.valor.usuarioLogado);
    this.formAtendimento.formDadosAtendimento.get('canaldeAtendimento').setValue(this.valor.canaldeAtendimento);
    this.formAtendimento.formDadosAtendimento.get('nomedoCliente').setValue(this.valor.nomedoCliente);
    this.formAtendimento.formDadosAtendimento.get('cursodeInteresse').setValue(this.valor.cursodeInteresse);
    this.formAtendimento.formDadosAtendimento.get('celular').setValue(this.valor.celular);
    this.formAtendimento.formDadosAtendimento.get('telefoneFixo').setValue(this.valor.telefoneFixo);
    this.formAtendimento.formDadosAtendimento.get('periodo').setValue(this.valor.periodo);
    this.formAtendimento.formDadosAtendimento.get('comonosConheceu').setValue(this.valor.comonosConheceu);
    this.formAtendimento.formDadosAtendimento.get('agendamentodaMatricula').setValue(this.valor.agendamentodaMatricula);
    this.formAtendimento.formDadosAtendimento.get('email').setValue(this.valor.email);
    this.formAtendimento.formDadosAtendimento.get('motivodeInteressenoCurso').setValue(this.valor.motivodeInteressenoCurso);
    this.formAtendimento.formDadosAtendimento.get('diadoAgendamento').setValue(this.valor.diadoAgendamento);
    this.formAtendimento.formDadosAtendimento.get('horadoAgendamento').setValue(this.valor.horadoAgendamento);
    this.formAtendimento.formDadosAtendimento.get('motivodoNaoAgendamento').setValue(this.valor.motivodoNaoAgendamento);
    this.formAtendimento.formDadosAtendimento.get('usuarioCadastro').setValue(this.valor.usuarioCadastro);
    this.formAtendimento.formDadosAtendimento.get('unidadeCadastro').setValue(this.valor.unidadeCadastro);
    this.formAtendimento.formDadosAtendimento.get('observacoes').setValue(this.valor.observacoes);
    this.formAtendimento.getHistoricoTentativas();
    this.formAtendimento.getHistoricoAgendamentos()
    this.formAtendimento.agendamentoSimNao();
    this.formAtendimento.informaData();
  }

  async salvarDataFormDadosCliente(): Promise<void> {

    this.formService.validateAllFields(this.formDadosCliente);

    if (!this.formDadosCliente.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    const formValue: any = this.formDadosCliente.value;

  }

  salvarDataConfirmacaoAgendamento(): void {
    // Validating form

    if (this.formConfirmacaoAgendamento.get('celular').value === null) {

      this.formConfirmacaoAgendamento.get('celular').setValue(this.valor.celular);
    }

    if (this.formConfirmacaoAgendamento.get('MotivodaNaoConfirmacao').value === 7) {
      this.agendamento.situacao = 7;
      this.reagendou();
      return;
    }

    this.agendamento.situacao = this.formConfirmacaoAgendamento.get('situacao').value;
    this.agendamento.dataeHoradoUltimoContato = new Date();
    this.agendamento.observacoes = this.formConfirmacaoAgendamento.get('observacoes').value;
    this.agendamento.celular = this.formConfirmacaoAgendamento.get('celular').value;
    ////debugger
    this.atendimentoService.atualizaAgendamento(this.agendamento).subscribe(val => {
      const message = 'Tentativa Registrada com sucesso';

      this.animationsService.showProgressBar(false);
      this.animationsService.showSuccessSnackBar(message);

      this.goToAgendados();
    });
  }

  reagendou(): void {
    let reagendamento: any = this.formConfirmacaoAgendamento.value;

    if (!this.validaDados(reagendamento)) {
      this.animationsService.showErrorSnackBar('Dados Incorretos, ou campos obrigatórios não preenchidos');
      return;
    }

    this.atendimentoService.atualizaAgendamento(this.agendamento).subscribe(val => {

    });

    let dataReagendada = new Date(this.formConfirmacaoAgendamento.get('diadoReagendamento').value);
    let dataFormatada = ((dataReagendada.getDate())) + "/" + ((dataReagendada.getMonth() + 1)) + "/" + dataReagendada.getFullYear();

    reagendamento.dataAgendamento = dataFormatada;
    reagendamento.horaAgendamento = this.formConfirmacaoAgendamento.get('horadoReagendamento').value;
    reagendamento.id = 0;
    reagendamento.situacao = 0;
    reagendamento.tipoAgendamento = this.agendamento.tipoAgendamento;
    reagendamento.idAtendimento = this.agendamento.idAtendimento;

    this.atendimentoService.cadastrarAgendamento(reagendamento).subscribe(val => {
      const message = 'Reagendamento Registrado com sucesso';
      this.animationsService.showProgressBar(false);
      this.animationsService.showSuccessSnackBar(message);

      this.goToAgendados();
    });
  }

  editarDados(): void {
    var dadosCliente = this.formDadosCliente.value
    const isMobileResolution = window.innerWidth < 768 ? true : false;
    const dialogRefMsg = this.dialog.open(DadosEditadosComponent, {
      width: isMobileResolution ? '98vw' : '50vw',
      data: { dadosCliente },
      autoFocus: true
    });
    dialogRefMsg.afterClosed().subscribe(result => {

    });
  }

  agendamentoSimNao(): void {
    if (this.formConfirmacaoAgendamento.get('AgendamentoConfirmado').value === 1) {

      this.agendamentoSim = 1;
      this.formConfirmacaoAgendamento.get('situacao').setValue(this.agendamentoSim);
      this.formConfirmacaoAgendamento.get('MotivodaNaoConfirmacao').setValue(0);
      this.reagendouSim = 0;
    }
    else

      this.agendamentoSim = 2;
  }

  reagendouSimNao(): void {

    if (this.formConfirmacaoAgendamento.get('MotivodaNaoConfirmacao').value === 7) {
      this.reagendouSim = 1;
      this.formConfirmacaoAgendamento.get('situacao').setValue(7);
    }
    else {
      this.reagendouSim = this.formConfirmacaoAgendamento.get('MotivodaNaoConfirmacao').value;
      this.formConfirmacaoAgendamento.get('situacao').setValue(this.reagendouSim);
    }
  }

  voltar(): void {
    this.location.back();
  }

  goToHome(): void {
    this.router.navigate(['menu-atendimento/m-a-home']); //, { state: { matriculaId } }
  }

  goToNovoAtendimento(): void {
    this.router.navigate(['menu-atendimento/m-a-home/novo-atendimento']); //, { state: { matriculaId } }
  }

  goToPesquisar(): void {
    this.router.navigate(['menu-atendimento/m-a-home/pesquisar']); //, { state: { matriculaId } }
  }

  goToOutbound(): void {
    this.router.navigate(['menu-atendimento/m-a-home/outbound']); //, { state: { matriculaId } }
  }

  goToAgendados(): void {
    this.router.navigate(['menu-atendimento/m-a-home/agendados']); //, { state: { matriculaId } }
  }

  goToContatosPrioritarios(): void {
    this.router.navigate(['menu-atendimento/m-a-home/contatos-prioritarios']); //, { state: { matriculaId } }
  }

  getHistoricoTentativas() {

    const id = this.id

    this.outboundService.getHistoricoTentativa(id)
      .subscribe(val => {

        if (val['status'] === 'error') {
          this.error = true;
        }
        else {
          this.historicoTentativa = val;
        }
      });
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

  getFiltrarUsuarioPorId(): void {

    const id = this.valor.usuarioCadastro;

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

  validaDados(dados: any): boolean {

    if (dados.diadoReagendamento === null) {
      return false
    }
    if (dados.horadoReagendamento === null) {
      return false
    }
    return true
  }
}
