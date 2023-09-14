import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NavbarModule } from 'src/app/components/navbar/navbar.module';
import { SidebarModule } from 'src/app/components/sidebar/sidebar.module';
import { InvestimentoRoutingModule } from './investimento-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { InvestimentoComponent } from './investimento.component';



@NgModule(
    {
        providers: [],
        declarations: [InvestimentoComponent],
        imports: [
            CommonModule,
            InvestimentoRoutingModule,
            NavbarModule,
            SidebarModule,
            FormsModule,
            ReactiveFormsModule,
            NgSelectModule
        ]
    }
)

export class InvestimentoModule { }