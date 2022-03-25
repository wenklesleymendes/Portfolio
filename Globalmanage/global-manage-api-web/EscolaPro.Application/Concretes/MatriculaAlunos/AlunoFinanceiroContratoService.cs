using AutoMapper;
using EscolaPro.Core.Helpers;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.ArquivoRemessa.ArquivoSimples;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.PainelMatricula.PlanoAluno;
using EscolaPro.Core.Model.Solicitacoes;
using EscolaPro.Repository.Interfaces.EmailEnviados;
using EscolaPro.Repository.Interfaces.Pagamentos;
using EscolaPro.Repository.Interfaces.Tickets;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.AdquirentesVO;
using EscolaPro.Service.Dto.CampanhaVO;
using EscolaPro.Service.Dto.DisparoSmsVO;
using EscolaPro.Service.Dto.EmailVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO.PlanoAlunoVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO.SolicitacaoVO;
using EscolaPro.Service.Dto.PagamentosVO;
using EscolaPro.Service.Dto.PlanoPagamentoVO;
using EscolaPro.Service.Dto.TicketVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.MatriculaAlunos
{
    public class AlunoFinanceiroContratoService : IAlunoFinanceiroContratoService
    {
        private readonly IMapper _mapper;
        private readonly IRegistroCobrancaService _registroCobrancaService;
        private readonly IMatriculaAlunoService _matriculaAlunoService;
        private readonly IAlunoFinanceiroContratoRepository _alunoFinanceiroContratoRepository;
        private readonly IAlunoService _alunoService;
        private readonly ICursoService _cursoService;
        private readonly IPlanoPagamentoService _planoPagamentoService;
        private readonly ICampanhaService _campanhaService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IUnidadeService _unidadeService;
        private readonly IEmailEnviadoRepository _emailEnviadoRepository;
        private readonly IAnexoService _anexoService;
        private readonly ISolicitacaoService _solicitacaoService;
        private readonly ITicketService _ticketService;
        private readonly IAssuntoTicketRepository _assuntoTicketRepository;
        private readonly ISolicitacaoAlunoService _solicitacaoAlunoService;
        private readonly IUsuarioService _usuarioService;
        private readonly AppSettings _appSettings;
        private readonly IDisparoSmsService _disparoSmsService;
        private readonly IWhatsAppService _whatsAppService;

        public AlunoFinanceiroContratoService(
            IRegistroCobrancaService registroCobrancaService,
            IMatriculaAlunoService matriculaAlunoService,
            IAlunoFinanceiroContratoRepository alunoFinanceiroContratoRepository,
            IPlanoPagamentoService planoPagamentoService,
            IAlunoService alunoService,
            ICursoService cursoService,
            ICampanhaService campanhaService,
            IEmailSenderService emailSenderService,
            IUnidadeService unidadeService,
            IAnexoService anexoService,
            ITicketService ticketService,
            ISolicitacaoService solicitacaoService,
            IEmailEnviadoRepository emailEnviadoRepository,
            ISolicitacaoAlunoService solicitacaoAlunoService,
            IAssuntoTicketRepository assuntoTicketRepository,
            IUsuarioService usuarioService,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IDisparoSmsService disparoSmsService,
            IWhatsAppService whatsAppService)
        {
            _registroCobrancaService = registroCobrancaService;
            _matriculaAlunoService = matriculaAlunoService;
            _alunoFinanceiroContratoRepository = alunoFinanceiroContratoRepository;
            _alunoService = alunoService;
            _planoPagamentoService = planoPagamentoService;
            _cursoService = cursoService;
            _campanhaService = campanhaService;
            _emailSenderService = emailSenderService;
            _unidadeService = unidadeService;
            _emailEnviadoRepository = emailEnviadoRepository;
            _anexoService = anexoService;
            _ticketService = ticketService;
            _assuntoTicketRepository = assuntoTicketRepository;
            _solicitacaoService = solicitacaoService;
            _usuarioService = usuarioService;
            _solicitacaoAlunoService = solicitacaoAlunoService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _disparoSmsService = disparoSmsService;
            _whatsAppService = whatsAppService;
        }

        public async Task<DtoAlunoFinanceiroGrid> ConsultarPainelFinanceiro(int matriculaId)
        {
            try
            {
                var pagamentos = await _alunoFinanceiroContratoRepository.ConsultarPainelFinanceiro(matriculaId);

                var matricula = await _matriculaAlunoService.BuscarPorId(matriculaId);

                List<DtoPagamento> dtoPagamentoGrids = new List<DtoPagamento>();

                var planoPagamentoParcelas = pagamentos.Where(x => !x.IsDelete && !x.PagamentoIdOld.HasValue).ToList();

                foreach (var item in planoPagamentoParcelas.OrderBy(x => x.DataVencimento))
                {
                    DtoPagamento dtoPagamentoGrid = new DtoPagamento(); //_mapper.Map<DtoPagamentoGrid>(item);

                    bool existeOld = pagamentos.Where(x => x.PagamentoIdOld.HasValue && x.PagamentoIdOld == item.Id).Count() > 0 ? true : false;
                    var dae = pagamentos.Where(x => x.PagamentoIdOld.HasValue && x.PagamentoIdOld == item.Id).ToList();

                    if (existeOld)
                    {
                        dtoPagamentoGrid = _mapper.Map<DtoPagamento>(pagamentos.Where(x => x.PagamentoIdOld == item.Id).Last());
                    }
                    else
                    {
                        dtoPagamentoGrid = _mapper.Map<DtoPagamento>(item);
                    }

                    TimeSpan quantidadesDias = new TimeSpan();

                    if (dtoPagamentoGrid.DataVencimento.HasValue)
                    {
                        quantidadesDias = DateTime.Now.Date - dtoPagamentoGrid.DataVencimento.Value.Date;
                    }

                    if (dtoPagamentoGrid.TipoSituacao != TipoSituacaoEnum.Pago && dtoPagamentoGrid.TipoSituacao != TipoSituacaoEnum.Isento)
                    {
                        if (dtoPagamentoGrid.DataVencimento.HasValue)
                        {
                            if (quantidadesDias.TotalDays >= 56)
                            {
                                if (dtoPagamentoGrid.TipoSituacao == TipoSituacaoEnum.Inadimplente || dtoPagamentoGrid.TipoSituacao == TipoSituacaoEnum.Aberto)
                                    dtoPagamentoGrid.TipoSituacao = TipoSituacaoEnum.InadimplenteBloqueado;
                            }
                            else if (dtoPagamentoGrid.DataVencimento.Value.Date != DateTime.Now.Date)
                            {
                                dtoPagamentoGrid.TipoSituacao = dtoPagamentoGrid.DataVencimento <= DateTime.Now ? TipoSituacaoEnum.Inadimplente : dtoPagamentoGrid.TipoSituacao;
                            }
                        }
                    }
                    dtoPagamentoGrid.Pagamento = new List<DtoPagamento>();

                    List<DtoPagamento> pagamentoListaOld = new List<DtoPagamento>();

                    foreach (var pagamentoComIdOld in pagamentos.Where(x => x.PagamentoIdOld.HasValue && dtoPagamentoGrid.Id == x.PagamentoIdOld))
                    {
                        if (pagamentoComIdOld.TipoSituacao != TipoSituacaoEnum.Pago)
                        {
                            if (pagamentoComIdOld.DataVencimento.HasValue)
                            {
                                quantidadesDias = DateTime.Now.Date - pagamentoComIdOld.DataVencimento.Value.Date;

                                if (quantidadesDias.TotalDays >= 56)
                                {
                                    if (pagamentoComIdOld.TipoSituacao == TipoSituacaoEnum.Inadimplente || pagamentoComIdOld.TipoSituacao == TipoSituacaoEnum.Aberto)
                                        pagamentoComIdOld.TipoSituacao = TipoSituacaoEnum.InadimplenteBloqueado;
                                }
                                else if (pagamentoComIdOld.DataVencimento.Value.Date != DateTime.Now.Date)
                                {
                                    pagamentoComIdOld.TipoSituacao = pagamentoComIdOld.DataVencimento <= DateTime.Now ? TipoSituacaoEnum.Inadimplente : pagamentoComIdOld.TipoSituacao;
                                }
                            }
                        }

                        if (pagamentos.Where(x => x.PagamentoIdOld.HasValue).Count() > 0)
                        {
                            if (item.Id == pagamentoComIdOld.PagamentoIdOld)
                            {
                                pagamentoListaOld.Add(_mapper.Map<DtoPagamento>(pagamentoComIdOld));
                            }
                        }
                        else
                        {
                            pagamentoListaOld.Add(_mapper.Map<DtoPagamento>(dtoPagamentoGrid));
                        }
                    }

                    if (existeOld)
                    {
                        if (item.TipoSituacao != TipoSituacaoEnum.Pago)
                        {
                            if (dtoPagamentoGrid.DataVencimento.HasValue)
                            {
                                if (quantidadesDias.TotalDays >= 56)
                                {
                                    if (dtoPagamentoGrid.TipoSituacao == TipoSituacaoEnum.Inadimplente || dtoPagamentoGrid.TipoSituacao == TipoSituacaoEnum.Aberto)
                                        dtoPagamentoGrid.TipoSituacao = TipoSituacaoEnum.InadimplenteBloqueado;
                                }
                                else if (dtoPagamentoGrid.DataVencimento.Value.Date != DateTime.Now.Date)
                                {
                                    item.TipoSituacao = item.DataVencimento <= DateTime.Now ? TipoSituacaoEnum.Inadimplente : item.TipoSituacao;
                                }
                            }
                        }

                        pagamentoListaOld.Add(_mapper.Map<DtoPagamento>(item));
                    }

                    if (pagamentoListaOld.Count > 0)
                    {
                        dtoPagamentoGrid.Pagamento = pagamentoListaOld.OrderByDescending(x => x.DataEmissao).ToList();
                    }

                    if (dtoPagamentoGrid.TipoSituacao != TipoSituacaoEnum.Pago && dtoPagamentoGrid.TipoSituacao != TipoSituacaoEnum.Isento)
                    {
                        if (dtoPagamentoGrid.DataVencimento.HasValue)
                        {
                            if (quantidadesDias.TotalDays >= 56)
                            {
                                if (dtoPagamentoGrid.TipoSituacao == TipoSituacaoEnum.Inadimplente || dtoPagamentoGrid.TipoSituacao == TipoSituacaoEnum.Aberto)
                                    dtoPagamentoGrid.TipoSituacao = TipoSituacaoEnum.InadimplenteBloqueado;
                            }
                            else if (dtoPagamentoGrid.DataVencimento.Value.Date != DateTime.Now.Date)
                            {
                                dtoPagamentoGrid.TipoSituacao = dtoPagamentoGrid.DataVencimento <= DateTime.Now ? TipoSituacaoEnum.Inadimplente : dtoPagamentoGrid.TipoSituacao;
                            }
                        }
                    }

                    dtoPagamentoGrids.Add(dtoPagamentoGrid);
                }

                // Calcula todos os valores do painel financeiro

                var totalEmAberto = dtoPagamentoGrids.Where(x => x.TipoSituacao == TipoSituacaoEnum.Aberto ||
                                                        x.TipoSituacao == TipoSituacaoEnum.Residual ||
                                                        x.TipoSituacao == TipoSituacaoEnum.Inadimplente ||
                                                        x.TipoSituacao == TipoSituacaoEnum.InadimplenteBloqueado).Sum(x => x.Valor);

                decimal totalDescontoCalculado = 0;

                foreach (var pagamento in dtoPagamentoGrids.Where(x => x.Desconto.HasValue && x.TipoSituacao == TipoSituacaoEnum.Aberto || x.TipoSituacao == TipoSituacaoEnum.Residual))
                {
                    decimal valorDesconto = (pagamento.Valor * pagamento.Desconto.Value) / 100;

                    totalDescontoCalculado = totalDescontoCalculado + valorDesconto;
                }

                var descontos = dtoPagamentoGrids.Where(x => x.TipoSituacao == TipoSituacaoEnum.Aberto ||
                                                      x.TipoSituacao == TipoSituacaoEnum.Residual).Sum(x => x.Desconto);

                var retorno = await _anexoService.DownloadDocumentoPorTipoEnum(matriculaId, TipoAnexoEnum.ContratoProcuracaoEja);

                bool contrato = retorno == null ? true : false;

                dtoPagamentoGrids = dtoPagamentoGrids.OrderBy(x => x.DataVencimento).ToList();

                return new DtoAlunoFinanceiroGrid
                {
                    Pagamento = dtoPagamentoGrids,
                    MatriculaId = matricula.Id,
                    PlanoPagamentoAluno = matricula.PlanoPagamentoAluno,
                    Total = totalEmAberto,
                    Desconto = totalDescontoCalculado,
                    Devido = totalEmAberto - totalDescontoCalculado,
                    ExistePendenciaContrato = contrato
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoAlunoFinanceiroGrid> ContratarPlano(DtoContratarPlano dtoContratarPlano)
        {
            DtoAlunoFinanceiroGrid dtoAlunoFinanceiro = new DtoAlunoFinanceiroGrid();

            DtoMatriculaAlunoResponse matricula = new DtoMatriculaAlunoResponse();

            try
            {
                var planoPagamentoSelecionado = await _planoPagamentoService.BuscarPorId(dtoContratarPlano.PlanoPagamentoId);

                dtoAlunoFinanceiro = new DtoAlunoFinanceiroGrid();

                List<DtoPagamento> pagamentosRetorno = new List<DtoPagamento>();

                matricula = await _matriculaAlunoService.BuscarPorIdSimples(dtoContratarPlano.MatriculaId);

                DtoMatriculaAlunoResponse matriculaNumero;

                if (string.IsNullOrEmpty(matricula.NumeroMatricula))
                {
                    // Gera o numero da matricula e salva o plano de pagamento escolhido
                    matriculaNumero = await _matriculaAlunoService.GerarNumeroMatricula(matricula, new PlanoPagamentoAluno
                    {
                        PlanoPagamentoId = dtoContratarPlano.PlanoPagamentoId,
                        CampanhaId = dtoContratarPlano.CampanhaId.HasValue ? dtoContratarPlano.CampanhaId : null,
                        DataPrimeiraParcela = dtoContratarPlano.PrimeiroPagamento,
                        DataSegundaParcela = dtoContratarPlano.SegundoPagamento.HasValue ? dtoContratarPlano.SegundoPagamento : null,
                        TipoPagamento = planoPagamentoSelecionado.TipoPagamento,
                        TemApostila = dtoContratarPlano.TemApostila
                    });
                }

                List<Pagamento> pagamentos = new List<Pagamento>();

                decimal valorTotalPago = 0;

                if (dtoContratarPlano.TemApostila)
                {
                    valorTotalPago = planoPagamentoSelecionado.ValorMaterialDidatico.HasValue ? planoPagamentoSelecionado.ValorMaterialDidatico.Value : 0;
                }

                valorTotalPago = valorTotalPago + planoPagamentoSelecionado.ValorTotalPlano + planoPagamentoSelecionado.ValorTaxaMatricula;


                SolicitacaoEfetuar solicitacaoEfetuar = new SolicitacaoEfetuar();

                switch (planoPagamentoSelecionado.TipoPagamento)
                {
                    case Core.Model.Enums.TipoPagamentoEnum.CartaoCredito:
                        // Efetuar pagamento com cartão de crédito
                        var valorParcelado = dtoContratarPlano.ValorTotal / planoPagamentoSelecionado.QuantidadeParcela;

                        pagamentos.Add(new Pagamento
                        {
                            Descricao = $"Crédito: {planoPagamentoSelecionado.QuantidadeParcela}X de R$ {valorParcelado.ToString("N2")}",
                            ValorPago = dtoContratarPlano.ValorTotal,
                            MatriculaId = matricula.Id,
                        });

                        solicitacaoEfetuar = new SolicitacaoEfetuar
                        {
                            Credito = true,
                            ValorTotal = dtoContratarPlano.ValorTotal,
                            NumeroCartao = dtoContratarPlano.NumeroCartao,
                            NumeroControle = dtoContratarPlano.NumeroControle,
                            QuantidadeParcela = dtoContratarPlano.QuantidadeParcela.Value,
                            ComprovanteCartao = dtoContratarPlano.ComprovanteCartao
                        };

                        await _alunoFinanceiroContratoRepository.AtualizarPagamento(pagamentos, _mapper.Map<PlanoPagamento>(planoPagamentoSelecionado), solicitacaoEfetuar, true);
                        await EfetuarSolicitacaoGerarCobranca(planoPagamentoSelecionado, dtoContratarPlano);
                        break;
                    case Core.Model.Enums.TipoPagamentoEnum.CartaoDebito:
                        pagamentos.Add(new Pagamento
                        {
                            Descricao = $"Débito: R$ {dtoContratarPlano.ValorTotal.ToString("N2")}",
                            ValorPago = dtoContratarPlano.ValorTotal,
                            MatriculaId = matricula.Id
                        });

                        solicitacaoEfetuar = new SolicitacaoEfetuar
                        {
                            Credito = false,
                            ValorTotal = dtoContratarPlano.ValorTotal,
                            NumeroCartao = dtoContratarPlano.NumeroCartao,
                            NumeroControle = dtoContratarPlano.NumeroControle,
                            QuantidadeParcela = dtoContratarPlano.QuantidadeParcela.Value,
                            ComprovanteCartao = dtoContratarPlano.ComprovanteCartao
                        };

                        await _alunoFinanceiroContratoRepository.AtualizarPagamento(pagamentos, _mapper.Map<PlanoPagamento>(planoPagamentoSelecionado), solicitacaoEfetuar, true);
                        await EfetuarSolicitacaoGerarCobranca(planoPagamentoSelecionado, dtoContratarPlano);
                        break;
                    case Core.Model.Enums.TipoPagamentoEnum.BoletoBancario:
                        // Montar os boletos do plano de pagamento selecionado e gera no itaú
                        var pagamentosBoleto = await MontarPagamentoBoletoLocal(planoPagamentoSelecionado, dtoContratarPlano, matricula);
                        pagamentosRetorno = await GerarPagamentos(new DtoAlunoFinanceiroGrid { MatriculaId = matricula.Id, Pagamento = pagamentosBoleto.OrderBy(x => x.Valor).ToList() }, dtoContratarPlano.UsuarioLogadoId);
                        await EfetuarSolicitacaoGerarCobranca(planoPagamentoSelecionado, dtoContratarPlano);

                        var aluno = await _alunoService.BuscarPorId(matricula.AlunoId);
                        if (aluno.Contato.ReceberEmail)
                        {
                            var pagamentosIds = pagamentosRetorno.Select(x => x.Id).ToList();
                            await GerarBoletoEnviarPorEmail(matricula.AlunoId, pagamentosIds, TipoAcaoBoletoEnum.EnviarPorEmail);
                        }
                        break;
                    default:
                        break;
                }

                await _matriculaAlunoService.SalvarPlanoPagamento(_mapper.Map<MatriculaAluno>(matricula), new PlanoPagamentoAluno
                {
                    PlanoPagamentoId = dtoContratarPlano.PlanoPagamentoId,
                    CampanhaId = dtoContratarPlano.CampanhaId.HasValue ? dtoContratarPlano.CampanhaId : null,
                    DataPrimeiraParcela = dtoContratarPlano.PrimeiroPagamento,
                    DataSegundaParcela = dtoContratarPlano.SegundoPagamento.HasValue ? dtoContratarPlano.SegundoPagamento : null,
                    TipoPagamento = planoPagamentoSelecionado.TipoPagamento,
                    TemApostila = dtoContratarPlano.TemApostila
                });

                await _matriculaAlunoService.AtivarAluno(matricula.Id);
                await _matriculaAlunoService.AtualizarMaterialLiberado(matricula.Id, planoPagamentoSelecionado.IsentarMaterialDidatico);
                dtoAlunoFinanceiro = new DtoAlunoFinanceiroGrid
                {
                    MatriculaId = matricula.Id,
                    Pagamento = pagamentosRetorno
                };

                await EfetuarDisparosBoasVindas(matricula);

                return dtoAlunoFinanceiro;
            }
            catch (Exception ex)
            {
                //await RollBackPlanoPagamento(dtoAlunoFinanceiro, matricula);
                throw ex;
            }
        }

        public async Task EfetuarDisparosBoasVindas(DtoMatriculaAlunoResponse matriculaAluno)
        {
            var matricula = _mapper.Map<MatriculaAluno>(matriculaAluno);

            var alunoResponse = await _alunoService.BuscarPorId((int)matricula.AlunoId);
            var alunoMap = _mapper.Map<Aluno>(alunoResponse);
            matricula.Aluno = alunoMap;
            var aluno = matricula.Aluno;

            var unidadeResponse = await _unidadeService.BuscarPorId((int)matricula.UnidadeId);
            var unidade = _mapper.Map<Unidade>(unidadeResponse);
            matricula.Unidade = unidade;

            if (aluno.Contato.RecebeSMS)
            {
                var PrimeiroNomeAluno = aluno.Nome.Substring(0, aluno.Nome.IndexOf(' '));
                var preposicao = string.Empty;
                var cursos = string.Empty;

                if (matricula.UnidadeId == 1)
                    preposicao = "a ";
                else
                    preposicao = "ao ";

                if (matricula.UnidadeId == 10)
                    cursos = "Cursos ";
                else
                    cursos = "";

                await _disparoSmsService.Enviar(new SmsBody
                {
                    mensagem = PrimeiroNomeAluno + "\nbem-vindo(a) " + preposicao + cursos + matricula.Unidade.NomeFantasia + ".Verifique seu e-mail ou Whatsapp para saber como acessar o Portal do Aluno e aproveitar o conteudo disponivel.",
                    numero = aluno.Contato.Celular,
                    alunoId = aluno.Id
                });
            }


            if (aluno.Contato.ReceberEmail)
            {
                var mailBody = Helpers.CoreHelpers.MontarEmailMatriculaAluno(aluno, matricula.Unidade, matricula.Curso.NacionatalTec);
                await _emailSenderService.SendEmailAsync(new string[] { aluno.Contato.Email }, "Sua matricula foi realizada com sucesso", mailBody, null, matricula.Curso.NacionatalTec, null, aluno.Id);
            }

            if (aluno.Contato.ReceberWhatsApp)
            {
                await this.EnviarWhatsAppMatriculaAluno(matricula);
            }
        }

        public async Task<bool> EfetuarSolicitacaoGerarCobranca(DtoPlanoPagamento planoPagamentoSelecionado, DtoContratarPlano dtoContratarPlano)
        {
            if (planoPagamentoSelecionado.IsentarMaterialDidatico)
            {
                DtoSolicitacaoEfetuar dtoSolicitacaoEfetuar = new DtoSolicitacaoEfetuar
                {
                    SolicitacaoId = _appSettings.SolicitacaoId,
                    MatriculaId = dtoContratarPlano.MatriculaId,
                    TipoPagamento = EscolaPro.Core.Model.Enums.TipoPagamentoEnum.BoletoBancario
                };

                await _solicitacaoAlunoService.EfetuarSolicitacao(dtoSolicitacaoEfetuar);
            }

            return true;
        }

        public async Task<DtoPagamento> GerarMultaCancelamento(DtoPagamento pagamento, bool isento)
        {
            if (isento)
            {
                var pg = await _alunoFinanceiroContratoRepository.AddAsync(_mapper.Map<Pagamento>(pagamento));
                pg.TipoSituacao = TipoSituacaoEnum.Isento;
                return _mapper.Map<DtoPagamento>(pg);
            }
            else
            {

                pagamento.TipoSituacao = TipoSituacaoEnum.Aberto;

                string nossoNumero = new Random().Next(20201190, 99991123).ToString();

                var matricula = await _matriculaAlunoService.BuscarPorId(pagamento.MatriculaId);

                var pagamentoRetorno = await GerarBoletos(pagamento, _mapper.Map<DtoMatriculaAlunoResponse>(matricula), nossoNumero);

                List<string> emails = new List<string>();

                if (pagamento.Id > 0)
                {
                    _ = await _alunoFinanceiroContratoRepository.UpdateAsync(pagamentoRetorno);

                }
                else
                {
                    _ = await _alunoFinanceiroContratoRepository.AddAsync(pagamentoRetorno);
                }

                return _mapper.Map<DtoPagamento>(pagamentoRetorno);
            }
        }
        public async Task<List<DtoPagamento>> GerarPagamentos(DtoAlunoFinanceiroGrid dtoAlunoFinanceiro, int usuarioLogadoId = 0)
        {
            List<DtoPagamento> pagamentosRetorno = new List<DtoPagamento>();
            List<Pagamento> listaPagamentos = new List<Pagamento>();
            try
            {

                if (!dtoAlunoFinanceiro.TEF)
                {
                    var matricula = await _matriculaAlunoService.BuscarPorId(dtoAlunoFinanceiro.MatriculaId);

                    foreach (var pagamento in dtoAlunoFinanceiro.Pagamento.OrderBy(x => x.TipoSituacao))
                    {
                        switch (pagamento.TipoPagamento)
                        {
                            case Core.Model.Enums.TipoPagamentoEnum.CartaoCredito:
                                break;
                            case Core.Model.Enums.TipoPagamentoEnum.CartaoDebito:
                                break;
                            case Core.Model.Enums.TipoPagamentoEnum.BoletoBancario:
                                string nossoNumero = new Random().Next(20201190, 99991123).ToString();
                                var pagamentoBoleto = await GerarBoletos(pagamento, matricula, nossoNumero);
                                listaPagamentos.Add(pagamentoBoleto);
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    SolicitacaoEfetuar solicitacaoEfetuar = new SolicitacaoEfetuar
                    {
                        Credito = dtoAlunoFinanceiro.Credito,
                        ValorTotal = dtoAlunoFinanceiro.Total,
                        NumeroCartao = dtoAlunoFinanceiro.NumeroCartao,
                        NumeroControle = dtoAlunoFinanceiro.NumeroControle,
                        QuantidadeParcela = dtoAlunoFinanceiro.QuantidadeParcela,
                        ComprovanteCartao = dtoAlunoFinanceiro.ComprovanteCartao
                    };

                    await _alunoFinanceiroContratoRepository.AtualizarPagamento(_mapper.Map<List<Pagamento>>(dtoAlunoFinanceiro.Pagamento), null, solicitacaoEfetuar, false);
                }

                // Gravar os boletos no banco de dados
                foreach (var pagamento in listaPagamentos)
                {
                    if (pagamento.Id > 0)
                    {
                        await _alunoFinanceiroContratoRepository.UpdateAsync(pagamento);

                        pagamentosRetorno.Add(_mapper.Map<DtoPagamento>(await _alunoFinanceiroContratoRepository.GetByIdAsync(pagamento.Id)));
                    }
                    else
                    {
                        var pgto = await _alunoFinanceiroContratoRepository.AddAsync(pagamento);
                        pagamentosRetorno.Add(_mapper.Map<DtoPagamento>(pgto));
                    }
                }

                return pagamentosRetorno;
            }
            catch (ArgumentException)
            {
                if (listaPagamentos.Count > 0)
                {
                    await _ticketService.TicketErroBoletoItau(dtoAlunoFinanceiro.MatriculaId, usuarioLogadoId, listaPagamentos);
                }
                throw;
            }
        }


        private async Task<Pagamento> GerarBoletos(DtoPagamento dtoPagamento, DtoMatriculaAlunoResponse matriculaAluno, string nossoNumero)
        {
            try
            {
                var simplesCorpoCobranca = await EnviarBoletoItau(dtoPagamento, matriculaAluno, nossoNumero);

                if (dtoPagamento.Valor > 0)
                {
                    var retorno = await _registroCobrancaService.GerarBoleto(simplesCorpoCobranca);

                    if (!string.IsNullOrEmpty(retorno.Codigo))
                    {
                        throw new ArgumentException(JsonConvert.SerializeObject(retorno.Campos));
                    }

                    retorno.data_vencimento = simplesCorpoCobranca.data_vencimento;
                    retorno.valor_cobrado = simplesCorpoCobranca.valor_cobrado;

                    if (!string.IsNullOrEmpty(retorno.numero_linha_digitavel))
                    {
                        var pagamento = _mapper.Map<Pagamento>(dtoPagamento);

                        if (dtoPagamento.Id > 0)
                        {
                            pagamento.Id = dtoPagamento.Id;
                        }
                        else
                        {
                            pagamento.Id = 0;
                        }

                        pagamento.Descricao = dtoPagamento.Descricao;
                        pagamento.DataEmissao = DateTime.Now;
                        pagamento.NossoNumero = retorno.nosso_numero.Substring(0, retorno.nosso_numero.Length - 1);
                        pagamento.CodigoBarras = retorno.codigo_barras;
                        pagamento.Valor = dtoPagamento.Valor;
                        pagamento.PromocaoBolsaConvenio = dtoPagamento.PromocaoBolsaConvenio;
                        pagamento.NumeroLinhaDigitavel = retorno.numero_linha_digitavel;
                        pagamento.TipoPagamento = Core.Model.Enums.TipoPagamentoEnum.BoletoBancario;
                        pagamento.MatriculaId = matriculaAluno.Id;
                        pagamento.NumeroRegistro = dtoPagamento.NumeroRegistro;

                        var boletoPDF = await _registroCobrancaService.BoletoImpressoPdf(retorno, matriculaAluno, dtoPagamento.Descricao, pagamento);

                        pagamento.BoletoHTML = boletoPDF;

                        return pagamento;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    var pagamento = _mapper.Map<Pagamento>(dtoPagamento);

                    pagamento.Descricao = dtoPagamento.Descricao;
                    pagamento.DataEmissao = null;
                    pagamento.NossoNumero = "";
                    pagamento.CodigoBarras = "";
                    pagamento.Valor = dtoPagamento.Valor;
                    pagamento.PromocaoBolsaConvenio = dtoPagamento.PromocaoBolsaConvenio;
                    pagamento.NumeroLinhaDigitavel = "";
                    pagamento.TipoPagamento = Core.Model.Enums.TipoPagamentoEnum.BoletoBancario;
                    pagamento.MatriculaId = matriculaAluno.Id;
                    pagamento.DataVencimento = null;

                    return pagamento;
                }
            }
            catch
            {
                throw;
            }
        }

        private async Task<List<DtoPagamento>> MontarPagamentoBoletoLocal(DtoPlanoPagamento planoPagamento, DtoContratarPlano dtoContratarPlano, DtoMatriculaAlunoResponse dtoMatriculaAluno)
        {
            try
            {
                List<DtoPagamento> pagamentos = new List<DtoPagamento>();

                DtoCampanha campanha = null;

                if (dtoContratarPlano.CampanhaId.HasValue)
                {
                    campanha = await _campanhaService.BuscarPorId(dtoContratarPlano.CampanhaId.Value);
                }

                int mes = 0;
                int countParcela = 1;

                // TAXA DE MATRICULA
                if (!planoPagamento.IsentarMatricula)
                {
                    decimal valorDescontoTaxaMatriculaCampanha = campanha != null ? (planoPagamento.ValorTaxaMatricula * Convert.ToDecimal(campanha.DescontoTaxaMatricula)) / 100 : 0;

                    decimal valorDescontoTaxaMatricula = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? (planoPagamento.ValorTaxaMatricula * planoPagamento.PorcentagemDescontoPontualidade.Value) / 100 : planoPagamento.PorcentagemDescontoPontualidade.Value;

                    pagamentos.Add(new DtoPagamento
                    {
                        Valor = planoPagamento.ValorTaxaMatricula - valorDescontoTaxaMatriculaCampanha,
                        Descricao = $"Taxa de Matricula - {1}/{1}",
                        Desconto = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? planoPagamento.PorcentagemDescontoPontualidade : 0,
                        TipoPagamento = Core.Model.Enums.TipoPagamentoEnum.BoletoBancario,
                        TipoSituacao = TipoSituacaoEnum.Aberto,
                        DataEmissao = DateTime.Now,
                        DescontoPontualidade = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? planoPagamento.PorcentagemDescontoPontualidade : 0,
                        PromocaoBolsaConvenio = campanha != null ? Convert.ToDecimal(campanha.DescontoTaxaMatricula) : 0,
                        DataPagamento = null,
                        DataVencimento = dtoContratarPlano.PrimeiroPagamento,
                        NumeroRegistro = $"{dtoMatriculaAluno.Unidade.Sigla}{dtoMatriculaAluno.NumeroMatricula}M1"
                    });
                }
                else
                {
                    pagamentos.Add(new DtoPagamento
                    {
                        Valor = 0,
                        Descricao = $"Taxa de Matricula - {1}/{1}",
                        Desconto = 0,
                        TipoPagamento = Core.Model.Enums.TipoPagamentoEnum.BoletoBancario,
                        TipoSituacao = TipoSituacaoEnum.Isento,
                        DataEmissao = DateTime.Now,
                        PromocaoBolsaConvenio = 0,
                        DataPagamento = null,
                        DataVencimento = null,
                        NumeroRegistro = $"{dtoMatriculaAluno.Unidade.Sigla}{dtoMatriculaAluno.NumeroMatricula}M1"
                    });
                }

                // APOSTILA, vence junto com a primeira parcela do grupo.
                if (!planoPagamento.IsentarMaterialDidatico)
                {
                    if (dtoContratarPlano.TemApostila)
                    {
                        decimal valorDescontoMaterialCampanha = campanha != null ?
                           (planoPagamento.ValorMaterialDidatico.Value * Convert.ToDecimal(campanha.DescontoTaxaMateriaDidatico)) / 100 : 0;

                        decimal valorDescontoMaterialDidatico = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? (planoPagamento.ValorMaterialDidatico.Value * planoPagamento.PorcentagemDescontoPontualidade.Value) / 100 : planoPagamento.PorcentagemDescontoPontualidade.Value;

                        pagamentos.Add(new DtoPagamento
                        {
                            Valor = planoPagamento.ValorMaterialDidatico.Value - valorDescontoMaterialCampanha,
                            Descricao = $"Apostila - {1}/{1}",
                            Desconto = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? planoPagamento.PorcentagemDescontoPontualidade : 0,
                            TipoPagamento = Core.Model.Enums.TipoPagamentoEnum.BoletoBancario,
                            TipoSituacao = TipoSituacaoEnum.Aberto,
                            DataEmissao = DateTime.Now,
                            DescontoPontualidade = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? planoPagamento.PorcentagemDescontoPontualidade : 0,
                            PromocaoBolsaConvenio = campanha != null ? Convert.ToDecimal(campanha.DescontoTaxaMateriaDidatico) : 0,
                            DataPagamento = null,
                            DataVencimento = dtoContratarPlano.PrimeiroPagamento,
                            NumeroRegistro = $"{dtoMatriculaAluno.Unidade.Sigla}{dtoMatriculaAluno.NumeroMatricula}A1"
                        });
                    }
                }
                else
                {
                    pagamentos.Add(new DtoPagamento
                    {
                        Valor = 0,
                        Descricao = $"Apostila - {1}/{1}",
                        Desconto = 0,
                        TipoPagamento = Core.Model.Enums.TipoPagamentoEnum.BoletoBancario,
                        TipoSituacao = TipoSituacaoEnum.Isento,
                        DataEmissao = DateTime.Now,
                        PromocaoBolsaConvenio = 0,
                        DataPagamento = null,
                        DataVencimento = null,
                        NumeroRegistro = $"{dtoMatriculaAluno.Unidade.Sigla}{dtoMatriculaAluno.NumeroMatricula}A1"
                    });
                }

                // PLANO DE PAGAMENTO - Montar as parcelas de pagamento do curso
                for (int numeroParcela = 0; numeroParcela < planoPagamento.QuantidadeParcela; numeroParcela++)
                {
                    decimal valorDescontoPlanoCampanha = campanha != null ? (planoPagamento.ValorParcela * Convert.ToDecimal(campanha.DescontoPlanoPagamento) / 100) : 0;

                    decimal valorDescontoParcela = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? (planoPagamento.ValorParcela * planoPagamento.PorcentagemDescontoPontualidade.Value) / 100 : planoPagamento.PorcentagemDescontoPontualidade.Value;

                    if (numeroParcela == 0)
                    {
                        pagamentos.Add(new DtoPagamento
                        {
                            Valor = planoPagamento.ValorParcela - valorDescontoPlanoCampanha,
                            Descricao = $"Parcela - {countParcela}/{planoPagamento.QuantidadeParcela}",
                            Desconto = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? planoPagamento.PorcentagemDescontoPontualidade : 0,
                            TipoPagamento = Core.Model.Enums.TipoPagamentoEnum.BoletoBancario,
                            TipoSituacao = TipoSituacaoEnum.Aberto,
                            DataEmissao = DateTime.Now,
                            DescontoPontualidade = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? planoPagamento.PorcentagemDescontoPontualidade : 0,
                            PromocaoBolsaConvenio = campanha != null ? Convert.ToDecimal(campanha.DescontoPlanoPagamento) : 0,
                            DataPagamento = null,
                            DataVencimento = dtoContratarPlano.PrimeiroPagamento,
                            NumeroRegistro = $"{dtoMatriculaAluno.Unidade.Sigla}{dtoMatriculaAluno.NumeroMatricula}P{countParcela}"
                        });
                    }
                    else
                    {
                        countParcela++;
                        pagamentos.Add(new DtoPagamento
                        {
                            Valor = planoPagamento.ValorParcela - valorDescontoPlanoCampanha,
                            Descricao = $"Parcela - {countParcela}/{planoPagamento.QuantidadeParcela}",
                            Desconto = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? planoPagamento.PorcentagemDescontoPontualidade : 0,
                            TipoPagamento = Core.Model.Enums.TipoPagamentoEnum.BoletoBancario,
                            TipoSituacao = TipoSituacaoEnum.Aberto,
                            DataEmissao = DateTime.Now,
                            DescontoPontualidade = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? planoPagamento.PorcentagemDescontoPontualidade : 0,
                            PromocaoBolsaConvenio = campanha != null ? Convert.ToDecimal(campanha.DescontoPlanoPagamento) : 0,
                            DataPagamento = null,
                            DataVencimento = dtoContratarPlano.SegundoPagamento.Value.AddMonths(mes),
                            NumeroRegistro = $"{dtoMatriculaAluno.Unidade.Sigla}{dtoMatriculaAluno.NumeroMatricula}P{countParcela}"
                        });
                        mes++;
                    }
                }

                // TAXA DE PROVA
                decimal valorDescontoTaxaCampanha = campanha != null ? (planoPagamento.ValorTotalInscricaoProva.Value * Convert.ToDecimal(campanha.DescontoTaxaInscricaoProvas)) / 100 : 0;

                decimal valorDescontoTaxaProva = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? (planoPagamento.ValorTotalInscricaoProva.Value * planoPagamento.PorcentagemDescontoPontualidade.Value) / 100 : planoPagamento.PorcentagemDescontoPontualidade.Value;

                int countTaxaMatricula = 1;
                int segundaDataVencimento = 1;
                for (int taxaParcela = 0; taxaParcela < 2; taxaParcela++)
                {
                    if (dtoContratarPlano.SegundoPagamento.HasValue)
                    {
                        pagamentos.Add(new DtoPagamento
                        {
                            Valor = (planoPagamento.ValorTotalInscricaoProva.Value - valorDescontoTaxaCampanha) / 2,
                            Descricao = $"Taxa de Inscrição - {countTaxaMatricula}/{2}",
                            Desconto = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? planoPagamento.PorcentagemDescontoPontualidade : 0,
                            TipoPagamento = Core.Model.Enums.TipoPagamentoEnum.BoletoBancario,
                            TipoSituacao = TipoSituacaoEnum.Aberto,
                            DataEmissao = DateTime.Now,
                            DescontoPontualidade = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? planoPagamento.PorcentagemDescontoPontualidade : 0,
                            PromocaoBolsaConvenio = campanha != null ? Convert.ToDecimal(campanha.DescontoTaxaInscricaoProvas) : 0,
                            DataPagamento = null,
                            DataVencimento = dtoContratarPlano.SegundoPagamento.HasValue ? dtoContratarPlano.SegundoPagamento.Value.AddMonths(mes) : dtoContratarPlano.PrimeiroPagamento,
                            NumeroRegistro = $"{dtoMatriculaAluno.Unidade.Sigla}{dtoMatriculaAluno.NumeroMatricula}X{countTaxaMatricula}"
                        });
                        mes++;
                        countTaxaMatricula++;
                    }
                    else
                    {
                        pagamentos.Add(new DtoPagamento
                        {
                            Valor = (planoPagamento.ValorTotalInscricaoProva.Value - valorDescontoTaxaCampanha) / 2,
                            Descricao = $"Taxa de Inscrição - {countTaxaMatricula}/{2}",
                            Desconto = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? planoPagamento.PorcentagemDescontoPontualidade : 0,
                            TipoPagamento = Core.Model.Enums.TipoPagamentoEnum.BoletoBancario,
                            TipoSituacao = TipoSituacaoEnum.Aberto,
                            DataEmissao = DateTime.Now,
                            DescontoPontualidade = planoPagamento.PorcentagemDescontoPontualidade.HasValue ? planoPagamento.PorcentagemDescontoPontualidade : 0,
                            PromocaoBolsaConvenio = campanha != null ? Convert.ToDecimal(campanha.DescontoTaxaInscricaoProvas) : 0,
                            DataPagamento = null,
                            DataVencimento = dtoContratarPlano.PrimeiroPagamento.AddMonths(segundaDataVencimento),
                            NumeroRegistro = $"{dtoMatriculaAluno.Unidade.Sigla}{dtoMatriculaAluno.NumeroMatricula}X{countTaxaMatricula}"
                        });
                        mes++;
                        countTaxaMatricula++;
                        segundaDataVencimento++;
                    }
                }

                return pagamentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> GerarBoletoEnviarPorEmail(int alunoId, List<int> pagamentoIds, TipoAcaoBoletoEnum tipoAcaoBoleto)
        {
            try
            {
                List<Pagamento> pagamentosLista = new List<Pagamento>();
                List<Pagamento> pagamentosListaTemp = await _alunoFinanceiroContratoRepository.BuscarPorId(pagamentoIds);

                if (pagamentosListaTemp.Any(x => x.Descricao.Contains("Taxa de Matricula")))
                {
                    pagamentosLista.AddRange(pagamentosListaTemp.Where(x => x.Descricao.Contains("Taxa de Matricula") &&
                                                                            !pagamentosLista.Any(y => y.Id == x.Id))
                                                                .OrderBy(x => x.DataVencimento));
                }
                if (pagamentosListaTemp.Any(x => x.Descricao.Contains("Apostila")))
                    pagamentosLista.AddRange(pagamentosListaTemp.Where(x => x.Descricao.Contains("Apostila") &&
                                                                            !pagamentosLista.Any(y => y.Id == x.Id))
                                                                .OrderBy(x => x.DataVencimento));

                if (pagamentosListaTemp.Any(x => x.Descricao.Contains("Parcela")))
                    pagamentosLista.AddRange(pagamentosListaTemp.Where(x => x.Descricao.Contains("Parcela") &&
                                                                            !pagamentosLista.Any(y => y.Id == x.Id))
                                                                .OrderBy(x => x.DataVencimento));

                if (pagamentosListaTemp.Any(x => x.Descricao.Contains("Taxa de Inscrição")))
                    pagamentosLista.AddRange(pagamentosListaTemp.Where(x => x.Descricao.Contains("Taxa de Inscrição") &&
                                                                            !pagamentosLista.Any(y => y.Id == x.Id))
                                                                .OrderBy(x => x.DataVencimento));

                pagamentosLista.AddRange(pagamentosListaTemp.Where(x => !pagamentosLista.Any(y => y.Id == x.Id)));

                List<Attachment> attachments = new List<Attachment>();

                var aluno = await _alunoService.BuscarPorId(alunoId);

                DtoMatriculaAlunoResponse matricula = null;

                foreach (var pagamentoRetorno in pagamentosLista)
                {

                    if (!string.IsNullOrEmpty(pagamentoRetorno.BoletoHTML))
                    {
                        var boletoPdf = await Core.Helpers.CoreHelpers.ConverterBoletoPDF(new List<string>() { pagamentoRetorno.BoletoHTML }, _appSettings.BoletoServiceUrl);

                        Stream stream = new MemoryStream(Convert.FromBase64String(boletoPdf.FirstOrDefault()));

                        attachments.Add(new Attachment(stream, $"{pagamentoRetorno.Descricao.Replace("/", "-")}.pdf"));
                    }

                    if (matricula == null)
                    {
                        matricula = await _matriculaAlunoService.BuscarPorId(pagamentoRetorno.MatriculaId);
                    }
                }

                var foto = await _unidadeService.SelecionarFoto(aluno.UnidadeId);

                StringBuilder conteudoDoEmail = Helpers.CoreHelpers.MontarConteudoEmail(_mapper.Map<List<DtoPagamento>>(pagamentosLista), foto, aluno.Nome);

                foreach (var pagamentoId in pagamentoIds)
                {
                    await _emailEnviadoRepository.AddAsync(new EmailEnviado
                    {
                        CorpoEmail = conteudoDoEmail.ToString(),
                        DataEnvio = DateTime.Now,
                        PagamentoId = pagamentoId,
                        EmailPara = aluno.Contato.Email
                    });
                }

                List<string> emails = new List<string>();

                emails.Add(aluno.Contato.Email);

                bool nacionalTec = true;

                if (matricula != null && matricula.Curso != null &&
                    (matricula.Curso.Descricao == "Alfabetização, Ensino Fundamental e Médio" ||
                    matricula.Curso.Descricao == "Ensino Fundamental e Médio" ||
                    matricula.Curso.Descricao == "Ensino Fundamental" ||
                    matricula.Curso.Descricao == "Ensino Médio"))
                    nacionalTec = false;

                if (pagamentosLista.Count == 1)
                {
                    await _emailSenderService.SendEmailAsync(emails.ToArray(), $"Seu boleto está disponível - Curso {aluno.Unidade.Nome}", conteudoDoEmail.ToString(), attachments, nacionalTec, null, alunoId);
                }
                else
                {
                    await _emailSenderService.SendEmailAsync(emails.ToArray(), $"Seus boletos estão disponíveis - Curso {aluno.Unidade.Nome}", conteudoDoEmail.ToString(), attachments, nacionalTec, null, alunoId);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<string>> EnviarBoletoPorEmailOuRecalcular(DtoGerarBoletoRequest dtoGerarBoleto)
        {
            try
            {
                List<Pagamento> pagamentosLista = new List<Pagamento>();

                List<Attachment> attachments = new List<Attachment>();

                var aluno = await _alunoService.BuscarPorId(dtoGerarBoleto.AlunoId);

                List<string> htmlLista = new List<string>();

                switch (dtoGerarBoleto.TipoAcao)
                {
                    case TipoAcaoBoletoEnum.GerarBoletoEnviaPorEmail:
                        foreach (var pagamentoId in dtoGerarBoleto.PagamentoIds)
                        {
                            var pagamentoRetorno = await _alunoFinanceiroContratoRepository.GetByIdAsync(pagamentoId);

                            if (!string.IsNullOrEmpty(pagamentoRetorno.BoletoHTML))
                            {
                                var boletoPdf = await Core.Helpers.CoreHelpers.ConverterBoletoPDF(new List<string>() { pagamentoRetorno.BoletoHTML }, _appSettings.BoletoServiceUrl);

                                string filePath = $"{Path.GetTempPath()}{pagamentoRetorno.Descricao.Replace("/", "-")}.pdf";

                                Stream stream = new MemoryStream(Convert.FromBase64String(boletoPdf.FirstOrDefault()));

                                if (dtoGerarBoleto.PDF)
                                {
                                    htmlLista.Add(boletoPdf.FirstOrDefault());
                                }
                                else
                                {
                                    htmlLista.Add(pagamentoRetorno.BoletoHTML);
                                }

                                attachments.Add(new Attachment(stream, $"{pagamentoRetorno.Descricao.Replace("/", "-")}.pdf"));
                            }

                            pagamentosLista.Add(pagamentoRetorno);
                        }

                        break;
                    case TipoAcaoBoletoEnum.RecalcularEnviarPorEmail:

                        var novosPagamentos = await RecalcularPagamento(dtoGerarBoleto.PagamentoIds);

                        foreach (var pagamento in novosPagamentos)
                        {
                            if (!string.IsNullOrEmpty(pagamento.BoletoHTML))
                            {
                                var boletoPdf = await Core.Helpers.CoreHelpers.ConverterBoletoPDF(new List<string>() { pagamento.BoletoHTML }, _appSettings.BoletoServiceUrl);

                                string filePath = $"{Path.GetTempPath()}{pagamento.Descricao.Replace("/", "-")}.pdf";

                                Stream stream = new MemoryStream(Convert.FromBase64String(boletoPdf.FirstOrDefault()));

                                if (dtoGerarBoleto.PDF)
                                {
                                    htmlLista.Add(boletoPdf.FirstOrDefault());
                                }
                                else
                                {
                                    htmlLista.Add(pagamento.BoletoHTML);
                                }

                                attachments.Add(new Attachment(stream, $"{pagamento.Descricao.Replace("/", "-")}.pdf"));
                            }

                            pagamentosLista.Add(_mapper.Map<Pagamento>(pagamento));
                        }

                        break;
                    default:
                        break;
                }


                var foto = await _unidadeService.SelecionarFoto(aluno.UnidadeId);

                StringBuilder conteudoDoEmail = Helpers.CoreHelpers.MontarConteudoEmail(_mapper.Map<List<DtoPagamento>>(pagamentosLista), foto, aluno.Nome);

                foreach (var pagamentoId in pagamentosLista)
                {
                    await _emailEnviadoRepository.AddAsync(new EmailEnviado
                    {
                        CorpoEmail = conteudoDoEmail.ToString(),
                        DataEnvio = DateTime.Now,
                        PagamentoId = pagamentoId.Id,
                        EmailPara = aluno.Contato.Email
                    });
                }

                List<string> emails = new List<string>();

                emails.Add(aluno.Contato.Email);

                var pgto = await _alunoFinanceiroContratoRepository.BuscarPorId(dtoGerarBoleto.PagamentoIds.FirstOrDefault());
                var matricula = await _matriculaAlunoService.BuscarPorId(pgto.MatriculaId);
                var nacionalTec = false;

                if (matricula != null && matricula.Curso != null)
                    nacionalTec = matricula.Curso.NacionatalTec;

                if (pagamentosLista.Count == 1)
                {
                    await _emailSenderService.SendEmailAsync(emails.ToArray(), $"Seu boleto está disponível - Curso {aluno.Unidade.Nome}", conteudoDoEmail.ToString(), attachments, nacionalTec);
                }
                else
                {
                    await _emailSenderService.SendEmailAsync(emails.ToArray(), $"Seus boletos estão disponíveis - Curso {aluno.Unidade.Nome}", conteudoDoEmail.ToString(), attachments, nacionalTec);
                }

                return htmlLista;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<DtoPagamento>> RecalcularPagamento(List<int> pagamentoIds)
        {
            try
            {
                List<DtoPagamento> pagamentosNovos = new List<DtoPagamento>();

                int matriculaId = 0;

                DtoMatriculaAlunoResponse matricula = new DtoMatriculaAlunoResponse();

                foreach (var pagamentoId in pagamentoIds)
                {
                    var pagamentoOld = await _alunoFinanceiroContratoRepository.BuscarPorId(pagamentoId);

                    if (matriculaId == 0)
                    {
                        matricula = await _matriculaAlunoService.BuscarPorId(pagamentoOld.MatriculaId);
                    }

                    DtoPagamento pagamentoNovo = _mapper.Map<DtoPagamento>(pagamentoOld);

                    matriculaId = pagamentoNovo.MatriculaId;

                    pagamentoNovo.Id = 0;
                    pagamentoNovo.DataEmissao = DateTime.Now;
                    pagamentoNovo.DataVencimento = DateTime.Now;
                    pagamentoNovo.Desconto = 0;

                    if (pagamentoOld.Descricao.Contains("Via"))
                    {
                        pagamentoNovo.Descricao = $"{pagamentoOld.Descricao}";
                    }
                    else
                    {
                        pagamentoNovo.Descricao = $"{pagamentoOld.Descricao} - 2ª Via";
                    }

                    pagamentoNovo.TipoSituacao = TipoSituacaoEnum.Aberto;

                    pagamentoNovo.NumeroRegistro = pagamentoOld.PagamentoIdOld.HasValue ? $"{pagamentoOld.NumeroRegistro}" : $"{pagamentoOld.NumeroRegistro}V";
                    // Salva sempre o Id do primeiro pagamento criado
                    pagamentoNovo.PagamentoIdOld = pagamentoOld.PagamentoIdOld.HasValue ? pagamentoOld.PagamentoIdOld : pagamentoOld.Id;

                    pagamentosNovos.Add(pagamentoNovo);
                }

                return await GerarPagamentos(new DtoAlunoFinanceiroGrid { MatriculaId = matriculaId, Pagamento = pagamentosNovos });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoEmailEnviado>> ConsultarEmail(int pagamentoId)
        {
            try
            {
                var email = await _emailEnviadoRepository.BuscarPorPagamento(pagamentoId);

                return _mapper.Map<IEnumerable<DtoEmailEnviado>>(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ItauSimplesCorpoCobranca> EnviarBoletoItau(DtoPagamento dtoPagamento, DtoMatriculaAlunoResponse matriculaAluno, string nossoNumero)
        {
            try
            {
                int nossoNumeroNovo = int.Parse(nossoNumero) + 1;

                var dac = Core.Helpers.CoreHelpers.GerarDAC(nossoNumeroNovo.ToString());

                var aluno = await _alunoService.BuscarPorId(matriculaAluno.AlunoId);

                string valor = dtoPagamento.Valor.ToString("N2").Replace(",", "").Replace(".", "");

                var simplesCorpoCobranca = new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesCorpoCobranca
                {
                    //tipo_ambiente = 2, // 1 Teste, 2 Produção
                    tipo_ambiente = _appSettings.HomologItau ? 1 : 2,
                    tipo_registro = 1,
                    tipo_cobranca = 1,
                    seu_numero = dtoPagamento.NumeroRegistro,
                    tipo_produto = "00006",
                    subproduto = "00008",
                    titulo_aceite = "S",
                    tipo_carteira_titulo = "109",
                    nosso_numero = nossoNumeroNovo.ToString(),
                    digito_verificador_nosso_numero = dac,
                    data_vencimento = dtoPagamento.DataVencimento.HasValue ? dtoPagamento.DataVencimento.Value.ToString("yyyy-MM-dd") : "", // Data de vencimento da parcela
                    valor_cobrado = Core.Helpers.CoreHelpers.ComplementarZeroEsquerda(valor),
                    especie = "04",
                    data_emissao = DateTime.Now.ToString("yyyy-MM-dd"), // data de emissão do boleto
                    tipo_pagamento = 3,
                    indicador_pagamento_parcial = "false",
                    beneficiario = new Core.Model.ArquivoRemessa.ItauBeneficiario
                    {
                        agencia_beneficiario = "0940",
                        cpf_cnpj_beneficiario = "09435113000194",
                        conta_beneficiario = "0014369",
                        digito_verificador_conta_beneficiario = "6",
                    },
                    pagador = new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesPagador
                    {
                        cpf_cnpj_pagador = aluno.CPF,
                        nome_pagador = aluno.Nome.Length < 30 ? aluno.Nome : aluno.Nome.Substring(0, 30),
                        logradouro_pagador = aluno.Endereco.Rua,
                        cidade_pagador = aluno.Endereco.Cidade.Length < 15 ? aluno.Endereco.Cidade : aluno.Endereco.Cidade.Substring(0, 15),
                        uf_pagador = aluno.Endereco.Estado,
                        cep_pagador = aluno.Endereco.CEP,
                        grupo_email_pagador = new List<Core.Model.ArquivoRemessa.ItauGrupoEmailPagador>
                        {
                            new Core.Model.ArquivoRemessa.ItauGrupoEmailPagador
                            {
                                email_pagador = aluno.Contato.Email
                            }
                        }
                    },
                    moeda = new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesMoeda
                    {
                        codigo_moeda_cnab = "09"
                    },
                    juros = new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesJuros
                    {
                        tipo_juros = 5
                    },
                    multa = new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesMulta
                    {
                        tipo_multa = 3
                    },
                    recebimento_divergente = new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesRecebimentoDivergente
                    {
                        tipo_autorizacao_recebimento = "1"
                    },
                };

                if (dtoPagamento.Desconto.HasValue)
                {
                    if (dtoPagamento.Desconto.HasValue)
                    {
                        if (dtoPagamento.Desconto > 0)
                        {
                            var descontoBoleto = (dtoPagamento.Valor * dtoPagamento.Desconto) / 100;

                            var descontoValorFormatado = descontoBoleto.Value.ToString("N2");

                            simplesCorpoCobranca.grupo_desconto = new List<Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesGrupoDesconto>
                        {
                            new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesGrupoDesconto
                            {
                                tipo_desconto = 1,
                                data_desconto = dtoPagamento.DataVencimento.Value.ToString("yyyy-MM-dd"),
                                percentual_desconto = dtoPagamento.Desconto.Value.ToString().Replace(",00", ""),
                                valor_desconto = Core.Helpers.CoreHelpers.ComplementarZeroEsquerda(descontoValorFormatado.Replace(",", "").Replace(".",""))
                            }
                        };
                        }
                        else
                        {
                            simplesCorpoCobranca.grupo_desconto = new List<Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesGrupoDesconto>
                            {
                                new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesGrupoDesconto
                                {
                                    tipo_desconto = 0,
                                }
                            };
                        }
                    }
                }

                return simplesCorpoCobranca;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DtoPagamento>> GerarPagamentoResidual(DtoGerarBoletoRequest dtoGerarBoletoRequest)
        {
            try
            {
                var nossoNumeroOld = await _alunoFinanceiroContratoRepository.UltimoNossoNumeroGerado();

                string nossoNumero = new Random().Next(20201190, 99991123).ToString();

                List<Pagamento> pagamentosLista = new List<Pagamento>();

                int matriculaId = 0;

                foreach (var pagamentoId in dtoGerarBoletoRequest.PagamentoIds)
                {
                    var pagamento = await _alunoFinanceiroContratoRepository.BuscarPorId(pagamentoId);

                    pagamento.NossoNumero = nossoNumero;

                    matriculaId = pagamento.MatriculaId;
                    pagamento.TipoSituacao = TipoSituacaoEnum.Aberto;
                    pagamento.DataVencimento = DateTime.Now;
                    pagamento.Desconto = 0;
                    pagamento.TarifaBanco = 0;

                    pagamentosLista.Add(pagamento);
                }

                var pagamentosRetorno = await GerarPagamentos(new DtoAlunoFinanceiroGrid { MatriculaId = matriculaId, Pagamento = _mapper.Map<List<DtoPagamento>>(pagamentosLista) });

                return pagamentosRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Efetua os pagamentos por portal do aluno / parcelas e solicitações
        /// </summary>
        /// <param name="dtoPagamentoCartaoCredito"></param>
        /// <returns></returns>
        public async Task<DtoPagamentoCreditoResponse> EfetuarPagamentoAPIAdquirente(DtoPagamentoCartaoCredito dtoPagamentoCartaoCredito)
        {
            try
            {
                List<Pagamento> pagamentosLista = new List<Pagamento>();

                var matricula = await _matriculaAlunoService.BuscarPorId(dtoPagamentoCartaoCredito.MatriculaId.Value);

                if (!dtoPagamentoCartaoCredito.SolicitacaoId.HasValue)
                {
                    foreach (var pagamentoId in dtoPagamentoCartaoCredito.PagamentoIds)
                    {
                        var pagamento = await _alunoFinanceiroContratoRepository.BuscarPorId(pagamentoId);

                        pagamentosLista.Add(pagamento);
                    }
                }
                else
                {
                    var solicitacao = await _solicitacaoService.BuscarPorId(dtoPagamentoCartaoCredito.SolicitacaoId.Value);

                    Pagamento pagamento = new Pagamento();
                    pagamento.Id = 0;
                    pagamento.Descricao = solicitacao.Descricao;
                    pagamento.DataPagamento = DateTime.Now;
                    pagamento.DataEmissao = DateTime.Now;
                    pagamento.TipoSituacao = TipoSituacaoEnum.Pago;
                    pagamento.DataPagamento = DateTime.Now;
                    pagamento.MatriculaId = matricula.Id;
                    pagamento.NossoNumero = "TEF";

                    pagamento.Valor = solicitacao.Valor.Value;

                    pagamentosLista.Add(pagamento);
                }

                var aluno = await _alunoService.BuscarPorId(matricula.AlunoId);

                string response = string.Empty;

                var acquirersSelecionada = AcquirersEnum.Cielo;

                dynamic retorno = null;

                string statusCode = string.Empty;

                if (acquirersSelecionada == AcquirersEnum.Cielo)
                {
                    var valor = dtoPagamentoCartaoCredito.ValorTotal * 100;

                    string mesVencimento = dtoPagamentoCartaoCredito.DadosCartaoAluno.MesValidade.Length == 1 ? $"0{int.Parse(dtoPagamentoCartaoCredito.DadosCartaoAluno.MesValidade) + 1}" : $"{int.Parse(dtoPagamentoCartaoCredito.DadosCartaoAluno.MesValidade) + 1}";

                    var cieloRequest = new DtoCieloRequest
                    {
                        MerchantOrderId = $"{matricula.Unidade.Nome.Substring(3, 1)}{new Random().Next(100000000, 999999999).ToString()}",
                        Customer = new Customer
                        {
                            Name = $"{aluno.Nome}",
                            Email = !string.IsNullOrEmpty(aluno.Contato.Email) ? aluno.Contato.Email : "",
                            Identity = aluno.CPF,
                            IdentityType = "cpf",
                        },
                        Payment = new Payment
                        {
                            Capture = true,
                            Authenticate = false,
                            Type = "CreditCard",
                            Amount = (int)valor,
                            SoftDescriptor = $"SUPLETIVO",
                            Recurrent = false,
                            ServiceTaxAmount = 0,
                            Interest = 0,
                            Provider = "Cielo",
                            Installments = dtoPagamentoCartaoCredito.QuantidadeParcela,
                            CreditCard = new CreditCard
                            {
                                CardNumber = dtoPagamentoCartaoCredito.DadosCartaoAluno.NumeroCartao,
                                Holder = dtoPagamentoCartaoCredito.DadosCartaoAluno.NomePessoa,
                                ExpirationDate = $"{mesVencimento}/{int.Parse(dtoPagamentoCartaoCredito.DadosCartaoAluno.AnoValidade)}",
                                SecurityCode = dtoPagamentoCartaoCredito.DadosCartaoAluno.CodigoSeguranca,
                                Brand = Helpers.CoreHelpers.BandeiraCartao(dtoPagamentoCartaoCredito.DadosCartaoAluno.NumeroCartao),
                                SaveCard = false,
                                CardOnFile = new CardOnFile
                                {
                                    Usage = "Used",
                                    Reason = "Unscheduled"
                                }
                            },
                            IsCryptoCurrencyNegotiation = true
                        },
                    };

                    response = await Helpers.CoreHelpers.GetTransaction(cieloRequest, acquirersSelecionada);

                    response = response.Replace("\"", "\\").Replace("\\\\", "\"").Replace("\\", "");

                    retorno = JsonConvert.DeserializeObject<DtoCieloRequest>(response.ToString());
                }
                else
                {
                    var pagamentoCartao = new DtoPagamentoCartaoRequest
                    {
                        capture = true,
                        reference = $"{matricula.NumeroMatricula}{DateTime.Now.Day}{DateTime.Now.Minute}{DateTime.Now.Millisecond}",
                        amount = dtoPagamentoCartaoCredito.ValorTotal.ToString().Replace(".", "").Replace(",", ""),
                        installments = dtoPagamentoCartaoCredito.QuantidadeParcela,
                        cardholderName = dtoPagamentoCartaoCredito.DadosCartaoAluno.NomePessoa,
                        cardNumber = dtoPagamentoCartaoCredito.DadosCartaoAluno.NumeroCartao,
                        expirationMonth = int.Parse(dtoPagamentoCartaoCredito.DadosCartaoAluno.MesValidade),
                        expirationYear = int.Parse(dtoPagamentoCartaoCredito.DadosCartaoAluno.AnoValidade),
                        securityCode = dtoPagamentoCartaoCredito.DadosCartaoAluno.CodigoSeguranca,
                    };

                    response = await Helpers.CoreHelpers.GetTransaction(pagamentoCartao, acquirersSelecionada);

                    response = response.Replace("\"", "\\").Replace("\\\\", "\"").Replace("\\", "");

                    retorno = JsonConvert.DeserializeObject<DtoCartaoCreditoResponse>(response.ToString());
                }

                TipoRetornoCartaoCredito tipoRetornoCartao = TipoRetornoCartaoCredito.ProblemasComCartaoCredito;

                if (retorno == null)
                {
                    tipoRetornoCartao = TipoRetornoCartaoCredito.ProblemasComCartaoCredito;
                }
                else
                {
                    if (acquirersSelecionada == AcquirersEnum.Cielo)
                    {
                        if (retorno.Payment != null)
                        {
                            statusCode = retorno.Payment.ReturnCode;
                        }
                    }
                    else
                    {

                        statusCode = retorno.StatusCode;
                    }
                }

                switch (statusCode)
                {
                    case "00":
                    case "4":
                    case "6":
                        tipoRetornoCartao = TipoRetornoCartaoCredito.OperacaoRealizadaComSuceso;

                        string authorizationCode = string.Empty;
                        string tid = string.Empty;

                        if (acquirersSelecionada == AcquirersEnum.Cielo)
                        {
                            tid = retorno.Payment.Tid;
                            authorizationCode = retorno.Payment.AuthorizationCode;
                        }
                        else
                        {
                            tid = retorno.Tid;
                            authorizationCode = retorno.AuthorizationCode;
                        }

                        foreach (var pagamento in pagamentosLista)
                        {
                            DadosCartao dadosCartao = new DadosCartao
                            {
                                Id = 0,
                                AcquirersEnum = acquirersSelecionada,
                                ValorParcela = dtoPagamentoCartaoCredito.ValorTotal / dtoPagamentoCartaoCredito.QuantidadeParcela,
                                QuantidadeParcela = dtoPagamentoCartaoCredito.QuantidadeParcela,
                                AnoValidade = dtoPagamentoCartaoCredito.DadosCartaoAluno.AnoValidade.ToString(),
                                MesValidade = dtoPagamentoCartaoCredito.DadosCartaoAluno.MesValidade.ToString(),
                                NomePessoa = dtoPagamentoCartaoCredito.DadosCartaoAluno.NomePessoa,
                                NumeroCartao = dtoPagamentoCartaoCredito.DadosCartaoAluno.NumeroCartao,
                                TipoPagamento = Core.Model.Enums.TipoPagamentoEnum.CartaoCredito,
                                ValorTotal = dtoPagamentoCartaoCredito.ValorTotal,
                                TID = tid,
                                CodigoAutorizacao = authorizationCode,
                                BandeiraCartao = Helpers.CoreHelpers.BandeiraCartao(dtoPagamentoCartaoCredito.DadosCartaoAluno.NumeroCartao)
                            };

                            var dadosCartaoRetorno = await _alunoFinanceiroContratoRepository.InserirDetalheCartao(dadosCartao);

                            decimal valorPago = 0;

                            if (pagamento.DataVencimento.HasValue)
                            {
                                if (DateTime.Now.Date > pagamento.DataVencimento.Value.Date)
                                {
                                    valorPago = pagamento.Valor;
                                }
                                else
                                {
                                    valorPago = pagamento.Valor - ((pagamento.Valor * pagamento.Desconto.Value) / 100);
                                }
                            }
                            else
                            {
                                valorPago = pagamento.Valor;
                            }

                            pagamento.DadosCartaoId = dadosCartaoRetorno.Id;
                            pagamento.TipoSituacao = TipoSituacaoEnum.Pago;
                            pagamento.DataPagamento = DateTime.Now;
                            pagamento.ValorPago = valorPago;

                            if (pagamento.Id > 0)
                            {
                                await _alunoFinanceiroContratoRepository.UpdateAsync(pagamento);
                            }
                            else
                            {
                                await _alunoFinanceiroContratoRepository.AddAsync(pagamento);
                            }

                            if (dtoPagamentoCartaoCredito.SolicitacaoId.HasValue)
                            {
                                await _solicitacaoAlunoService.Inserir(dtoPagamentoCartaoCredito.SolicitacaoId.Value, matricula.Id, StatusPagamentoEnum.Pago, dtoPagamentoCartaoCredito.UsuarioLogadoId);
                            }
                            else if (pagamento.SolicitacaoAlunoId.HasValue)
                            {
                                await _solicitacaoAlunoService.AtualizarPagamentoSolicitacao(pagamento.SolicitacaoAlunoId.Value, StatusPagamentoEnum.Pago);

                                var solicitacao = await _solicitacaoAlunoService.BuscarPorId(pagamento.SolicitacaoAlunoId.Value);

                                if (solicitacao.Descricao.ToUpper().Contains("APOSTILA"))
                                    await _matriculaAlunoService.AtualizarMaterialLiberado(matricula.Id, true);
                            }
                        }

                        if (pagamentosLista.Any(x => x.TipoPagamento == Core.Model.Enums.TipoPagamentoEnum.BoletoBancario))
                            await TicketEnviar(matricula.Id, dtoPagamentoCartaoCredito.UsuarioLogadoId, pagamentosLista.Select(x => x.Id).ToArray());

                        break;
                    case "11":
                        tipoRetornoCartao = TipoRetornoCartaoCredito.TransacaoJaConfirmada;
                        break;
                    case "58":
                    case "84":
                    case "70":
                        tipoRetornoCartao = TipoRetornoCartaoCredito.ProblemasComCartaoCredito;
                        break;
                    case "118":
                    case "78":
                    case "14":
                        tipoRetornoCartao = TipoRetornoCartaoCredito.CartaoBloqueado;
                        break;
                    case "115":
                    case "101":
                        tipoRetornoCartao = TipoRetornoCartaoCredito.ExceceuLimiteTransacoes;
                        break;
                    case "112":
                    case "72":
                    case "57":
                        tipoRetornoCartao = TipoRetornoCartaoCredito.CartaoExpirado;
                        break;
                    case "37":
                    case "05":
                        tipoRetornoCartao = TipoRetornoCartaoCredito.NaoAutorizada;
                        break;
                    default:
                        tipoRetornoCartao = TipoRetornoCartaoCredito.ProblemasComCartaoCredito;
                        break;
                }

                return new DtoPagamentoCreditoResponse { RetornoCartao = tipoRetornoCartao };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoDadosCartao> BuscarDetalhePagamento(int pagamentoId)
        {
            try
            {
                var pagamento = await _alunoFinanceiroContratoRepository.BuscarPorId(pagamentoId);
                DtoDadosCartao detalhePagamento = new DtoDadosCartao();
                if (pagamento.TipoPagamento == Core.Model.Enums.TipoPagamentoEnum.CartaoCredito
                    || pagamento.TipoPagamento == Core.Model.Enums.TipoPagamentoEnum.CartaoDebito)
                {
                    if (!string.IsNullOrEmpty(pagamento.DadosCartao?.NumeroCartao))
                    {
                        pagamento.DadosCartao.NumeroCartao = pagamento.DadosCartao.NumeroCartao.Substring(12, 4);
                        detalhePagamento = _mapper.Map<DtoDadosCartao>(pagamento.DadosCartao);
                    }


                    detalhePagamento.ValorPago = pagamento.ValorPago;
                    detalhePagamento.DataPagamento = pagamento.DataPagamento ?? pagamento.DataEmissao ?? new DateTime();
                }
                else
                {
                    detalhePagamento.TipoPagamento = pagamento.TipoPagamento;

                    detalhePagamento.ValorPago = pagamento.ValorPago;
                    detalhePagamento.Valor = pagamento.Valor;
                    detalhePagamento.Desconto = pagamento.Desconto;
                    detalhePagamento.DataPagamento = pagamento.DataPagamento ?? new DateTime();

                }
                return detalhePagamento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoPagamento> BuscarPorId(int pagamentoId)
        {
            try
            {
                var pagamento = await _alunoFinanceiroContratoRepository.BuscarPorId(pagamentoId);

                return _mapper.Map<DtoPagamento>(pagamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> ConsultarComprovante(int pagamentoId)
        {
            try
            {
                var pagamento = await _alunoFinanceiroContratoRepository.BuscarPorId(pagamentoId);

                return pagamento.ComprovanteCartao.Substring(1, 1030);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task RollBackPlanoPagamento(DtoAlunoFinanceiroGrid dtoAlunoFinanceiro, DtoMatriculaAlunoResponse matricula)
        {
            try
            {

                //if (!string.IsNullOrEmpty(matricula.NumeroMatricula))
                //{
                // Gera o numero da matricula e salva o plano de pagamento escolhido
                matricula.NumeroMatricula = "";
                matricula.PlanoPagamentoAluno = null;

                //await _matriculaAlunoService.Update(_mapper.Map<MatriculaAluno>(matricula));

                // await _alunoFinanceiroContratoRepository.RemoverPorMatricula(matricula.Id);

                //await _matriculaAlunoService.GerarNumeroMatricula(matricula, new PlanoPagamentoAluno
                //{
                //    PlanoPagamentoId = null,
                //    CampanhaId = null,
                //    DataPrimeiraParcela = null,
                //    DataSegundaParcela = null,
                //    TipoPagamento = 0,
                //    TemApostila = null
                //}, true);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task TicketEnviar(int matriculaId, int UsuarioLogadoId, int[] pagamentoIds)
        {
            try
            {
                var matricula = await _matriculaAlunoService.BuscarPorId(matriculaId);

                var usuarioLogado = await _usuarioService.BuscarPorId(UsuarioLogadoId);

                var usuarioLista = await _usuarioService.BuscarUsuarioAtendente();

                var unidade = await _unidadeService.BuscarPorId(usuarioLogado.Unidade.Id);

                #region criar ticket para envio
                var assunto = _assuntoTicketRepository.BuscarBaixaBoleto();

                var ticket = new Dto.TicketVO.DtoTicket
                {
                    DataAbertura = DateTime.Now,
                    Status = Core.Model.Tickets.StatusTicketEnum.Aberto,
                    AssuntoTicketId = assunto.Id,
                    UsuarioLogadoId = UsuarioLogadoId,
                    IdFuncionarioAtendente = UsuarioLogadoId,
                    MatriculaId = matriculaId,
                    BaixaBoleto = true
                };

                ticket.DestinatarioTicket = new Dto.TicketVO.DtoDestinatarioTicket();

                ticket.DestinatarioTicket.UnidadeId = unidade.Id;

                ticket.DestinatarioTicket.DepartamentoId = unidade.CentroCusto.FirstOrDefault().Id;

                string descricao = $"Aluno(a): {matricula.Aluno.Nome}\nRM: {matricula.NumeroMatricula}\nCPF: {Core.Helpers.CoreHelpers.FormatCNPJouCPF(matricula.Aluno.CPF)}\n\n";

                foreach (var pagamentoId in pagamentoIds)
                {
                    var pagamento = await _alunoFinanceiroContratoRepository.BuscarPorId(pagamentoId);

                    if (pagamentoIds.Count() == 1)
                    {
                        descricao = descricao + $"Nosso Número: {pagamento.NossoNumero} \n" +
                            $"Seu Número: {pagamento.NumeroRegistro} \n" +
                            $"Data Vencimento: {pagamento.DataVencimento?.ToString("dd/MM/yyyy")} \n" +
                            $"Valor: R$ {pagamento.Valor.ToString("N2")}\n";
                    }
                    else
                    {
                        descricao = descricao + $"| Nosso Número: {pagamento.NossoNumero} \n" +
                            $"Seu Número: {pagamento.NumeroRegistro} \n" +
                            $"Data Vencimento: {pagamento.DataVencimento?.ToString("dd/MM/yyyy")} \n" +
                            $"Valor: R$ {pagamento.Valor.ToString("N2")}\n";
                    }
                }

                ticket.DestinatarioTicket.Mensagem = descricao;
                ticket.DestinatarioTicket.UsuarioLogadoId = UsuarioLogadoId;
                ticket.DestinatarioTicket.StatusTicket = Core.Model.Tickets.StatusTicketEnum.Aberto;
                ticket.DataAbertura = DateTime.Now;
                ticket.NumeroProtocolo = new Random(DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Minute).Next().ToString();

                ticket.DestinatarioTicket.UsuarioDestinarioTicket = new List<DtoUsuarioDestinarioTicket>();

                foreach (var item in usuarioLista)
                {
                    var usuario = await _usuarioService.FiltrarUsuario(new Dto.UsuarioVO.DtoFiltrarUsuario { FuncionarioId = item.Funcionario?.Id });

                    ticket.DestinatarioTicket.UsuarioDestinarioTicket.Add(new Dto.TicketVO.DtoUsuarioDestinarioTicket
                    {
                        FuncionarioId = usuario.FirstOrDefault().Id
                    });
                }

                await _ticketService.Inserir(ticket);

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task EnviarWhatsAppMatriculaAluno(MatriculaAluno matricula)
        {
            var mensagemWhatsApp = string.Empty;
            var tipoUnidadeCurso = string.Empty;
            var nomeFantasiaCurso = string.Empty;
            var nomeApp = string.Empty;
            var youtubeURL = string.Empty;
            var telegramURL = string.Empty;

            //var usuarioAluno = await _usuarioService.BuscarPorAlunoId(matricula.AlunoId);

            var unidade = await _unidadeService.BuscarPorId(matricula.UnidadeId.Value);

            if (matricula.UnidadeId == 10)
            {
                tipoUnidadeCurso = nomeFantasiaCurso = nomeApp = "Cursos NacionalTec";
                youtubeURL = "https://www.youtube.com/channel/UCfgpZT7ibTWvyGNTnWTheBw";
                telegramURL = "https://t.me/cursosnacionaltec";
            }
            else
            {
                tipoUnidadeCurso = "Curso Supletivo preparatório";
                nomeFantasiaCurso = "Supletivo – " + matricula.Unidade.NomeFantasia;
                nomeApp = "Supletivo Portal";
                youtubeURL = "https://www.youtube.com/c/SupletivoPreparatório";
                telegramURL = "https://t.me/supletivopreparatorioejaencceja";
            }

            mensagemWhatsApp = string.Format(@"*Olá, {0}*

*Seja bem-vindo(a) ao {1}.* 
Sua matricula foi realizada com sucesso!

*Baixe nosso aplicativo '{2}' na Google Play e aproveite todo conteúdo:*
•	Videoaulas
•	Simulados
•	Solicitações
•	Pagamentos
•	Envio de Documentos
Tudo isso na palma da sua mão!

Ou acesse o Portal do Aluno através do site: www.portaldoalunocurso.com.br

Para acessar informe:
*Login:* {3}
*Senha:* {4}

• Inscreva-se em nosso canal do Youtube e participe das aulas lives: {5}

• Estamos também no Telegram: {6}

_*{7}*_ ",
                                            matricula.Aluno.Nome,
                                            tipoUnidadeCurso,
                                            nomeApp,
                                            //usuarioAluno.UserName,
                                            //usuarioAluno.Password,
                                            matricula.Aluno.CPF,
                                            matricula.Aluno.CPF.Substring(0, 4),
                                            youtubeURL,
                                            telegramURL,
                                            nomeFantasiaCurso
                                            );

            string retorno = await _whatsAppService.SendMessage(unidade.Contato.Instance, unidade.Contato.Token, matricula.Aluno.Contato.Celular, mensagemWhatsApp, matricula.AlunoId);

            System.Diagnostics.Debug.Write(retorno);

            return;
        }

        public async Task<DtoPagamento> IsentarPagemento(DtoPagamento pagamento)
        {
            pagamento.TipoSituacao = TipoSituacaoEnum.Isento;
            await _alunoFinanceiroContratoRepository.UpdateAsync(_mapper.Map<Pagamento>(pagamento));
            return pagamento;
        }
        public async Task<bool> BaixaManual(BaixaManualDto baixaManual)
        {
            try
            {
                List<Pagamento> pagamentos = new List<Pagamento>();
                if (!(baixaManual.PagamentoIds.Count > 0))
                    return false;
                foreach (var pagamentoId in baixaManual.PagamentoIds)
                {

                    var pagamento = await _alunoFinanceiroContratoRepository.BuscarPorId(pagamentoId);

                    pagamentos.Add(pagamento);
                    if (baixaManual.TipoPagamento != Core.Model.Enums.TipoPagamentoEnum.BoletoBancario)
                    {
                        DadosCartao dadosCartao = new DadosCartao
                        {
                            Id = 0,
                            AcquirersEnum = baixaManual.Acquirer.Value,
                            ValorParcela = baixaManual.ValorPago / baixaManual.QuantidadeParcela.Value,
                            QuantidadeParcela = baixaManual.QuantidadeParcela.Value,
                            AnoValidade = string.Empty,
                            MesValidade = string.Empty,
                            NomePessoa = string.Empty,
                            NumeroCartao = "************" + baixaManual.NumeroCartao,
                            TipoPagamento = Core.Model.Enums.TipoPagamentoEnum.CartaoCredito,
                            ValorTotal = baixaManual.ValorPago,
                            TID = string.Empty,
                            CodigoAutorizacao = baixaManual.CodigoAutorizacao,
                            BandeiraCartao = baixaManual.BandeiraCartao
                        };                       

                        var dadosCartaoRetorno = await _alunoFinanceiroContratoRepository.InserirDetalheCartao(dadosCartao);

                        pagamento.DadosCartaoId = dadosCartaoRetorno.Id;
                    }

                    if (pagamento.SolicitacaoAlunoId > 0)
                    {
                        await _solicitacaoAlunoService.AtualizarPagamentoSolicitacao(pagamento.SolicitacaoAlunoId.Value, StatusPagamentoEnum.Pago);

                        if (pagamento.Descricao.ToUpper().Contains("APOSTILA"))
                            await _matriculaAlunoService.AtualizarMaterialLiberado(pagamento.MatriculaId, true);
                    }

                    decimal valorPago = 0;

                    if (pagamento.DataVencimento.HasValue)
                    {
                        if (DateTime.Now.Date > pagamento.DataVencimento.Value.Date)
                        {
                            valorPago = pagamento.Valor;
                        }
                        else
                        {
                            valorPago = pagamento.Valor - ((pagamento.Valor * pagamento.Desconto.Value) / 100);
                        }
                    }
                    else
                    {
                        valorPago = pagamento.Valor;
                    }

                    pagamento.TipoPagamento = baixaManual.TipoPagamento;
                    pagamento.TipoSituacao = TipoSituacaoEnum.Pago;

                    pagamento.DataPagamento = baixaManual.DataPagamento;

                    pagamento.ValorPago = valorPago;
                    await _alunoFinanceiroContratoRepository.UpdateAsync(pagamento);

                }
                var matricula = await _matriculaAlunoService.BuscarPorId(pagamentos.FirstOrDefault().MatriculaId);
                var unidade = await _unidadeService.BuscarPorId(matricula.UnidadeId);

                var mailBody = Helpers.CoreHelpers.MontarEmailBaixaManual(matricula, unidade, matricula.Curso.NacionatalTec, pagamentos);
                await _emailSenderService.SendEmailAsync(new string[] { "andrerc77@hotmail.com", "financeiro.suportecentral@gmail.com" }, "Baixa manual realizada com sucesso", mailBody, null, matricula.Curso.NacionatalTec, null, matricula.AlunoId);


                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
