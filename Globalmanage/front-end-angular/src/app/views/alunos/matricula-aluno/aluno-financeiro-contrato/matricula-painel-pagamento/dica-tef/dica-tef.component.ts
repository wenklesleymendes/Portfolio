import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-dica-tef',
  templateUrl: './dica-tef.component.html',
  styleUrls: ['./dica-tef.component.scss']
})
export class DicaTefComponent implements OnInit {
  displayedColumns: string[] = ['label', 'value'];
  dataSource = new MatTableDataSource([
    { label: 'Até R$ 180,00', value: '1 Parcela' },
    { label: 'De R$ 181,00 até R$ 360,00', value: '2 Parcelas' },
    { label: 'De R$ 361,00 até R$ 540,00', value: '4 Parcelas' },
    { label: 'De R$ 541,00 até R$ 700,00', value: '5 Parcelas' },
    { label: 'De R$ 701,00 até R$ 880,00', value: '6 Parcelas' },
    { label: 'Acima de R$ 881,00', value: '12 Parcelas' }
  ]);

  constructor(
    public dialogRef: MatDialogRef<DicaTefComponent>,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  ngOnInit(): void {
  }

}
