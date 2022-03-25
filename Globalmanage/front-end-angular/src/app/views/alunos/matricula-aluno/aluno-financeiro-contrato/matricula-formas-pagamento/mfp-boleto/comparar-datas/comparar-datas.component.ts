import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'app-comparar-datas',
    templateUrl: './comparar-datas.component.html',
    styleUrls: ['./comparar-datas.component.scss']
})

export class CompararDatasComponent {

    constructor(
        public dialogRef: MatDialogRef<CompararDatasComponent>,
        @Inject(MAT_DIALOG_DATA) public data
    ) { }
    
    close(): void {
        this.dialogRef.close();
    }
}