import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InvestimentoComponent } from './investimento.component';

const routes: Routes = [{
    path: '',
    component: InvestimentoComponent,
  }];

  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })

  export class InvestimentoRoutingModule{}