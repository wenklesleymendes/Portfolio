import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-detalhe-historico',
  templateUrl: './detalhe-historico.component.html',
  styleUrls: ['./detalhe-historico.component.scss']
})
export class DetalheHistoricoComponent implements OnInit {
  @Input() historicoDespesa: any;
  constructor() { }

  ngOnInit(): void {
  }

}
