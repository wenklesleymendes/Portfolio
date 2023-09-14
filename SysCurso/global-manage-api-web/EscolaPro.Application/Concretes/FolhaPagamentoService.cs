using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.FolhaPagamentos;
using EscolaPro.Core.Model.Funcionario;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.FolhaPagamentos;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.FolhaPagamentoVO;
using EscolaPro.Service.Dto.UnidadeVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class FolhaPagamentoService : IFolhaPagamentoService
    {
        private readonly IFolhaPagamentoRepository _folhaPagamentoRepository;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IUnidadeService _unidadeService;
        private readonly IAnexoRepository _anexoRepository;
        private readonly IControlePontoRepository _controlePontoRepository;
        private readonly IMapper _mapper;

        public FolhaPagamentoService(
            IFolhaPagamentoRepository folhaPagamentoRepository,
            IFuncionarioService funcionarioService,
            IUnidadeService unidadeService,
            IAnexoRepository anexoRepository,
            IControlePontoRepository controlePontoRepository,
            IMapper mapper)
        {
            _folhaPagamentoRepository = folhaPagamentoRepository;
            _unidadeService = unidadeService;
            _funcionarioService = funcionarioService;
            _anexoRepository = anexoRepository;
            _controlePontoRepository = controlePontoRepository;
            _mapper = mapper;
        }

        public async Task<DtoFolhaPagamento> BuscarPorId(int idFolhaPagamento)
        {
            var folhaPagamento = await _folhaPagamentoRepository.BuscarPorId(idFolhaPagamento);

            var funcionario = await _funcionarioService.BuscarPorId(folhaPagamento.FuncionarioId.Value);

            var folhaPagamentoRetorno = _mapper.Map<DtoFolhaPagamento>(folhaPagamento);

            folhaPagamentoRetorno.Funcionario = funcionario;

            return folhaPagamentoRetorno;
        }

        public async Task<IEnumerable<DtoFolhaPagamentoGrid>> BuscarTodos(DtoFiltrarBusca filtrarBusca)
        {
            var folhaPagamentosLista = await _folhaPagamentoRepository.BuscarPorFiltro(filtrarBusca.CPF, filtrarBusca.Nome, filtrarBusca.InicioPeriodo, filtrarBusca.FimPeriodo, filtrarBusca.UnidadeId);

            List<DtoFolhaPagamentoGrid> folhaPagamentos = new List<DtoFolhaPagamentoGrid>();

            foreach (var item in folhaPagamentosLista.Where(x => !x.IsDelete))
            {
                var folhaPagamento = await _folhaPagamentoRepository.BuscarPorId(item.Id);

                var unidade = await _unidadeService.BuscarPorId(folhaPagamento.UnidadeId);

                var funcionario = await _funcionarioService.BuscarPorId(folhaPagamento.FuncionarioId.Value);

                var folhaPagamentoItem = new DtoFolhaPagamentoGrid
                {
                    Id = folhaPagamento.Id,
                    FuncionarioId = folhaPagamento.Funcionario.Id,
                    NomeColaborador = folhaPagamento.Funcionario.Nome,
                    RegimeContratacao = funcionario.DadosContratacao.TipoRegimeContratacao,
                    StatusPagamento = folhaPagamento.StatusPagamento,
                    Unidade = unidade.Nome,
                    ValorPagamento = folhaPagamento.ValorTotalPagamento.HasValue ? folhaPagamento.ValorTotalPagamento.Value : 0,
                    Competencia = folhaPagamento.Competencia
                };

                folhaPagamentos.Add(folhaPagamentoItem);
            }

            return folhaPagamentos;
        }

        public async Task<DtoAnexo> DownloadComprovanteBancario(int idFolhaPagamento)
        {
            try
            {
                var anexo = await _anexoRepository.DownloadComprovanteBancario(idFolhaPagamento);

                return _mapper.Map<DtoAnexo>(anexo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Excluir(int idFolhaPagamento)
        {
            var folhaPagamento = await _folhaPagamentoRepository.GetByIdAsync(idFolhaPagamento);
            folhaPagamento.IsDelete = true;
            await _folhaPagamentoRepository.UpdateAsync(folhaPagamento);
            return folhaPagamento.IsDelete;
        }

        public async Task<DtoHoleriteFolhaPagamento> ImprimirReciboPagamento(int idFolhaPagamento)
        {
            try
            {
                var folhaPagamento = await _folhaPagamentoRepository.BuscarPorId(idFolhaPagamento);

                var funcionario = await _funcionarioService.BuscarPorId(folhaPagamento.FuncionarioId.Value);

                var unidade = await _unidadeService.BuscarPorId(folhaPagamento.UnidadeId);

                DtoHoleriteFolhaPagamento holeriteFolhaPagamento = new DtoHoleriteFolhaPagamento
                {
                    Funcionario = funcionario,
                    ValorTotalAPagar = folhaPagamento.ValorTotalPagamento.HasValue ? folhaPagamento.ValorTotalPagamento.Value : 0,
                    TotalDesconto = folhaPagamento.ValorTotalDesconto.HasValue ? folhaPagamento.ValorTotalDesconto.Value : 0,
                    DataPagamento = folhaPagamento.DataPagamento,
                    Unidade = _mapper.Map<DtoUnidadeResponse>(unidade),
                    ListaPagamento = MontarLista(folhaPagamento, funcionario.DadosContratacao.TipoRegimeContratacao),
                    Competencia = folhaPagamento.Competencia
                };

                return holeriteFolhaPagamento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoFolhaPagamento> Inserir(DtoFolhaPagamento dtoFolhaPagamento)
        {
            try
            {
                dtoFolhaPagamento.StatusPagamento = Core.Model.MetasComissoes.StatusPagamentoEnum.AReceber;

                var funcionario = await _funcionarioService.BuscarPorId(dtoFolhaPagamento.FuncionarioId.Value);

                if (dtoFolhaPagamento.Id == 0)
                {
                    if (funcionario.DadosContratacao.TipoRegimeContratacao == RegimeContratacaoEnum.PROFESSOR_AUTONOMO)
                    {
                        var folhaPagamentoAnteriorLista = await _folhaPagamentoRepository.BuscarPagamentosPendente(dtoFolhaPagamento.FuncionarioId.Value);

                        var folhaJaCriadaParaUnidade = folhaPagamentoAnteriorLista.Where(x => x.UnidadeId == dtoFolhaPagamento.UnidadeId.Value).FirstOrDefault();

                        if (folhaJaCriadaParaUnidade != null)
                        {
                            throw new Exception("Já existe uma folha de pagamento pendente.");
                        }

                        var folhaPagamento = await _folhaPagamentoRepository.AddAsync(_mapper.Map<FolhaPagamento>(dtoFolhaPagamento));

                        // BaixarHorasExtra
                        await BaixarHoraExtra(folhaPagamento.Id, funcionario.Id, dtoFolhaPagamento.InicioHoraExtraPaga, dtoFolhaPagamento.TerminoHoraExtraPaga);

                        return _mapper.Map<DtoFolhaPagamento>(folhaPagamento);
                    }
                    else
                    {
                        if (await _folhaPagamentoRepository.VerificarSeExistePagamentoPendente(dtoFolhaPagamento.FuncionarioId.Value))
                        {
                            throw new Exception("Já existe uma folha de pagamento pendente.");
                        }

                        var folhaPagamento = await _folhaPagamentoRepository.AddAsync(_mapper.Map<FolhaPagamento>(dtoFolhaPagamento));

                        // BaixarHorasExtra
                        await BaixarHoraExtra(folhaPagamento.Id, funcionario.Id, dtoFolhaPagamento.InicioHoraExtraPaga, dtoFolhaPagamento.TerminoHoraExtraPaga);

                        return _mapper.Map<DtoFolhaPagamento>(folhaPagamento);
                    }
                }
                else
                {
                    await _folhaPagamentoRepository.DeletarHoraExtra(dtoFolhaPagamento.Id);

                    await _folhaPagamentoRepository.AdicionarHoraExtra(_mapper.Map<List<HoraExtra>>(dtoFolhaPagamento.HoraExtra), dtoFolhaPagamento.Id);

                    await _folhaPagamentoRepository.UpdateAsync(_mapper.Map<FolhaPagamento>(dtoFolhaPagamento));

                    // BaixarHorasExtra
                    await BaixarHoraExtra(dtoFolhaPagamento.Id, funcionario.Id, dtoFolhaPagamento.InicioHoraExtraPaga, dtoFolhaPagamento.TerminoHoraExtraPaga);

                    return await BuscarPorId(dtoFolhaPagamento.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DtoPagamentoFolha> MontarLista(FolhaPagamento folhaPagamento, RegimeContratacaoEnum regimeContratacao)
        {
            List<DtoPagamentoFolha> folhaPagamentoDetalhes = new List<DtoPagamentoFolha>();

            switch (regimeContratacao)
            {
                case RegimeContratacaoEnum.CLT_SEG_SEX:
                case RegimeContratacaoEnum.PROFESSOR_CLT:
                case RegimeContratacaoEnum.CLT_SEG_SAB:
                case RegimeContratacaoEnum.AUTONOMO_PRE_CLT_SEG_SAB:
                case RegimeContratacaoEnum.AUTONOMO_PRE_CLT_SEG_SEX:
                    if (folhaPagamento.SalarioLiquido.HasValue)
                        folhaPagamentoDetalhes.Add(new DtoPagamentoFolha { Descricao = "Salário Liquido", Valor = folhaPagamento.SalarioLiquido.Value });
                    break;
                case RegimeContratacaoEnum.PROFESSOR_AUTONOMO:
                    if (folhaPagamento.SalarioLiquido.HasValue)
                        folhaPagamentoDetalhes.Add(new DtoPagamentoFolha { Descricao = "Remuneração Total de Aulas", Valor = folhaPagamento.SalarioLiquido.Value });
                    break;
                case RegimeContratacaoEnum.PROFISSIONAL_AUTONOMO:
                    if (folhaPagamento.SalarioLiquido.HasValue)
                        folhaPagamentoDetalhes.Add(new DtoPagamentoFolha { Descricao = "Remuneração Total de Dias", Valor = folhaPagamento.SalarioLiquido.Value });
                    break;
                case RegimeContratacaoEnum.ESTAGIO_SEG_SEX:
                case RegimeContratacaoEnum.ESTAGIO_SEG_SAB:
                case RegimeContratacaoEnum.AUTONOMO_PRE_ESTAGIO_SEG_SEX:
                case RegimeContratacaoEnum.AUTONOMO_PRE_ESTAGIO_SEG_SAB:
                    if (folhaPagamento.SalarioLiquido.HasValue)
                        folhaPagamentoDetalhes.Add(new DtoPagamentoFolha { Descricao = "Bolsa auxilio", Valor = folhaPagamento.SalarioLiquido.Value });
                    break;
                default:
                    break;
            }

            if (folhaPagamento.Alimentacao.HasValue)
                folhaPagamentoDetalhes.Add(new DtoPagamentoFolha { Descricao = "Alimentação", Valor = folhaPagamento.Alimentacao.Value });

            if (folhaPagamento.Transporte.HasValue)
                folhaPagamentoDetalhes.Add(new DtoPagamentoFolha { Descricao = "Transporte", Valor = folhaPagamento.Transporte.Value });

            if (folhaPagamento.ComissaoPrimeiraParcelaPaga.HasValue)
                folhaPagamentoDetalhes.Add(new DtoPagamentoFolha { Descricao = "Comissão 1ª Parcela paga", Valor = folhaPagamento.ComissaoPrimeiraParcelaPaga.Value });

            if (folhaPagamento.BonusMetaPeriodo.HasValue)
                folhaPagamentoDetalhes.Add(new DtoPagamentoFolha { Descricao = "Bônus Meta Período", Valor = folhaPagamento.BonusMetaPeriodo.Value });

            if (folhaPagamento.ValorFerias.HasValue)
                folhaPagamentoDetalhes.Add(new DtoPagamentoFolha { Descricao = "Férias", Valor = folhaPagamento.ValorFerias.Value });

            if (folhaPagamento.ValorDiasDSR.HasValue)
                folhaPagamentoDetalhes.Add(new DtoPagamentoFolha { Descricao = "DSR - Descanso Semanal Remunerado", Valor = folhaPagamento.ValorDiasDSR.Value });

            if (folhaPagamento.ValorDecimoTerceiro.HasValue)
                folhaPagamentoDetalhes.Add(new DtoPagamentoFolha { Descricao = "Décimo Terceiro", Valor = folhaPagamento.ValorDecimoTerceiro.Value });

            if (folhaPagamento.ValorAdicional.HasValue)
                folhaPagamentoDetalhes.Add(new DtoPagamentoFolha { Descricao = "Valor Adicional", Valor = folhaPagamento.ValorAdicional.Value });

            if (folhaPagamento.MonitoriaProva.HasValue)
                folhaPagamentoDetalhes.Add(new DtoPagamentoFolha { Descricao = "Monitoria Prova", Valor = folhaPagamento.MonitoriaProva.Value });

            if (folhaPagamento.HoraExtra != null)
            {
                foreach (var item in folhaPagamento.HoraExtra)
                {
                    folhaPagamentoDetalhes.Add(new DtoPagamentoFolha { Descricao = $"Hora Extra: {item.Porcentagem}%", Valor = item.Valor });
                }
            }

            if (folhaPagamento.ValorTotalDesconto.HasValue)
                folhaPagamentoDetalhes.Add(new DtoPagamentoFolha { Descricao = $"Desconto: {folhaPagamento.JustificativaDesconto}", Valor = folhaPagamento.ValorTotalDesconto.Value });


            return folhaPagamentoDetalhes;
        }

        private async Task BaixarHoraExtra(int folhaPagamentoId, int funcionarioId, DateTime? dataInicio, DateTime? dataFim)
        {
            try
            {
                var controlePontoListaOld = await _controlePontoRepository.BuscarPorFolhaPagamentoId(folhaPagamentoId);

                if (controlePontoListaOld.Count() > 0)
                {
                    foreach (var pontoEletronico in controlePontoListaOld)
                    {
                        pontoEletronico.FolhaPagamentoId = 0;
                        pontoEletronico.Pago = false;
                        await _controlePontoRepository.UpdateAsync(pontoEletronico);
                    }
                }

                if (dataInicio.HasValue && dataFim.HasValue)
                {
                    var controlePontoLista = await _controlePontoRepository.BuscarPorFuncionario(funcionarioId, dataInicio.Value, dataFim.Value);

                    if (controlePontoLista.Count() > 0)
                    {
                        foreach (var pontoEletronico in controlePontoLista)
                        {
                            pontoEletronico.FolhaPagamentoId = folhaPagamentoId;
                            pontoEletronico.Pago = true;

                            await _controlePontoRepository.UpdateAsync(pontoEletronico);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
