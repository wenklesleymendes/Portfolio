import { Component, Inject } from '@angular/core';
import { Navigation, Router } from '@angular/router';
import { Location } from '@angular/common';
import { DadosEditadosComponent } from './dados-editados/dados-editados.component';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'src/app/services/form.service';
import { AtendimentoService } from 'src/app/services/Atendimento/Atendimento.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { TelMask, CelMask, CPFMask, CepMask } from 'src/app/utils/mask/mask';
import { UsuarioService } from 'src/app/services/portal-adm/usuario.service';
import { HistoricoTentativasComponent } from '../../../outbound/historico-tentativas/historico-tentativas.component';
import { OutboundService } from 'src/app/services/Atendimento/OutboundService.service';
import { HistoricoAgendadosComponent } from '../../../agendados/historico-agendados/historico-agendados.component';


@Component({
  selector: 'app-editar-resultados',
  templateUrl: './editar-resultados.component.html',
  styleUrls: ['./editar-resultados.component.scss']
})

export class EditarResultadosComponent {

  isLoadingResults: boolean = true;
  formDadosCliente: FormGroup;
  id = 0;
  filtro: any;
  error: boolean = false;
  maskTelefoneFixoPrincipal = TelMask;
  maskCelular = TelMask;
  agendamentoSim: number = 0;
  minDate: Date;
  historicoOutbound: any;
  historicoAgendamento: any;
  dataAgendamentoAtual: any;
  horaAgendamentoAtual: any;

  constructor(
    private router: Router,
    private location: Location,
    private dialog: MatDialog,
    private formService: FormService,
    private fb: FormBuilder,
    private atendimentoService: AtendimentoService,
    private animationsService: AnimationsService,
    private usuarioService: UsuarioService,
    private outboundService: OutboundService,
  ) {
    const currentNavigation: Navigation = this.router.getCurrentNavigation();
    if (currentNavigation && currentNavigation.extras && currentNavigation.extras.state) {
      const state = currentNavigation.extras.state;
      this.filtro = state.id;
    }
  }

  ngOnInit(): void {

    this.id = this.filtro;
    this.buildFormDadosCliente();
    this.gethistoricoOutbounds();
    this.getHistoricoAgendamentos();
    this.minDate = new Date(Date.now());
    Promise.all([
    ])
      .then(() => {
        this.isLoadingResults = false;
        this.loadData();
      })
      .catch(() => this.isLoadingResults = false);
  }

  buildFormDadosCliente(): void {
    this.formDadosCliente = this.fb.group({
      id: 0,
      dataeHoradoAtendimento: [Date],
      usuarioLogado: [null],
      canaldeAtendimento: [null],
      nomedoCliente: [null, [Validators.required]],
      cursodeInteresse: [null],
      celular: [null, [Validators.required]],
      telefoneFixo: [null],
      periodo: [null],
      comonosConheceu: [null],
      agendamentodaMatricula: [null],
      email: [null],
      motivodeInteressenoCurso: [null],
      diadoAgendamento: [null],
      horadoAgendamento: [null],
      motivodoNaoAgendamento: [null],
      usuarioCadastro: [null],
      unidadeCadastro: [null],
      nomeUsuarioCadastroAtendimento: [null],
      score: [null],
      status: [null],
      existeAgendamento: [null],
      observacoes: [null],
      dataeHoradoAgendamento: [null],
      ehNovoAgendamentoEditado: [null]
    })

    // Change mask of all contact numbers
    this.formDadosCliente.get('telefoneFixo').valueChanges
      .subscribe(val => this.maskTelefoneFixoPrincipal = (val && val.length > 10) ? CelMask : TelMask);

    this.formDadosCliente.get('celular').valueChanges
      .subscribe(val => this.maskCelular = (val && val.length > 10) ? CelMask : TelMask);
  }

  loadData(): void {

    if (this.id !== void 0) {
      this.isLoadingResults = true;
      this.atendimentoService.getPorId(this.id).subscribe(val => {
        if (!val) return;
        if (val?.status === 'error') return this.error = true;

        this.dataAgendamentoAtual = val.diadoAgendamento;
        this.horaAgendamentoAtual = val.horadoAgendamento;
        this.formDadosCliente.patchValue(val);
        this.getFiltrarUsuarioPorId();
        this.agendamentoSimNao();
        this.informaData();
        this.isLoadingResults = false;
      })
    }
  }

  async salvarDataFormDadosCliente(): Promise<void> {

    this.formService.validateAllFields(this.formDadosCliente);
    if (!this.formDadosCliente.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    // Validating form
    const formValue: any = this.formDadosCliente.value;

  }

  agendamentoSimNao(): void {

    if (this.formDadosCliente.get('agendamentodaMatricula').value == 1)
      this.agendamentoSim = 1;

    if (this.formDadosCliente.get('agendamentodaMatricula').value == 2)
      this.agendamentoSim = 2;

    if (this.formDadosCliente.get('agendamentodaMatricula').value == 3)
      this.agendamentoSim = 3;
  }

  informaData(): void {
    var dataFormatacao = this.formDadosCliente.get('diadoAgendamento').value;

    if (dataFormatacao === null) return;

    var data = dataFormatacao.split("/");

    this.formDadosCliente.get('diadoAgendamento').setValue(new Date(data[2], data[1] - 1, data[0]));
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

  goToResultados(): void {
    this.router.navigate(['menu-atendimento/m-a-home/pesquisar/resultados']); //, { state: { matriculaId } }
  }

  editarDados(): void {

    const isMobileResolution = window.innerWidth < 768 ? true : false;

    const dadosCliente: any = this.formDadosCliente.value;


    if (!this.validaDados(dadosCliente)) {
      this.animationsService.showErrorSnackBar('Dados Incorretos, ou campos obrigatórios não preenchidos');
      return;
    }

    debugger
    if (dadosCliente.horadoAgendamento !== null) {
      let diaAgendamento = new Date(dadosCliente.diadoAgendamento);
      let diaAgendamentoFormatado = diaAgendamento.toLocaleDateString();

      dadosCliente.ehNovoAgendamentoEditado = this.dataAgendamentoAtual !== diaAgendamentoFormatado
        || this.horaAgendamentoAtual !== dadosCliente.horadoAgendamento;
    }

    const dialogRefMsg = this.dialog.open(DadosEditadosComponent, {
      width: isMobileResolution ? '98vw' : '50vw',
      data: { dadosCliente },
      autoFocus: true
    });
    dialogRefMsg.afterClosed().subscribe(result => {

    });
  }

  getFiltrarUsuarioPorId(): void {

    const id = this.formDadosCliente.get('usuarioCadastro').value;

    this.usuarioService.getPorId(id)
      .subscribe(val => {
        if (val['status'] === 'error') {
          this.error = true;
        }
        else {

          const nome = val.userName;
          this.formDadosCliente.get('nomeUsuarioCadastroAtendimento').setValue(nome);
        }
      });
  }

  gethistoricoOutbounds() {
    //debugger;

    const id = this.id

    this.outboundService.getHistoricoTentativa(id)
      .subscribe(val => {

        if (val['status'] === 'error') {
          this.error = true;
        }
        else {
          this.historicoOutbound = val;
        }
      });
  }

  abrirHistoricoCompleto(): void {
    const dadosTentativas = this.historicoOutbound;
    const isMobileResolution = window.innerWidth < 768 ? true : false;
    const dialogRefMsg = this.dialog.open(HistoricoTentativasComponent, {
      width: isMobileResolution ? '98vw' : '90vw',
      data: { dadosTentativas },
      autoFocus: true
    });
    dialogRefMsg.afterClosed().subscribe(() => {

    });
  }

  getHistoricoAgendamentos() {

    const id = this.id;

    this.atendimentoService.getHistoricoAgendamentos(id)
      .subscribe(val => {

        if (val['status'] === 'error') {
          this.error = true;
        }
        else {
          this.historicoAgendamento = val;
        }
      });
  }

  abrirHistoricoAgendamento(): void {

    const dadosAgendamentos = this.historicoAgendamento;
    const isMobileResolution = window.innerWidth < 768 ? true : false;
    const dialogRefMsg = this.dialog.open(HistoricoAgendadosComponent, {
      width: isMobileResolution ? '98vw' : '90vw',
      data: { dadosAgendamentos },
      autoFocus: true
    });
    dialogRefMsg.afterClosed().subscribe(() => {

    });
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
}
