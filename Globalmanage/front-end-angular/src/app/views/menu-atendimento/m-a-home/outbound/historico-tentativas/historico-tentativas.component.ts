import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UsuarioService } from 'src/app/services/portal-adm/usuario.service';
import { AuthService } from 'src/app/security/auth.service';
import { Observable } from 'rxjs';

@Component({
    selector: 'app-historico-tentativas',
    templateUrl: './historico-tentativas.component.html',
    styleUrls: ['./historico-tentativas.component.scss']
})

export class HistoricoTentativasComponent {

    nomeUsuario: any;
    error: boolean = false;
    idUnidade: number = 0;
    usuariosUnidade: Observable<any[]>;

    constructor(
        private usuarioService: UsuarioService,
        private authService: AuthService,
        public dialogRef: MatDialogRef<HistoricoTentativasComponent>,
        @Inject(MAT_DIALOG_DATA) public data
    ) {

    }

    ngOnInit(): void {
        const token = this.authService.getToken();

        if (token != null) {
            this.idUnidade = token.user.unidade.id;
        }

        this.data;
        this.getFiltrarUsuarioPorId();
    }

    getFiltrarUsuarioPorId(): void {

        this.usuarioService.getBusarUsuarioPorUnidade({ unidadeId: this.idUnidade, ehAtendimento: true })
            .subscribe(val => {
                if (val['status'] === 'error') {
                    this.error = true;
                }
                else {
                    const usuario = this.authService.getToken();
                    const registro = val;
                    this.usuariosUnidade = registro;
                }
            });
    }

    close(): void {
        this.dialogRef.close();
    }
}