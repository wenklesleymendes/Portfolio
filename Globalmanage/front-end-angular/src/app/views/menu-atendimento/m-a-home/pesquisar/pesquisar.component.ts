import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { HourMinuteMask, CPFMask, TelMask, CelMask } from 'src/app/utils/mask/mask';
import { Observable } from 'rxjs';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { AuthService } from 'src/app/security/auth.service';
import { Router } from '@angular/router';
import { AtendimentoService } from 'src/app/services/Atendimento/Atendimento.service';
import { UsuarioService } from 'src/app/services/portal-adm/usuario.service';

@Component({
    selector: 'app-pesquisar',
    templateUrl: './pesquisar.component.html',
    styleUrls: ['./pesquisar.component.scss']
})

export class PesquisarComponent implements OnInit {
    form: FormGroup;
    hourMinute = HourMinuteMask;
    error: boolean = false;
    visualizarTodasUnidades: boolean;
    isLoadingResults: boolean = false;
    cpfMask: string = CPFMask;
    maskCelular = TelMask;
    cursos: any[] = null;
    filterUnidades: Observable<any[]>;
    unidadesDefault = null;
    salas: number[] = Array(20).fill('').map((x, i) => i + 1);
    years: number[] = null;
    anos = Array(2).fill('').map((x, i) => (new Date().getUTCFullYear() + i).toString());
    startYear: Date = new Date('2020-01-01');
    maskTelefoneFixoPrincipal = TelMask;
    usuariosUnidade: Observable<any[]>;
    idUnidade: 0;
    nomeUnidade: string;
    showUsuario: boolean;


    constructor(
        private fb: FormBuilder,
        private animationService: AnimationsService,
        private unidadeService: UnidadeService,
        private authService: AuthService,
        private formService: FormService,
        private router: Router,
        private atendimentoService: AtendimentoService,
        private usuarioService: UsuarioService,

        @Inject(MAT_DIALOG_DATA) public data,
    ) { }

    // --------------------------------------------------------------------------
    //  LIFECYCLE HOOKS
    // --------------------------------------------------------------------------
    ngOnInit(): void {

        const token = this.authService.getToken();

        if (token != null) {
            this.nomeUnidade = token.user.unidade.nome;
            this.idUnidade = token.user.unidade.id;
        }

        this.buildForm();
        this.getFiltarUsuarioUnidade();
    }

    // --------------------------------------------------------------------------
    //  BUSSINES RULE
    // --------------------------------------------------------------------------
    buildForm(): void {

        this.form = this.fb.group({
            nomedoCliente: [null],
            datadoAtendimento: [null],
            canaldeAtendimento: [null],
            celular: [null],
            telefoneFixo: [null],
            statusdoAtendimento: [null],
            pesquisarDataInicial: [null],
            pesquisarDataFinal: [null],
            idAtendente: [null],
            idUnidade: [null],
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

    getunidades(): void {
        this.unidadeService.getAll()
            .subscribe(val => {
                if (val['status'] === 'error') { this.error = true; }
                else {

                    this.unidadesDefault = val;
                    this.unidadesDefault?.length === 1 ? this.form.get('unidadeSelect').setValue(this.unidadesDefault[0].nome) : this.form.get('unidadeSelect').setValue('');
                }
            });
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

        const formValue = this.form.value;

        if (this.form.value.datadoAtendimento != null)
            formValue.datadoAtendimento = new Date(this.form.value.datadoAtendimento).toLocaleDateString();

        if (this.form.value.pesquisarDataInicial != null)
            formValue.pesquisarDataInicial = new Date(this.form.value.pesquisarDataInicial).toLocaleDateString();

        if (this.form.value.pesquisarDataFinal != null)
            formValue.pesquisarDataFinal = new Date(this.form.value.pesquisarDataFinal).toLocaleDateString();

        formValue.idUnidade = this.idUnidade;

        this.router.navigate(['menu-atendimento/m-a-home/pesquisar/resultados'], { state: { formValue } }); //, { state: { matriculaId } }
    }
}