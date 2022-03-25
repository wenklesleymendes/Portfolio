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
    alunoJaCadastrado: boolean = false;
    agendamentoSim: number = 0;
    nomeUsuario: string;

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
    ) {
        // Get id
        const id = this.routerActive.snapshot.paramMap.get('id');
        this.id = id ? parseInt(id) : 0;
    }

    // --------------------------------------------------------------------------
    //  LIFECYCLE HOOKS
    // --------------------------------------------------------------------------
    ngOnInit(): void {
        //debugger
        
        this.buildForm();

        const token = this.authService.getToken();
        if(token != null)
        {
            // debugger
            this.unidadesDefault = token.user.unidade;
            this.nomeUnidade = token.user.unidade.nome;
            this.idUsuarioLogado = token.user.id;
            this.nomeUsuario = token.user.userName;
        }   

        Promise.all([ ]).then(() => {
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
        dataeHoradoAtendimento: [Date, [Validators.required]],
        usuarioLogado: [null, [Validators.required]],
        canaldeAtendimento: [null, [Validators.required]],
        nomedoCliente: [null, [Validators.required]],
        cursodeInteresse: [null, [Validators.required]],
        celular: [null, [Validators.required]],
        telefoneFixo: [null, [Validators.required]],
        periodo: [null, [Validators.required]],
        comonosConheceu: [null, [Validators.required]],
        agendamentodaMatricula: [null, [Validators.required]],
        email: [null, [Validators.required]],
        motivodeInteressenoCurso: [null, [Validators.required]],
        diadoAgendamento: [null, [Validators.required]],
        horadoAgendamento: [null, [Validators.required]],
        motivodoNaoAgendamento: [null, [Validators.required]],
        observacoes: [null, [Validators.required]],
        usuarioCadastro: [null, [Validators.required]],
        unidadeCadastro: [null, [Validators.required]],
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
        // debugger
        this.formService.validateAllFields(this.form);
        if (!this.form.valid) {
            this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
            return;
        }
    }

    abrirQuaseLa(): void{

        const isMobileResolution = window.innerWidth < 768 ? true : false;
        var dadosAlunoForm = this.form.value;

        const dialogRef = this.dialog.open(QuaseLaComponent, {
            width: isMobileResolution ? '95vw' : '90vw',
            autoFocus: false,
            data: { dadosAlunoForm }
        });
            dialogRef.afterClosed().subscribe(result => {
        });
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

    agendamentoSimNao(): void {
        
        if (this.form.get('agendamentodaMatricula').value == 1)
            this.agendamentoSim = 1;

        if (this.form.get('agendamentodaMatricula').value == 2)
            this.agendamentoSim = 2;

        if (this.form.get('agendamentodaMatricula').value == 3)
            this.agendamentoSim = 3;
    }
}
