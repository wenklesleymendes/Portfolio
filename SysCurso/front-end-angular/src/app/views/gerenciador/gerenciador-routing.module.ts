import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UnidadesComponent } from './unidades/unidades.component';
import { CursosTurmasComponent } from './cursos-turmas/cursos-turmas.component';
import { PlanosPagamentosComponent } from './planos-pagamentos/planos-pagamentos.component';
import { PromocoesBolsasConvenioComponent } from './promocoes-bolsas-convenio/promocoes-bolsas-convenio.component';
import { UnidadeIndividualComponent } from './unidades/unidade-individual/unidade-individual.component';
import { TurmaIndividualComponent } from './cursos-turmas/turma/turma-individual/turma-individual.component';
import { PlanoPagamentoIndividualComponent } from './planos-pagamentos/plano-pagamento-individual/plano-pagamento-individual.component';
import { PromocoesBolsasConvenioIndividualComponent } from './promocoes-bolsas-convenio/promocoes-bolsas-convenio-individual/promocoes-bolsas-convenio-individual.component';
import { CursoIndividualComponent } from './cursos-turmas/curso/curso-individual/curso-individual.component';
import { SolicitacoesComponent } from './solicitacoes/solicitacoes.component';
import { SolicitacoesIndividualComponent } from './solicitacoes/solicitacoes-individual/solicitacoes-individual.component';

const routes: Routes = [
    { path: '', redirectTo: 'unidades', pathMatch: 'full' },
    { path: 'unidades', component: UnidadesComponent },
    { path: 'unidades/adicionar/:id', component: UnidadeIndividualComponent },
    { path: 'curso-turmas', component: CursosTurmasComponent },
    { path: 'curso-turmas/adicionar/:id', component: TurmaIndividualComponent },
    { path: 'curso-turmas/adicionar-curso/:id', component: CursoIndividualComponent },
    { path: 'planos-pagamentos', component: PlanosPagamentosComponent },
    { path: 'planos-pagamentos/adicionar/:id', component: PlanoPagamentoIndividualComponent },
    { path: 'promocoes-bolsas-convenio', component: PromocoesBolsasConvenioComponent },
    { path: 'promocoes-bolsas-convenio/adicionar/:id', component: PromocoesBolsasConvenioIndividualComponent },
    { path: 'solicitacoes', component: SolicitacoesComponent },
    { path: 'solicitacoes/adicionar/:id', component: SolicitacoesIndividualComponent },
    
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
})
export class UnidadesRoutingModule { } 
