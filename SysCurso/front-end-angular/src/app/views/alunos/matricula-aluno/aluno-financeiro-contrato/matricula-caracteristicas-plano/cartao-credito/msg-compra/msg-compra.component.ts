import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EventEmitterService } from 'src/app/services/EventEmitterService';
import { AlunoFinanceiroContratoComponent } from 'src/app/views/alunos/matricula-aluno/aluno-financeiro-contrato/aluno-financeiro-contrato.component'


@Component({
  selector: 'app-msg-compra',
  templateUrl: './msg-compra.component.html',
  styleUrls: ['./msg-compra.component.scss']
})
export class MsgCompraComponent implements OnInit {
  success: boolean = false;
  msg: string = null;
  onlyMsg: boolean = false;
  onlyMsgTitle: string = null;
  alunoFinanceiroContratoComponent: AlunoFinanceiroContratoComponent;

  constructor(
    private dialogRef: MatDialogRef<MsgCompraComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ) { }

  ngOnInit(): void {
    this.success = this.data.success;
    this.msg = `<span> ${this.data.msg} </span>`;
    this.onlyMsg = !!this.data.onlyMsg;
    this.onlyMsgTitle = this.data.onlyMsgTitle;

    if (this.success)
    this.alunoFinanceiroContratoComponent.loadData();
  }

  close(): void {
    // EventEmitterService.get('refreshPainelFinanceiro').emit(true);
    this.dialogRef.close(this.success);
  }
}