import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { DadosEditadosComponent } from '../../pesquisar/resultados/editar-resultados/dados-editados/dados-editados.component';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlunoService } from 'src/app/services/aluno/aluno.service';
import { FormService } from 'src/app/services/form.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { TelMask, CelMask, CPFMask, CepMask } from 'src/app/utils/mask/mask';
import { HistoricoTentativasComponent } from '../../outbound/historico-tentativas/historico-tentativas.component';


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

    constructor (
        private router: Router,
        private location: Location,
        private dialog: MatDialog,
        private fb: FormBuilder,
        private alunoService: AlunoService,
        private formService: FormService,
        private animationsService: AnimationsService,
    ){

    }

    ngOnInit(): void {
        this.buildFormDadosCliente();
        this.buildFormConfirmacaoAgendamento();

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
        id: [0],
        DatadeCriacaoDadosCliente: [null, [Validators.required]],
        AtendenteDadosCliente: [null, [Validators.required]],
        NomedoClienteDadosCliente: [null, [Validators.required]],
        ComonosConheceuDadosCliente: [null, [Validators.required]],
        CelularDadosCliente: [null, [Validators.required]],
        TelefoneFixoDadosCliente: [null, [Validators.required]],
        EmailDadosCliente: [null, [Validators.required]],
        CursoDadosCliente: [null, [Validators.required]],
        PeriodoDadosCliente: [null, [Validators.required]],
        CanaldeAtendimentoDadosCliente: [null, [Validators.required]],
        AgendamentodaMatriculaDadosCliente: [null, [Validators.required]],
        MotivodeInteressenoCursoDadosCliente: [null, [Validators.required]],
        MotivodoNaoAgendamentoDadosCliente: [null, [Validators.required]],
        StatusDadosCliente: [null, [Validators.required]],
        })

        // Change mask of all contact numbers
        this.formDadosCliente.get('TelefoneFixoDadosCliente').valueChanges
        .subscribe(val => this.maskTelefoneFixoPrincipal = (val && val.length > 10) ? CelMask : TelMask);
        this.formDadosCliente.get('CelularDadosCliente').valueChanges
        .subscribe(val => this.maskCelular = (val && val.length > 10) ? CelMask : TelMask);
    }

    buildFormConfirmacaoAgendamento(): void {
        this.formConfirmacaoAgendamento = this.fb.group({
        id: [0],
        HorariodeContato: [null, [Validators.required]],
        Atendente: [null, [Validators.required]],
        AgendamentoConfirmado: [null, [Validators.required]],
        DiadoAgendamento: [null, [Validators.required]],
        HoradoAgendamento: [null, [Validators.required]],
        MotivodaNaoConfirmacao: [null, [Validators.required]],
        Observacoes: [null, [Validators.required]],
        DiadoReagendamento: [null, [Validators.required]],
        HoradoReagendamento: [null, [Validators.required]]
        })

        var dataAtual = new Date();
        this.formConfirmacaoAgendamento.get('HorariodeContato').setValue(dataAtual.toLocaleString());
    }


    loadData(): void {

        if (this.id != 0) {
            this.isLoadingResults = true;
            this.alunoService.getPorId(this.id).subscribe(val => {
              if (!val) return;
              if (val?.status === 'error') return this.error = true;

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

    async salvarDataConfirmacaoAgendamento(): Promise<void> {
        this.formService.validateAllFields(this.formConfirmacaoAgendamento);
        if (!this.formConfirmacaoAgendamento.valid) {
            this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
        return;
        }

        // Validating form
        const formValue: any = this.formConfirmacaoAgendamento.value;

    }

    editarDados(): void {
        const isMobileResolution = window.innerWidth < 768 ? true : false;
        const dialogRefMsg = this.dialog.open(DadosEditadosComponent, {
          width: isMobileResolution ? '98vw' : '50vw',
          data: {  },
          autoFocus: true
        });
        dialogRefMsg.afterClosed().subscribe(result => {

        });
    }

    agendamentoSimNao(): void {

        if (this.formConfirmacaoAgendamento.get('AgendamentoConfirmado').value == 1)
            this.agendamentoSim = 1;
        else
            this.agendamentoSim = 2;
    }

    reagendouSimNao(): void {
        if (this.formConfirmacaoAgendamento.get('MotivodaNaoConfirmacao').value == 6)
            this.reagendouSim = 1;
        else
            this.reagendouSim = 2;
    }

    voltar(): void {
        this.location.back();
    }

    goToHome(): void {
        this.router.navigate(['menu-atendimento/m-a-home']); //, { state: { matriculaId } }
    }

    goToNovoAtendimento(): void{
        this.router.navigate(['menu-atendimento/m-a-home/novo-atendimento']); //, { state: { matriculaId } }
    }

    goToPesquisar(): void{
        this.router.navigate(['menu-atendimento/m-a-home/pesquisar']); //, { state: { matriculaId } }
    }

    goToOutbound(): void{
        this.router.navigate(['menu-atendimento/m-a-home/outbound']); //, { state: { matriculaId } }
    }

    goToAgendados(): void{
        this.router.navigate(['menu-atendimento/m-a-home/agendados']); //, { state: { matriculaId } }
    }

    goToContatosPrioritarios(): void{
        this.router.navigate(['menu-atendimento/m-a-home/contatos-prioritarios']); //, { state: { matriculaId } }
    }

    abrirHistoricoCompleto(): void {
        // const dadosTentativas = this.historicoTentativa;
        const isMobileResolution = window.innerWidth < 768 ? true : false;
        const dialogRefMsg = this.dialog.open(HistoricoTentativasComponent, {
          width: isMobileResolution ? '98vw' : '90vw',
          data: {  },
          autoFocus: true
        });
        dialogRefMsg.afterClosed().subscribe(() => {
    
        });
    }
}
