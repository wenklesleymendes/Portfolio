import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { AlunoStoreState, AlunoStoreSelectors } from 'src/app/_store/aluno-store';
import { AlunoService } from '../../../../../services/aluno/aluno.service';

@Component({
  selector: 'app-aluno-provas-certificados-confirmacao',
  templateUrl: './aluno-provas-certificados-confirmacao.html',
  styleUrls: ['./aluno-provas-certificados-confirmacao.scss']
})
export class AlunoProvasCertificadosConfirmacaoComponent implements OnInit, OnDestroy {
  id: any;
  error: boolean = false;
  isLoadingResults: boolean = false;
  loadingTurma: boolean = false;
  form: FormGroup;
  nome: string = '';
  alunoId: any;
  aluno: any;
  matriculaAluno$: Subscription;
  dataNascimento: string;
  estadoCivil: string;
  sexo: string;
  cpfFormatado: string;
  cepFormatado: string;

  constructor(
    public dialogRef: MatDialogRef<AlunoProvasCertificadosConfirmacaoComponent>,
    private location: Location,
    private fb: FormBuilder,
    private store: Store<AlunoStoreState.Aluno>,
    private alunoService: AlunoService,
    @Inject(MAT_DIALOG_DATA) public data,
    private router: Router
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.matriculaAluno$ = this.store.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => {
      this.alunoId = val.alunoId;
    });
    this.buildForm();

    Promise.all([
    ])
    .then(() => {
      this.isLoadingResults = false;
      this.loadData();
    })
    .catch(() => this.isLoadingResults = false);
  }

  ngOnDestroy(): void {
    this.matriculaAluno$.unsubscribe();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {

  }

  loadData(): void {
    this.alunoService.getPorId(this.alunoId).subscribe(val => {
      if (!val || val.id == 0) return;
      if (val?.status === 'error') return this.error = true;
      this.aluno = val;

      const data = new Date(val.dataNascimento);
      this.dataNascimento = `${data.getDate()}/${data.getMonth() + 1}/${data.getFullYear()}`;
      this.estadoCivil = this.getEstadoCivil(val.estadoCivil);
      this.sexo = this.getSexo(val.sexo);
      this.cpfFormatado = val.cpf.slice(0, 3) + '.' + val.cpf.slice(3, 6) + '.' + val.cpf.slice(6, 9) + '-' + val.cpf.slice(9);
      this.cepFormatado = val.endereco?.cep.slice(0, 5) + '-' + val.endereco?.cep.slice(5);
    })
  }

  getEstadoCivil(val) : string {
    switch(val) {
      case 1:
        return 'Solteiro(a)';
      case 2:
        return 'Casado(a)';
      case 3:
        return 'Viúvo(a)';
      case 4:
        return 'Divorciado(a)';
      default:
        return '';
    }
  }

  getSexo(val): string {
    switch(val) {
      case 1:
        return 'Masculino';
      case 2:
        return 'Feminino';
      case 3:
        return 'Outros';
      default:
        return '';
    }
  }
  
  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------

  close(element): void {
    if(!element) return;
    const formValue = this.form.value;
    let data = { ...element, ...formValue };
    
    this.dialogRef.close(false);
  }

  editarDados(): void {
    this.router.navigateByUrl(`alunos/cadastrar-aluno/adicionar/${this.alunoId}`)
    this.dialogRef.close(false);
  }

  salvarData(): void {
    this.dialogRef.close(true);
  }
}
