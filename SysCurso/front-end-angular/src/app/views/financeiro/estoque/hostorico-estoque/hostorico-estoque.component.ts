import { Component, OnInit, Inject, OnDestroy, AfterViewChecked, AfterContentChecked } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { HourMinuteMask } from 'src/app/utils/mask/mask';
import { EstoqueService } from 'src/app/services/financeiro/estoque.service';

@Component({
  selector: 'app-hostorico-estoque',
  templateUrl: './hostorico-estoque.component.html',
  styleUrls: ['./hostorico-estoque.component.scss']
})
export class HostoricoEstoqueComponent implements OnInit, OnDestroy, AfterViewChecked, AfterContentChecked {
  hourMinute = HourMinuteMask;
  error: boolean = false;
  isLoadingResults: boolean = false
  historico: any[] = null;
  statusAtual: number = null;
  dataAtual: Date = new Date(2000,0,1);

  constructor(
    public dialogRef: MatDialogRef<HostoricoEstoqueComponent>,
    private estoqueService: EstoqueService,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }
  
  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.loadData();
  }
  
  ngOnDestroy(): void {
    this.dialogRef.close();
  }

  ngAfterViewChecked() {
    this.statusAtual = null;
    this.dataAtual = new Date(2000,0,1);
  }

  ngAfterContentChecked(): void {
    this.statusAtual = null;
    this.dataAtual = new Date(2000,0,1); 
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  loadData(): void {
    this.isLoadingResults = true
    this.estoqueService.getHistorico(this.data['id']).subscribe(val => {
      if (val['status'] === 'error') this.error = true;
      else this.historico = val;
      this.isLoadingResults = false
    })
  }

  mudarStatus(item: any): number {
    const { tipoHistorico, dataCadastro } = item;
    const dateItem = new Date(dataCadastro);

    if (
      dateItem.getUTCDay() !== this.dataAtual.getUTCDay() || 
      dateItem.getUTCMonth() !== this.dataAtual.getUTCMonth() ||
      dateItem.getUTCFullYear() !== this.dataAtual.getUTCFullYear()) {
      this.dataAtual = dateItem;
      this.statusAtual = tipoHistorico;
      return tipoHistorico;
    } 
    else if (this.statusAtual !== tipoHistorico) {
      this.statusAtual = tipoHistorico;
      return tipoHistorico;
    }
    else {
      return null;
    }
  }
}
