// Angular
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA, DEFAULT_CURRENCY_CODE } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatTabsModule } from '@angular/material/tabs';
import { MatChipsModule } from '@angular/material/chips';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatDatepickerModule } from '@angular/material/datepicker';
// Securty
import { AuthService } from './security/auth.service';
import { AuthGuard } from './security/auth-guard.guard';
// Others modules
import { MaterialModule } from './material.module';
import { PipeModule } from './utils/pipes/pipe.module';
import { NgxMaskModule } from 'ngx-mask';
// Services
import { NavService } from './views/menu-list-item/nav.service';
import { ErrorHandlerService } from './services/error-handler.service';
import { AnimationsService } from './services/animations.service';
import { NoCacheInterceptor } from './services/no-cache.interceptor';
// Components
import { AppComponent } from './app.component';
import { BodyComponent } from './views/body/body.component';
import { LoginComponent } from './views/login/login.component';
import { Page404Component } from './views/page404/page404.component';
import { MenuListItemComponent } from './views/menu-list-item/menu-list-item.component';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from '../environments/environment';
import { EffectsModule } from '@ngrx/effects';
import { RootStoreModule } from './_store/root-store.module';
import { FirstPageAlunoComponent } from './views/initial/initial.component';
import { MAHomeComponent } from './views/menu-atendimento/m-a-home/m-a-home.component';
import { NovoAtendimentoComponent } from './views/menu-atendimento/m-a-home/novo-atendimento/novo-atendimento.component';
import { PesquisarComponent } from './views/menu-atendimento/m-a-home/pesquisar/pesquisar.component';
import { OutboundComponent } from './views/menu-atendimento/m-a-home/outbound/outbound.component';
import { AgendadosComponent } from './views/menu-atendimento/m-a-home/agendados/agendados.component';
import { ContatosPrioritatiosComponent } from './views/menu-atendimento/m-a-home/contatos-prioritarios/contatos-prioritarios.component';
import { ConfirmarDadosComponent } from './views/menu-atendimento/m-a-home/novo-atendimento/confirmar-dados/confirmar-dados.component';
import { QuaseLaComponent } from './views/menu-atendimento/m-a-home/novo-atendimento/quase-la/quase-la.component';
import { ResultadosComponent } from './views/menu-atendimento/m-a-home/pesquisar/resultados/resultados.component';
import { EditarResultadosComponent } from './views/menu-atendimento/m-a-home/pesquisar/resultados/editar-resultados/editar-resultados.component';
import { DadosEditadosComponent } from './views/menu-atendimento/m-a-home/pesquisar/resultados/editar-resultados/dados-editados/dados-editados.component';
import { RegistroCompletoComponent } from './views/menu-atendimento/m-a-home/agendados/registro-completo/registro-completo.component';
import { PortalComponent } from './views/afiliado/portal/portal.component';
import { LojaComponent } from './views/afiliado/loja/loja.component';
import { ConfirmacaoSairComponent } from './views/menu-atendimento/m-a-home/confirmacao-sair/confirmacao-sair.component';
import { HistoricoTentativasComponent } from './views/menu-atendimento/m-a-home/outbound/historico-tentativas/historico-tentativas.component';
import { AfiliacaoComponent } from './views/afiliado/afiliacao/afiliacao.component';
import { AfiliadoComponent } from './views/afiliado/afiliado.component';
import { AHomeComponent } from './views/afiliado/modulos/home/a-home.component';

@NgModule({
  declarations: [
    AppComponent,
    BodyComponent,
    LoginComponent,
    Page404Component,
    MenuListItemComponent,
    FirstPageAlunoComponent,
    MAHomeComponent,
    NovoAtendimentoComponent,
    PesquisarComponent,
    OutboundComponent,
    AgendadosComponent,
    ContatosPrioritatiosComponent,
    ConfirmarDadosComponent,
    QuaseLaComponent,
    ResultadosComponent,
    EditarResultadosComponent,
    DadosEditadosComponent,
    RegistroCompletoComponent,
    ConfirmacaoSairComponent,
    HistoricoTentativasComponent,
    AfiliadoComponent,
    PortalComponent,
    LojaComponent,
    AfiliacaoComponent,
    AHomeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    PipeModule,
    NgxMaskModule.forRoot(),
    StoreModule.forRoot({}, {}),
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: environment.production }),
    EffectsModule.forRoot([]),
    RootStoreModule,
    MatButtonModule,
    MatCardModule,
    MatTabsModule,
    MatChipsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
  ],
  providers: [
    AuthGuard,
    AuthService,
    NavService,
    ErrorHandlerService,
    AnimationsService,
    // { provide: HTTP_INTERCEPTORS, useClass: NoCacheInterceptor, multi: true },
    { provide: DEFAULT_CURRENCY_CODE, useValue: 'BRL' }
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
