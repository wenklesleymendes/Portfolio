import { Component, OnInit, ViewChild, ChangeDetectorRef } from "@angular/core";
import { AuthService } from 'src/app/security/auth.service';
import { NavigationEnd, NavigationStart, Router } from "@angular/router";
import { MatSidenav } from '@angular/material/sidenav';
import { AnimationsService } from 'src/app/services/animations.service';
import { NavItem } from 'src/app/interfaces/nav-item.interface';
import { NAVITEMLIST } from "./modulos/nav-bar/nav-iem";
import { NavService } from "../menu-list-item/nav.service";


@Component({
    selector: 'app-afiliado',
    templateUrl: './afiliado.component.html',
    styleUrls: ['./afiliado.component.scss']
})

export class AfiliadoComponent implements OnInit {
  @ViewChild('menuList', { static: true }) menuList: MatSidenav;
  
  navItemList: any[] = NAVITEMLIST;
  menuIsChanging: boolean = false;
  sidebar: HTMLElement;
  nav: boolean = false;
  showBar: boolean = false;
  showFiller = false;
  sidebarMode: string = 'over';

  constructor(
    private router: Router,
    private authService: AuthService,
    private animationsService: AnimationsService,
    private _changeDetectionRef : ChangeDetectorRef,
    private navService: NavService,
  ) {
  }

  ngOnInit(): void {

    this.sidebar = document.getElementById("hamburgerMenu");
    this.animationsService.getProgressBar().subscribe(val => {
    this.showBar = val;
    this._changeDetectionRef.detectChanges();

  });
    this.menuList._animationStarted.subscribe(_ => this.menuIsChanging = true);
    this.menuList._animationEnd.subscribe(_ => this.menuIsChanging = false);
  }

  ngAfterViewInit(): void {
    const isMobileResolution = window.innerWidth < 768 ? false : false;
    this.navService.menuList = this.menuList;
    // Due to the lazyload delay of the modules
    if (!isMobileResolution) setTimeout(() => this.menuList.toggle(), 250);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  menuClick(): void {
    if(!this.menuIsChanging) this.menuList.toggle();
  }

  openNav() {
    /* Troca a animação do icone no menu */
    this.sidebar.classList.add("change")
    /* Marca que o menu está aberto */
    this.nav = true;
  }

  closeNav() {
    /* Troca a animação do icone no menu */
    this.sidebar.classList.remove("change")
    /* Marca que o menu está fechado */
    this.nav = false;
  }

  toggleNav() {
    var menuHamburguer = (<HTMLInputElement>document.getElementById('hamburgerMenu'));
    if(menuHamburguer != null)
    {
      menuHamburguer.classList.contains("change") ? this.closeNav(): this.openNav();
    }
  }

  adaptMenu(): void {
    // const isAluno = this.authService.isAluno();
    // if(isAluno) {
    //   this.navItemList = NAVITEMLISTALUNO;

    //   var cursoId = Number(window.localStorage.getItem('cursoIdLocalStorage'));
    //     this.navItemList.forEach(element => {
    //       if(cursoId != 1 && cursoId != 2 && cursoId !=3 && cursoId != 4)
    //       {
    //         if(element.displayName == "Eja - Encceja")
    //         {
    //           element.displayName = "EAD";
    //         }
    //       }
    //       else
    //       {
    //         if(element.displayName == "EAD")
    //         {
    //           element.displayName = "Eja - Encceja";
    //         }
    //       }

          // //raul teste youtube
          // if(cursoId != 1 && cursoId != 2 && cursoId !=3 && cursoId != 4)
          // {
          //   if(element.displayName == "YouTube")
          //   {
          //     element.route = "https://www.youtube.com/channel/UCfgpZT7ibTWvyGNTnWTheBw";
              
          //   }
          // }
          // else
          // {
          //   if(element.displayName == "YouTube")
          //   {
          //     element.route = "https://www.youtube.com/c/SupletivoPreparat%C3%B3rio";
          //   }
          // }

          // //raul teste telegram
          // if(cursoId != 1 && cursoId != 2 && cursoId !=3 && cursoId != 4)
          // {
          //   if(element.displayName == "Telegram")
          //   {
          //     element.route = "https://t.me/cursosnacionaltec";
          //   }
          // }
          // else
          // {
          //   if(element.displayName == "Telegram")
          //   {              
          //     element.route = "https://t.me/supletivopreparatorioejaencceja";
          //     window.open('https://t.me/supletivopreparatorioejaencceja', '_blank');
          //   }
          // }

    //     });
    //   return;
    // }

    const token = this.authService.getToken();
    const perfil = token?.user?.perfilUsuario;
    if(!perfil) return;

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

    let newNaviList = NAVITEMLIST.map(elemParent => {
      if(elemParent?.children) {
        const children: any[] = elemParent?.children;
        const newParent: NavItem = elemParent;
        const newChildren: NavItem[] = [];
        children.forEach(elemChildren => {
          const find = routeRelattion.find(elemFind => elemFind.inRoute === elemChildren.route);
          if(find?.inToken) {
            if(perfil[find.inToken]) {
              newChildren.push(elemChildren);
            }
          }
        });
        if(newChildren?.length > 0) {
          newParent.children = newChildren;
          return newParent;
        } else {
          return null;
        }
      } else {
        const find = routeRelattion.find(elemFind => elemFind.inRoute === elemParent.route);

        if(find?.inToken) {
          if(perfil[find.inToken]) {
            return elemParent;
          }
        }
      }
    })
    newNaviList = newNaviList.filter(elem => elem?.displayName);
    this.navItemList = newNaviList;
  }
}