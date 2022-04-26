// Modules
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RhRoutingModule } from './rh-routing.module';
import { PipeModule } from 'src/app/utils/pipes/pipe.module';
import { MaterialModule } from 'src/app/material.module';
import { UtilsComponentModule } from 'src/app/utils/components/utils-components.module';
import { NgxMaskModule } from 'ngx-mask';
import { NgxCurrencyModule } from 'ngx-currency';
// Services
import { DataProvider } from './funcionario/funcionario-individual/data-provider';
import { InstituicaoBancariaService } from 'src/app/services/instituicao-bancaria.service';
import { FuncionarioService } from 'src/app/services/rh/funcionario.service';
import { FormService } from 'src/app/services/form.service';
import { ControlePontoService } from 'src/app/services/rh/controle-ponto.service';
// Components
import { FuncionarioComponent } from './funcionario/funcionario.component';
import { ControlePontoComponent } from './controle-ponto/controle-ponto.component';
import { EscalaServicoComponent } from './escala-servico/escala-servico.component';
import { FuncionarioIndividualComponent } from './funcionario/funcionario-individual/funcionario-individual.component';
import { DadosPessoaisComponent } from './funcionario/funcionario-individual/dados-pessoais/dados-pessoais.component';
import { DadosContratacaoComponent } from './funcionario/funcionario-individual/dados-contratacao/dados-contratacao.component';
import { AdicionarPeriodoFeriasComponent } from './controle-ponto/adicionar-periodo-ferias/adicionar-periodo-ferias.component';
import { OcorrenciaComponent } from './controle-ponto/ocorrencia/ocorrencia.component';
import { FuncionarioAnexoComponent } from './funcionario/funcionario-anexo/funcionario-anexo.component';
import { UploadPontoComponent } from './upload-ponto/upload-ponto.component';
import { FuncionarioDetalhamentoComponent } from './funcionario/funcionario-detalhamento/funcionario-detalhamento.component';
import { AuthInterceptor } from 'src/app/security/auth-interceptor.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
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
    FuncionarioComponent,
    ControlePontoComponent,
    EscalaServicoComponent,
    FuncionarioIndividualComponent,
    DadosPessoaisComponent,
    DadosContratacaoComponent,
    AdicionarPeriodoFeriasComponent,
    OcorrenciaComponent,
    FuncionarioAnexoComponent,
    UploadPontoComponent,
    FuncionarioDetalhamentoComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RhRoutingModule,
    UtilsComponentModule,
    PipeModule,
    NgxMaskModule.forRoot(),
    NgxCurrencyModule.forRoot(customCurrencyMaskConfig),
    DirectiveModule
  ],
  providers: [
    DataProvider,
    InstituicaoBancariaService,
    FuncionarioService,
    FormService,
    ControlePontoService,
    //{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ]
})
export class RhModule { }
