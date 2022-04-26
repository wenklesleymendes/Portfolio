import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-mpp-detalhe-email-individual',
  templateUrl: './mpp-detalhe-email-individual.component.html',
  styleUrls: ['./mpp-detalhe-email-individual.component.scss']
})
export class MppDetalheEmailIndividualComponent implements OnInit {
  error: boolean = false;
  isLoadingResults: boolean = false;
  displayedColumns: string[] = ['data', 'para', 'options'];
  dataSource = new MatTableDataSource([]);
  corpoEmail: string = null;

  constructor(
    public dialogRef: MatDialogRef<MppDetalheEmailIndividualComponent>,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.corpoEmail = this.data?.corpoEmail
  }


}
