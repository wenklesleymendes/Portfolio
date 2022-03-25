import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-boleto-aviso2',
  templateUrl: './boleto-aviso2.component.html',
  styleUrls: ['./boleto-aviso2.component.scss']
})
export class BoletoAviso2Component {
  vantagens: string[] = [    
  '1 - Valor do curso é mais barato.',
  '2 - Pode parcelar em até 10x.',
  '3 - Não será bloqueado o limite do cartão.',
  '4 - Acesso a vídeo aulas on-line de imediato.'
  ];

  constructor(
    public dialogRef: MatDialogRef<BoletoAviso2Component>,
    @Inject(MAT_DIALOG_DATA) public data
  ) { }

  navegar(pagina: number): void {
    this.dialogRef.close(pagina);
  }
}
