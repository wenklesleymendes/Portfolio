using AutoMapper;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.PainelMatricula.Enums;
using EscolaPro.Core.Model.Solicitacoes;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using EscolaPro.Repository.Interfaces.Pagamentos;
using EscolaPro.Repository.Interfaces.Solicitacoes;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO.SolicitacaoVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using EscolaPro.Service.Interfaces.ProvasCertificados;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.util.collections;

namespace EscolaPro.Service.Concretes.MatriculaAlunos
{
    public class SolicitacaoService : ISolicitacaoService
    {
        private readonly ISolicitacaoRepository _solicitacaoRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IMatriculaAlunoService _matriculaAlunoService;
        private readonly IAlunoFinanceiroContratoRepository _alunoFinanceiroContratoRepository;
        private readonly IPlanoPagamentoService _planoPagamentoService;
        private readonly ICursoService _cursoService;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;
        private readonly IProvaAlunoRepository _provaAlunoRepository;
        private readonly ICertificadoProvaService _certificadoProvaService;

        public SolicitacaoService(
            ISolicitacaoRepository solicitacaoRepository,
            IFuncionarioRepository funcionarioRepository,
            IMatriculaAlunoService matriculaAlunoService,
            IUsuarioService usuarioService,
            IAlunoFinanceiroContratoRepository alunoFinanceiroContratoRepository,
            IPlanoPagamentoService planoPagamentoService,
            ICursoService cursoService,
            IProvaAlunoRepository provaAlunoRepository,
            ICertificadoProvaService certificadoProvaService,
            IMapper mapper)
        {
            _mapper = mapper;
            _cursoService = cursoService;
            _funcionarioRepository = funcionarioRepository;
            _solicitacaoRepository = solicitacaoRepository;
            _alunoFinanceiroContratoRepository = alunoFinanceiroContratoRepository;
            _planoPagamentoService = planoPagamentoService;
            _matriculaAlunoService = matriculaAlunoService;
            _usuarioService = usuarioService;
            _provaAlunoRepository = provaAlunoRepository;
            _certificadoProvaService = certificadoProvaService;
        }

        public async Task<IEnumerable<DtoSolicitacao>> BuscarPorCursoId(int cursoId, int matriculaId, int usuarioId)
        {
            try
            {
                Dto.ControleUsuarioVO.DtoUsuario usuario = await _usuarioService.BuscarPorId(usuarioId);


                var solicitacaoLista = await _solicitacaoRepository.BuscarPorCursoId(cursoId);
                var matricula = await _matriculaAlunoService.BuscarPorId(matriculaId);

                Dto.PlanoPagamentoVO.DtoPlanoPagamento planoPagamento = null;

                if (matricula?.PlanoPagamentoAluno?.PlanoPagamentoId != null)
                    planoPagamento = await _planoPagamentoService.BuscarPorId(matricula.PlanoPagamentoAluno.PlanoPagamentoId.Value);

                if (usuario.IsAluno)
                    solicitacaoLista = solicitacaoLista.Where(x => x.IsBalcao).ToList();

                List<DtoSolicitacao> solicitacaoRetorno = new List<DtoSolicitacao>();

                foreach (var item in solicitacaoLista)
                {
                    StringBuilder mensagem = new StringBuilder();

                    DtoSolicitacao solicitacao = _mapper.Map<DtoSolicitacao>(item);

                    if (planoPagamento != null && planoPagamento.TipoPagamento == Core.Model.Enums.TipoPagamentoEnum.BoletoBancario)
                    {
                        //if (item.TipoSolicitacao == TipoSolicitacaoEnum.Gratis)
                        //{
                        var pagamentos = await _alunoFinanceiroContratoRepository.ConsultarPainelFinanceiro(matriculaId);

                        if (item.QuantidadeParcelaPaga.Value == 1)
                        {
                            var primeiraParcelaPaga = pagamentos.Where(x => x.Descricao.Contains($"Parcela - 1/{planoPagamento.QuantidadeParcela}")).FirstOrDefault();

                            if (primeiraParcelaPaga != null)
                            {
                                if (primeiraParcelaPaga.TipoSituacao != Core.Model.Pagamentos.TipoSituacaoEnum.Pago)
                                {
                                    solicitacao.ExistePendencia = true;
                                    mensagem.Append("Para esta solicitação, realize o pagamento da 1ª parcela do curso. <br />");
                                }
                            }
                        }
                        else if (item.QuantidadeParcelaPaga.Value == 2)
                        {
                            if (pagamentos.Any(x =>
                                (x.Descricao.Contains("Parcela - ") && x.TipoSituacao != TipoSituacaoEnum.Pago && x.TipoSituacao != TipoSituacaoEnum.Isento) ||
                                (x.Descricao.Contains("Taxa de Matricula") && x.TipoSituacao != TipoSituacaoEnum.Pago && x.TipoSituacao != TipoSituacaoEnum.Isento) ||
                                (x.Descricao.Contains("Taxa de Inscrição") && x.TipoSituacao != TipoSituacaoEnum.Pago && x.TipoSituacao != TipoSituacaoEnum.Isento)))
                            {
                                solicitacao.ExistePendencia = true;
                                mensagem.Append("Para esta solicitação, realizar o pagamento total do curso. <br />");
                            }
                        }                       
                    }

                    if (item.IsPendenciaDocumental)
                    {
                        var documentosPendentes = await _matriculaAlunoService.ConsultarDocumentosPendentes(_mapper.Map<MatriculaAluno>(matricula));

                        if (documentosPendentes.DocumentosPendentes.Count() > 0)
                        {
                            solicitacao.ExistePendencia = true;
                            mensagem.Append("Não é possível solicitar com pendência documental. <br />");
                        }
                    }

                    if (solicitacao.StatusProvaEnum?.Count > 0)
                    {
                        var provaAluno = _provaAlunoRepository.BuscarInscritoPorMatricula(matriculaId);

                        if (solicitacao.StatusProvaEnum.Count == 1
                            && solicitacao.StatusProvaEnum.First().StatusProvaEnum == StatusProvaEnum.NaoInscrito
                            && provaAluno != null
                            && provaAluno.StatusProva != StatusProvaEnum.NaoInscrito)
                        {
                            mensagem.Append("Para esta solicitação o status da prova de ser:  <br /> Não Inscrito . <br />");
                            solicitacao.ExistePendencia = true;
                        }
                        else if (provaAluno == null || !solicitacao.StatusProvaEnum.Any(y => y.StatusProvaEnum == provaAluno.StatusProva))
                        {
                            mensagem.Append("Para esta solicitação o status da prova deve ser: <br />");
                            solicitacao.ExistePendencia = true;

                            foreach (var statusProvaEnum in solicitacao.StatusProvaEnum)
                            {
                                switch (statusProvaEnum.StatusProvaEnum)
                                {
                                    case StatusProvaEnum.NaoInscrito:
                                        mensagem.Append("- Não Inscrito<br />");
                                        break;
                                    case StatusProvaEnum.InscritoParaProva:
                                        mensagem.Append("- Inscrito para prova <br />");
                                        break;
                                    case StatusProvaEnum.Aprovado:
                                        mensagem.Append("- Aprovado <br />");
                                        break;
                                    case StatusProvaEnum.Reprovado:
                                        mensagem.Append("- Reprovado <br />");
                                        break;
                                    case StatusProvaEnum.Faltou:
                                        mensagem.Append("- Faltou na prova<br />");
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }


                    if (solicitacao.StatusCertificado?.Count > 0)
                    {
                        var certificados = await _certificadoProvaService.BuscarPorMatriculaId(matriculaId);

                        if (certificados == null || !certificados.Any(x => solicitacao.StatusCertificado.Any(y => y.StatusCertificadoEnum == x.StatusCertificado)))
                        {
                           var provaAluno = _provaAlunoRepository.BuscarInscritoPorMatricula(matriculaId);
                            if (!(provaAluno!= null && solicitacao.StatusCertificado.Any(x => x.StatusCertificadoEnum == StatusCertificadoEnum.AguardandoEmissao)
                                && provaAluno.StatusProva == StatusProvaEnum.Aprovado && certificados.Count() == 0))
                            {

                                mensagem.Append("Para esta solicitação o status do certificado deve ser: <br />");
                                solicitacao.ExistePendencia = true;

                                foreach (var statusCertificado in solicitacao.StatusCertificado)
                                {
                                    switch (statusCertificado.StatusCertificadoEnum)
                                    {
                                        case StatusCertificadoEnum.AguardandoEmissao:
                                            mensagem.Append("- Aguardando Emissão <br />");
                                            break;
                                        case StatusCertificadoEnum.DisponivelProva:
                                            mensagem.Append("- Disponível para retirada <br />");
                                            break;
                                        case StatusCertificadoEnum.EntregueAluno:
                                            mensagem.Append("- Entregue ao Aluno <br />");
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem?.ToString()))
                    {
                        solicitacao.ExistePendencia = true;
                        solicitacao.Mensagem = mensagem.ToString();
                    }
                    if (solicitacao.NacionalTec.HasValue)
                    {
                        if (matricula.Curso.NacionatalTec) // curso é supletivo
                        {
                            //if (!solicitacao.NacionalTec.Value)
                            //{
                                solicitacaoRetorno.Add(solicitacao);
                            //}
                        }
                        //else // curso é nacional tec
                        //{
                        //    if (!solicitacao.NacionalTec.Value)
                        //    {
                        //        solicitacaoRetorno.Add(solicitacao);
                        //    }
                        //}
                    }
                    else
                    {
                        solicitacaoRetorno.Add(solicitacao);
                    }
                }

                var found = solicitacaoRetorno.Find(x => x.Id == 12);
                if (found != null) solicitacaoRetorno.Remove(found);

                return solicitacaoRetorno; // _mapper.Map<IEnumerable<DtoSolicitacao>>(solicitacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoSolicitacao> BuscarPorId(int solicitacaoId)
        {
            try
            {
                var solicitacao = await _solicitacaoRepository.BuscarPorId(solicitacaoId);

                return _mapper.Map<DtoSolicitacao>(solicitacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoSolicitacao>> BuscarTodos()
        {
            try
            {
                var solicitacoesLista = await _solicitacaoRepository.BuscarTodos();

                return _mapper.Map<List<DtoSolicitacao>>(solicitacoesLista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Excluir(int solicitacaoId)
        {
            try
            {
                var solicitacao = await _solicitacaoRepository.GetByIdAsync(solicitacaoId);
                solicitacao.IsDelete = true;
                var id = await _solicitacaoRepository.UpdateAsync(solicitacao);
                return id > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoSolicitacao> Inserir(DtoSolicitacao dtoSolicitacao)
        {
            try
            {
                int solicitacaoId = dtoSolicitacao.Id;

                if (dtoSolicitacao.Id == 0)
                {
                    var solicitacao = await _solicitacaoRepository.AddAsync(_mapper.Map<Solicitacao>(dtoSolicitacao));

                    solicitacaoId = solicitacao.Id;

                    List<DtoSolicitacaoCurso> solicitacaoCursos = new List<DtoSolicitacaoCurso>();

                    foreach (var solicitacaoCursoId in dtoSolicitacao.SolicitacaoCursoIds)
                    {
                        var curso = await _cursoService.BuscarPorId(solicitacaoCursoId);

                        solicitacaoCursos.Add(new DtoSolicitacaoCurso
                        {
                            CursoId = curso.Id,
                            SolicitacaoId = solicitacao.Id
                        });
                    }


                    await _solicitacaoRepository.InserirCursoSolicitacao(_mapper.Map<List<SolicitacaoCurso>>(solicitacaoCursos), solicitacao.Id);

                    // Inserir solicitação de funcionário
                    List<DtoSolicitacaoFuncionarioTicket> solicitacaoFuncionarioTickets = new List<DtoSolicitacaoFuncionarioTicket>();

                    if (dtoSolicitacao.FuncionarioIds != null)
                    {
                        foreach (var funcionarioId in dtoSolicitacao.FuncionarioIds)
                        {
                            var funcionario = await _funcionarioRepository.GetByIdAsync(funcionarioId);

                            solicitacaoFuncionarioTickets.Add(new DtoSolicitacaoFuncionarioTicket
                            {
                                FuncionarioId = funcionario.Id,
                                SolicitacaoId = solicitacao.Id
                            });
                        }

                        await _solicitacaoRepository.InserirSolicitacaoFuncionarioTicket(_mapper.Map<List<SolicitacaoFuncionarioTicket>>(solicitacaoFuncionarioTickets), solicitacao.Id);
                    }

                }
                else
                {
                    var solicitacaoOld = await _solicitacaoRepository.BuscarPorId(dtoSolicitacao.Id);

                    var fotoSolicitacao = await SelecionarFoto(dtoSolicitacao.Id);

                    if (fotoSolicitacao != null)
                    {
                        dtoSolicitacao.Imagem = fotoSolicitacao.Foto;
                        dtoSolicitacao.Extensao = fotoSolicitacao.Extensao;
                    }

                    if (solicitacaoOld.IsPreDefinida)
                    {
                        dtoSolicitacao.Descricao = solicitacaoOld.Descricao;
                        dtoSolicitacao.IsPreDefinida = solicitacaoOld.IsPreDefinida;
                    }

                    await _solicitacaoRepository.UpdateAsync(_mapper.Map<Solicitacao>(dtoSolicitacao));

                    List<DtoSolicitacaoCurso> solicitacaoCursos = new List<DtoSolicitacaoCurso>();

                    foreach (var solicitacaoCursoId in dtoSolicitacao.SolicitacaoCursoIds)
                    {
                        var curso = await _cursoService.BuscarPorId(solicitacaoCursoId);

                        solicitacaoCursos.Add(new DtoSolicitacaoCurso
                        {
                            CursoId = curso.Id,
                            SolicitacaoId = dtoSolicitacao.Id
                        });
                    }

                    await _solicitacaoRepository.InserirCursoSolicitacao(_mapper.Map<List<SolicitacaoCurso>>(solicitacaoCursos), dtoSolicitacao.Id);

                    // 
                    List<DtoSolicitacaoFuncionarioTicket> solicitacaoFuncionarioTickets = new List<DtoSolicitacaoFuncionarioTicket>();

                    if (dtoSolicitacao.FuncionarioIds != null)
                    {
                        foreach (var funcionarioId in dtoSolicitacao.FuncionarioIds)
                        {
                            var funcionario = await _funcionarioRepository.GetByIdAsync(funcionarioId);

                            solicitacaoFuncionarioTickets.Add(new DtoSolicitacaoFuncionarioTicket
                            {
                                FuncionarioId = funcionario.Id,
                                SolicitacaoId = dtoSolicitacao.Id
                            });
                        }

                        await _solicitacaoRepository.InserirSolicitacaoFuncionarioTicket(_mapper.Map<List<SolicitacaoFuncionarioTicket>>(solicitacaoFuncionarioTickets), dtoSolicitacao.Id);
                    }
                }

                await _solicitacaoRepository.ApagarEmailsAntigo(solicitacaoId);

                if (dtoSolicitacao.EmailDestinatarios != null)
                {
                    List<EmailDestinatario> emailDestinatarios = new List<EmailDestinatario>();

                    foreach (var item in dtoSolicitacao.EmailDestinatarios)
                    {
                        emailDestinatarios.Add(new EmailDestinatario
                        {
                            Email = item,
                            SolicitacaoId = solicitacaoId
                        });
                    }

                    await _solicitacaoRepository.InserirEmails(emailDestinatarios, solicitacaoId);
                }

                if (dtoSolicitacao.StatusCertificados != null)
                {
                    List<StatusCertificado> statusCertificados = new List<StatusCertificado>();

                    foreach (var item in dtoSolicitacao.StatusCertificados)
                    {
                        statusCertificados.Add(new StatusCertificado
                        {
                            StatusCertificadoEnum = (StatusCertificadoEnum)item,
                            SolicitacaoId = solicitacaoId
                        }); ;
                    }

                    await _solicitacaoRepository.InserirCertificados(statusCertificados, solicitacaoId);
                }

                if (dtoSolicitacao.StatusProvas != null)
                {
                    List<StatusProva> statusProvas = new List<StatusProva>();

                    foreach (var item in dtoSolicitacao.StatusProvas)
                    {
                        statusProvas.Add(new StatusProva
                        {
                            StatusProvaEnum = (StatusProvaEnum)item,
                            SolicitacaoId = solicitacaoId
                        });
                    }

                    await _solicitacaoRepository.InserirProvasSolicitacao(statusProvas, solicitacaoId);
                }

                return await BuscarPorId(solicitacaoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoAlunoFoto> SelecionarFoto(int solicitacaoId)
        {
            try
            {
                var solicitacao = await _solicitacaoRepository.SelecionarFoto(solicitacaoId);

                return new DtoAlunoFoto { SolicitacaoId = solicitacao.Id, Extensao = solicitacao.Extensao, Foto = solicitacao.Imagem };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoAlunoFoto> UploadFoto(byte[] file, int solicitacaoId, string extensao)
        {
            try
            {
                await _solicitacaoRepository.UploadFoto(file, solicitacaoId, extensao);

                return await SelecionarFoto(solicitacaoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExcluirFoto(int solicitacaoId)
        {
            try
            {
                var solicitacao = await _solicitacaoRepository.BuscarPorId(solicitacaoId);
                solicitacao.Imagem = null;
                var sucesso = await _solicitacaoRepository.UpdateAsync(solicitacao);
                return sucesso > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
