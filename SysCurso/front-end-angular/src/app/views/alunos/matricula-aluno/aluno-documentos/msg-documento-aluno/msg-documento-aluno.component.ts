import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-msg-documento-aluno',
  templateUrl: './msg-documento-aluno.component.html',
  styleUrls: ['./msg-documento-aluno.component.scss']
})
export class MsgDocumentoAlunoComponent implements OnInit {
  opcoes: any[] = [
    { tipoRecusa: 1, mensagem: 'R.G. não atualizado. Providenciar R.G atualizado com menos de 10 anos de emissão.' },
    { tipoRecusa: 2, mensagem: 'Documento não legível ou incompleto. Providenciar documento que esteja legível, completo, frente e verso sem corte' },
    { tipoRecusa: 3, mensagem: 'Histórico ou certificado de conclusão do ensino fundamental falta a publicação em Diário Oficial ou publicação no sistema GDAE/SED.' },
    { tipoRecusa: 4, mensagem: 'Histórico ou certificado de conclusão do ensino fundamental falta autenticação em cartório. Providenciar autenticação do documento em cartório.' },
    { tipoRecusa: 5, mensagem: 'O comprovante de endereço apresentado não possui o CEP. Providenciar comprovante de endereço com CEP.' },
    { tipoRecusa: 6, mensagem: 'Documentos apresentados possuem divergência nas informações.' },
    { tipoRecusa: 7, mensagem: 'Documento apresentado não aceito para comprovação de alfabetização. Providenciar documento que seja aceito para comprovação de alfabetização.' },
  ];

  tipoRecusa: string = null;

  constructor(
    private dialogRef: MatDialogRef<MsgDocumentoAlunoComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ) { }

  ngOnInit(): void {
    const data = this.opcoes.find(elem => this.data?.tipoRecusa === elem.tipoRecusa);
    this.tipoRecusa = data?.mensagem ? data?.mensagem : ' - ';
  }

  close(): void {
    this.dialogRef.close();
  }


}