import { Component, OnInit } from '@angular/core';
import { TicketService } from 'src/app/services/ticket/ticket.service';
import { AuthService } from 'src/app/security/auth.service';

@Component({
  selector: 'app-administracao-ticket',
  templateUrl: './administracao-ticket.component.html',
  styleUrls: ['./administracao-ticket.component.scss']
})
export class AdministracaoTicketComponent implements OnInit {
  totalOcorrencias: number;
  resolvidos: number;
  abertos: number;
  atrasados: number;

  constructor(
    private ticketService: TicketService,
    private authService: AuthService
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.getDashboard();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
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
