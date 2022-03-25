import { NavItem } from '../../interfaces/nav-item.interface';

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
  },
  {
    displayName: 'Financeiro',
    iconName: 'monetization_on',
    route: '/alunos/matricula-aluno/aluno-financeiro-contrato'
  },
  {
    displayName: 'Solicitação',
    iconName: 'outbox',
    route: '/alunos/matricula-aluno/aluno-solicitacoes'
  },
  {
    displayName: 'Documentos',
    iconName: 'folder_open',
    route: '/alunos/matricula-aluno/aluno-documentos'
  },
  {
    displayName: 'Conteúdo',
    iconName: 'task_alt',
    route: '/alunos/matricula-aluno/aluno-portal'
  },
  {
    displayName: 'Eja - Encceja',
    iconName: 'school',
    route: 'alunos/eja-encceja'
  },
  {
    displayName: 'YouTube',
    iconName: 'smart_display',
    route: '/alunos/matricula-aluno/aluno-portal' //colocar rota atual para não mudar a pagina
  },
  {
    displayName: 'Telegram',
    iconName: 'telegram',
    route: '/alunos/matricula-aluno/aluno-portal' //colocar rota atual para não mudar a pagina
  },
  {
    displayName: 'Sair',
    iconName: 'close',
    route: '/login'
  }
]

export const NAVITEMLIST: NavItem[] = [
  {
    displayName: 'Alunos',
    iconName: 'account_circle',
    route: 'alunos/consultar-alunos'
  },
  {
    displayName: 'Relatórios',
    iconName: 'note',
    route: 'relatorios',
    children: [
      {
        displayName: 'Matrículas',
        iconName: null,
        route: 'relatorios/matriculas'
      },
      {
        displayName: 'Cancelamentos',
        iconName: null,
        route: 'relatorios/cancelamentos'
      },
      {
        displayName: 'Comissões',
        iconName: null,
        route: 'relatorios/comissoes'
      },
      {
        displayName: 'Financeiro',
        iconName: null,
        route: 'relatorios/financeiro'
      },
      {
        displayName: 'Provas',
        iconName: null,
        route: 'relatorios/provas'
      },
      {
        displayName: 'Certificados',
        iconName: null,
        route: 'relatorios/certificados'
      },
      {
        displayName: 'Disparos Realizados',
        iconName: null,
        route: 'relatorios/disparos-realizados'
      },
      {
        displayName: 'Afiliados',
        iconName: null,
        route: 'relatorios/afiliados'
      }
    ]
  },
  {
    displayName: 'Portal do Adminstrador',
    iconName: 'lock',
    route: 'portal-adm',
    children: [
      {
        displayName: 'Cadastro de Usuários',
        iconName: null,
        route: 'portal-adm/cadastro-usuario'
      },
      {
        displayName: 'Permissões de Usuários',
        iconName: null,
        route: 'portal-adm/permissoes-usuarios'
      },
      {
        displayName: 'Configurador de Parâmetros',
        iconName: null,
        route: 'portal-adm/configuracao-parametros'
      }
    ]
  },
  {
    displayName: 'Ticket',
    iconName: 'work',
    route: 'ticket',
    children: [
      {
        displayName: 'Tickets',
        iconName: null,
        route: 'ticket/meus-ticket'
      },
      {
        displayName: 'Administração de Ticket',
        iconName: null,
        route: 'ticket/administracao-ticket'
      }
    ]
  },
  {
    displayName: 'Comunicação',
    iconName: 'chat',
    route: 'comunicacao'
  },
  {
    displayName: 'Recursos humanos',
    iconName: 'group',
    route: 'rh',
    children: [
      {
        displayName: 'Cadastro de Funcionário',
        iconName: null,
        route: 'rh/cadastro-funcionario'
      },
      {
        displayName: 'Controle de Ponto',
        iconName: null,
        route: 'rh/controle-ponto'
      },
      {
        displayName: 'Escala de Serviço',
        iconName: null,
        route: 'rh/escala-servico'
      }
      ,
      {
        displayName: 'Upload Ponto',
        iconName: null,
        route: 'rh/upload-ponto'
      }
    ]
  },
  {
    displayName: 'Financeiro',
    iconName: 'attach_money',
    route: 'financeiro',
    children: [
      {
        displayName: 'Cadastro Fornecedor ou Cliente',
        iconName: null,
        route: 'financeiro/cadastro-fornecedor-cliente'
      },
      {
        displayName: 'Contas a Pagar',
        iconName: null,
        route: 'financeiro/contas-pagar'
      },
      {
        displayName: 'Estoque',
        iconName: null,
        route: 'financeiro/estoque'
      },
      {
        displayName: 'Folha de Pagamento',
        iconName: null,
        route: 'financeiro/folha-pagamento'
      }
    ]
  },
  {
    displayName: 'Provas',
    iconName: 'import_contacts',
    route: 'provas',
    children: [
      {
        displayName: 'Criar Colégio Autorizado',
        iconName: null,
        route: 'provas/colegio-autorizado'
      },
      {
        displayName: 'Criar Agenda de Provas',
        iconName: null,
        route: 'provas/criar-agenda-provas'
      },
      // {
      //   displayName: 'Lista de Passageiros',
      //   iconName: null,
      //   route: 'provas/lista-passageiros'
      // },
      {
        displayName: 'Histórico de Provas',
        iconName: null,
        route: 'provas/historico-prova'
      }
    ]
  },
  {
    displayName: 'Gerenciador',
    iconName: 'visibility',
    route: 'gerenciador',
    children: [
      {
        displayName: 'Unidades',
        iconName: null,
        route: 'gerenciador/unidades'
      },
      {
        displayName: 'Curso e Turmas',
        iconName: null,
        route: 'gerenciador/curso-turmas'
      },
      {
        displayName: 'Planos de pagamentos',
        iconName: null,
        route: 'gerenciador/planos-pagamentos'
      },
      {
        displayName: 'Promoções, Bolsa e Convênio',
        iconName: null,
        route: 'gerenciador/promocoes-bolsas-convenio'
      },
      {
        displayName: 'Solicitações',
        iconName: null,
        route: 'gerenciador/solicitacoes'
      }
    ]
  },
  {
    displayName: 'Metas e Comissões',
    iconName: 'open_in_new',
    route: 'metas-comissoes',
    children: [
      {
        displayName: 'Criação de Metas',
        iconName: null,
        route: 'metas-comissoes/criacao-metas'
      },
      {
        displayName: 'Painel de Metas e Comissões',
        iconName: null,
        route: 'metas-comissoes/painel-metas-comissoes'
      },
      {
        displayName: 'Criação de Comissões',
        iconName: null,
        route: 'metas-comissoes/criacao-comissao'
      }
    ]
  },
  {
    displayName: 'Menu Atendimento',
    iconName: 'phone',
    route: 'menu-atendimento/home'
  },
  {
    displayName: 'Aulas On-line',
    iconName: 'school',
    route: 'aula-online',
    children: [
      {
        displayName: 'Criar Aulas On-line',
        iconName: null,
        route: 'aula-online/criar-aula-online'
      },
      {
        displayName: 'Minhas Aulas',
        iconName: null,
        route: 'aula-online/minhas-aulas'
      }
    ]
  },
  {
    displayName: 'Eja - Encceja',
    iconName: 'local_library',
    route: 'alunos/eja-encceja'
  },
  {
    displayName: 'Sair',
    iconName: 'close',
    route: '/login'
  },
];
