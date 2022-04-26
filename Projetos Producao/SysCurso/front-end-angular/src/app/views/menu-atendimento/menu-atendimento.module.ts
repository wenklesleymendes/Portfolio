import { NgModule } from '@angular/core';
import { MaterialModule } from 'src/app/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UtilsComponentModule } from 'src/app/utils/components/utils-components.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MenuAtendimentoRoutingModule } from './menu-atendimento-routing.module';
import { AlunoService } from 'src/app/services/aluno/aluno.service';
import { MAHomeComponent } from './m-a-home/m-a-home.component';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { PesquisarComponent } from './m-a-home/pesquisar/pesquisar.component';
import { AgendadosComponent } from './m-a-home/agendados/agendados.component';
import { ContatosPrioritatiosComponent } from './m-a-home/contatos-prioritarios/contatos-prioritarios.component';
import { ConfirmarDadosComponent } from './m-a-home/novo-atendimento/confirmar-dados/confirmar-dados.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ConfirmacaoSairComponent } from './m-a-home/confirmacao-sair/confirmacao-sair.component';
import { HistoricoTentativasComponent } from './m-a-home/outbound/historico-tentativas/historico-tentativas.component';

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
        PesquisarComponent,
        AgendadosComponent,
        ContatosPrioritatiosComponent,
        ConfirmarDadosComponent,
        ConfirmacaoSairComponent,
        HistoricoTentativasComponent
    ],
    imports: [
        CommonModule,
        MenuAtendimentoRoutingModule,
        MaterialModule,
        FormsModule,
        ReactiveFormsModule,
        UtilsComponentModule,
        FlexLayoutModule,
        HttpClientModule,
        MatDatepickerModule
    ],
    providers: [
        AlunoService,
    ]
})
export class MenuAtendimentoModule { }