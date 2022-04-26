import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/security/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmacaoSairComponent } from './confirmacao-sair/confirmacao-sair.component';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
    selector: 'app-m-a-home',
    templateUrl: './m-a-home.component.html',
    styleUrls: ['./m-a-home.component.scss']
})

export class MAHomeComponent implements OnInit {
    
    form: FormGroup;
    idUsuarioLogado: 0;
    unidade : any[];
    nomeUnidade: string;

    isLoadingResults: boolean = false;
    error: boolean = false;

    constructor(
        private authService: AuthService,
        private router: Router,
        private dialog: MatDialog,
        ) { }

    ngOnInit() {
        
        const token = this.authService.getToken();
        if(token != null)
        {
            this.unidade = token.user.unidade;
            this.nomeUnidade = token.user.unidade.nome;
            this.idUsuarioLogado = token.user.id;
        }      
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

    goToOutbound(id: string = '0'): void{
        this.router.navigate(['menu-atendimento/m-a-home/outbound']); //, { state: { matriculaId } }
    }

    goToAgendados(): void{
        this.router.navigate(['menu-atendimento/m-a-home/agendados']); //, { state: { matriculaId } }
    }

    goToContatosPrioritarios(): void{
        this.router.navigate(['menu-atendimento/m-a-home/contatos-prioritarios']); //, { state: { matriculaId } }
    }

    logout(): void {
        this.authService.logout();
        this.router.navigate(['/login']);
    }

    abrirConfirmacaoSair(): void{
        const isMobileResolution = window.innerWidth < 768 ? true : false;
    
        const dialogRef = this.dialog.open(ConfirmacaoSairComponent, {
          width: isMobileResolution ? '95vw' : '90vw',
          autoFocus: false,
          data: {}
        });
        dialogRef.afterClosed().subscribe(result => {
        });
    }

    // buildForm(): void {
        
    // }
}


