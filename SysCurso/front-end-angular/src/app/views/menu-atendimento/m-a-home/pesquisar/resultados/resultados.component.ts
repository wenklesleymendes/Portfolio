import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';


@Component({
  selector: 'app-resultados',
  templateUrl: './resultados.component.html',
  styleUrls: ['./resultados.component.scss']
})

export class ResultadosComponent {
    historicoColumns: string[] = ['NomeCliente', 'StatusdoCliente', 'TelefoneCliente'];


    constructor(
        private router: Router,
        private location: Location,
    ) {}

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

    goToResultados(): void {
        this.router.navigate(['menu-atendimento/m-a-home/pesquisar/resultados']); //, { state: { matriculaId } }
    }
    
    goToEditarResultados(): void{
        this.router.navigate(['menu-atendimento/m-a-home/pesquisar/resultados/editar-resultados']); //, { state: { matriculaId } }
    }
}