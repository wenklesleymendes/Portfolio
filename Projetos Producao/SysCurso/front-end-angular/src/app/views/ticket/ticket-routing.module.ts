import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministracaoTicketComponent } from './administracao-ticket/administracao-ticket.component';
import { FirstPageFuncionarioComponent } from './first-page-funcionario/first-page-funcionario.component';
import { PainelTicketComponent } from './painel-ticket/painel-ticket.component';
import { TicketIndividualComponent } from './painel-ticket/ticket-individual/ticket-individual.component';

const routes: Routes = [
    { path: '', redirectTo: 'administracao-ticket', pathMatch: 'full' },
    { path: 'initial', component: FirstPageFuncionarioComponent },
    { path: 'administracao-ticket', component: AdministracaoTicketComponent },
    { path: 'meus-ticket', component: PainelTicketComponent },
    { path: 'meus-ticket/adicionar/:id', component: TicketIndividualComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
})
export class TicketRoutingModule { } 
