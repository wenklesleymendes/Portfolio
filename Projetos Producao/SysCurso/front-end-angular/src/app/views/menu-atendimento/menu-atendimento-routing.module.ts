import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/security/auth-guard.guard';
import { AgendadosComponent } from './m-a-home/agendados/agendados.component';
import { ContatosPrioritatiosComponent } from './m-a-home/contatos-prioritarios/contatos-prioritarios.component';
import { MAHomeComponent } from './m-a-home/m-a-home.component';
import { NovoAtendimentoComponent } from './m-a-home/novo-atendimento/novo-atendimento.component';
import { OutboundComponent } from './m-a-home/outbound/outbound.component';
import { PesquisarComponent } from './m-a-home/pesquisar/pesquisar.component';


const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: '', canActivate: [AuthGuard], data: { aluno: false, cantFuncionario: true } },
    { path: './m-a-home', component: MAHomeComponent },
    { path: './m-a-home/novo-atendimento', component: NovoAtendimentoComponent },
    { path: './m-a-home/pesquisar', component: PesquisarComponent},
    { path: './m-a-home/outbound', component: OutboundComponent},
    { path: './m-a-home/agendados', component: AgendadosComponent},
    { path: './m-a-home/contatos-prioritarios', component: ContatosPrioritatiosComponent}
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
  })
  export class MenuAtendimentoRoutingModule { }