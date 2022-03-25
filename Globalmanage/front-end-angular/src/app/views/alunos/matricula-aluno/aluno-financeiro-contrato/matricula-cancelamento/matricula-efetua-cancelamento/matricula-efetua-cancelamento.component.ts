import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialog } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormService } from 'src/app/services/form.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { MatriculaCancelamentoService } from 'src/app/services/aluno/matricula-cancelamento.service';
import { AnexoService } from 'src/app/services/anexo.service';

@Component({
  selector: 'app-matricula-efetua-cancelamento',
  templateUrl: './matricula-efetua-cancelamento.component.html',
  styleUrls: ['./matricula-efetua-cancelamento.component.scss']
})
export class MatriculaEfetuaCancelamentoComponent implements OnInit {
  form: FormGroup;
  update: boolean = false;
  sendingAnexo: boolean = false;
  cartaAnexa: boolean = false;
  msg: string = null;
  cancelamentoMatricula: any[];
  constructor(
    public dialogRef: MatDialogRef<MatriculaEfetuaCancelamentoComponent>,
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
    this.cartaAnexa = this.cancelamentoMatricula["anexoCartaCancelamentoId"] > 0;
    if (!this.cartaAnexa)
      this.msg = "Efetue o upload da carta de cancelamento para dar continuidade."
    this.buildForm();
  }

  buildForm(): void {
    this.form = this.fb.group({

    });
  }

  closeModal() {
    this.dialogRef.close(this.cancelamentoMatricula["anexoCartaCancelamentoId"]);
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
          this.cancelamentoMatricula["anexoCartaCancelamentoId"] = val?.response?.id;
          this.cancelamentoMatricula["validar"] = true;
          this.matriculaCancelamentoService.efetuarCancelamento(this.cancelamentoMatricula).subscribe(val => {
            if (!val || val['status'] === 'error') {
              return;
            }
            this.animationService.showSuccessSnackBar('Upload efetuado.');
            if (val.mensagem) {
              this.msg = val.mensagem;
              this.cartaAnexa = true;
            }
            else
              this.closeModal();

          });
        }
        if (val?.status == 'error') this.sendingAnexo = false;

      });
    };
  }
}
