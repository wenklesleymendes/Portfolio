import { Component, OnInit, Input } from '@angular/core';
import { FolhaPagamentoService } from 'src/app/services/financeiro/folha-pagamento.service';

@Component({
  selector: 'app-folha-pagamento-transacao',
  templateUrl: './folha-pagamento-transacao.component.html',
  styleUrls: ['./folha-pagamento-transacao.component.scss']
})
export class FolhaPagamentoTransacaoComponent implements OnInit {
  @Input() id: number;
  constructor(
    private folhaPagamentoService: FolhaPagamentoService
  ) { }

  ngOnInit(): void {
  }

  download(): void {
    this.folhaPagamentoService.download(this.id);
  }
}
