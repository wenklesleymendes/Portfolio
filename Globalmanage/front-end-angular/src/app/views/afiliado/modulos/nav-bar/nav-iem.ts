import { NavItem } from "src/app/interfaces/nav-item.interface";

export const NAVITEMLIST: NavItem[] = [
  {
    displayName: 'Home',
    iconName: 'home',
    route: '/afiliado/home'
  },
  {
    displayName: 'Meus Dados',
    iconName: 'person',
    route: '/afiliado/meus-dados'
  },
  {
    displayName: 'Minha Loja Virtual',
    iconName: 'store',
    route: '/afiliado/minha-loja-virtual'
  },
  {
    displayName: 'Dinheiro no Bolso',
    iconName: 'monetization_on',
    route: '/afiliado/dinheiro-no-bolso'
  },
  {
    displayName: 'Telegram',
    iconName: 'telegram',
    route: '/alunos/matricula-aluno/aluno-curso-turma'
  },
  {
    displayName: 'Sair',
    iconName: 'close',
    route: '/login'
  }
]