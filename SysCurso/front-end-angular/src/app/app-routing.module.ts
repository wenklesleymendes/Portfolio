import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './security/auth-guard.guard';

import { LoginComponent } from './views/login/login.component';
import { Page404Component } from './views/page404/page404.component';
import { BodyComponent } from './views/body/body.component';

import { FirstPageAlunoComponent } from './views/initial/initial.component';

import { MAHomeComponent } from './views/menu-atendimento/m-a-home/m-a-home.component';
import { NovoAtendimentoComponent } from './views/menu-atendimento/m-a-home/novo-atendimento/novo-atendimento.component';
import { PesquisarComponent } from './views/menu-atendimento/m-a-home/pesquisar/pesquisar.component';
import { AgendadosComponent } from './views/menu-atendimento/m-a-home/agendados/agendados.component';
import { OutboundComponent } from './views/menu-atendimento/m-a-home/outbound/outbound.component';
import { ContatosPrioritatiosComponent } from './views/menu-atendimento/m-a-home/contatos-prioritarios/contatos-prioritarios.component';
import { ResultadosComponent } from './views/menu-atendimento/m-a-home/pesquisar/resultados/resultados.component';
import { EditarResultadosComponent } from './views/menu-atendimento/m-a-home/pesquisar/resultados/editar-resultados/editar-resultados.component';
import { RegistroCompletoComponent } from './views/menu-atendimento/m-a-home/agendados/registro-completo/registro-completo.component';
import { PortalComponent } from './views/afiliado/portal/portal.component';
import { LojaComponent } from './views/afiliado/loja/loja.component';
import { AfiliacaoComponent } from './views/afiliado/afiliacao/afiliacao.component';
import { AfiliadoComponent } from './views/afiliado/afiliado.component';
import { AHomeComponent } from './views/afiliado/modulos/home/a-home.component';


const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'initial', component: FirstPageAlunoComponent},
  { path: 'afiliado', component: AfiliadoComponent},
  { path: 'afiliado/portal', component: PortalComponent},
  { path: 'afiliado/loja', component: LojaComponent},
  { path: 'afiliado/afiliacao', component: AfiliacaoComponent},
  { path: 'afiliado/home', component: AHomeComponent},
  { path: 'menu-atendimento/m-a-home', component: MAHomeComponent},
  { path: 'menu-atendimento/m-a-home/novo-atendimento', component: NovoAtendimentoComponent },
  { path: 'menu-atendimento/m-a-home/pesquisar', component: PesquisarComponent },
  { path: 'menu-atendimento/m-a-home/pesquisar/resultados', component: ResultadosComponent },
  { path: 'menu-atendimento/m-a-home/pesquisar/resultados/editar-resultados', component: EditarResultadosComponent },
  { path: 'menu-atendimento/m-a-home/outbound', component: OutboundComponent },
  { path: 'menu-atendimento/m-a-home/agendados', component: AgendadosComponent },
  { path: 'menu-atendimento/m-a-home/agendados/registro-completo', component: RegistroCompletoComponent },
  { path: 'menu-atendimento/m-a-home/contatos-prioritarios', component: ContatosPrioritatiosComponent },
  { path: '', component: BodyComponent, canActivateChild: [AuthGuard], children: [
    { path: 'gerenciador', canActivate: [AuthGuard], data: { aluno: false, menu: 'gerenciador' },
      loadChildren: () => import('./views/gerenciador/gerenciador.module').then(mod => mod.GerenciadorModule) },
    { path: 'rh', canActivate: [AuthGuard], data: { aluno: false, menu: 'rh' },
      loadChildren: () => import('./views/rh/rh.module').then(mod => mod.RhModule) },
    { path: 'portal-adm', canActivate: [AuthGuard], data: { aluno: false, menu: 'portal-adm' },
      loadChildren: () => import('./views/portal-administrativo/portal-administrativo.module').then(mod => mod.PortalAdministrativoModule) },
    { path: 'ticket', canActivate: [AuthGuard], data: { aluno: false, menu: 'ticket' },
      loadChildren: () => import('./views/ticket/ticket.module').then(mod => mod.TicketModule) },
    { path: 'metas-comissoes', canActivate: [AuthGuard], data: { aluno: false, menu: 'metas-comissoes' },
      loadChildren: () => import('./views/meta-comissoes/meta-comissoes.module').then(mod => mod.MetaComissoesModule) },
    { path: 'financeiro', canActivate: [AuthGuard], data: { aluno: false, menu: 'financeiro' },
      loadChildren: () => import('./views/financeiro/financeiro.module').then(mod => mod.FinanceiroModule) },
    { path: 'alunos', canActivate: [AuthGuard], data: { aluno: true, menu: 'alunos' },
      loadChildren: () => import('./views/alunos/alunos.module').then(mod => mod.AlunosModule) },
    { path: 'provas', canActivate: [AuthGuard], data: { aluno: false, menu: 'provas' },
      loadChildren: () => import('./views/provas/provas.module').then(mod => mod.ProvasModule) },
    { path: 'aula-online', canActivate: [AuthGuard], data: { aluno: true, menu: 'aula-online' },
      loadChildren: () => import('./views/aula-online/aula-online.module').then(mod => mod.AulaOnlineModule) },
  ]},
  { path: '**', component: Page404Component }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
