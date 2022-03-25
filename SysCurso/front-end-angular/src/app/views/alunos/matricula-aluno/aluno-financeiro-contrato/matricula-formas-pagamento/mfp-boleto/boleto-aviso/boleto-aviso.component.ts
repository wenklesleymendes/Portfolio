import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AuthService } from 'src/app/security/auth.service';

@Component({
  selector: 'app-boleto-aviso',
  templateUrl: './boleto-aviso.component.html',
  styleUrls: ['./boleto-aviso.component.scss']
})
export class BoletoAvisoComponent implements OnInit {
  isAluno: boolean = false;
  vantagens: string[] = [
    '1 - Valor do curso é mais barato.',
    '2 - Pode parcelar em até 12x.',
    '3 - Acesso a vídeo aulas on-line de imediato.',
    '4 - Apostila impressa gratuita. ',
    '5 - Agendamento de provas de imediato.',
  ];

  constructor(
    public dialogRef: MatDialogRef<BoletoAvisoComponent>,
    private authService: AuthService,
    @Inject(MAT_DIALOG_DATA) public data
  ) { }

  ngOnInit(): void {
    this.isAluno = this.authService.isAluno();
  }

  navegar(pagina: number): void {
    this.dialogRef.close(pagina);
  }
}
