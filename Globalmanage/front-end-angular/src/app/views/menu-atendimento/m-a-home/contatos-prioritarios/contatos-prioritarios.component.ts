import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/security/auth.service';
import { Location } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { DadosEditadosComponent } from '../pesquisar/resultados/editar-resultados/dados-editados/dados-editados.component';
import { DirectiveModule } from 'src/app/utils/directive/directive.module';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { TelMask, CelMask, CPFMask, CepMask } from 'src/app/utils/mask/mask';
import { Estados } from 'src/app/utils/variables/locations';
import { LocationService } from 'src/app/services/location.service';
import { debounceTime, distinctUntilChanged, pairwise } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { AlunoService } from 'src/app/services/aluno/aluno.service';
import * as moment from 'moment';
import { validarCPF } from 'src/app/utils/form-validation/cpf.validation';
import { NacionalidadeService } from 'src/app/services/aluno/nacionalidade.service';
import { DeleteService } from 'src/app/services/delete.service';
import { NaturalidadeService } from 'src/app/services/aluno/naturalidade.service';
import { AlunoStoreActions, AlunoStoreState } from 'src/app/_store/aluno-store';
import { Store } from '@ngrx/store';
import { MatriculaAlunoService } from 'src/app/services/aluno/matricula-aluno.service';


@Component({
    selector: 'app-contatos-prioritarios',
    templateUrl: './contatos-prioritarios.component.html',
    styleUrls: ['./contatos-prioritarios.component.scss']
})

export class ContatosPrioritatiosComponent implements OnInit {
    imgProfileSrc: string = null;
    formTentativa: FormGroup;
    formDadosCliente: FormGroup;
    id = 0;
    error: boolean = false;
    isLoadingResults: boolean = true;
    onlyNumbers = /^-?(0|[1-9]\d*)?$/;
    cpfMask = CPFMask;
    maskTelefoneFixoPrincipal = TelMask;
    maskCelular = TelMask;
    maskCep = CepMask;
    bancos: any = null;
    estados: string[] = Estados;
    filterUnidades: Observable<any[]>;
    unidadesDefault = null;
    today: Date = new Date();
    file: any;
    updateImg: boolean = false;
    filterNacionalidade: Observable<any[]> = null;
    nacionalidadeDefault = null;
    filterNaturalidade: Observable<any[]> = null;
    naturalidadeDefault = null;
    alunoJaCadastrado: boolean = false;
    agendamentoSim: number = 0;

    constructor(
        private location: Location,
        private fb: FormBuilder,
        private animationsService: AnimationsService,
        private alunoService: AlunoService,
        private deleteService: DeleteService,
        private nacionalidadeService: NacionalidadeService,
        private naturalidadeService: NaturalidadeService,
        private unidadeService: UnidadeService,
        private locationService: LocationService,
        private routerActive: ActivatedRoute,
        private route: Router,
        private formService: FormService,
        private store: Store<AlunoStoreState.Aluno>,
        private matriculaAlunoService: MatriculaAlunoService,
        private dialog: MatDialog,
        private authService: AuthService,
        private router: Router,
    ) {
        // Get id
        const id = this.routerActive.snapshot.paramMap.get('id');
        this.id = id ? parseInt(id) : 0;
    }

    ngOnInit(): void {
        this.buildFormTentativa();
        this.buildFormDadosCliente();

        Promise.all([
        ])
        .then(() => {
        this.isLoadingResults = false;
        this.loadData();
        })
        .catch(() => this.isLoadingResults = false);
    }

    // --------------------------------------------------------------------------
    //  BUSSINES RULE
    // --------------------------------------------------------------------------
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

    buildFormTentativa(): void {
        this.formTentativa = this.fb.group({
        id: [0],
        HorariodoContato: [null, [Validators.required]],
        Atendente: [null, [Validators.required]],
        existeAgendamento: [null, [Validators.required]],
        DiadoAgendamento: [null, [Validators.required]],
        HoradoAgendamento: [null, [Validators.required]],
        MotivodoNaoAgendamento: [null, [Validators.required]],
        Observacoes: [null, [Validators.required]],
        })

        var dataAtual = new Date();
        this.formTentativa.get('HorariodoContato').setValue(dataAtual.toLocaleString());
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

    // --------------------------------------------------------------------------
    //  BTN EVENTS
    // --------------------------------------------------------------------------
    voltar(): void {
        this.location.back();
    }

    async registrarTentativasOutbound(): Promise<void> {
        // //debugger
        this.formService.validateAllFields(this.formTentativa);
        if (!this.formTentativa.valid) {
            this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
        return;
        }

        // Validating form
        const formValue: any = this.formTentativa.value;

    }

    async salvarDataFormDadosCliente(): Promise<void> {
        // //debugger
        this.formService.validateAllFields(this.formDadosCliente);
        if (!this.formDadosCliente.valid) {
            this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
        return;
        }

        // Validating form
        const formValue: any = this.formDadosCliente.value;

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

        if (this.formTentativa.get('existeAgendamento').value == 1)
            this.agendamentoSim = 1;
        else
            this.agendamentoSim = 2;
    }

}
