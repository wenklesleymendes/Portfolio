import { Injectable } from '@angular/core';
import {
  CanActivateChild,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
  CanActivate
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanActivateChild {
  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const isAluno = this.authService.isAluno();
    // return true;
    if (!localStorage.getItem('accessToken')) {
      this.router.navigate(['login']);
      return false;
    }

    if(next?.data?.noOne) {
      if(isAluno) {
        this.router.navigate(['initial']);
        return false;
      } else {
        this.router.navigate(['ticket/initial']);
        return false;
      }
    }

    if(isAluno){
      const canAluno = next?.data?.aluno;
      if(canAluno) {
        return true;
      } else {
        this.router.navigate(['initial']);
        return false;
      }
    } else {
      this.canAtiveFuncionario(next);
      return true;
    }
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      const isAluno = this.authService.isAluno();
      
      if(next?.data?.noOne) {
        if(isAluno) {
          this.router.navigate(['initial']);
          return false;
        } else {
          this.router.navigate(['ticket/initial']);
          return false;
        }
      }

    if(isAluno){
      const canAluno = next?.data?.aluno;
      if(canAluno) {
        return true;
      } else {
        return false;
      }
    } else {
      const cantFuncionario = next?.data?.cantFuncionario;
      if(cantFuncionario) {
        this.router.navigate(['ticket/initial']);
        return false;
      } else {
        // this.canAtiveFuncionario(next);
        return true
      }
    }
  }

  canAtiveFuncionario(next: ActivatedRouteSnapshot): boolean {
    let url: string = '';
    const token = this.authService.getToken();
    const perfil = token?.user?.perfilUsuario;
    if(!perfil) return true;
    next.pathFromRoot.forEach(elem => {
      if(elem.url.length) url += `${elem.url}/`;
    });
    url = url.slice(0, (url.length - 1));
    if(url === 'ticket/initial') return true;
    
    const routeRelattion = [
      { inToken: 'consultarAluno',            inRoute: 'alunos/consultar-alunos'},
      { inToken: 'relatorios',                inRoute: 'relatorios'},
      { inToken: 'criacaoUsuario',            inRoute: 'portal-adm/cadastro-usuario'},
      { inToken: 'criacaoPerfil',             inRoute: 'portal-adm/permissoes-usuarios'},
      { inToken: 'configuradorParametros',    inRoute: 'portal-adm/configuracao-parametros'},
      { inToken: 'ticketPainel',              inRoute: 'ticket/meus-ticket'},
      { inToken: 'ticketAdministracao',       inRoute: 'ticket/administracao-ticket'},
      { inToken: 'comunicacao',               inRoute: 'comunicacao'},
      { inToken: 'cadastroFuncionario',       inRoute: 'rh/cadastro-funcionario'},
      { inToken: 'controlePonto',             inRoute: 'rh/controle-ponto'},
      { inToken: 'escalaServico',             inRoute: 'rh/escala-servico'},
      { inToken: 'uploadPonto',               inRoute: 'rh/upload-ponto'},
      { inToken: 'cadastroFornecedorCliente', inRoute: 'financeiro/cadastro-fornecedor-cliente'},
      { inToken: 'contasAPagar',              inRoute: 'financeiro/contas-pagar'},
      { inToken: 'estoque',                   inRoute: 'financeiro/estoque'},
      { inToken: 'folhaPagamento',            inRoute: 'financeiro/folha-pagamento'},
      { inToken: 'criarColegioAutorizado',    inRoute: 'provas/colegio-autorizado'},
      { inToken: 'criarAgendaProva',          inRoute: 'provas/criar-agenda-provas'},
      { inToken: 'listaPassageiros',          inRoute: 'provas/lista-passageiros'},
      { inToken: 'listaPassageiros',          inRoute: 'provas/historico-prova'},
      { inToken: 'unidade',                   inRoute: 'gerenciador/unidades'},
      { inToken: 'cursoTurma',                inRoute: 'gerenciador/curso-turmas'},
      { inToken: 'planoPagamento',            inRoute: 'gerenciador/planos-pagamentos'},
      { inToken: 'promocoesBolsaConvenio',    inRoute: 'gerenciador/promocoes-bolsas-convenio'},
      { inToken: 'solicitacao',               inRoute: 'gerenciador/solicitacoes'},
      { inToken: 'criarMeta',                 inRoute: 'metas-comissoes/criacao-metas'},
      { inToken: 'metaPainel',                inRoute: 'metas-comissoes/painel-metas-comissoes'},
      { inToken: 'criarComissoes',            inRoute: 'metas-comissoes/criacao-comissao'},
      { inToken: 'criarAulaOnline',           inRoute: 'aula-online/criar-aula-online'},
      { inToken: 'minhasAulas',               inRoute: 'aula-online/minhas-aulas'},
      { inToken: 'ejaEncceja',                inRoute: 'alunos/eja-encceja'}
    ];
    
    const find = routeRelattion.find(elem => elem.inRoute == url);
    if(find) {
      if(perfil[find.inToken]) {
        return true;
      } else {
        this.router.navigate(['ticket/initial']);
        return false;
      }
    }
    return false;
  }
}
