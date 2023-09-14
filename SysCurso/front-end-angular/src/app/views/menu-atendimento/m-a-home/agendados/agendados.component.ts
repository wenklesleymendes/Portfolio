import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/security/auth.service';
import { AlunoService } from 'src/app/services/aluno/aluno.service';
import { OutboundService } from 'src/app/services/Atendimento/OutboundService.service';
import { AtendimentoService } from 'src/app/services/Atendimento/Atendimento.service';
import { TelMask, CelMask } from 'src/app/utils/mask/mask';
import { FormService } from 'src/app/services/form.service';
import { Observable } from 'rxjs';


@Component({
    selector: 'app-agendados',
    templateUrl: './agendados.component.html',
    styleUrls: ['./agendados.component.scss']
})

export class AgendadosComponent implements OnInit {
    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    form: FormGroup;
    error: boolean = false;
    filterAtendimentosOutbound: Observable<any[]> = null;
    isLoadingResults: boolean = true;
    idUnidade: string;
    AgendadosColumns: string[] = [
        'Nome',
        'HorarioAgendado',
        'Telefone',
        'Situacao',
        'TipoAgendamento',
        'DataContato',
        'SituacaoContato'
    ];
    AgendadosSource = new MatTableDataSource([]);

    constructor(
        private atendimentoService: AtendimentoService,
        private outboundService: OutboundService,
        private fb: FormBuilder,
        private routerActive: ActivatedRoute,
        private authService: AuthService,
        private router: Router,
    ) { }

    ngOnInit(): void {
        const token = this.authService.getToken();
        if (token != null) {
            this.idUnidade = token.user.unidade.id;
        }
        this.AgendadosSource.paginator = this.paginator;
        this.buildForm();
        Promise.all([
        ])
            .then(() => {
                this.isLoadingResults = false;
            })
            .catch(() => this.isLoadingResults = false);
        this.getAgendamentoAtendimento();
    }

    buildForm(): void {
        this.form = this.fb.group({
            nomedoCliente: [null],
            horaAgendamento: [null],
            dataAgendamento: [null],
            tipoAgendamento: [null],
            celular: [null],
            situacao: [null],
            dataeHoradoUltimoContato: [null],
            situacaoContato: [null]
        })
    }

    getAgendamentoAtendimento() {

        this.isLoadingResults = true;
        this.error = false;

        this.atendimentoService.getAgendamentos(this.idUnidade)
            .subscribe(val => {
                this.isLoadingResults = false;
                if (val['status'] === 'error') this.error = true;
                else {
                    this.AgendadosSource.data = val;
                };
            });
    }

    ajustarMaskTelefone(telefoneFixo: string): string {
        if (!telefoneFixo) return '';
        else if (telefoneFixo.length === 10) return TelMask;
        else if (telefoneFixo.length === 11) return CelMask;
        return '';
    }

    goToRegistroCompleto(): void {
        this.router.navigate(['menu-atendimento/m-a-home/agendados/registro-completo']);
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
}