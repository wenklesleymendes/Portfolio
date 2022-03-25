import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CriacaoMetaComponent } from './criacao-meta/criacao-meta.component';
import { CriacaoMetaIndividualComponent } from './criacao-meta/criacao-meta-individual/criacao-meta-individual.component';
import { PainelMetasComissoesComponent } from './painel-metas-comissoes/painel-metas-comissoes.component';
import { CriacaoComissaoComponent } from './criacao-comissao/criacao-comissao.component';
import { CriacaoComissaoIndividualComponent } from './criacao-comissao/criacao-comissao-individual/criacao-comissao-individual.component';

const routes: Routes = [
  { path: '', redirectTo: 'administracao-ticket', pathMatch: 'full' },
  { path: 'criacao-metas', component: CriacaoMetaComponent },
  { path: 'criacao-metas/adicionar/:id', component: CriacaoMetaIndividualComponent },
  { path: 'painel-metas-comissoes', component: PainelMetasComissoesComponent },
  { path: 'criacao-comissao', component: CriacaoComissaoComponent },
  { path: 'criacao-comissao/adicionar/:id', component: CriacaoComissaoIndividualComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MetaComissoesRoutingModule { }
