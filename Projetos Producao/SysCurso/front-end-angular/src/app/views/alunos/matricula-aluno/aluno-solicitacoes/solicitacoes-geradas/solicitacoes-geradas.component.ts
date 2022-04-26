import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-solicitacoes-geradas',
  templateUrl: './solicitacoes-geradas.component.html',
  styleUrls: ['./solicitacoes-geradas.component.scss']
})
export class SolicitacoesGeradasComponent {
  constructor(
    public dialogRef: MatDialogRef<SolicitacoesGeradasComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ) { }

  close(opcao: number): void {
    this.dialogRef.close(opcao);
  }
}
