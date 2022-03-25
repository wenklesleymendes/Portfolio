import {
  Component,
  AfterViewInit,
  ViewChild,
  OnDestroy,
  OnInit,
  ChangeDetectorRef
} from "@angular/core";
import { NavigationEnd, NavigationStart, Router } from "@angular/router";
import { NAVITEMLIST, NAVITEMLISTALUNO } from "../menu-list-item/nav-iem";
import { NavService } from "../menu-list-item/nav.service";
import { AuthService } from 'src/app/security/auth.service';
import { MatSidenav } from '@angular/material/sidenav';
import { version } from "../../../../package.json";
import { AnimationsService } from 'src/app/services/animations.service';
import { NavItem } from 'src/app/interfaces/nav-item.interface';

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrls: ['./body.component.scss']
})
export class BodyComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('menuList', { static: true }) menuList: MatSidenav;

  navItemList: any[] = NAVITEMLIST;
  version = version;
  showBar: boolean = false;
  sidebarMode: string = 'over';
  user: any = null
  nome: string = null;
  menuIsChanging: boolean = false;
  logoLocalStorage = '';
  isAlunoBody: boolean = false;
  nav: boolean = false;
  sidebar: HTMLElement;

  constructor(
    private router: Router,
    private authService: AuthService,
    private navService: NavService,
    private animationsService: AnimationsService,
    private _changeDetectionRef : ChangeDetectorRef,

  ) {
    this.isAlunoBody = this.authService.isAluno();
    if(!this.isAlunoBody)
      window.localStorage.setItem('logoLocalStorage', '../../../assets/portal_logos.png');
    this.logoLocalStorage = window.localStorage.getItem('logoLocalStorage');
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {

    this.isAlunoBody = this.authService.isAluno();
    this.adaptMenu();
    this.sidebar = document.getElementById("hamburgerMenu");
    this.animationsService.getProgressBar().subscribe(val => {
      this.showBar = val;
      this._changeDetectionRef.detectChanges();

    });

    //const isMobileResolution = window.innerWidth < 768 ? true : false;
    //this.sidebarMode = isMobileResolution ? 'over' : 'over';

    // Due to the Highchart reflow method
   // this.menuList._animationEnd.subscribe(() => window.dispatchEvent(new Event('resize')))

    const user = this.authService.getToken();
    this.user = user?.user;

    if(user?.user?.funcionario?.nome) {
      const nome = user.user.funcionario.nome.split(' ')[0];
      this.nome = nome;
    }

    this.menuList._animationStarted.subscribe(_ => this.menuIsChanging = true);
    this.menuList._animationEnd.subscribe(_ => this.menuIsChanging = false);
  }

  ngAfterViewInit(): void {
    const isMobileResolution = window.innerWidth < 768 ? false : false;
    this.navService.menuList = this.menuList;
    // Due to the lazyload delay of the modules
    if (!isMobileResolution) setTimeout(() => this.menuList.toggle(), 250);
  }

  ngOnDestroy(): void {
    this.animationsService.completeAllSubjects();
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  menuClick(): void {
    if(!this.menuIsChanging) this.menuList.toggle();
  }

  goHome(): void {
    const isAluno = this.authService.isAluno();
    if(isAluno) {
      this.router.navigate(['/initial']);
    } else {
      this.router.navigate(['ticket/initial']);
    }
  }

  adaptMenu(): void {
    const isAluno = this.authService.isAluno();
    if(isAluno) {
      this.navItemList = NAVITEMLISTALUNO;

      var cursoId = Number(window.localStorage.getItem('cursoIdLocalStorage'));
        this.navItemList.forEach(element => {
          if(cursoId != 1 && cursoId != 2 && cursoId !=3 && cursoId != 4)
          {
            if(element.displayName == "Eja - Encceja")
            {
              element.displayName = "EAD";
            }
          }
          else
          {
            if(element.displayName == "EAD")
            {
              element.displayName = "Eja - Encceja";
            }
          }

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

        });
      return;
    }

    const token = this.authService.getToken();
    const perfil = token?.user?.perfilUsuario;
    if(!perfil) return;

    const routeRelattion = [
      { inToken: 'consultarAluno',            inRoute: 'alunos/consultar-alunos'},
      { inToken: 'relatorios',                inRoute: 'relatorios/matriculas'},
      { inToken: 'relatorios',                inRoute: 'relatorios/cancelamentos'},
      { inToken: 'relatorios',                inRoute: 'relatorios/comissoes'},
      { inToken: 'relatorios',                inRoute: 'relatorios/financeiro'},
      { inToken: 'relatorios',                inRoute: 'relatorios/provas'},
      { inToken: 'relatorios',                inRoute: 'relatorios/certificado'},
      { inToken: 'relatorios',                inRoute: 'relatorios/disparos-realizados'},
      { inToken: 'relatorios',                inRoute: 'relatorios/afiliados'},
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

  // public icon = 'close'; {

  //   public changeIcon(newIcon: string ){
  //     this.icon = newIcon ;
  //   }
  // }

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
    /* Abre e fecha o menu */
    // this.nav ? this.closeNav() : this.openNav();

    var menuHamburguer = (<HTMLInputElement>document.getElementById('hamburgerMenu'));
    if(menuHamburguer != null)
    {
      menuHamburguer.classList.contains("change") ? this.closeNav(): this.openNav();
    }
  }
}
