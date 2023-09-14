import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { OcorrenciaService } from 'src/app/services/ticket/ocorrencia.service';
import { AlunoStoreSelectors, AlunoStoreState } from 'src/app/_store/aluno-store';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { AuthService } from 'src/app/security/auth.service';
import { Subscription } from 'rxjs';
import { select, Store } from '@ngrx/store';

@Component({
  selector: 'app-aluno-ocorrencia-novo',
  templateUrl: './aluno-ocorrencia-novo.component.html',
  styleUrls: ['./aluno-ocorrencia-novo.component.scss']
})
export class AlunoOcorrenciaNovoComponent implements OnInit {
  id: number = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;
  formData: FormData = new FormData();
  form: FormGroup;
  disableButton: boolean = false;
  matricula$: Subscription;
  matriculaId: any;
  update = false;
  constructor(
    public dialogRef: MatDialogRef<AlunoOcorrenciaNovoComponent>,
    @Inject(MAT_DIALOG_DATA) public data,
    private fb: FormBuilder,
    private storeAluno: Store<AlunoStoreState.Aluno>,
    private ocorrenciaService: OcorrenciaService,
    private animationsService: AnimationsService,
    private authService: AuthService,
    private formService: FormService,
  ) { }



  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.id = this.data?.id;
    this.buildForm();
    Promise.all([
      this.getMatricula(),
    ])
      .catch(() => this.isLoadingResults = false)
  }

  ngOnDestroy(): void {
    this.matricula$.unsubscribe();

    this.dialogRef.close(this.update);
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------

  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      assunto: [null, [Validators.required]],
      mensagem: [null, [Validators.required]],
    });
  }

  getMatricula(): void {
    this.matricula$ = this.storeAluno.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => {
      if (val?.id) {
        this.matriculaId = val?.id;
      }
    });
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  salvarData(): void {
    this.disableButton = true;
    // Validating form
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      this.disableButton = false;
      return;
    }

    const { assunto, mensagem } = this.form.value;

    const usuario = this.authService.getToken();

    if (!usuario) {
      this.animationsService.showErrorSnackBar('Favor logar no sistema');
      this.disableButton = false;
      return;
    }

    const data = {
      id: this.id,
      assunto,
      matriculaId: this.matriculaId,
      ticketId: this.data.id,
      usuarioLogadoId: usuario?.user?.id,
      mensagem
    };

    // Make request
    this.ocorrenciaService.cadastrar(data).subscribe(val => {
      if (!val || val?.status === 'error') {
        this.disableButton = false;
        return this.error = true;
      }
      this.animationsService.showSuccessSnackBar('Salvo com sucesso');
      this.form.reset();
      this.dialogRef.close();
      this.update = true;
    })
  }


}
