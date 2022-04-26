import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CriarAgendaComponent } from './criar-agenda/criar-agenda.component';
import { CriarAgendaIndividualComponent } from './criar-agenda/criar-agenda-individual/criar-agenda-individual.component';
import { ColegioAutorizadoComponent } from './colegio-autorizado/colegio-autorizado.component';
import { ColegioAutorizadoIndividualComponent } from './colegio-autorizado/colegio-autorizado-individual/colegio-autorizado-individual.component';
import { HistoricoProvaComponent } from './historico-prova/historico-prova.component';

const routes: Routes = [
    { path: '', redirectTo: 'criar-agenda-provas', pathMatch: 'full' },
    { path: 'criar-agenda-provas', component: CriarAgendaComponent },
    { path: 'criar-agenda-provas/adicionar/:id', component: CriarAgendaIndividualComponent },
    { path: 'colegio-autorizado', component: ColegioAutorizadoComponent },
    { path: 'colegio-autorizado/adicionar/:id', component: ColegioAutorizadoIndividualComponent },
    { path: 'historico-prova', component: HistoricoProvaComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
})
export class ProvasRoutingModule { } 
