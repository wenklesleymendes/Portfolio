import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-as-opcoes',
  templateUrl: './as-opcoes.component.html',
  styleUrls: ['./as-opcoes.component.scss']
})
export class AsOpcoesComponent {
  constructor(
    public dialogRef: MatDialogRef<AsOpcoesComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ) { }

  close(opcao: number): void {
    this.dialogRef.close(opcao);
  }
}
