import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { AlunoFinanceiroService } from 'src/app/services/aluno/aluno-financeiro.service';
import { MppDetalheEmailIndividualComponent } from './mpp-detalhe-email-individual/mpp-detalhe-email-individual.component';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-mpp-detalhe-email',
  templateUrl: './mpp-detalhe-email.component.html',
  styleUrls: ['./mpp-detalhe-email.component.scss']
})
export class MppDetalheEmailComponent implements OnInit {
  error: boolean = false;
  isLoadingResults: boolean = false;
  displayedColumns: string[] = ['data', 'hora', 'para', 'options'];
  dataSource = new MatTableDataSource([]);
  selection = new SelectionModel<any>(true, []);

  constructor(
    public dialogRef: MatDialogRef<MppDetalheEmailComponent>,
    private alunoFinanceiroService: AlunoFinanceiroService,
    private dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.getData();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getData(): void {
    this.alunoFinanceiroService.consultarEmail(this.data?.id).subscribe(val => this.dataSource.data = val);
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  openDetalhePagamentoIndividual(corpoEmail?: any, dataEnvio?: string): void {
    const dialogRef = this.dialog.open(MppDetalheEmailIndividualComponent, {
      width: '50vw',
      autoFocus: false,
      data: { corpoEmail, dataEnvio }
    });
    dialogRef.afterClosed().subscribe(result => {
      
    });
  }

}
