import { NavItem } from "src/app/interfaces/nav-item.interface";

export const NAVITEMLISTALUNO: NavItem[] = [
  {
    displayName: 'Home',
    iconName: 'home',
    route: 'initial'
  },
  {
    displayName: 'Minhas Aulas',
    iconName: 'cast_for_education',
    route: '/alunos/matricula-aluno/aluno-curso-turma'
  }
]