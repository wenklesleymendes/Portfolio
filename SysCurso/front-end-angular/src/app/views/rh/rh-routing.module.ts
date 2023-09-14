import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FuncionarioComponent } from './funcionario/funcionario.component';
import { ControlePontoComponent } from './controle-ponto/controle-ponto.component';
import { EscalaServicoComponent } from './escala-servico/escala-servico.component';
import { FuncionarioIndividualComponent } from './funcionario/funcionario-individual/funcionario-individual.component';
import { UploadPontoComponent } from './upload-ponto/upload-ponto.component';

const routes: Routes = [
    { path: '', redirectTo: 'cadastro-funcionario', pathMatch: 'full' },
    { path: 'cadastro-funcionario', component: FuncionarioComponent },
    { path: 'cadastro-funcionario/adicionar/:id', component: FuncionarioIndividualComponent },
    { path: 'controle-ponto', component: ControlePontoComponent },
    // { path: 'escala-servico', component: EscalaServicoComponent },
    { path: 'upload-ponto', component: UploadPontoComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
})
export class RhRoutingModule { } 
