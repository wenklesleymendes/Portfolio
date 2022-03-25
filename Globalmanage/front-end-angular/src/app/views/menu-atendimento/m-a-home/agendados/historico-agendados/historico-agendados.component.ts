import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UsuarioService } from 'src/app/services/portal-adm/usuario.service';
import { AuthService } from 'src/app/security/auth.service';
import { Observable } from 'rxjs';
import { AtendimentoService } from 'src/app/services/Atendimento/Atendimento.service';

@Component({
  selector: 'app-historico-agendados',
  templateUrl: './historico-agendados.component.html',
  styleUrls: ['./historico-agendados.component.scss']
})
export class HistoricoAgendadosComponent implements OnInit {

  nomeUsuario: any;
  error: boolean = false;
  idUnidade: number = 0;
  usuariosUnidade: Observable<any[]>;
  contadorAgendamentos: number = this.data.dadosAgendamentos?.length;

  constructor(
    private usuarioService: UsuarioService,
    private authService: AuthService,
    private atendimentoService: AtendimentoService,
    public dialogRef: MatDialogRef<HistoricoAgendadosComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ) { }

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
