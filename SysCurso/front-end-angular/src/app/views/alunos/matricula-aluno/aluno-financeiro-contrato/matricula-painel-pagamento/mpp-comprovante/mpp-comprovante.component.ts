import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TefService } from 'src/app/services/tef/tef.service';

@Component({
  selector: 'app-mpp-comprovante',
  templateUrl: './mpp-comprovante.component.html',
  styleUrls: ['./mpp-comprovante.component.scss']
})
export class MppComprovanteComponent implements OnInit {

  comprovante: string = null;

  constructor(
    public dialogRef: MatDialogRef<MppComprovanteComponent>,
    private tefService: TefService,
    @Inject(MAT_DIALOG_DATA) public data
  ) { }

  ngOnInit(): void {
    const val: string = this.data?.row?.comprovanteCartao;
    const regExp = /\n/gi;
    if(val) this.comprovante = val.replace(regExp, '<br/>')
  }

  close(opcao: number): void {
    this.dialogRef.close(opcao);
  }

  imprimirComprovante(): void {
    const data = {
      comprovante: this.data?.row?.comprovanteCartao,
      pagamentoId: this.data?.row?.id
    }
    this.tefService.imprimirComprovante(data).subscribe();
  }
}
