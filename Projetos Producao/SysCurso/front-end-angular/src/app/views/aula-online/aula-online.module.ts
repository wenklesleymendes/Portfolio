import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AulaOnlineRoutingModule } from './aula-online-routing.module';
import { MaterialModule } from 'src/app/material.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UtilsComponentModule } from 'src/app/utils/components/utils-components.module';
import { PipeModule } from 'src/app/utils/pipes/pipe.module';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { AuthService } from 'src/app/security/auth.service';
import { CriarAulaComponent } from './criar-aula/criar-aula.component';
import { AddCursoOnlineComponent } from './criar-aula/add-curso-online/add-curso-online.component';
import { MateriasOnlineComponent } from './criar-aula/materias-online/materias-online.component';
import { MateriasOnlineInitComponent } from './criar-aula/materias-online/materias-online-init/materias-online-init.component';
import { VideoAulaService } from 'src/app/services/aula-online/video-aula.service';
import { PreviewVideoComponent } from './criar-aula/materias-online/materias-online-init/preview-video/preview-video.component';
import { PerguntaService } from 'src/app/services/aula-online/pergunta.service';
import { MinhaAulaComponent } from './minha-aula/minha-aula.component';
import { QuestionarioOnlineComponent, MensagemRespostaComponent } from './minha-aula/questionario-online/questionario-online.component';
import { VimeoAulaOnlineComponent } from './minha-aula/vimeo-aula-online/vimeo-aula-online.component';
import { PerguntasOnlineIndividualComponent } from './criar-aula/materias-online/materias-online-init/perguntas-online/perguntas-online-individual/perguntas-online-individual.component';
import { PerguntasOnlineComponent } from './criar-aula/materias-online/materias-online-init/perguntas-online/perguntas-online.component';
import { QuestaoOnlineComponent } from './minha-aula/questionario-online/questao-online/questao-online.component';
import { QuestionarioStateService } from './minha-aula/questionario-online/questionario-state.service';
import { DirectiveModule } from 'src/app/utils/directive/directive.module';


@NgModule({
  declarations: [
  CriarAulaComponent,
  AddCursoOnlineComponent,
  MateriasOnlineComponent,
  MateriasOnlineInitComponent,
  PreviewVideoComponent,
  PerguntasOnlineComponent,
  MinhaAulaComponent,
  QuestionarioOnlineComponent,
  VimeoAulaOnlineComponent,
  PerguntasOnlineIndividualComponent,
  QuestaoOnlineComponent,
  MensagemRespostaComponent
  ],
  imports: [
    CommonModule,
    AulaOnlineRoutingModule,
    CommonModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    UtilsComponentModule,
    PipeModule,
    DirectiveModule,
  ],
  providers: [
    UnidadeService,
    AuthService,
    VideoAulaService,
    PerguntaService,
    QuestionarioStateService
  ],
  entryComponents: [
    MensagemRespostaComponent
  ]
})
export class AulaOnlineModule { }
