import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'app-dados-editados',
    templateUrl: './dados-editados.component.html',
    styleUrls: ['./dados-editados.component.scss']
})
  
export class DadosEditadosComponent {
    constructor(
        public dialogRef: MatDialogRef<DadosEditadosComponent>,
        @Inject(MAT_DIALOG_DATA) public data
    ) { }
    
    close(opcao: number): void {
    this.dialogRef.close(opcao);
    }
}