import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CriarAgendaComponent } from './criar-agenda/criar-agenda.component';
import { CriarAgendaIndividualComponent } from './criar-agenda/criar-agenda-individual/criar-agenda-individual.component';
import { ProvasRoutingModule } from './provas-routing.module';
import { MaterialModule } from 'src/app/material.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UtilsComponentModule } from 'src/app/utils/components/utils-components.module';
import { PipeModule } from 'src/app/utils/pipes/pipe.module';
import { NgxMaskModule } from 'ngx-mask';
import { ColegioAutorizadoComponent } from './colegio-autorizado/colegio-autorizado.component';
import { ColegioAutorizadoService } from 'src/app/services/provas/colegio-autorizado.service';
import { ProvasService } from 'src/app/services/provas/provas.service';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { AuthService } from 'src/app/security/auth.service';
import { ColegioAutorizadoIndividualComponent } from './colegio-autorizado/colegio-autorizado-individual/colegio-autorizado-individual.component';
import { HistoricoProvaComponent } from './historico-prova/historico-prova.component';
import { DirectiveModule } from 'src/app/utils/directive/directive.module';

@NgModule({
  declarations: [
    CriarAgendaComponent,
    CriarAgendaIndividualComponent,
    ColegioAutorizadoComponent,
    ColegioAutorizadoIndividualComponent,
    HistoricoProvaComponent
  ],
  imports: [
    CommonModule,
    ProvasRoutingModule,
    CommonModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    UtilsComponentModule,
    PipeModule,
    NgxMaskModule.forRoot(),
    DirectiveModule,
  ],
  providers: [
    ColegioAutorizadoService,
    ProvasService,
    UnidadeService,
    AuthService
  ]
})
export class ProvasModule { }
