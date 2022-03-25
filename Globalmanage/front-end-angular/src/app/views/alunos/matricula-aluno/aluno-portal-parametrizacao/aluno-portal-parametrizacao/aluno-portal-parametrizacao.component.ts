import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/security/auth.service';
import { select, Store } from '@ngrx/store';
import { AlunoStoreSelectors, AlunoStoreState,AlunoStoreActions } from 'src/app/_store/aluno-store';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';
import { MatriculaAlunoService } from "src/app/services/aluno/matricula-aluno.service";

@Component({
  selector: 'app-aluno-portal-parametrizacao',
  templateUrl: './aluno-portal-parametrizacao.component.html',
  styleUrls: ['./aluno-portal-parametrizacao.component.scss']
})
export class AlunoPortalParametrizacaoComponent implements OnInit {

  isAluno: boolean = false;
  usuario:any;
  matriculaId: number = null;
  materialLiberado:boolean= false;
  form: FormGroup;
  matriculaAluno: any;
  matricula$: Subscription;
  constructor(private authService: AuthService,
    private storeAluno: Store<AlunoStoreState.Aluno>,
    private fb: FormBuilder,
    private matriculaAlunoService: MatriculaAlunoService) { }

  ngOnInit(): void {
    this.getMatricula();
    this.isAluno = this.authService.isAluno();
    this.usuario = this.authService.getToken()
    this.buildForm();
  }
  buildForm(): void {
    this.form = this.fb.group({
      materialLiberado: this.materialLiberado
    });

  }

  changeMaterialLiberado(materialLiberado: { checked: any; }) {

    const data = {
      matriculaId:this.matriculaId,
      materialLiberado :materialLiberado.checked
    }
    this.matriculaAlunoService.AtualizarMaterialLiberado(data).subscribe(val => {
      this.matriculaAluno.materialLiberado = materialLiberado.checked;
      this.storeAluno.dispatch(AlunoStoreActions.updateMatriculaAluno({ payload: this.matriculaAluno }));
      if(val?.status) return;
    });
  }
  getMatricula(): void {
    this.matricula$ = this.storeAluno.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => {
      if (val?.id) {
        this.matriculaId = val?.id;
        this.materialLiberado = val?.materialLiberado;
        this.matriculaAluno = val;
      }
    });
  }
}
