import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/security/auth-guard.guard';
import { LojaComponent } from './loja/loja.component';
import { PortalComponent } from './portal/portal.component';

const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: '', canActivate: [AuthGuard], data: { aluno: true, cantFuncionario: true } },
    { path: './portal', component: PortalComponent },
    { path: './loja', component: LojaComponent },
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
  })
  export class AfiliadoRoutingModule { }