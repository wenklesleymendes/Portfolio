import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialog } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormService } from 'src/app/services/form.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { MatriculaCancelamentoService } from 'src/app/services/aluno/matricula-cancelamento.service';
import { AnexoService } from 'src/app/services/anexo.service';

@Component({
  selector: 'app-matricula-cancelamento-autorizacao',
  templateUrl: './matricula-cancelamento-autorizacao.component.html',
  styleUrls: ['./matricula-cancelamento-autorizacao.component.scss']
})
export class MatriculaCancelamentoAutorizacaoComponent implements OnInit {

  form: FormGroup;
  // Hide password
  pwdhide: boolean = true;
  update: boolean = false;
  motivoIsencao: Number;
  cancelamentoMatricula: any[];
  parccelas: any[];
  disableButton: boolean = false;
  sendingAnexo: boolean = false;
  atestadoAnexado: boolean = false;
  constructor(
    public dialogRef: MatDialogRef<MatriculaCancelamentoAutorizacaoComponent>,
    private dialog: MatDialog,
    private animationService: AnimationsService,
    @Inject(MAT_DIALOG_DATA) public data,
    private formService: FormService,
    private fb: FormBuilder,
    private anexoService: AnexoService,
    private matriculaCancelamentoService: MatriculaCancelamentoService,
  ) { }

  ngOnInit(): void {
    this.cancelamentoMatricula = this.data.cancelamentoMatricula;
    this.parccelas = this.data.parcelas;
    this.buildForm();
  }

  buildForm(): void {
    this.form = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
      motivoIsencao: [null, [Validators.required]],
      justificativa: [null, [Validators.required]]
    });
  }

  autorizarCancelamento() {
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    const { userName, password, motivoIsencao, justificativa, } = this.form.value;


    if (motivoIsencao == 2 && !this.atestadoAnexado) {

      this.animationService.showErrorSnackBar('Atenção: Fazer upload do atestado médico');
      return;
    }
    this.cancelamentoMatricula["motivoIsencao"] = motivoIsencao;

    const data = {
      login: userName,
      senha: password,
      justificativa: justificativa,
      cancelamentoMatricula: this.cancelamentoMatricula,
      pagamentos: this.parccelas
    }

    this.disableButton = true;

    this.matriculaCancelamentoService.salvarAutorizacaoIsencao(data).subscribe(val => {
      if (!val) {
        this.disableButton = false;
        this.animationService.showErrorSnackBar('Usuário não autorizado a isentar cancelamento.');
        return
      }
      if (val['status'] === 'error') {
        this.disableButton = false;
        this.animationService.showErrorSnackBar('Ocorreu uma falha ao tentar salvar.');
        return;
      }
      this.animationService.showSuccessSnackBar('Autorização salva com sucesso.');
      this.dialogRef.close(this.cancelamentoMatricula["matriculaAlunoId"]);
    });
  }

  uploadAtestado(event): void {
    let reader = new FileReader();
    // Select file
    let file: File = event.target.files[0];

    const maxSize: number = 50 * 1024 * 1024;
    if (file?.size > maxSize) {
      this.animationService.showErrorSnackBar('Arquivo ultrapassa 50mb');
      return;
    }

    // Render file
    reader.readAsArrayBuffer(file);

    reader.onloadend = () => {
      this.sendingAnexo = true;
      const formData: FormData = new FormData();

      if (file.type != 'application/pdf') {
        this.animationService.showErrorSnackBar('Insira somente arquivo PDF.');
        return;
      }

      // Set FormData
      formData.append('file', file);
      formData.append('tipoAnexo', "37");
      formData.append('MatriculaAlunoId', this.cancelamentoMatricula["matriculaAlunoId"]);

      this.anexoService.upload(formData).subscribe(val => {
        if (val && val['status'] && val['status'] == 'done') {
          this.sendingAnexo = false;
          this.atestadoAnexado = true;
          this.cancelamentoMatricula["anexoAtestadoMedicoId"] = val?.response?.id
        }
        if (val?.status == 'error') this.sendingAnexo = false;

      });
    };
  }

}
