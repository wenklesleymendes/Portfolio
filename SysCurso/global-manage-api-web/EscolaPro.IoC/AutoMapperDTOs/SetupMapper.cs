using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.AlunoQuestionarioProva;
using EscolaPro.Core.Model.ArquivoRemessa;
using EscolaPro.Core.Model.ArquivoRemessa.ArquivoSimples;
using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Core.Model.AulasOnline;
using EscolaPro.Core.Model.BolsaConvenio;
using EscolaPro.Core.Model.CadastroAluno;
using EscolaPro.Core.Model.ContasPagar;
using EscolaPro.Core.Model.ControlePontoEletronico;
using EscolaPro.Core.Model.CursoTurma;
using EscolaPro.Core.Model.DadosFuncionario;
using EscolaPro.Core.Model.EstoqueProdutos;
using EscolaPro.Core.Model.FolhaPagamentos;
using EscolaPro.Core.Model.Fornecedores;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.PainelMatricula.PlanoAluno;
using EscolaPro.Core.Model.PortalAlunoProfessor;
using EscolaPro.Core.Model.Provas;
using EscolaPro.Core.Model.ReguaContato;
using EscolaPro.Core.Model.Solicitacoes;
using EscolaPro.Core.Model.Tickets;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.AgendaProvaVO;
using EscolaPro.Service.Dto.AlunoQuestionarioProvaVO;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.AtendimentoVO;
using EscolaPro.Service.Dto.AulaOnlineVO;
using EscolaPro.Service.Dto.CampanhaVO;
using EscolaPro.Service.Dto.ContasAPagarVO;
using EscolaPro.Service.Dto.ControlePontoVO;
using EscolaPro.Service.Dto.ControleUsuarioVO;
using EscolaPro.Service.Dto.DocumentosAlunoVO;
using EscolaPro.Service.Dto.EmailVO;
using EscolaPro.Service.Dto.EstoqueVO;
using EscolaPro.Service.Dto.FolhaPagamentoVO;
using EscolaPro.Service.Dto.FornecedorVO;
using EscolaPro.Service.Dto.FuncionarioVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO.PlanoAlunoVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO.SolicitacaoVO;
using EscolaPro.Service.Dto.MetasComissoesVO;
using EscolaPro.Service.Dto.PagamentosVO;
using EscolaPro.Service.Dto.PlanoPagamentoVO;
using EscolaPro.Service.Dto.PortalAlunoProfessorVO;
using EscolaPro.Service.Dto.ReguaContato;
using EscolaPro.Service.Dto.TicketVO;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Globalization;

namespace EscolaPro.IoC
{
    public static class SetupMapper
    {
        public static void Start(IMapperConfigurationExpression cfg)
        {
            cfg.AllowNullCollections = true;

            cfg.CreateMap<DtoTurma, Turma>();
            cfg.CreateMap<Turma, DtoTurma>();

            cfg.CreateMap<DtoUnidadeRequest, Unidade>();
            cfg.CreateMap<DtoUnidadeResponse, Unidade>();
            cfg.CreateMap<Unidade, DtoUnidadeResponse>();
            cfg.CreateMap<DtoUnidadeTurma, Unidade>();
            cfg.CreateMap<Unidade, DtoUnidadeTurma>();

            cfg.CreateMap<DtoUnidadeResponse, DtoUnidadeTurma>();
            cfg.CreateMap<DtoUnidadeTurma, DtoUnidadeResponse>();

            cfg.CreateMap<CentroCusto, DtoCentroCusto>();
            cfg.CreateMap<DtoCentroCusto, CentroCusto>();

            cfg.CreateMap<Contato, DtoContato>();
            cfg.CreateMap<DtoContato, Contato>();

            cfg.CreateMap<Contato, DtoContatoSimples>();
            cfg.CreateMap<DtoContatoSimples, Contato>();

            cfg.CreateMap<Endereco, DtoEndereco>();
            cfg.CreateMap<DtoEndereco, Endereco>();

            cfg.CreateMap<ContratoLocacao, DtoContratoLocacao>();
            cfg.CreateMap<DtoContratoLocacao, ContratoLocacao>();

            cfg.CreateMap<DadosBancario, DtoDadosBancario>();
            cfg.CreateMap<DtoDadosBancario, DadosBancario>();

            cfg.CreateMap<HorarioFuncionamento, DtoHorarioFuncionamento>();
            cfg.CreateMap<DtoHorarioFuncionamento, HorarioFuncionamento>();

            cfg.CreateMap<DadosBancario, DtoDadosBancario>();
            cfg.CreateMap<DtoDadosBancario, DadosBancario>();

            cfg.CreateMap<HistoricoOcorrencias, DtoHistoricoOcorrencias>();
            cfg.CreateMap<DtoHistoricoOcorrencias, HistoricoOcorrencias>();

            cfg.CreateMap<UnidadeDespesa, DtoUnidadeDespesa>();
            cfg.CreateMap<DtoUnidadeDespesa, UnidadeDespesa>();

            cfg.CreateMap<Anexo, DtoAnexo>();
            cfg.CreateMap<DtoAnexo, Anexo>();

            //cfg.CreateMap<AnexoFiltrar, DtoAnexoFiltrar>();
            //cfg.CreateMap<DtoAnexoFiltrar, AnexoFiltrar>();


            cfg.CreateMap<DtoPlanoPagamento, PlanoPagamento>();
            cfg.CreateMap<PlanoPagamento, DtoPlanoPagamento>();

            cfg.CreateMap<Campanha, DtoCampanha>();
            cfg.CreateMap<DtoCampanha, Campanha>();

            cfg.CreateMap<CampanhaTipoPagamento, DtoCampanhaPlanoPagamento>();
            cfg.CreateMap<DtoCampanhaPlanoPagamento, CampanhaTipoPagamento>();

            cfg.CreateMap<CampanhaCurso, DtoCampanhaCurso>();
            cfg.CreateMap<DtoCampanhaCurso, CampanhaCurso>();

            cfg.CreateMap<CampanhaUnidade, DtoCampanhaUnidade>();
            cfg.CreateMap<DtoCampanhaUnidade, CampanhaUnidade>();

            // Funcionarios
            cfg.CreateMap<Funcionario, DtoFuncionarioResponse>();
            cfg.CreateMap<DtoFuncionarioResponse, Funcionario>();

            cfg.CreateMap<Funcionario, DtoFuncionario>();
            cfg.CreateMap<DtoFuncionario, Funcionario>();

            cfg.CreateMap<Contato, DtoContatoFuncionario>();
            cfg.CreateMap<DtoContatoFuncionario, Contato>();

            cfg.CreateMap<DadosContratacao, DtoDadosContratacao>();
            cfg.CreateMap<DtoDadosContratacao, DadosContratacao>();

            cfg.CreateMap<AgenteIntegracao, DtoAgenteIntegracao>();
            cfg.CreateMap<DtoAgenteIntegracao, AgenteIntegracao>();

            cfg.CreateMap<JornadaTrabalho, DtoJornadaTrabalho>();
            cfg.CreateMap<DtoJornadaTrabalho, JornadaTrabalho>();

            cfg.CreateMap<SalarioUnidade, DtoSalarioUnidade>();
            cfg.CreateMap<DtoSalarioUnidade, SalarioUnidade>();

            cfg.CreateMap<MateriaProfessor, DtoMateriaProfessor>();
            cfg.CreateMap<DtoMateriaProfessor, MateriaProfessor>();

            cfg.CreateMap<PontoEletronico, DtoControlePontoHorario>();
            cfg.CreateMap<DtoControlePontoHorario, PontoEletronico>();

            cfg.CreateMap<ArquivoPonto, DtoArquivoPonto>();
            cfg.CreateMap<DtoArquivoPonto, ArquivoPonto>();

            cfg.CreateMap<FeriasFuncionario, DtoFeriasFuncionario>();
            cfg.CreateMap<DtoFeriasFuncionario, FeriasFuncionario>();

            // Usuario e Perfil
            cfg.CreateMap<Usuario, DtoUsuario>();
            cfg.CreateMap<DtoUsuario, Usuario>();

            cfg.CreateMap<PerfilUsuario, DtoPerfilUsuario>();
            cfg.CreateMap<DtoPerfilUsuario, PerfilUsuario>();

            cfg.CreateMap<Usuario, DtoUsuarioRequest>();
            cfg.CreateMap<DtoUsuarioRequest, Usuario>();

            // Cursos e Materias
            cfg.CreateMap<Curso, DtoCurso>();
            cfg.CreateMap<DtoCurso, Curso>();

            cfg.CreateMap<Curso, DtoCursoMatricula>();
            cfg.CreateMap<DtoCursoMatricula, Curso>();

            cfg.CreateMap<Materia, DtoMateria>();
            cfg.CreateMap<DtoMateria, Materia>();

            cfg.CreateMap<CursoProfessor, DtoCursoProfessor>();
            cfg.CreateMap<DtoCursoProfessor, CursoProfessor>();

            cfg.CreateMap<MateriaCursoProfessor, DtoMateriaCursoProfessor>();
            cfg.CreateMap<DtoMateriaCursoProfessor, MateriaCursoProfessor>();

            // Ticket 
            cfg.CreateMap<AssuntoTicket, DtoAssuntoTicket>();
            cfg.CreateMap<DtoAssuntoTicket, AssuntoTicket>();

            cfg.CreateMap<FuncionarioAssuntoTicket, DtoFuncionarioAssuntoTicket>();
            cfg.CreateMap<DtoFuncionarioAssuntoTicket, FuncionarioAssuntoTicket>();

            cfg.CreateMap<Ticket, DtoTicket>();
            cfg.CreateMap<DtoTicket, Ticket>();

            cfg.CreateMap<Ocorrencia, DtoOcorencia>();
            cfg.CreateMap<DtoOcorencia, Ocorrencia>();

            cfg.CreateMap<FiltrarTicket, DtoFiltrarTicket>();
            cfg.CreateMap<DtoFiltrarTicket, FiltrarTicket>();

            cfg.CreateMap<MensagemTicket, DtoMensagemTicket>();
            cfg.CreateMap<DtoMensagemTicket, MensagemTicket>();

            cfg.CreateMap<DestinatarioTicket, DtoDestinatarioTicket>();
            cfg.CreateMap<DtoDestinatarioTicket, DestinatarioTicket>();

            cfg.CreateMap<Unidade, DtoUnidadeTicket>();
            cfg.CreateMap<DtoUnidadeTicket, Unidade>();

            cfg.CreateMap<DtoDestinatarioTicket, DtoMensagemTicket>();
            cfg.CreateMap<DtoMensagemTicket, DtoDestinatarioTicket>();

            cfg.CreateMap<Ticket, DtoTicketRetorno>();
            cfg.CreateMap<DtoTicketRetorno, Ticket>();

            cfg.CreateMap<DestinatarioTicket, DtoMensagemTicket>();
            cfg.CreateMap<DtoMensagemTicket, DestinatarioTicket>();

            // Metas e Comissões
            cfg.CreateMap<Meta, DtoMetas>();
            cfg.CreateMap<DtoMetas, Meta>();

            cfg.CreateMap<DetalhamentoMeta, DtoDetalhamentoMeta>();
            cfg.CreateMap<DtoDetalhamentoMeta, DetalhamentoMeta>();

            cfg.CreateMap<Comissoes, DtoComissao>();
            cfg.CreateMap<DtoComissao, Comissoes>();

            cfg.CreateMap<ComissaoUnidade, DtoComissaoUnidade>();
            cfg.CreateMap<DtoComissaoUnidade, ComissaoUnidade>();

            cfg.CreateMap<ComissaoParcela, DtoComissaoParcelas>();
            cfg.CreateMap<DtoComissaoParcelas, ComissaoParcela>();

            // Fornecedor
            cfg.CreateMap<Fornecedor, DtoFornecedor>();
            cfg.CreateMap<DtoFornecedor, Fornecedor>();

            cfg.CreateMap<Fornecedor, DtoFornecedorResponse>();
            cfg.CreateMap<DtoFornecedorResponse, Fornecedor>();

            cfg.CreateMap<Categoria, DtoCategoria>();
            cfg.CreateMap<DtoCategoria, Categoria>();

            cfg.CreateMap<Produto, DtoProduto>();
            cfg.CreateMap<DtoProduto, Produto>();

            cfg.CreateMap<ItemProduto, DtoItemProduto>();
            cfg.CreateMap<DtoItemProduto, ItemProduto>();

            cfg.CreateMap<HistoricoEstoque, DtoHistoricoEstoque>();
            cfg.CreateMap<DtoHistoricoEstoque, HistoricoEstoque>();

            // Folha de Pagamento
            cfg.CreateMap<FolhaPagamento, DtoFolhaPagamento>();
            cfg.CreateMap<DtoFolhaPagamento, FolhaPagamento>();

            cfg.CreateMap<HoraExtra, DtoHoraExtra>();
            cfg.CreateMap<DtoHoraExtra, HoraExtra>();

            // Alunos e Matriculas
            cfg.CreateMap<Aluno, DtoAluno>();
            cfg.CreateMap<DtoAluno, Aluno>();

            cfg.CreateMap<FiltrarAluno, DtoFiltrarAluno>();
            cfg.CreateMap<DtoFiltrarAluno, FiltrarAluno>();

            cfg.CreateMap<InconsistenciaDocumento, DtoInconsistenciaDocumento>();
            cfg.CreateMap<DtoInconsistenciaDocumento, InconsistenciaDocumento>();
            
            // Agenda de Provas
            cfg.CreateMap<AgendaProva, DtoAgendaProva>();
            cfg.CreateMap<DtoAgendaProva, AgendaProva>();

            cfg.CreateMap<AgendaCurso, DtoAgendaCurso>();
            cfg.CreateMap<DtoAgendaCurso, AgendaCurso>();

            cfg.CreateMap<ColegioAutorizado, DtoColegioAutorizado>();
            cfg.CreateMap<DtoColegioAutorizado, ColegioAutorizado>();

            cfg.CreateMap<UnidadeParticipanteProva, DtoUnidadeParticipanteProva>();
            cfg.CreateMap<DtoUnidadeParticipanteProva, UnidadeParticipanteProva>();

            // Contas a Pagar
            cfg.CreateMap<Despesa, DtoDespesa>();
            cfg.CreateMap<DtoDespesa, Despesa>();

            cfg.CreateMap<DespesaParcela, DtoDespesaParcela>();
            cfg.CreateMap<DtoDespesaParcela, DespesaParcela>();

            cfg.CreateMap<ImpostoDespesa, DtoImpostoDespesa>();
            cfg.CreateMap<DtoImpostoDespesa, ImpostoDespesa>();

            cfg.CreateMap<DespesaDARF, DtoDespesaDARF>();
            cfg.CreateMap<DtoDespesaDARF, DespesaDARF>();

            cfg.CreateMap<DespesaGPS, DtoDespesaGPS>();
            cfg.CreateMap<DtoDespesaGPS, DespesaGPS>();

            // Perguntas e Respostas
            cfg.CreateMap<Pergunta, DtoPergunta>();
            cfg.CreateMap<DtoPergunta, Pergunta>();

            cfg.CreateMap<Resposta, DtoResposta>();
            cfg.CreateMap<DtoResposta, Resposta>();

            cfg.CreateMap<AlunoQuestionario, DtoAlunoQuestionario>();
            cfg.CreateMap<DtoAlunoQuestionario, AlunoQuestionario>();

            cfg.CreateMap<AlunoQuestionarioReposta, DtoAlunoQuestionarioReposta>();
            cfg.CreateMap<DtoAlunoQuestionarioReposta, AlunoQuestionarioReposta>();

            // Itau
            cfg.CreateMap<ItauCorpoCobranca, ItauSimplesCorpoCobranca>();
            cfg.CreateMap<ItauSimplesCorpoCobranca, ItauCorpoCobranca>();

            // Matricula Aluno
            cfg.CreateMap<MatriculaAluno, DtoMatriculaAluno>();
            cfg.CreateMap<DtoMatriculaAluno, MatriculaAluno>();

            cfg.CreateMap<CancelamentoMatricula, DtoCancelamentoMatriculaResult>();
            cfg.CreateMap<DtoCancelamentoMatriculaResult, CancelamentoMatricula>();

            cfg.CreateMap<CancelamentoMatricula, DtoCancelamentoMatriculaRequest>();
            cfg.CreateMap<DtoCancelamentoMatriculaRequest, CancelamentoMatricula>();

            cfg.CreateMap<CancelamentoIsencaoPagamento, DtoCancelamentoAutorizacaoIsencao>();
            cfg.CreateMap<DtoCancelamentoAutorizacaoIsencao, CancelamentoIsencaoPagamento>();

            cfg.CreateMap<MatriculaAluno, DtoMatriculaAlunoResponse>();
            cfg.CreateMap<DtoMatriculaAlunoResponse, MatriculaAluno>();

            cfg.CreateMap<ProvaAluno, DtoProvaAluno>();
            cfg.CreateMap<DtoProvaAluno, ProvaAluno>();

            cfg.CreateMap<ProvaMateriaAluno, DtoProvaMateriaAluno>();
            cfg.CreateMap<DtoProvaMateriaAluno, ProvaMateriaAluno>();

            cfg.CreateMap<UnidadeTransporteProva, DtoUnidadeTransporteProva>();
            cfg.CreateMap<DtoUnidadeTransporteProva, UnidadeTransporteProva>();

            cfg.CreateMap<CertificadoProva, DtoCertificadoProva>();
            cfg.CreateMap<DtoCertificadoProva, CertificadoProva>();

            cfg.CreateMap<Nacionalidade, DtoNacionalidade>();
            cfg.CreateMap<DtoNacionalidade, Nacionalidade>();

            cfg.CreateMap<Naturalidade, DtoNaturalidade>();
            cfg.CreateMap<DtoNaturalidade, Naturalidade>();

            cfg.CreateMap<UsuarioDestinarioTicket, DtoUsuarioDestinarioTicket>();
            cfg.CreateMap<DtoUsuarioDestinarioTicket, UsuarioDestinarioTicket>();

            cfg.CreateMap<Solicitacao, DtoSolicitacao>();
            cfg.CreateMap<DtoSolicitacao, Solicitacao>();

            cfg.CreateMap<SolicitacaoAluno, DtoSolicitacaoAluno>();
            cfg.CreateMap<DtoSolicitacaoAluno, SolicitacaoAluno>();

            cfg.CreateMap<SolicitacaoEmail, DtoSolicitacaoEmail>();
            cfg.CreateMap<DtoSolicitacaoEmail, SolicitacaoEmail>();

            // Criação de AulasOnline
            cfg.CreateMap<AulaOnline, DtoAulaOnline>();
            cfg.CreateMap<DtoAulaOnline, AulaOnline>();

            cfg.CreateMap<VideoAula, DtoVideoAula>();
            cfg.CreateMap<DtoVideoAula, VideoAula>();

            cfg.CreateMap<MateriaOnline, DtoMateriaOnline>();
            cfg.CreateMap<DtoMateriaOnline, MateriaOnline>();

            cfg.CreateMap<CursoOnline, DtoCursoOnline>();
            cfg.CreateMap<DtoCursoOnline, CursoOnline>();

            cfg.CreateMap<VideoPausado, DtoVideoPausado>();
            cfg.CreateMap<DtoVideoPausado, VideoPausado>();

            // Chat Professor e Aluno
            cfg.CreateMap<MensagemAlunoProfessor, DtoMensagemAlunoProfessor>();
            cfg.CreateMap<DtoMensagemAlunoProfessor, MensagemAlunoProfessor>();

            //Financeiro Aluno
            cfg.CreateMap<DtoPagamento, Pagamento>();
            cfg.CreateMap<Pagamento, DtoPagamento>();

            cfg.CreateMap<DtoDadosCartao, DadosCartao>();
            cfg.CreateMap<DadosCartao, DtoDadosCartao>();

            cfg.CreateMap<DtoPlanoPagamentoAluno, PlanoPagamentoAluno>();
            cfg.CreateMap<PlanoPagamentoAluno, DtoPlanoPagamentoAluno>();

            // Email
            cfg.CreateMap<DtoEmailEnviado, EmailEnviado>();
            cfg.CreateMap<EmailEnviado, DtoEmailEnviado>();

            cfg.CreateMap<SolicitacaoCurso, DtoSolicitacaoCurso>();
            cfg.CreateMap<DtoSolicitacaoCurso, SolicitacaoCurso>();

            cfg.CreateMap<DtoSolicitacaoEfetuar, SolicitacaoEfetuar>();
            cfg.CreateMap<SolicitacaoEfetuar, DtoSolicitacaoEfetuar>();

            cfg.CreateMap<SolicitacaoFuncionarioTicket, DtoSolicitacaoFuncionarioTicket>();
            cfg.CreateMap<DtoSolicitacaoFuncionarioTicket, SolicitacaoFuncionarioTicket>();

            // Atendimentos
            cfg.CreateMap<Atendimento, DtoAtendimento>()
               .ForMember(dest => dest.DataeHoradoAtendimento, src => src.MapFrom((s, d) =>
               {
                   return s.DataeHoradoAtendimento.ToString().Replace(" ", ", ");
               }))
               .ForMember(dest => dest.DataeHoradoAgendamento, src => src.MapFrom((s, d) =>
               {
                   return s.DataeHoradoAgendamento.ToString().Replace(" ", ", ");
               }));

            cfg.CreateMap<DtoAtendimento, Atendimento>()
                .ForMember(dest => dest.DataeHoradoAtendimento, src => src.MapFrom((s, d) => {
                    CultureInfo cultures = new CultureInfo("pt-BR");
                    DateTime? data = Convert.ToDateTime(s.DataeHoradoAtendimento.Replace(",", ""), cultures);
                    return data;

                }))
                .ForMember(dest => dest.DataeHoradoAgendamento, src => src.MapFrom((s, d) => {
                    CultureInfo cultures = new CultureInfo("pt-BR");

                    string dt = s.DiadoAgendamento != null
                                    ? s.DiadoAgendamento.Split("T")[0] + " " + s.HoradoAgendamento
                                    : null;

                    DateTime? data = Convert.ToDateTime(dt, cultures);
                    return data;
                }));

            cfg.CreateMap<AtendimentoOutbound, DtoAtendimentoOutbound>();
            cfg.CreateMap<DtoAtendimentoOutbound, AtendimentoOutbound>();

            cfg.CreateMap<AtendimentoAgendamento, DtoAtendimentoAgendamento>()
            .ForMember(dest => dest.DataeHoradoUltimoContato, src => src.MapFrom((s, d) =>
             {
                 return s.DataeHoradoUltimoContato.ToString().Replace(" ", ", ");
             }))
            .ForMember(dest => dest.DataAgendamento, src => src.MapFrom((s, d) => {
                 string data = s.DataAgendamento.Split(" ")[0];
                 return data;
             }));

            cfg.CreateMap<DtoAtendimentoAgendamento, AtendimentoAgendamento>()
                .ForMember(dest => dest.DataeHoradoUltimoContato, src => src.MapFrom((s, d) => {
                CultureInfo cultures = new CultureInfo("pt-BR");
                DateTime? data = Convert.ToDateTime(s.DataeHoradoUltimoContato.Replace("", ""), cultures);
                return data;

                }))
                .ForMember(dest => dest.DataAgendamento, src => src.MapFrom((s, d) => {
                    string data = s.DataAgendamento.Split(" ")[0];
                    return data;
                }));

            cfg.CreateMap<ReguaContatoFilaDto, ReguaContatoFila>();

            cfg.CreateMap<DtoLeads, Leads>();
            cfg.CreateMap<Leads, DtoLeads>();
        }
    }
}
