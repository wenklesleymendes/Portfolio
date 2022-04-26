import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CadastroUsuarioComponent } from './cadastro-usuario/cadastro-usuario.component';
import { PermissaoUsuarioComponent } from './permissao-usuario/permissao-usuario.component';
import { ConfiguracaoParametrosComponent } from './configuracao-parametros/configuracao-parametros.component';
import { PermissaoUsuarioIndividualComponent } from './permissao-usuario/permissao-usuario-individual/permissao-usuario-individual.component';
import { CadastroUsuarioIndividualComponent } from './cadastro-usuario/cadastro-usuario-individual/cadastro-usuario-individual.component';

const routes: Routes = [
    { path: '', redirectTo: 'cadastro-usuario', pathMatch: 'full' },
    { path: 'cadastro-usuario', component: CadastroUsuarioComponent },
    { path: 'cadastro-usuario/adicionar/:id', component: CadastroUsuarioIndividualComponent },
    { path: 'permissoes-usuarios', component: PermissaoUsuarioComponent },
    { path: 'permissoes-usuarios/adicionar/:id', component: PermissaoUsuarioIndividualComponent },
    // { path: 'configuracao-parametros', component: ConfiguracaoParametrosComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
})
export class PortalAdministrativoRoutingModule { } 
