import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConsultarAlunosComponent } from './consultar-alunos/consultar-alunos.component';
import { CadastrarAlunosComponent } from './consultar-alunos/cadastrar-alunos/cadastrar-alunos.component';
import { MatriculaAlunoComponent } from './matricula-aluno/matricula-aluno.component';
import { AuthGuard } from 'src/app/security/auth-guard.guard';
import { EjaEnccejaComponent } from './eja-encceja/eja-encceja.component';
import { FirstPageAlunoComponent } from '../initial/initial.component';
import { AlunoFinanceiroContratoComponent } from './matricula-aluno/aluno-financeiro-contrato/aluno-financeiro-contrato.component';
import { AlunoDocumentosComponent } from './matricula-aluno/aluno-documentos/aluno-documentos.component';
import { AlunoSolicitacoesComponent } from './matricula-aluno/aluno-solicitacoes/aluno-solicitacoes.component';
import { AlunoCursoTurmaComponent } from './matricula-aluno/aluno-curso-turma/aluno-curso-turma.component';
import { AlunoPortalComponent } from './matricula-aluno/aluno-portal/aluno-portal.component';


const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '', canActivate: [AuthGuard], data: { aluno: true, cantFuncionario: true } },
  { path: 'consultar-alunos', component: ConsultarAlunosComponent, canActivate: [AuthGuard], data: { aluno: false } },
  { path: 'cadastrar-aluno/adicionar/:id', component: CadastrarAlunosComponent, canActivate: [AuthGuard], data: { aluno: true } },
  { path: 'matricula-aluno', component: MatriculaAlunoComponent, canActivate: [AuthGuard], data: { aluno: true } },
  { path: 'eja-encceja', component: EjaEnccejaComponent },
  // { path: 'no-one', component: ConsultarAlunosComponent },
  { path: 'matricula-aluno/aluno-financeiro-contrato', component: AlunoFinanceiroContratoComponent },
  { path: 'matricula-aluno/aluno-documentos', component: AlunoDocumentosComponent},
  { path: 'matricula-aluno/aluno-solicitacoes', component: AlunoSolicitacoesComponent},
  { path: 'matricula-aluno/aluno-curso-turma', component: AlunoCursoTurmaComponent},
  { path: 'matricula-aluno/aluno-portal', component: AlunoPortalComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AlunosRoutingModule { }
