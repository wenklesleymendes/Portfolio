import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CadastroFornecedorClienteComponent } from './cadastro-fornecedor-cliente/cadastro-fornecedor-cliente.component';
import { CadastroFornecedorClienteIndividualComponent } from './cadastro-fornecedor-cliente/cadastro-fornecedor-cliente-individual/cadastro-fornecedor-cliente-individual.component';
import { ContasPagarComponent } from './contas-pagar/contas-pagar.component';
import { ContasPagarIndividualComponent } from './contas-pagar/contas-pagar-individual/contas-pagar-individual.component';
import { EstoqueComponent } from './estoque/estoque.component';
import { EstoqueIndividualComponent } from './estoque/estoque-individual/estoque-individual.component';
import { FolhaPagamentoComponent } from './folha-pagamento/folha-pagamento.component';
import { FolhaPagamentoIndividualComponent } from './folha-pagamento/folha-pagamento-individual/folha-pagamento-individual.component';
import { EstoqueItemComponent } from './estoque/estoque-item/estoque-item.component';

const routes: Routes = [
  { path: '', redirectTo: 'cadastro-fornecedor-cliente', pathMatch: 'full' },
  { path: 'cadastro-fornecedor-cliente', component: CadastroFornecedorClienteComponent },
  { path: 'cadastro-fornecedor-cliente/adicionar/:id', component: CadastroFornecedorClienteIndividualComponent },
  { path: 'estoque', component: EstoqueComponent },
  { path: 'estoque/adicionar/:id', component: EstoqueIndividualComponent },
  { path: 'estoque/item/:id', component: EstoqueItemComponent },
  { path: 'contas-pagar', component: ContasPagarComponent },
  { path: 'contas-pagar/adicionar/:id', component: ContasPagarIndividualComponent },
  { path: 'folha-pagamento', component: FolhaPagamentoComponent },
  { path: 'folha-pagamento/adicionar/:id', component: FolhaPagamentoIndividualComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FinanceiroRoutingModule { }
