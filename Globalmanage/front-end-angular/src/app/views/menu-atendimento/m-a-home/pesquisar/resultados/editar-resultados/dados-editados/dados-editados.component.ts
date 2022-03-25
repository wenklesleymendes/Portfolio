import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AtendimentoService } from 'src/app/services/Atendimento/Atendimento.service';

@Component({
    selector: 'app-dados-editados',
    templateUrl: './dados-editados.component.html',
    styleUrls: ['./dados-editados.component.scss']
})

export class DadosEditadosComponent {

    sucesso: boolean = true;
    isLoadingResults: boolean = true;

    constructor(
        public dialogRef: MatDialogRef<DadosEditadosComponent>,
        @Inject(MAT_DIALOG_DATA) public data,
        private atendimentoService: AtendimentoService,
    ) { }

    ngOnInit(): void {

        Promise.all([
        ])
            .then(() => {
                this.isLoadingResults = false;
            })
            .catch(() => this.isLoadingResults = false);

        this.salvaDadosAtendimento()
    }

    salvaDadosAtendimento(): void {

        this.isLoadingResults = true;
        this.atendimentoService.editar(this.data.dadosCliente).subscribe(val => {
            this.sucesso = this.validaDados(this.data);
            this.isLoadingResults = false;
        });
        this.sucesso = false;
    }
    goToAgendados() {
        throw new Error('Method not implemented.');
    }


    close(): void {
        this.dialogRef.close();
    }

    validaDados(dados: any): boolean {

        if (dados.dadosCliente.celular === null || dados.dadosCliente.celular === undefined) {
            return false;
        }
        if (dados.dadosCliente.nomedoCliente === null || dados.dadosCliente.nomedoCliente === undefined) {
            return false;
        }

        return true
    }
}