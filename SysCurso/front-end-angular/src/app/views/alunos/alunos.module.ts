import { NgModule } from '@angular/core';
import { CommonModule, CurrencyPipe, PercentPipe } from '@angular/common';
import { FiltroAlunosComponent } from './consultar-alunos/filtro-alunos/filtro-alunos.component';
import { MaterialModule } from 'src/app/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UtilsComponentModule } from 'src/app/utils/components/utils-components.module';
import { PipeModule } from 'src/app/utils/pipes/pipe.module';
import { NgxMaskModule } from 'ngx-mask';
import { ClipboardModule } from '@angular/cdk/clipboard';
// Service
import { AlunoService } from 'src/app/services/aluno/aluno.service';
// Components
import { AlunosRoutingModule } from './alunos-routing.module';
import { ConsultarAlunosComponent } from './consultar-alunos/consultar-alunos.component';
import { CadastrarAlunosComponent } from './consultar-alunos/cadastrar-alunos/cadastrar-alunos.component';
import { MatriculasComponent } from './consultar-alunos/matriculas/matriculas.component';
import { AuthInterceptor } from 'src/app/security/auth-interceptor.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NacionalidadeService } from 'src/app/services/aluno/nacionalidade.service';
import { NaturalidadeService } from 'src/app/services/aluno/naturalidade.service';
import { MatriculaAlunoComponent } from './matricula-aluno/matricula-aluno.component';
import { AlunoCursoTurmaComponent } from './matricula-aluno/aluno-curso-turma/aluno-curso-turma.component';
import { AlunoFinanceiroContratoComponent } from './matricula-aluno/aluno-financeiro-contrato/aluno-financeiro-contrato.component';
import { AlunoDocumentosComponent } from './matricula-aluno/aluno-documentos/aluno-documentos.component';
import { AlunoProvasCertificadosComponent } from './matricula-aluno/aluno-provas-certificados/aluno-provas-certificados.component';
import { AlunoProvasCertificadosConfirmacaoComponent } from './matricula-aluno/aluno-provas-certificados/aluno-provas-certificados-confirmacao/aluno-provas-certificados-confirmacao';
import { AlunoSolicitacoesComponent } from './matricula-aluno/aluno-solicitacoes/aluno-solicitacoes.component';
import { AlunoTicketsComponent } from './matricula-aluno/aluno-tickets/aluno-tickets.component';
import { AlunoTicketNovoComponent } from'./matricula-aluno/aluno-tickets/aluno-ticket-novo/aluno-ticket-novo.component'
import { AlunoComunicacaoComponent } from './matricula-aluno/aluno-comunicacao/aluno-comunicacao.component';
import { AlunoPortalComponent } from './matricula-aluno/aluno-portal/aluno-portal.component';
import { AlunoCursoTurmaSelectComponent } from './matricula-aluno/aluno-curso-turma/aluno-curso-turma-select/aluno-curso-turma-select.component';
import { MatriculaAlunoService } from 'src/app/services/aluno/matricula-aluno.service';
import { DocumentoAlunoService } from 'src/app/services/aluno/documento-aluno.service';
import { MatriculaFormasPagamentoComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-formas-pagamento/matricula-formas-pagamento.component';
import { MfpCreditoComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-formas-pagamento/mfp-credito/mfp-credito.component';
import { MfpDebitoComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-formas-pagamento/mfp-debito/mfp-debito.component';
import { MfpBoletoComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-formas-pagamento/mfp-boleto/mfp-boleto.component';
import { MfpDespesaRecorrenteComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-formas-pagamento/mfp-despesa-recorrente/mfp-despesa-recorrente.component';
import { NgxCurrencyModule } from 'ngx-currency';
import { MatriculaCaracteristicasPlanoComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-caracteristicas-plano/matricula-caracteristicas-plano.component';
import { MatriculaDetalhesPagamentoComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-detalhes-pagamento/matricula-detalhes-pagamento.component';
import { MatriculaPainelPagamentoComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-painel-pagamento/matricula-painel-pagamento.component';
import { MppDetalheEmailComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-painel-pagamento/mpp-detalhe-email/mpp-detalhe-email.component';
import { MppDetalhePagamentoComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-painel-pagamento/mpp-detalhe-pagamento/mpp-detalhe-pagamento.component';
import { BoletoAvisoComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-formas-pagamento/mfp-boleto/boleto-aviso/boleto-aviso.component';
import { CampanhaService } from 'src/app/services/gerenciador/campanha.service';
import { BoletoAviso2Component } from './matricula-aluno/aluno-financeiro-contrato/matricula-formas-pagamento/mfp-boleto/boleto-aviso2/boleto-aviso2.component';
import { MatriculaDetalheCampanhaComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-detalhe-campanha/matricula-detalhe-campanha.component';
import { AlunoFinanceiroService } from 'src/app/services/aluno/aluno-financeiro.service';
import { MppDetalheTabelaComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-painel-pagamento/mpp-detalhe-tabela/mpp-detalhe-tabela.component';
import { MppDetalheEmailIndividualComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-painel-pagamento/mpp-detalhe-email/mpp-detalhe-email-individual/mpp-detalhe-email-individual.component';
import { CartaoCreditoComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-caracteristicas-plano/cartao-credito/cartao-credito.component';
import { DrawCreditComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-caracteristicas-plano/cartao-credito/draw-credit/draw-credit.component';
import { MsgCompraComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-caracteristicas-plano/cartao-credito/msg-compra/msg-compra.component';
import { TefService } from 'src/app/services/tef/tef.service';
import { MppReciboComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-painel-pagamento/mpp-recibo/mpp-recibo.component';
import { ContratoAlunoService } from 'src/app/services/aluno/contrato-aluno.service';
import { DicaTefComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-painel-pagamento/dica-tef/dica-tef.component';
import { AsOpcoesComponent } from './matricula-aluno/aluno-solicitacoes/as-opcoes/as-opcoes.component';
import { MsgRecusarComponent } from './matricula-aluno/aluno-documentos/msg-recusar/msg-recusar.component';
import { MppComprovanteComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-painel-pagamento/mpp-comprovante/mpp-comprovante.component';
import { MsgDocumentoAlunoComponent } from './matricula-aluno/aluno-documentos/msg-documento-aluno/msg-documento-aluno.component';
import { InconsistenciaDocumentoService } from 'src/app/services/aluno/inconsistencia-documento.service';
import { EjaEnccejaComponent } from './eja-encceja/eja-encceja.component';
import { DirectiveModule } from 'src/app/utils/directive/directive.module';
import { AlunoProvasCertificadosMateriasComponent } from 'src/app/views/alunos/matricula-aluno/aluno-provas-certificados/aluno-provas-certificados-materias/aluno-provas-certificados-materias.component';
import { AlunoProvaCertificadoEmissaoComponent } from './matricula-aluno/aluno-provas-certificados/aluno-prova-certificado-emissao/aluno-prova-certificado-emissao.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SolicitacoesGeradasComponent } from './matricula-aluno/aluno-solicitacoes/solicitacoes-geradas/solicitacoes-geradas.component';
import { BoletoDigitalMobileComponent } from './matricula-aluno/aluno-financeiro-contrato/boleto-digital-mobile/boleto-digital-mobile.component';
import { ConfirmarDadosComponent } from './consultar-alunos/cadastrar-alunos/confirmar-dados/confirmar-dados.component';
import { AlunoOcorrenciaNovoComponent } from './matricula-aluno/aluno-tickets/aluno-ocorrencia-novo/aluno-ocorrencia-novo.component';
import { AlunoOcorrenciaDetalheComponent } from './matricula-aluno/aluno-tickets/aluno-ocorrencia-detalhe/aluno-ocorrencia-detalhe.component';
import { MatriculaCancelamentoComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-cancelamento/matricula-cancelamento.component';
import { MatriculaCancelamentoAutorizacaoComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-cancelamento/matricula-cancelamento-autorizacao/matricula-cancelamento-autorizacao.component';
import { CompararDatasComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-formas-pagamento/mfp-boleto/comparar-datas/comparar-datas.component';
import { MatriculaEfetuaCancelamentoComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-cancelamento/matricula-efetua-cancelamento/matricula-efetua-cancelamento.component';
import { MppBaixaPagamentoComponent } from './matricula-aluno/aluno-financeiro-contrato/matricula-painel-pagamento/mpp-baixa-pagamento/mpp-baixa-pagamento.component';


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
    ConsultarAlunosComponent,
    CadastrarAlunosComponent,
    FiltroAlunosComponent,
    MatriculasComponent,
    MatriculaAlunoComponent,
    AlunoCursoTurmaComponent,
    AlunoFinanceiroContratoComponent,
    AlunoDocumentosComponent,
    EjaEnccejaComponent,
    AlunoProvasCertificadosComponent,
    AlunoProvasCertificadosConfirmacaoComponent,
    AlunoProvasCertificadosMateriasComponent,
    AlunoSolicitacoesComponent,
    AlunoTicketsComponent,
    AlunoTicketNovoComponent,
    AlunoComunicacaoComponent,
    AlunoPortalComponent,
    AlunoCursoTurmaSelectComponent,
    MatriculaFormasPagamentoComponent,
    MfpCreditoComponent,
    MfpDebitoComponent,
    MfpBoletoComponent,
    MfpDespesaRecorrenteComponent,
    MatriculaCaracteristicasPlanoComponent,
    MatriculaDetalhesPagamentoComponent,
    MatriculaPainelPagamentoComponent,
    MppDetalheEmailComponent,
    MppDetalhePagamentoComponent,
    BoletoAvisoComponent,
    BoletoAviso2Component,
    MatriculaDetalheCampanhaComponent,
    MppDetalheTabelaComponent,
    MppDetalheEmailIndividualComponent,
    CartaoCreditoComponent,
    DrawCreditComponent,
    MsgCompraComponent,
    MppReciboComponent,
    DicaTefComponent,
    AsOpcoesComponent,
    SolicitacoesGeradasComponent,
    MsgRecusarComponent,
    MppComprovanteComponent,
    MsgDocumentoAlunoComponent,
    AlunoProvaCertificadoEmissaoComponent,
    BoletoDigitalMobileComponent,
    ConfirmarDadosComponent,
    AlunoOcorrenciaNovoComponent,
    AlunoOcorrenciaDetalheComponent,
    MatriculaCancelamentoComponent,
    MatriculaCancelamentoAutorizacaoComponent,
    CompararDatasComponent,
    MatriculaEfetuaCancelamentoComponent,
    MppBaixaPagamentoComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    AlunosRoutingModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    UtilsComponentModule,
    PipeModule,
    NgxMaskModule.forRoot(),
    NgxCurrencyModule.forRoot(customCurrencyMaskConfig),
    DirectiveModule,
    FlexLayoutModule,
    ClipboardModule,
  ],
  providers: [
    AlunoService,
    NacionalidadeService,
    NaturalidadeService,
    MatriculaAlunoService,
    DocumentoAlunoService,
    CampanhaService,
    CurrencyPipe,
    PercentPipe,
    AlunoFinanceiroService,
    TefService,
    ContratoAlunoService,
    InconsistenciaDocumentoService
  ]
})
export class AlunosModule { }
