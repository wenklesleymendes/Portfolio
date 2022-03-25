import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'app-historico-tentativas',
    templateUrl: './historico-tentativas.component.html',
    styleUrls: ['./historico-tentativas.component.scss']
})

export class HistoricoTentativasComponent {
    
    constructor(

        public dialogRef: MatDialogRef<HistoricoTentativasComponent>,
        @Inject(MAT_DIALOG_DATA) public data
    )
    {

    }

    ngOnInit(): void {
    }

    close(): void {
        this.dialogRef.close();
    }
}