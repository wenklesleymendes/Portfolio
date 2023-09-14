import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { DeleteService } from 'src/app/services/delete.service';
import { CriacaoAssuntosIndividualComponent } from './criacao-assuntos-individual/criacao-assuntos-individual.component';
import { MatDialog } from '@angular/material/dialog';
import { AssuntoTicketService } from 'src/app/services/ticket/assunto-ticket.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-criacao-assuntos',
  templateUrl: './criacao-assuntos.component.html',
  styleUrls: ['./criacao-assuntos.component.scss']
})
export class CriacaoAssuntosComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  dataSource = new MatTableDataSource([]);
  isLoadingResults: boolean = false;
  error: boolean = false;
  displayedColumns: string[] = ['descricao', 'sla', 'options'];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private assuntoTicket: AssuntoTicketService,
    private deleteService: DeleteService,
    private dialog: MatDialog
  ) { }
  
  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.getAll();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getAll() {
    this.isLoadingResults = true;
    this.error = false;

    this.assuntoTicket.getAll()
    .subscribe(val => {
      if (!val) return;
      if (val['status'] === 'error') this.error = true;
      else this.dataSource.data = val;
      this.isLoadingResults = false;
    });
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  openAssunto(id: number = 0, element: any = null): void {
    const dialogRef = this.dialog.open(CriacaoAssuntosIndividualComponent, {
      width: '90vw',
      data: { id, element },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getAll();
    });
  }

  delete(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.assuntoTicket.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
