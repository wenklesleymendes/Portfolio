import { Component, OnInit, ViewChild } from '@angular/core';
import { TicketService } from 'src/app/services/ticket/ticket.service';
import { AuthService } from 'src/app/security/auth.service';
import { DetalheTicketComponent } from '../administracao-ticket/detalhe-ticket/detalhe-ticket.component';


@Component({
  selector: 'app-first-page-funcionario',
  templateUrl: './first-page-funcionario.component.html',
  styleUrls: ['./first-page-funcionario.component.scss']
})
export class FirstPageFuncionarioComponent implements OnInit {
  @ViewChild(DetalheTicketComponent, { static: false }) detalheTicketComponent: DetalheTicketComponent;
  totalOcorrencias: number;
  resolvidos: number;
  abertos: number;
  atrasados: number;

  constructor(
    private ticketService: TicketService,
    private authService: AuthService,

  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.getDashboard();
  }



  filtrarPorStatusTodos() {
    this.detalheTicketComponent.filtrarPorStatus(0);
  }

  filtrarPorStatusFinalisado() {
    this.detalheTicketComponent.filtrarPorStatus(4);
  }

  filtrarPorStatusAberto() {
    this.detalheTicketComponent.filtrarPorStatus(1);
  }

  filtrarPorStatusAtrasado() {
    this.detalheTicketComponent.filtrarPorStatus(5);
  }

  getDashboard() {
    const usuario = this.authService.getToken();
    this.ticketService.getDashboard(usuario?.user?.id)
      .subscribe(val => {
        if (!val) return;
        const { totalOcorrencias, resolvidos, abertos, atrasados } = val;
        this.totalOcorrencias = totalOcorrencias;
        this.resolvidos = resolvidos;
        this.abertos = abertos;
        this.atrasados = atrasados;
      });
  }

}
