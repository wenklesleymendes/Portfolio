import { DirectiveModule } from 'src/app/utils/directive/directive.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from 'src/app/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TicketRoutingModule } from './ticket-routing.module';
import { UtilsComponentModule } from 'src/app/utils/components/utils-components.module';
import { PipeModule } from 'src/app/utils/pipes/pipe.module';
import { NgxMaskModule } from 'ngx-mask';
// Service
import { TicketService } from 'src/app/services/ticket/ticket.service';
import { AssuntoTicketService } from 'src/app/services/ticket/assunto-ticket.service';
// Components
import { AdministracaoTicketComponent } from './administracao-ticket/administracao-ticket.component';
import { CriacaoAssuntosComponent } from './administracao-ticket/criacao-assuntos/criacao-assuntos.component';
import { DetalheTicketComponent } from './administracao-ticket/detalhe-ticket/detalhe-ticket.component';
import { CriacaoAssuntosIndividualComponent } from './administracao-ticket/criacao-assuntos/criacao-assuntos-individual/criacao-assuntos-individual.component';
import { DetalheTicketIndividualComponent } from './administracao-ticket/detalhe-ticket/detalhe-ticket-individual/detalhe-ticket-individual.component';
import { AuthInterceptor } from 'src/app/security/auth-interceptor.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { PainelTicketComponent } from './painel-ticket/painel-ticket.component';
import { TicketIndividualComponent } from './painel-ticket/ticket-individual/ticket-individual.component';
import { AnimationsService } from 'src/app/services/animations.service';
import { AuthService } from 'src/app/security/auth.service';
import { FirstPageFuncionarioComponent } from './first-page-funcionario/first-page-funcionario.component';

@NgModule({
  declarations: [
    AdministracaoTicketComponent,
    CriacaoAssuntosComponent,
    DetalheTicketComponent,
    CriacaoAssuntosIndividualComponent,
    DetalheTicketIndividualComponent,
    PainelTicketComponent,
    TicketIndividualComponent,
    FirstPageFuncionarioComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    TicketRoutingModule,
    UtilsComponentModule,
    PipeModule,
    NgxMaskModule.forRoot(),
    DirectiveModule,
  ],
  providers: [
    TicketService,
    AssuntoTicketService,
    AnimationsService,
    AuthService,
    //{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ]
})
export class TicketModule { }
