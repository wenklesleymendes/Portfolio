import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TelMask, CelMask, CPFMask } from 'src/app/utils/mask/mask';
import { OutboundService } from 'src/app/services/Atendimento/OutboundService.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { HistoricoTentativasComponent } from '../../outbound/historico-tentativas/historico-tentativas.component';
import { HistoricoAgendadosComponent } from '../../agendados/historico-agendados/historico-agendados.component';
import { Observable } from 'rxjs';
import { AtendimentoService } from 'src/app/services/Atendimento/Atendimento.service';
import { FormService } from 'src/app/services/form.service';
import { UsuarioService } from 'src/app/services/portal-adm/usuario.service';
import { MatDialog } from '@angular/material/dialog';
import { I } from '@angular/cdk/keycodes';

@Component({
  selector: 'app-form-cliente',
  templateUrl: './form-cliente.component.html',
  styleUrls: ['./form-cliente.component.scss']
})
export class FormClienteComponent implements OnInit {

  formDadosAtendimento: FormGroup;
  error: boolean = false;
  cpfMask = CPFMask;
  maskTelefoneFixoPrincipal = TelMask;
  maskCelular = TelMask;
  agendamentoSim: number = 0;
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
  historicoAgendamento: any;
  outbouns: any[];
  minDate: Date;
  btnSalvar: boolean = false;
  btnEditar: boolean = true;

  constructor(
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private formService: FormService,
    private dialog: MatDialog,
    private outboundService: OutboundService,
    private usuarioService: UsuarioService,
    private atendimentoService: AtendimentoService,
  ) { }

  @Input() atendimento: any;

  ngOnInit(): void {

    this.buildForm();
    this.loadData();
    this.formDadosAtendimento.disable();
  }

  buildForm(): void {
    this.formDadosAtendimento = this.fb.group({
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
    })
    var dataAtual = new Date();
    this.formDadosAtendimento.get('dataeHoradoAtendimento').setValue(dataAtual.toLocaleString());

    // Change mask of all contact numbers
    this.formDadosAtendimento.get('telefoneFixo').valueChanges
      .subscribe(val => this.maskTelefoneFixoPrincipal = (val && val.length > 10) ? CelMask : TelMask);

    this.formDadosAtendimento.get('celular').valueChanges
      .subscribe(val => this.maskCelular = (val && val.length > 10) ? CelMask : TelMask);
  }

  loadData(): void {
   // //debugger
    this.formDadosAtendimento.patchValue = this.atendimento;

  }

  onSubmit() {
    // aqui você pode implementar a logica para fazer seu formulário salvar
    console.log(this.formDadosAtendimento.value);
  }

  agendamentoSimNao(): void {
    ////debugger
    if (this.formDadosAtendimento.get('agendamentodaMatricula').value == 1)
      this.agendamentoSim = 1;

    if (this.formDadosAtendimento.get('agendamentodaMatricula').value == 2)
      this.agendamentoSim = 2;

    if (this.formDadosAtendimento.get('agendamentodaMatricula').value == 3)
      this.agendamentoSim = 3;
  }

  informaData(): void {
    //debugger
    var dataFormatacao = this.formDadosAtendimento.get('diadoAgendamento').value

    if (dataFormatacao === null) return

    var data = dataFormatacao.split("/")
    this.formDadosAtendimento.get('diadoAgendamento').setValue(new Date(data[2], data[1] - 1, data[0]));
  }

  getHistoricoTentativas() {

    const id = this.formDadosAtendimento.value.id

    this.outboundService.getHistoricoTentativa(id)
      .subscribe(val => {

        if (val['status'] === 'error') {
          this.error = true;
        }
        else {

          ////debugger

          if (val.length == 0) {
            this.outboundNumero = 1;
            this.scoreInicial = 1000,
              this.scoreAplicado = 990
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

  abrirHistoricoCompleto(): void {
    //debugger
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

  getHistoricoAgendamentos() {

    const id = this.formDadosAtendimento.value.id

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
    //debugger
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

  editar(): void {
    this.formDadosAtendimento.enable();
    this.btnSalvar = true;
    this.btnEditar = false;
  }

  salvar(): void {

    this.atendimentoService.atualizar(this.formDadosAtendimento.value).subscribe(val => {

      var dadosAlunoForm = this.formDadosAtendimento.value;

      if (!this.validaDados(dadosAlunoForm)) {
        this.animationsService.showErrorSnackBar('Dados Incorretos, ou campos obrigatórios não preenchidos');
        return;
      }

      const message = 'Alteração salva com sucesso';

      this.animationsService.showProgressBar(false);
      this.animationsService.showSuccessSnackBar(message);

      this.btnSalvar = false;
      this.btnEditar = true;
      this.formDadosAtendimento.disable();
    });
  }

  validaDados(dados: any): boolean {

    if (dados.agendamentodaMatricula !== 3 && dados.agendamentodaMatricula !== 0) {
      if (dados.diadoAgendamento === null) {
        return false
      }
      if (dados.horadoAgendamento === null) {
        return false
      }
    }

    if (!this.formDadosAtendimento.valid) {
      return false;
    }

    return true
  }
}
