import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CriarAulaComponent } from './criar-aula/criar-aula.component';
import { MateriasOnlineComponent } from './criar-aula/materias-online/materias-online.component';
import { MateriasOnlineInitComponent } from './criar-aula/materias-online/materias-online-init/materias-online-init.component';
import { MinhaAulaComponent } from './minha-aula/minha-aula.component';
import { PerguntasOnlineComponent } from './criar-aula/materias-online/materias-online-init/perguntas-online/perguntas-online.component';
import { PerguntasOnlineIndividualComponent } from './criar-aula/materias-online/materias-online-init/perguntas-online/perguntas-online-individual/perguntas-online-individual.component';

const routes: Routes = [
  { path: '', redirectTo: 'criar-aula-online', pathMatch: 'full' },
  { path: 'criar-aula-online', component: CriarAulaComponent },
  { path: 'materias-online/:id', component: MateriasOnlineComponent },
  { path: 'materias-online/:aulaOnlineId/:materiaId', component: MateriasOnlineInitComponent },
  { path: 'materias-online/:aulaOnlineId/:materiaId/:videoId', component: PerguntasOnlineComponent },
  { path: 'materias-online/:aulaOnlineId/:materiaId/:videoId/:perguntaId', component: PerguntasOnlineIndividualComponent },
  { path: 'minhas-aulas', component: MinhaAulaComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AulaOnlineRoutingModule { }
