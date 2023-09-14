// Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UnidadesRoutingModule } from './gerenciador-routing.module';
import { PipeModule } from 'src/app/utils/pipes/pipe.module';
import { MaterialModule } from 'src/app/material.module';
import { UtilsComponentModule } from 'src/app/utils/components/utils-components.module';
import { NgxMaskModule } from 'ngx-mask';
import { NgxCurrencyModule } from 'ngx-currency';
// Services
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { LocationService } from 'src/app/services/location.service';
import { DataProvider } from './unidades/unidade-individual/data-provider';
import { InstituicaoBancariaService } from 'src/app/services/instituicao-bancaria.service';
import { CentroCustoService } from 'src/app/services/gerenciador/centro-custo.service';
import { AnexoService } from 'src/app/services/anexo.service';
import { CursoService } from 'src/app/services/gerenciador/curso.service';
import { TurmaService } from 'src/app/services/gerenciador/turma.service';
import { DeleteService } from 'src/app/services/delete.service';
import { PlanoPagamentoService } from 'src/app/services/gerenciador/plano-pagamento.service';
import { CampanhaService } from 'src/app/services/gerenciador/campanha.service';
import { FormService } from 'src/app/services/form.service';
// Components
import { UnidadesComponent } from './unidades/unidades.component';
import { CursosTurmasComponent } from './cursos-turmas/cursos-turmas.component';
import { PlanosPagamentosComponent } from './planos-pagamentos/planos-pagamentos.component';
import { PromocoesBolsasConvenioComponent } from './promocoes-bolsas-convenio/promocoes-bolsas-convenio.component';
import { UnidadeIndividualComponent } from './unidades/unidade-individual/unidade-individual.component';
import { InformacoesUnidadeComponent } from './unidades/unidade-individual/informacoes-unidade/informacoes-unidade.component';
import { DadosBancariosComponent } from './unidades/unidade-individual/dados-bancarios/dados-bancarios.component';
import { ContratoLocacaoComponent } from './unidades/unidade-individual/contrato-locacao/contrato-locacao.component';
import { DocumentoUnidadeComponent } from './unidades/unidade-individual/documento-unidade/documento-unidade.component';
import { HistoricoOcorrenciasComponent } from './unidades/unidade-individual/historico-ocorrencias/historico-ocorrencias.component';
import { CentroCustoComponent } from './unidades/centro-custo/centro-custo.component';
import { AnexoComponent } from './unidades/anexo/anexo.component';
import { CursoComponent } from './cursos-turmas/curso/curso.component';
import { TurmaComponent } from './cursos-turmas/turma/turma.component';
import { TurmaIndividualComponent } from './cursos-turmas/turma/turma-individual/turma-individual.component';
import { PlanoPagamentoIndividualComponent } from './planos-pagamentos/plano-pagamento-individual/plano-pagamento-individual.component';
import { PromocoesBolsasConvenioIndividualComponent } from './promocoes-bolsas-convenio/promocoes-bolsas-convenio-individual/promocoes-bolsas-convenio-individual.component';
import { CursoIndividualComponent } from './cursos-turmas/curso/curso-individual/curso-individual.component';
import { AuthInterceptor } from 'src/app/security/auth-interceptor.service';
import { SolicitacoesComponent } from './solicitacoes/solicitacoes.component';
import { SolicitacoesIndividualComponent } from './solicitacoes/solicitacoes-individual/solicitacoes-individual.component';
import { SolicitacoesService } from 'src/app/services/gerenciador/solicitacoes.service';
import { DirectiveModule } from 'src/app/utils/directive/directive.module';


export const customCurrencyMaskConfig = {
  align: 'left',
  allowNegative: true,
  allowZero: true,
  decimal: ',',
  precision: 2,
  prefix: '',
  suffix: '',
  thousands: '.',
  nullable: true
};

@NgModule({
  declarations: [
    UnidadesComponent,
    CursosTurmasComponent,
    PlanosPagamentosComponent,
    PromocoesBolsasConvenioComponent,
    UnidadeIndividualComponent,
    InformacoesUnidadeComponent,
    DadosBancariosComponent,
    ContratoLocacaoComponent,
    DocumentoUnidadeComponent,
    HistoricoOcorrenciasComponent,
    CentroCustoComponent,
    AnexoComponent,
    CursoComponent,
    TurmaComponent,
    TurmaIndividualComponent,
    PlanoPagamentoIndividualComponent,
    PromocoesBolsasConvenioIndividualComponent,
    CursoIndividualComponent,
    SolicitacoesComponent,
    SolicitacoesIndividualComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    UnidadesRoutingModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    UtilsComponentModule,
    PipeModule,
    NgxMaskModule.forRoot(),
    NgxCurrencyModule.forRoot(customCurrencyMaskConfig),
    DirectiveModule
  ],
  providers: [
    UnidadeService,
    DataProvider,
    LocationService,
    InstituicaoBancariaService,
    CentroCustoService,
    AnexoService,
    DeleteService,
    CursoService,
    TurmaService,
    PlanoPagamentoService,
    CampanhaService,
    FormService,
    SolicitacoesService
    // { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  entryComponents: [
    CentroCustoComponent
  ]
})
export class GerenciadorModule { }
