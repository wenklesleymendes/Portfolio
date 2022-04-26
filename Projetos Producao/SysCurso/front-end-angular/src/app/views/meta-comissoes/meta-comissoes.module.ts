import { DirectiveModule } from 'src/app/utils/directive/directive.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MetaComissoesRoutingModule } from './meta-comissoes-routing.module';
import { CriacaoMetaComponent } from './criacao-meta/criacao-meta.component';
import { MaterialModule } from 'src/app/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PipeModule } from 'src/app/utils/pipes/pipe.module';
import { NgxMaskModule } from 'ngx-mask';
import { NgxCurrencyModule } from 'ngx-currency';
// Services
import { ComissoesService } from 'src/app/services/metas-comissoes/comissoes.service';
// Components
import { UtilsComponentModule } from 'src/app/utils/components/utils-components.module';
import { CriacaoMetaIndividualComponent } from './criacao-meta/criacao-meta-individual/criacao-meta-individual.component';
import { PainelMetasComissoesComponent } from './painel-metas-comissoes/painel-metas-comissoes.component';
import { VisaoSemanalComponent } from './painel-metas-comissoes/visao-semanal/visao-semanal.component';
import { VisaoMensalComponent } from './painel-metas-comissoes/visao-mensal/visao-mensal.component';
import { VisaoFinalComponent } from './painel-metas-comissoes/visao-final/visao-final.component';
import { MinhasComissoesComponent } from './painel-metas-comissoes/minhas-comissoes/minhas-comissoes.component';
import { CriacaoComissaoComponent } from './criacao-comissao/criacao-comissao.component';
import { CriacaoComissaoIndividualComponent } from './criacao-comissao/criacao-comissao-individual/criacao-comissao-individual.component';

import * as Highcharts from 'highcharts';
import exporting from 'highcharts/modules/exporting';
import { AuthInterceptor } from 'src/app/security/auth-interceptor.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
exporting(Highcharts)

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

Highcharts.setOptions({
  lang: {
    printChart:   'Imprimir gráfico',
    downloadJPEG: 'Download gráfico JPEG',
    downloadPDF:  'Download gráfico PDF ',
    downloadPNG:  'Download gráfico PNG',
    contextButtonTitle: 'Opções do gráfico'
  }
})

@NgModule({
  declarations: [
    CriacaoMetaComponent,
    CriacaoMetaIndividualComponent,
    PainelMetasComissoesComponent,
    VisaoSemanalComponent,
    VisaoMensalComponent,
    VisaoFinalComponent,
    MinhasComissoesComponent,
    CriacaoComissaoComponent,
    CriacaoComissaoIndividualComponent
  ],
  imports: [
    CommonModule,
    MetaComissoesRoutingModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    UtilsComponentModule,
    PipeModule,
    NgxMaskModule.forRoot(),
    NgxCurrencyModule.forRoot(customCurrencyMaskConfig),
    DirectiveModule,
  ],
  providers: [
    ComissoesService,
    //{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ]
})
export class MetaComissoesModule { }
