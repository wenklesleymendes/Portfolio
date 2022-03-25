import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-folha-pagamento-historico',
  templateUrl: './folha-pagamento-historico.component.html',
  styleUrls: ['./folha-pagamento-historico.component.scss']
})
export class FolhaPagamentoHistoricoComponent implements OnInit {
  @Input() historico: any;
  constructor() { }

  ngOnInit(): void {
  }

}
