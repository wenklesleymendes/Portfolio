import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder } from '@angular/forms';
import { OcorrenciaService } from 'src/app/services/ticket/ocorrencia.service';

@Component({
  selector: 'app-aluno-ocorrencia-detalhe',
  templateUrl: './aluno-ocorrencia-detalhe.component.html',
  styleUrls: ['./aluno-ocorrencia-detalhe.component.scss']
})
export class AlunoOcorrenciaDetalheComponent implements OnInit, OnDestroy {

  error: boolean = false;
  isLoadingResults: boolean = false;
  formData: FormData = new FormData();
  form: FormGroup;
  nome: string = '';
  updated: boolean = false;
  statusTicket: number;
  aberturaChamado: Date | string;
  mensagens: any[] = [];
  assunto: string = null;
  constructor(
    public dialogRef: MatDialogRef<AlunoOcorrenciaDetalheComponent>,
    @Inject(MAT_DIALOG_DATA) public data,
    private fb: FormBuilder,
    private ocorrenciaService: OcorrenciaService,) { }

  ngOnInit(): void {
    this.assunto = this.data?.assunto;
    this.isLoadingResults = true;
    Promise.all([
    ])
      .then(() => this.loadData())
      .catch(() => this.isLoadingResults = false)
  }
  ngOnDestroy(): void {
    this.dialogRef.close(this.updated);
  }
  loadData(): void {
    if (this.data.id != 0) {
      this.isLoadingResults = true;
      this.ocorrenciaService.buscarTimeline(this.data.id)
              .subscribe(val => {
          if (!val || val['status'] === 'error') {
            this.error = true
            return;
          }
          const { statusTicket, mensagens, usuarioResponsavel } = val;
          this.statusTicket = statusTicket;
          this.mensagens = mensagens;

          if(usuarioResponsavel?.statusTicket) {
            this.form.get('statusTicket').setValue(usuarioResponsavel.statusTicket);
          }

          if(mensagens.length > 0) this.aberturaChamado = mensagens[0].data;

          this.isLoadingResults = false;
        })
    }
  }
}
