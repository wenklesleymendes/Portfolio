import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/security/auth.service';
import { OutboundService } from 'src/app/services/Atendimento/OutboundService.service';
import { AtendimentoService } from 'src/app/services/Atendimento/Atendimento.service';
import { TelMask, CelMask } from 'src/app/utils/mask/mask';
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
    dataAgendamento: string;
    AgendadosColumns: string[] = [
        'Nome',
        'HorarioAgendado',
        'Telefone',
        'Situacao',
        'TipoAgendamento',
        'DataContato',
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
            id: [0],
            idAtendimento: [0],
            nomedoCliente: [null],
            horaAgendamento: [null],
            dataAgendamento: [null],
            tipoAgendamento: [null],
            celular: [null],
            situacao: [null],
            dataeHoradoUltimoContato: [null],
            situacaoContato: [null],
            Data: [null]
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

                    for (let key in val) {

                        val[key].situacao = this.situacaoAgendamento(val[key].situacao);
                    }

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

    goToRegistroCompleto(id: string = '0'): void {

        this.router.navigate(['menu-atendimento/m-a-home/agendados/registro-completo'], { state: { id } });
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

    situacaoAgendamento(tipoSituacao: any): string {

        var retornoSituacao;
        switch (tipoSituacao) {
            case 1:
                return retornoSituacao = "Confirmado";

            case 2:
                return retornoSituacao = "Cliente Está Indeciso";

            case 3:
                return retornoSituacao = "Desempregado";

            case 4:
                return retornoSituacao = "Sem condições financeiras";

            case 5:
                return retornoSituacao = "Para outra pessoa";

            case 6:
                return retornoSituacao = "Confirmar com terceiros";

            case 7:
                return retornoSituacao = "Reagendado";

            case 8:
                return retornoSituacao = "Telefone Incorreto ou Não Existe";

            case 9:
                return retornoSituacao = "Já matriculado em nossa escola";

            case 10:
                return retornoSituacao = "Realizou matricula em outra escola";

            case 11:
                return retornoSituacao = "Não tem mais interesse";

            case 12:
                return retornoSituacao = "Caixa postal";

            case 13:
                return retornoSituacao = "Não atende";

            case 14:
                return retornoSituacao = "Não tem idade para matricula";

            case 15:
                return retornoSituacao = "Outros";

            default:
                return retornoSituacao = "Não Confirmado";

        }
    }

    filtraAgendamentosPorData(): void {

        this.isLoadingResults = true;

        let dataAgendamentos = new Date(this.form.get('Data').value);
        let dataFormatada = dataAgendamentos.toLocaleDateString('pt-BR')

        this.atendimentoService.getFiltraAgendamentosPorData(dataFormatada)
            .subscribe(val => {

                this.isLoadingResults = false;
                let patch: any;
                if (val['status'] === 'error') this.error = true;
                else {

                    for (let key in val) {

                        val[key].situacao = this.situacaoAgendamento(val[key].situacao);
                    }

                    var agendados = val.filter(a => a.unidade === this.idUnidade);

                    this.AgendadosSource = new MatTableDataSource(agendados);
                    this.AgendadosSource.paginator = this.paginator;
                };
            });
    }
}