import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { NgxCurrencyModule } from 'ngx-currency';
import { NgxMaskModule } from 'ngx-mask';
import { MaterialModule } from 'src/app/material.module';
import { AlunoService } from 'src/app/services/aluno/aluno.service';
import { UtilsComponentModule } from 'src/app/utils/components/utils-components.module';
import { PipeModule } from 'src/app/utils/pipes/pipe.module';
import { AfiliadoRoutingModule } from './afiliado-routing.module';
import { DinheiroNoBolsoComponent } from './modulos/dinheiro-no-bolso/dinheiro-no-bolso.component';
import { AHomeComponent } from './modulos/home/home.component';
import { MeusDadosComponent } from './modulos/meus-dados/meus-dados.component';
import { MinhaLojaVirtualComponent } from './modulos/minha-loja-virtual/minha-loja-virtual.component';
import { PortalComponent } from './portal/portal.component';

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
        // AHomeComponent,
        // DinheiroNoBolsoComponent,
        // MeusDadosComponent,
        // MinhaLojaVirtualComponent,
        // PortalComponent
    ],
    imports: [
        AfiliadoRoutingModule,
        CommonModule,
        MaterialModule,
        FormsModule,
        ReactiveFormsModule,
        UtilsComponentModule,
        FlexLayoutModule,
        HttpClientModule,
        MatDatepickerModule,
        NgxMaskModule.forRoot(),
        NgxCurrencyModule.forRoot(customCurrencyMaskConfig),
        PipeModule,
        MaterialModule,
    ],
    providers: [
        //AlunoService,
    ]
})
export class AfiliadoModule { }