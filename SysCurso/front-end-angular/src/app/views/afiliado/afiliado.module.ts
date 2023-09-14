import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MaterialModule } from 'src/app/material.module';
import { AlunoService } from 'src/app/services/aluno/aluno.service';
import { UtilsComponentModule } from 'src/app/utils/components/utils-components.module';
import { AfiliacaoComponent } from './afiliacao/afiliacao.component';
import { AfiliadoRoutingModule } from './afiliado-routing.module';
import { LojaComponent } from './loja/loja.component';
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
        PortalComponent,
        LojaComponent,
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
        MatDatepickerModule
    ],
    providers: [
        AlunoService,
    ]
})
export class AfiliadoModule { }