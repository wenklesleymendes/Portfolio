import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DinheiroNoBolsoComponent } from './modulos/dinheiro-no-bolso/dinheiro-no-bolso.component';
import { AHomeComponent } from './modulos/home/home.component';
import { MeusDadosComponent } from './modulos/meus-dados/meus-dados.component';
import { MinhaLojaVirtualComponent } from './modulos/minha-loja-virtual/minha-loja-virtual.component';
import { PortalComponent } from './portal/portal.component';

const routes: Routes = [
    { path: '', redirectTo: 'afiliado/portal', pathMatch: 'full' },
    { path: 'portal', component: PortalComponent },
    { path: 'home', component: AHomeComponent },
    { path: 'meus-dados', component: MeusDadosComponent },
    { path: 'minha-loja-virtual', component: MinhaLojaVirtualComponent },
    { path: 'dinheiro-no-bolso', component: DinheiroNoBolsoComponent },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
  })
  export class AfiliadoRoutingModule { }