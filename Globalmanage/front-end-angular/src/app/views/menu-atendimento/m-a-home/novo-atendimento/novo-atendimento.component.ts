import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/security/auth.service';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { TelMask, CelMask, CPFMask } from 'src/app/utils/mask/mask';
import { AlunoService } from 'src/app/services/aluno/aluno.service';
import { MatDialog } from '@angular/material/dialog';
import { QuaseLaComponent } from './quase-la/quase-la.component';
import { ClienteCadastradoComponent } from './cliente-cadastrado/cliente-cadastrado.component';
import { AtendimentoService } from 'src/app/services/Atendimento/Atendimento.service';

@Component({
    selector: 'app-novo-atendimento',
    templateUrl: './novo-atendimento.component.html',
    styleUrls: ['./novo-atendimento.component.scss']
})

export class NovoAtendimentoComponent implements OnInit {
    imgProfileSrc: string = null;
    form: FormGroup;
    id = 0;
    idUsuarioLogado = 0;
    error: boolean = false;
    isLoadingResults: boolean = true;
    cpfMask = CPFMask;
    maskTelefoneFixoPrincipal = TelMask;
    maskCelular = TelMask;
    unidadesDefault = null;
    nomeUnidade: string;
    idUnidade: number = 0;
    alunoJaCadastrado: boolean = false;
    agendamentoSim: number = 0;
    nomeUsuario: string;
    minDate: Date;

    constructor(
        private location: Location,
        private fb: FormBuilder,
        private animationsService: AnimationsService,
        private alunoService: AlunoService,
        private routerActive: ActivatedRoute,
        private formService: FormService,
        private dialog: MatDialog,
        private authService: AuthService,
        private router: Router,
        private atendimentoService: AtendimentoService,
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

        const token = this.authService.getToken();
        if (token != null) {

            this.unidadesDefault = token.user.unidade;
            this.idUnidade = token.user.unidade.id;
            this.nomeUnidade = token.user.unidade.nome;
            this.idUsuarioLogado = token.user.id;
            this.nomeUsuario = token.user.userName;
        }

        this.minDate = new Date(Date.now());

        Promise.all([]).then(() => {
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
            observacoes: [null],
            usuarioCadastro: [null],
            unidadeCadastro: [null],
        })

        var dataAtual = new Date();
        this.form.get('dataeHoradoAtendimento').setValue(dataAtual.toLocaleString());

        // Change mask of all contact numbers
        this.form.get('telefoneFixo').valueChanges
            .subscribe(val => this.maskTelefoneFixoPrincipal = (val && val.length > 10) ? CelMask : TelMask);

        this.form.get('celular').valueChanges
            .subscribe(val => this.maskCelular = (val && val.length > 10) ? CelMask : TelMask);
    }

    loadData(): void {

        if (this.id != 0) {
            this.isLoadingResults = true;
            this.alunoService.getPorId(this.id).subscribe(val => {
                if (!val) {
                    return
                };

                if (val?.status === 'error') {
                    return this.error = true
                };

                this.isLoadingResults = false;
            })
        }

        this.form.get('usuarioLogado').setValue(this.nomeUsuario);
    }

    // --------------------------------------------------------------------------
    //  BTN EVENTS
    // --------------------------------------------------------------------------
    voltar(): void {
        this.location.back();
    }

    async salvarData(): Promise<void> {

        this.formService.validateAllFields;
        if (!this.form.valid) {
            this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
            return;
        }
    }

    consultaCeluar(): void {

        var celular = this.form.get('celular').value;

        this.atendimentoService.getPorCelular(celular).subscribe(val => {

            if (!val) return;

            if (val?.status === 'error') return this.error = true;

            if (val.unidadeCadastro === this.idUnidade) {
                this.id = val.id;
                this.abrirClienteCadastrado();
            }
        });
    }

    abrirQuaseLa(): void {

        const isMobileResolution = window.innerWidth < 768 ? true : false;
        var dadosAlunoForm = this.form.value;

        if (!this.validaDados(dadosAlunoForm)) {
            this.animationsService.showErrorSnackBar('Dados Incorretos, ou campos obrigatórios não preenchidos');
            return;
        }

        const dialogRef = this.dialog.open(QuaseLaComponent, {
            width: isMobileResolution ? '95vw' : '90vw',
            autoFocus: false,
            data: { dadosAlunoForm }
        });

        dialogRef.afterClosed().subscribe(result => {
        });
    }

    abrirClienteCadastrado(): void {

        const isMobileResolution = window.innerWidth < 768 ? true : false;

        var id = this.id;

        if (id) {

            const dialogRef = this.dialog.open(ClienteCadastradoComponent, {
                width: isMobileResolution ? '95vw' : '90vw',
                autoFocus: false,
                data: { id }
            });
            dialogRef.afterClosed().subscribe(result => {
            });
        }
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

    agendamentoSimNao(): void {

        if (this.form.get('agendamentodaMatricula').value == 1)
            this.agendamentoSim = 1;
        else if (this.form.get('agendamentodaMatricula').value == 2)
            this.agendamentoSim = 2;
        else {
            this.agendamentoSim = 3;
            this.form.get('diadoAgendamento').reset();
            this.form.get('horadoAgendamento').reset();
        }
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

        if (!this.form.valid) {
            return false;
        }

        return true
    }
}
