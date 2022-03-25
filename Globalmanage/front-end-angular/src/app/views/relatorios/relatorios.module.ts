import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PipeModule } from 'src/app/utils/pipes/pipe.module';
import { MaterialModule } from 'src/app/material.module';
import { UtilsComponentModule } from 'src/app/utils/components/utils-components.module';
import { NgxMaskModule } from 'ngx-mask';
import { NgxCurrencyModule } from 'ngx-currency';
import { RelatoriosRoutineModule } from './relatorios-routing.module';
import { DirectiveModule } from 'src/app/utils/directive/directive.module';
import { HttpClientModule } from '@angular/common/http';
import { RCancelamentosComponent } from './relatorio-cancelamentos/r-cancelamentos.component';
import { RFinanceiroComponent } from './relatorio-financeiro/r-financeiro.component';
import { RProvasComponent } from './relatorio-provas/r-provas.component';
import { RCertificadosComponent } from './relatorio-certificados/r-certificados.component';
import { RDisparosRealizadosComponent } from './relatorio-disparos-realizados/r-disparos-realizados.component';
import { RAfiliadosComponent } from './relatorio-afiliados/r-afiliados.component';
import { RAlunosComponent } from './relatorio-matriculas/r-alunos.component';
import { RComissoesComponent } from './relatorio-comissoes/r-comissoes.component';

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
        RAlunosComponent,
        RCancelamentosComponent,
        RFinanceiroComponent,
        RProvasComponent,
        RCertificadosComponent,
        RDisparosRealizadosComponent,
        RAfiliadosComponent,
        RComissoesComponent
    ],

    imports: [
        CommonModule,
        MaterialModule,
        FormsModule,
        HttpClientModule,
        ReactiveFormsModule,
        RelatoriosRoutineModule,
        UtilsComponentModule,
        PipeModule,
        NgxMaskModule.forRoot(),
        NgxCurrencyModule.forRoot(customCurrencyMaskConfig),
        DirectiveModule
    ],

    providers: [
        //{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    ]
})
export class RelatioriosModule { }