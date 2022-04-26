import { DirectiveModule } from 'src/app/utils/directive/directive.module';
import { NgModule } from '@angular/core';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { MaterialModule } from 'src/app/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UtilsComponentModule } from 'src/app/utils/components/utils-components.module';
import { PipeModule } from 'src/app/utils/pipes/pipe.module';
import { NgxMaskModule } from 'ngx-mask';
import { NgxCurrencyModule } from 'ngx-currency';
import { FinanceiroRoutingModule } from './financeiro-routing.module';
// Services
import { DeleteService } from 'src/app/services/delete.service';
import { EstoqueService } from 'src/app/services/financeiro/estoque.service';
import { FornecedorService } from 'src/app/services/financeiro/fornecedor.service';
import { FolhaPagamentoService } from 'src/app/services/financeiro/folha-pagamento.service';
// Components
import { CadastroFornecedorClienteComponent } from './cadastro-fornecedor-cliente/cadastro-fornecedor-cliente.component';
import { CadastroFornecedorClienteIndividualComponent } from './cadastro-fornecedor-cliente/cadastro-fornecedor-cliente-individual/cadastro-fornecedor-cliente-individual.component';
import { ContasPagarComponent } from './contas-pagar/contas-pagar.component';
import { ContasPagarIndividualComponent } from './contas-pagar/contas-pagar-individual/contas-pagar-individual.component';
import { EstoqueComponent } from './estoque/estoque.component';
import { EstoqueIndividualComponent } from './estoque/estoque-individual/estoque-individual.component';
import { HostoricoEstoqueComponent } from './estoque/hostorico-estoque/hostorico-estoque.component';
import { AnexoFornecedorComponent } from './cadastro-fornecedor-cliente/anexo-fornecedor/anexo-fornecedor.component';
import { AnexoContasPagarComponent } from './contas-pagar/anexo-contas-pagar/anexo-contas-pagar.component';
import { ImprimirContasPagarComponent } from './contas-pagar/imprimir-contas-pagar/imprimir-contas-pagar.component';
import { ContasPagarDetalheComponent } from './contas-pagar/contas-pagar-detalhe/contas-pagar-detalhe.component';
import { DetalheBaixaManualComponent } from './contas-pagar/contas-pagar-detalhe/detalhe-baixa-manual/detalhe-baixa-manual.component';
import { DetalheHistoricoComponent } from './contas-pagar/contas-pagar-detalhe/detalhe-historico/detalhe-historico.component';
import { DetalheComprovantesComponent } from './contas-pagar/contas-pagar-detalhe/detalhe-comprovantes/detalhe-comprovantes.component';
import { FolhaPagamentoComponent } from './folha-pagamento/folha-pagamento.component';
import { FolhaPagamentoIndividualComponent } from './folha-pagamento/folha-pagamento-individual/folha-pagamento-individual.component';
import { FolhaPagamentoDetalheComponent } from './folha-pagamento/folha-pagamento-detalhe/folha-pagamento-detalhe.component';
import { FolhaPagamentoHistoricoComponent } from './folha-pagamento/folha-pagamento-detalhe/folha-pagamento-historico/folha-pagamento-historico.component';
import { FolhaPagamentoReciboComponent } from './folha-pagamento/folha-pagamento-detalhe/folha-pagamento-recibo/folha-pagamento-recibo.component';
import { FolhaPagamentoTransacaoComponent } from './folha-pagamento/folha-pagamento-detalhe/folha-pagamento-transacao/folha-pagamento-transacao.component';
import { EstoqueItemComponent } from './estoque/estoque-item/estoque-item.component';
import { DetalhePendenteComponent } from './contas-pagar/contas-pagar-detalhe/detalhe-pendente/detalhe-pendente.component';
import { CancelarPagamentoComponent } from './contas-pagar/contas-pagar-individual/cancelar-pagamento/cancelar-pagamento.component';
import { AuthInterceptor } from 'src/app/security/auth-interceptor.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

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
    CadastroFornecedorClienteComponent,
    CadastroFornecedorClienteIndividualComponent,
    ContasPagarComponent,
    ContasPagarIndividualComponent,
    EstoqueComponent,
    EstoqueIndividualComponent,
    HostoricoEstoqueComponent,
    AnexoFornecedorComponent,
    AnexoContasPagarComponent,
    ImprimirContasPagarComponent,
    ContasPagarDetalheComponent,
    DetalheBaixaManualComponent,
    DetalheHistoricoComponent,
    DetalheComprovantesComponent,
    FolhaPagamentoComponent,
    FolhaPagamentoIndividualComponent,
    FolhaPagamentoDetalheComponent,
    FolhaPagamentoHistoricoComponent,
    FolhaPagamentoReciboComponent,
    FolhaPagamentoTransacaoComponent,
    EstoqueItemComponent,
    DetalhePendenteComponent,
    CancelarPagamentoComponent
  ],
  imports: [
    CommonModule,
    FinanceiroRoutingModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    UtilsComponentModule,
    PipeModule,
    NgxMaskModule.forRoot(),
    NgxCurrencyModule.forRoot(customCurrencyMaskConfig),
    DirectiveModule,
  ],
  providers: [
    DeleteService,
    HttpClientModule,
    FornecedorService,
    EstoqueService,
    FolhaPagamentoService,
    CurrencyPipe,
    DatePipe,
    //{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ]
})
export class FinanceiroModule { }
