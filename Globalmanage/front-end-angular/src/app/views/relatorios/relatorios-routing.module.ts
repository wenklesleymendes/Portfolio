import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RAfiliadosComponent } from './relatorio-afiliados/r-afiliados.component';
import { RCancelamentosComponent } from './relatorio-cancelamentos/r-cancelamentos.component';
import { RCertificadosComponent } from './relatorio-certificados/r-certificados.component';
import { RComissoesComponent } from './relatorio-comissoes/r-comissoes.component';
import { RDisparosRealizadosComponent } from './relatorio-disparos-realizados/r-disparos-realizados.component';
import { RFinanceiroComponent } from './relatorio-financeiro/r-financeiro.component';
import { RAlunosComponent } from './relatorio-matriculas/r-alunos.component';
import { RProvasComponent } from './relatorio-provas/r-provas.component';

const routes: Routes = [
    { path: '', redirectTo: 'relatorios', pathMatch: 'full' },
    { path: 'matriculas', component: RAlunosComponent },
    { path: 'cancelamentos', component: RCancelamentosComponent },
    { path: 'financeiro', component: RFinanceiroComponent },
    { path: 'provas', component: RProvasComponent },
    { path: 'certificados', component: RCertificadosComponent },
    { path: 'disparos-realizados', component: RDisparosRealizadosComponent },
    { path: 'afiliados', component: RAfiliadosComponent },
    { path: 'comissoes', component: RComissoesComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
})

export class RelatoriosRoutineModule { } 