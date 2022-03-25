import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { Navigation, Router } from '@angular/router';
import { Location } from '@angular/common';
import { MatPaginator } from '@angular/material/paginator';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from 'src/app/security/auth.service';
import { AtendimentoService } from 'src/app/services/Atendimento/Atendimento.service';
import { CelMask, TelMask } from 'src/app/utils/mask/mask';
import { Observable } from 'rxjs';


@Component({
    selector: 'app-resultados',
    templateUrl: './resultados.component.html',
    styleUrls: ['./resultados.component.scss']
})

export class ResultadosComponent implements OnInit {
    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    form: FormGroup;
    error: boolean = false;
    filterAtendimentosOutbound: Observable<any[]> = null;
    isLoadingResults: boolean = true;
    idUnidade: string;
    filtro: any;
    historicoColumns: string[] = [
        'nomedoCliente',
        'status',
        'celular',
    ];
    historicoSource = new MatTableDataSource([]);


    constructor(
        private router: Router,
        private location: Location,
        private atendimentoService: AtendimentoService,
        private fb: FormBuilder,
        private authService: AuthService,
    ) {
        const currentNavigation: Navigation = this.router.getCurrentNavigation();
        if (currentNavigation && currentNavigation.extras && currentNavigation.extras.state) {
            const state = currentNavigation.extras.state;
            this.filtro = state.formValue;
        }
    }

    ngOnInit(): void {

        const token = this.authService.getToken();
        if (token != null) {
            this.idUnidade = token.user.unidade.id;
        }
        this.historicoSource.paginator = this.paginator;
        this.buildForm();
        Promise.all([
        ])
            .then(() => {
                this.isLoadingResults = false;
            })
            .catch(() => this.isLoadingResults = false);
        this.getAtendimentos();

    }

    buildForm(): void {
        this.form = this.fb.group({
            id: [null],
            nomedoCliente: [null],
            celular: [null],
            status: [null],
        })
    }

    ajustarMaskTelefone(telefoneFixo: string): string {
        if (!telefoneFixo) return '';
        else if (telefoneFixo.length === 10) return TelMask;
        else if (telefoneFixo.length === 11) return CelMask;
        return '';
    }

    getAtendimentos(): void {

        this.isLoadingResults = true;
        this.error = false;

        this.atendimentoService.getFiltraAtendimentos(this.filtro)
            .subscribe(val => {
                this.isLoadingResults = false;
                if (val['status'] === 'error') this.error = true;
                else {

                    this.historicoSource.data = val;
                };
            });
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

    goToEditarResultados(id: string = '0'): void {
        this.router.navigate(['menu-atendimento/m-a-home/pesquisar/resultados/editar-resultados'], { state: { id } }); //, { state: { matriculaId } }
    }
}