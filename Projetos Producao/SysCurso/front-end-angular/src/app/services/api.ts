export const API = {
    endereco: 'https://viacep.com.br/ws',
    instituicaoBancaria: '/api/InstituicaoBancaria/BuscarTodos',
    usuario: {
        login: '/api/Usuario/Login',
        cadastrar: '/api/Usuario/Cadastrar',
        buscarTodos: '/api/Usuario/BuscarTodos',
        buscarAtendente: '/api​/Usuario​/BuscarUsuarioAtendente',
        filtrarUsuario: '/api/Usuario/FiltrarUsuario',
        buscarPorId: '/api/Usuario/BuscarPorId',
        desativarAtivar: '/api/Usuario/DesativarOuAtivar',
        deletar: '/api/Usuario/Excluir'
    },
    unidade: {
        cadastrar: '/api/Unidade/Cadastrar',
        buscarTodos: '/api/Unidade/BuscarTodos',
        buscarUnidadesTicket: '/api/Unidade/BuscarUnidadesTicket',
        buscarPorId: '/api/Unidade/BuscarPorId',
        deletarPorId: '/api/Unidade/DeletarPorId',
        uploadFoto: '/api/Unidade/UploadFoto',
        selecionarFoto: '/api/Unidade/SelecionarFoto',
        removerFoto: '/api/Unidade/RemoverFoto',
    },
    centroCusto: {
        cadastrar: '/api/CentroCusto/Cadastrar',
        buscarPorUnidade: '/api/CentroCusto/BuscarPorUnidade',
        deletar: '/api/CentroCusto/Deletar'
    },
    anexo: {
        cadastrar: '/api/Anexo/Cadastrar',
        buscarAnexo: '/api/Anexo/BuscarAnexo',
        buscarDocumentosDespesa: '/api/Anexo/BuscarDocumentosDespesa',
        deletar: '/api/Anexo/Deletar',
        download: '/api/Anexo/Download',
        recusarDocumento: '/api/Anexo/RecusarDocumento'
    },
    curso: {
        cadastrar: '/api/Curso/Cadastrar',
        cadastrarMateria: '/api/Curso/CadastrarMaterias',
        buscarTodos: '/api/Curso/BuscarTodos',
        buscarPorUnidade: '/api/Curso/BuscarPorUnidade',
        buscarCursosComMaterias: '/api/Curso/BuscarCursosComMaterias',
        buscarPorId: '/api/Curso/BuscarPorId',
        deletar: '/api/Curso/Excluir',
        deletarMateria: '/api/Curso/ExcluirMateria'
    },
    turma: {
        cadastrar: '/api/Turma/Cadastrar',
        transferirDeTurma: '/api/Turma/TransferirDeTurma',
        buscarPorId: '/api/Turma/BuscarPorId',
        buscarTurmasDisponiveis: '/api/Turma/BuscarTurmasDisponiveis',
        buscarTodos: '/api/Turma/BuscarTodos',
        deletar: '/api/Turma/Excluir'
    },
    planoPagamento: {
        cadastrar: '/api/PlanoPagamento/Cadastrar',
        buscarPorId: '/api/PlanoPagamento/BuscarPorId',
        buscarTodos: '/api/PlanoPagamento/BuscarTodos',
        buscarPlanoPagamento: '/api/PlanoPagamento/BuscarPlanoPagamento',
        deletar: '/api/PlanoPagamento/Excluir',
        desativarAtivar: '/api/PlanoPagamento/DesativarOuAtivar'
    },
    campanha: {
        cadastrar: '/api/Campanha/Cadastrar',
        buscarPorId: '/api/Campanha/BuscarPorId',
        buscarTodos: '/api/Campanha/BuscarTodos',
        buscarVigente: '/api/Campanha/BuscarCampanhaVigente',
        deletar: '/api/Campanha/Excluir',
        desativarAtivar: '/api/Campanha/DesativarOuAtivar'
    },
    funcionario: {
        cadastrar: '/api/Funcionario/Cadastrar',
        atualizarPontoEletronico: '/api/Funcionario/AtualizarPontoEletronico',
        cadastrarFerias: '/api/Funcionario/CadastrarFerias',
        buscarTodos: '/api/Funcionario/BuscarTodos',
        buscarPorId: '/api/Funcionario/BuscarPorId',
        buscarPorCPF: '/api/Funcionario/BuscarPorCPF',
        buscarPontoEletronico: '/api/Funcionario/BuscarPontoEletronico',
        buscarFeriasPorFuncionario: '/api/Funcionario/BuscarFeriasPorFuncionario',
        buscarDetalhamentoFerias: '/api/Funcionario/BuscarDetalhamentoFerias',
        deletar: '/api/Funcionario/Excluir',
        deletarFerias: '/api/Funcionario/ExcluirFerias',
        desativarAtivar: '/api/Funcionario/DesativarOuAtivar'
    },
    perfilUsuario: {
        cadastrar: '/api/PerfilUsuario/Cadastrar',
        buscarPorId: '/api/PerfilUsuario/BuscarPorId',
        buscarTodosAtivos: '/api/PerfilUsuario/BuscarAtivos',
        buscarTodos: '/api/PerfilUsuario/BuscarTodos',
        deletar: '/api/PerfilUsuario/Excluir',
        desativarAtivar: '/api/PerfilUsuario/DesativarOuAtivar'
    },
    assuntoTicket: {
        cadastrar: '/api/AssuntoTicket/Cadastrar',
        buscarPorId: '/api/AssuntoTicket/BuscarPorId',
        buscarTodos: '/api/AssuntoTicket/BuscarTodos',
        buscarPorUnidadeDepartamento: '/api/AssuntoTicket/BuscarPorUnidadeDepartamento',
        deletar: '/api/AssuntoTicket/Excluir'
    },
    ticket: {
        cadastrar: '/api/Ticket/Cadastrar',
        buscarPorId: '/api/Ticket/BuscarPorId',
        buscarTodos: '/api/Ticket/BuscarTodos',
        filtrar: '/api/Ticket/Filtrar',
        buscarMensagensTicket: '/api/Ticket/BuscarMensagensTicket',
        responderTicket: '/api/Ticket/ResponderTicket',
        consultarDashboard: '/api/Ticket/ConsultarDashboard',
        deletar: '/api/Ticket/Excluir'
    },
    ocorrencia: {
        cadastrar: '/api/Ocorrencia/Cadastrar',
        buscarTimeline: '/api/Ocorrencia/BuscarTimeline',
    },
    comissoes: {
        cadastrar: '/api/Comissoes/Cadastrar',
        buscarPorId: '/api/Comissoes/BuscarPorId',
        buscarTodos: '/api/Comissoes/BuscarTodos',
        buscarTodosFiltro: '/api/Comissoes/Filtrar',
        dashboardMinhasComissoes: '/api/Comissoes/DashboardMinhasComissoes',
        deletar: '/api/Comissoes/Excluir'
    },
    metas: {
        cadastrar: '/api/Metas/Cadastrar',
        buscarPorId: '/api/Metas/BuscarPorId',
        buscarTodos: '/api/Metas/BuscarTodos',
        buscarTodosFiltro: '/api/Metas/Filtrar',
        buscarDashboard: '/api/Metas/ConsultarDashboard',
        listaNomeMetas: '/api/Metas/ListaNomeMetas',
        deletar: '/api/Metas/Excluir'
    },
    fornecedor: {
        cadastrar: '/api/Fornecedor/Cadastrar',
        buscarPorId: '/api/Fornecedor/BuscarPorId',
        buscarTodos: '/api/Fornecedor/BuscarTodos',
        deletar: '/api/Fornecedor/Excluir',
        desativarAtivar: '/api/Fornecedor/DesativarOuAtivar'
    },
    categoria: {
        cadastrar: '/api/Categoria/Cadastrar',
        buscarPorId: '/api/Categoria/BuscarPorId',
        buscarTodos: '/api/Categoria/BuscarTodos',
        deletar: '/api/Categoria/Excluir'
    },
    controlePonto: {
        subir: '/api/ControlePonto/SubirArquivoPonto',
        buscarTodos: '/api/ControlePonto/BuscarTodosArquivosPonto',
        buscarSaldoHorasExtras: '/api/ControlePonto/BuscarSaldoHorasExtras',
        deletar: '/api/ControlePonto/ExcluirArquivoPonto',
        deletarPontoEletronico: '/api/ControlePonto/ExcluirPontoEletronico'
    },
    estoque: {
        cadastrar: '/api/Estoque/Cadastrar',
        adicionarItem: '/api/Estoque/AdicionarItem',
        buscarPorId: '/api/Estoque/BuscarPorId',
        buscarTodos: '/api/Estoque/BuscarTodos',
        buscarHistoricoPorEstoque: '/api/Estoque/BuscarHistoricoPorEstoque',
        buscarItemProdutoPorEstoque: '/api/Estoque/BuscarItemProdutoPorEstoque',
        deletar: '/api/Estoque/Excluir',
        deletarItem: '/api/Estoque/ExcluirItem'
    },
    folhaPagamento: {
        cadastrar: '/api/FolhaPagamento/Cadastrar',
        buscarPorId: '/api/FolhaPagamento/BuscarPorId',
        buscarTodos: '/api/FolhaPagamento/Filtrar',
        downloadComprovanteBancario: '/api/FolhaPagamento/DownloadComprovanteBancario',
        ImprimirReciboPagamento: '/api/FolhaPagamento/ImprimirReciboPagamento',
        deletar: '/api/FolhaPagamento/Excluir'
    },
    aluno: {
        cadastrar: '/api/Aluno/Cadastrar',
        buscarPorId: '/api/Aluno/BuscarPorId',
        buscarTodos: '/api/Aluno/BuscarTodos',
        filtrarAluno: '/api/Aluno/FiltrarAluno',
        buscarPorCPF: '/api/Aluno/BuscarPorCPF',
        deletar: '/api/Aluno/Excluir',
        selecionarFoto: '/api/Aluno/SelecionarFoto',
        uploadFoto: '/api/Aluno/UploadFoto',
        removerFoto: '/api/Aluno/RemoverFoto'
    },
    alunoFinanceiroContrato: {
        contratarPlano: '/api/AlunoFinanceiroContrato/ContratarPlano',
        consultarPainelFinanceiro: '/api/AlunoFinanceiroContrato/ConsultarPainelFinanceiro',
        efetuarPagamento: '/api/AlunoFinanceiroContrato/EfetuarPagamento',
        enviarBoletoPorEmail: '/api/AlunoFinanceiroContrato/EnviarBoletoPorEmail',
        enviarBoletoPorEmailOuRecalcular: '/api/AlunoFinanceiroContrato/EnviarBoletoPorEmailOuRecalcular',
        consultarEmail: '/api/AlunoFinanceiroContrato/ConsultarEmail',
        gerarBoletoResidual: '/api/AlunoFinanceiroContrato/GerarBoletoResidual',
        efetuarPagamentoCartaoCredito: '/api/AlunoFinanceiroContrato/EfetuarPagamentoCartaoCredito',
        buscarDetalhePagamento: '/api/AlunoFinanceiroContrato/BuscarDetalhePagamento'
    },
    contasPagar: {
        cadastrar: '/api/ContasAPagar/Cadastrar',
        buscarPorId: '/api/ContasAPagar/BuscarPorId',
        buscarTodos: '/api/ContasAPagar/Filtrar',
        buscarDetalheDespesa: '/api/ContasAPagar/BuscarDetalheDespesa',
        liquidarPagamento: '/api/ContasAPagar/LiquidarPagamento',
        imprimirRecibo: '/api/ContasAPagar/ImprimirRecibo',
        deletar: '/api/ContasAPagar/Excluir',
        deletarPagamento: '/api/ContasAPagar/CancelarPagamento'
    },
    naturalidade: {
        cadastrar: '/api/Naturalidade/Cadastrar',
        buscarTodos: '/api/Naturalidade/BuscarTodos',
        deletar: '/api/Naturalidade/Excluir'
    },
    nacionalidade: {
        cadastrar: '/api/Nacionalidade/Cadastrar',
        buscarTodos: '/api/Nacionalidade/BuscarTodos',
        deletar: '/api/Nacionalidade/Excluir'
    },
    provas: {
        cadastrar: '/api/AgendaProvas/Cadastrar',
        buscarPorId: '/api/AgendaProvas/BuscarPorId',
        buscarTodos: '/api/AgendaProvas/BuscarTodos',
        deletar: '/api/AgendaProvas/Excluir'
    },
    colegioAutorizado: {
        cadastrar: '/api/ColegioAutorizado/Cadastrar',
        buscarPorId: '/api/ColegioAutorizado/BuscarPorId',
        buscarTodos: '/api/ColegioAutorizado/BuscarTodos',
        deletar: '/api/ColegioAutorizado/Excluir'
    },
    aulaOnline: {
        cadastrar: '/api/AulaOnline/Cadastrar',
        buscarPorId: '/api/AulaOnline/BuscarPorId',
        buscarPorCurso: '/api/AulaOnline/BuscarPorCurso',
        buscarMaterias: '/api/AulaOnline/BuscarMaterias',
        minhasAulasOnline: '/api/AulaOnline/MinhasAulasOnline',
        buscarTodos: '/api/AulaOnline/BuscarTodos',
        deletar: '/api/AulaOnline/Excluir'
    },
    videoAula: {
        cadastrar: '/api/VideoAula/Cadastrar',
        buscarPorId: '/api/VideoAula/BuscarPorId',
        buscarPorMateria: '/api/VideoAula/BuscarPorMateria',
        deletar: '/api/VideoAula/Excluir',
        buscarUltimaSessao: '/api/VideoAula/BuscarUltimaSessao',
        salvarUltimaSessao: '/api/VideoAula/SalvarUltimaSessao',
        upload: '/api/VideoAula/Upload',
    },
    pergunta: {
        cadastrar: '/api/Pergunta/Cadastrar',
        buscarPorId: '/api/Pergunta/BuscarPorId',
        buscarPorVideoAula: '/api/Pergunta/BuscarPorVideoAula',
        deletar: '/api/Pergunta/Excluir'
    },
    solicitacoes: {
        cadastrar: '/api/Solicitacao/Cadastrar',
        buscarPorId: '/api/Solicitacao/BuscarPorId',
        buscarPorCursoId: '/api/Solicitacao/BuscarPorCursoId',
        buscarTodos: '/api/Solicitacao/BuscarTodos',
        deletar: '/api/Solicitacao/Excluir',
        selecionarFoto: '/api/Solicitacao/SelecionarFoto',
        uploadFoto: '/api/Solicitacao/UploadFoto',
        removerFoto: '/api/Solicitacao/RemoverFoto'
    },
    solicitacaoAluno: {
        efetuarSolicitacao: '/api/SolicitacaoAluno/EfetuarSolicitacao',
        historicoSolicitacao: '/api/SolicitacaoAluno/HistoricoSolicitacao',
        gerarSolicitacao: '/api/SolicitacaoAluno/GerarSolicitacao'
    },
    matriculaAluno: {
        cadastrar: '/api/MatriculaAluno/MatricularAluno',
        buscarPorId: '/api/MatriculaAluno/BuscarPorId',
        buscarMinhasMatriculas: '/api/MatriculaAluno/BuscarMinhasMatriculas',
        consultarMeusProfessores: '/api/MatriculaAluno/ConsultarMeusProfessores',
        jaExistenteMatricula: '/api/MatriculaAluno/JaExistenteMatricula',
        consultarDocumentosPendentes: '/api/MatriculaAluno/ConsultarDocumentosPendentes',
        gerarPendenciaDocumental: '/api/MatriculaAluno/GerarPendenciaDocumental',
        deletar: '/api/MatriculaAluno/Excluir'
    },
    cancelamentoMatricula: {
        buscarPorMatriculaId: '/api/CancelamentoMatricula/BuscarPorMatricula',
        efetuarCancelamento: '/api/CancelamentoMatricula/EfetuarCancelamento',
        salvarAutorizacaoIsencao: '/api/CancelamentoMatricula/SalvarAutorizacaoIsencao',
        gerarMultaCancelamento: '/api/CancelamentoMatricula/GerarMultaCancelamento',
        gerarCartaCancelamento: '/api/CancelamentoMatricula/GerarCartaCancelamento',
    },
    documentosAluno: {
        consultarDocumentosPendentes: '/api/DocumentosAluno/ConsultarDocumentosPendentes',
        gerarPendenciaDocumental: '/api/DocumentosAluno/GerarPendenciaDocumental',
        downloadDeclaracaoPendenciaDocumental: '/api/DocumentosAluno/DownloadDeclaracaoPendenciaDocumental',
        uploadDeclaracaoPendenciaDocumental: '/api/DocumentosAluno/UploadDeclaracaoPendenciaDocumental',
        buscarComprovanteBolsaConvenio: '/api/DocumentosAluno/BuscarComprovanteBolsaConvenio',
        reciboPagamentoMensalidade: '/api/DocumentosAluno/ReciboPagamentoMensalidade'
    },
    contrato: {
        uploadContrato: '/api/Contrato/UploadContrato',
        downloadContrato: '/api/Contrato/DownloadContrato',
        gerarContrato: '/api/Contrato/GerarContrato',
    },
    inconsistenciaDocumento: {
        cadastrar: '/api/InconsistenciaDocumento/Cadastrar',
        buscarTodos: '/api/InconsistenciaDocumento/BuscarTodos',
        buscarPorMatriculaId: '/api/InconsistenciaDocumento/BuscarPorMatriculaId',
        excluir: '/api/InconsistenciaDocumento/Excluir'
    },
    provaAluno: {
        cadastrar: '/api/ProvaAluno/Cadastrar',
        atualizarStatusProva: '/api/ProvaAluno/AtualizarStatusProva',
        buscarPorId: '/api/ProvaAluno/BuscarPorId',
        buscarPorMatriculaId: '/api/ProvaAluno/BuscarPorMatriculaId',
        informacoesCadastro: '/api/ProvaAluno/InformacoesCadastro',
        buscarProvasDisponiveis: '/api/ProvaAluno/BuscarProvasDisponiveis',
        cadastrarCredenciais: '/api/ProvaAluno/CadastrarCredenciais',
        cancelarInscricao: '/api/ProvaAluno/CancelarInscricao',
        imprimirFormulario: '/api/ProvaAluno/ImprimirFormulario',
        buscarProvasRealizadas: '/api/ProvaAluno/BuscarProvasRealizadas'
    },
    certificadoProva: {
        cadastrar: '/api/CertificadoProva/Cadastrar',
        buscarPorMatriculaId: '/api/CertificadoProva/BuscarPorMatriculaId',
        buscarSolicitacaoAtual: '/api/CertificadoProva/BuscarSolicitacaoAtual',
    },
    transporteProva: {
        buscarProximoOnibus: '/api/UnidadeTransporteProva/BuscarProximoOnibus',
        buscarOnibus: '/api/UnidadeTransporteProva/BuscarOnibus'
    },
    historicoProvas: {
        ListaColegioAutorizadoExcel: '/api/HistoricoProvas/ListaColegioAutorizadoExcel',
        ListaGeralDeInscritosParaProvaExcel: '/api/HistoricoProvas/ListaGeralDeInscritosParaProvaExcel',
        ListaDeChamadaOnibusExcel: '/api/HistoricoProvas/ListaDeChamadaOnibusExcel',
        NumeroOnibus: '/api/HistoricoProvas/NumeroOnibus'
    },
    materiaOnline: {
        BuscarMateriasPorCurso: '/api/MateriaOnline/BuscarMateriasPorCurso'
    },
    atendimento: {
        cadastrar: '/api/Atendimento/Cadastrar',
        buscarPorId: '/api/Atendimento/BuscarPorId',
        buscarAgendamentos: '/api/AtendimentoAgendamento/BuscarPorUnidade',
    },
    outbound: {
        cadastrar: '/api/AtendimentoOutbound/Cadastrar',
        listaOutbond: '/api/AtendimentoOutbound/ListaOutbound',
        historicoTentativa: '/api/AtendimentoOutbound/HistoricoTentativas',
    },
}
