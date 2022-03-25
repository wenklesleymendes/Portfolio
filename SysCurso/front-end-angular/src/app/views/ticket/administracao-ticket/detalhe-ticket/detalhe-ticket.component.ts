import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { DetalheTicketIndividualComponent } from './detalhe-ticket-individual/detalhe-ticket-individual.component';
import { TicketService } from 'src/app/services/ticket/ticket.service';
import { AuthService } from 'src/app/security/auth.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-detalhe-ticket',
  templateUrl: './detalhe-ticket.component.html',
  styleUrls: ['./detalhe-ticket.component.scss']
})
export class DetalheTicketComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  dataSource = new MatTableDataSource([]);
  isLoadingResults: boolean = false;
  error: boolean = false;
  displayedColumns: string[] = [
    'protocolo',
    'assunto',
    'unidade',
    'rm',
    'aluno',
    'dataAbertura',
    'dataAtendimento',
    'sla',
    'status',
    'responsavel',
    'atendente',
    'options'
  ];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private ticketService: TicketService,
    private authService: AuthService,
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

    const usuario = this.authService.getToken();
    const data = { usuarioLogadoId: usuario?.user?.id, statusTickets:1 };

    this.ticketService.filtrarTicket(data).subscribe(val => {
      if (!val) return;
      if (val['status'] === 'error') this.error = true;
      else this.dataSource.data = val;

      this.isLoadingResults = false;
    });
  }

  filtrarPorStatus(statusTickets: number): void {
    this.isLoadingResults = true;
    this.error = false;

    const usuario = this.authService.getToken();
    const data = { usuarioLogadoId: usuario?.user?.id, statusTickets };

    this.ticketService.filtrarTicket(data).subscribe(val => {
      if (!val) return;
      if (val['status'] === 'error') this.error = true;
      else this.dataSource.data = val;

      this.isLoadingResults = false;
    });
  }
  ajustarStatus(status): any {
    if (!status) return { label: ' - ', style: '' };
    else if (status === 1) return { label: 'Aberto', style: 'bg-orange' };
    else if (status === 2) return { label: 'Devolvido', style: 'bg-yellow' };
    else if (status === 3) return { label: 'Em atendimento', style: 'bg-blue' };
    else if (status === 4) return { label: 'Finalizado', style: 'bg-green' };
    else return { label: ' - ', style: '' };
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  openDetalhe(id: number = 0, assunto: string = ''): void {
    const dialogRef = this.dialog.open(DetalheTicketIndividualComponent, {
      width: '90vw',
      data: { id, assunto },
      autoFocus: false,
      panelClass: 'full-width-dialog'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getAll();
      }
    });
  }
}
