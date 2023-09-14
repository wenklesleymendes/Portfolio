using AutoMapper;
using EscolaPro.Core.Helpers;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.ContasPagar;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.ContasAPagar;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.ContasAPagarVO;
using EscolaPro.Service.Dto.FolhaPagamentoVO;
using EscolaPro.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class ContasPagarService : IContasPagarService
    {
        private readonly IContasPagarRepository _contasPagarRepository;
        private readonly IDespesaParcelaRepository _despesaParcelaRepository;
        private readonly IAnexoRepository _anexoRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public ContasPagarService(
            IContasPagarRepository contasPagarRepository,
            IDespesaParcelaRepository despesaParcelaRepository,
            IFornecedorService fornecedorService,
            IAnexoRepository anexoRepository,
            IMapper mapper)
        {
            _contasPagarRepository = contasPagarRepository;
            _despesaParcelaRepository = despesaParcelaRepository;
            _fornecedorService = fornecedorService;
            _anexoRepository = anexoRepository;
            _mapper = mapper;
        }

        public async Task<DtoDetalheDespesa> BuscarDetalheDespesa(int idDespesa)
        {
            try
            {
                var despesa = await BuscarPorId(idDespesa);

                var proximoVencimento = despesa.DespesaParcela.Where(x => x.StatusPagamento == StatusPagamentoEnum.AReceber).FirstOrDefault();

                int parcelaAtual = despesa.DespesaParcela.Where(x => x.StatusPagamento == StatusPagamentoEnum.Pago).Count() >= 1 ? despesa.DespesaParcela.Where(x => x.StatusPagamento == StatusPagamentoEnum.Pago).Count() : 1;

                parcelaAtual = parcelaAtual == 0 ? 1 : parcelaAtual;

                List<DtoHistoricoDespesa> historicoDespesa = MontarHistoricoDespesa(despesa);

                DtoDetalheDespesa detalheDespesa = new DtoDetalheDespesa();

                switch (detalheDespesa.TipoParcela)
                {
                    case TipoParcelaEnum.Unica:
                        detalheDespesa.QuantidadeParcela = despesa.DespesaParcela.Any() ? $"{despesa.DespesaParcela.Where(x => x.StatusPagamento != StatusPagamentoEnum.AReceber).Count()}/{despesa.DespesaParcela.Count()}" : "1/1";
                        break;
                    case TipoParcelaEnum.Parcelada:
                        detalheDespesa.QuantidadeParcela = despesa.DespesaParcela.Any() ? $"{despesa.DespesaParcela.Where(x => x.StatusPagamento != StatusPagamentoEnum.AReceber).Count()}/{despesa.DespesaParcela.Count()}" : "1/1";
                        break;
                    case TipoParcelaEnum.DespesaRecorrente:
                        detalheDespesa.QuantidadeParcela = "Recorrente";
                        break;
                    default:
                        break;
                }

                detalheDespesa = BuscarDetalheDespesa(despesa.TipoParcela, despesa, historicoDespesa);

                return detalheDespesa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoDespesa> BuscarPorId(int idDespesa)
        {
            var despesa = await _contasPagarRepository.BuscarPorId(idDespesa);

            var despesaRetorno = _mapper.Map<DtoDespesa>(despesa);

            if (despesa.FornecedorId.HasValue)
            {
                var fornecedor = await _fornecedorService.BuscarPorId(despesa.FornecedorId.Value);

                despesaRetorno.Fornecedor = new DtoFornecedorResponse
                {
                    Id = fornecedor.Id,
                    CpfCnpj = fornecedor.CpfCnpj,
                    Endereco = fornecedor.Endereco,
                    NomeFantasia = fornecedor.NomeFantasia,
                    RazaoSocial = fornecedor.RazaoSocial,
                    TipoPessoa = fornecedor.TipoPessoa,
                    DadosBancario = fornecedor.DadosBancario
                };
            }

            var anexos = await _anexoRepository.BuscarPorIdDespesa(despesa.Id, true);

            if (anexos.Any())
            {
                despesaRetorno.Documentos = _mapper.Map<IEnumerable<DtoAnexo>>(anexos);
            }

            return despesaRetorno;
        }

        public async Task<DtoGridDespesaResponse> BuscarTodos(DtoFiltrarBusca filtrarBusca)
        {
            var despesasLista = await _contasPagarRepository.Filtrar(filtrarBusca.CPF, filtrarBusca.Categoria, filtrarBusca.InicioPeriodo, filtrarBusca.FimPeriodo, filtrarBusca.UnidadeId, filtrarBusca.TipoPagamento, filtrarBusca.TipoPessoa, filtrarBusca.StatusPagamento);

            DtoGridDespesaResponse dtoGridDespesa = await MontarGridPrincipal(despesasLista.ToList());

            return dtoGridDespesa;
        }

        public async Task<bool> Excluir(int idDespesa)
        {
            var despesa = await _contasPagarRepository.GetByIdAsync(idDespesa);
            despesa.IsDelete = true;
            await _contasPagarRepository.UpdateAsync(despesa);
            return despesa.IsDelete;
        }

        public async Task<bool> CancelarPagamento(DtoDespesaCancelar despesaCancelar)
        {
            try
            {
                var despesaParcela = await _despesaParcelaRepository.GetByIdAsync(despesaCancelar.DespesaParcelaId);

                despesaParcela.IsActive = true;
                despesaParcela.DataPagamento = DateTime.Now;
                despesaParcela.StatusPagamento = StatusPagamentoEnum.Cancelado;
                despesaCancelar.Observacao = despesaCancelar.Observacao;

                var sucesso = await _despesaParcelaRepository.UpdateAsync(despesaParcela);
                return sucesso > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoDespesa> Inserir(DtoDespesa dtoDespesa)
        {
            try
            {
                if (dtoDespesa.DataVencimento.HasValue)
                {
                    DateTime dataVencimento = new DateTime(dtoDespesa.DataVencimento.Value.Year, dtoDespesa.DataVencimento.Value.Month, dtoDespesa.DataVencimento.Value.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                    dtoDespesa.DataVencimento = dataVencimento;
                }


                if (dtoDespesa.Id == 0)
                {
                    dtoDespesa.DataCriacao = DateTime.Now;

                    var despesa = await _contasPagarRepository.AddAsync(_mapper.Map<Despesa>(dtoDespesa));

                    foreach (var item in despesa.DespesaParcela)
                    {
                        item.StatusPagamento = StatusPagamentoEnum.AReceber;
                        item.DespesaId = despesa.Id;
                        await _despesaParcelaRepository.UpdateAsync(item);
                    }


                    return await BuscarPorId(despesa.Id);
                }
                else
                {
                    var despesaAtualizar = await _contasPagarRepository.BuscarPorId(dtoDespesa.Id);

                    dtoDespesa.DataCriacao = despesaAtualizar.DataCriacao;

                    await _despesaParcelaRepository.Inserir(_mapper.Map<List<DespesaParcela>>(dtoDespesa.DespesaParcela), dtoDespesa.Id);


                    switch (dtoDespesa.TipoDespesa)
                    {
                        case TipoDespesaEnum.GuiaDARF:
                            if (dtoDespesa.DespesaDARF != null)
                            {
                                dtoDespesa.DespesaDARFId = dtoDespesa.DespesaDARF.Id;
                            }
                            else
                            {
                                dtoDespesa.DespesaDARF.Id = dtoDespesa.DespesaDARFId.Value;
                            }

                            break;
                        case TipoDespesaEnum.GuiaGPS:
                            if (dtoDespesa.DespesaDARF != null)
                            {
                                dtoDespesa.DespesaGPSId = dtoDespesa.DespesaGPS.Id;
                            }
                            else
                            {
                                dtoDespesa.DespesaGPSId = dtoDespesa.DespesaGPS.Id;
                            }
                            break;
                        default:
                            break;
                    }

                    if (dtoDespesa.Documentos != null)
                    {
                        if (dtoDespesa.Documentos.Any())
                        {
                            foreach (var anexo in dtoDespesa.Documentos)
                            {
                                anexo.DespesaId = dtoDespesa.Id;
                                await _anexoRepository.AddAsync(_mapper.Map<Anexo>(anexo));
                            }
                        }
                    }

                    await _contasPagarRepository.UpdateAsync(_mapper.Map<Despesa>(dtoDespesa));

                    return await BuscarPorId(dtoDespesa.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> LiquidarPagamento(DtoLiquidarDespesa dtoLiquidarDespesa)
        {
            try
            {
                var despesa = await _contasPagarRepository.BuscarPorId(dtoLiquidarDespesa.IdDespesa);

                var despesaParcelaPagamento = despesa.DespesaParcela.OrderBy(x => x.DataVencimento)
                                                                    .Where(x =>
                                                                    x.StatusPagamento == StatusPagamentoEnum.AReceber &&
                                                                    x.LancamentoManual).FirstOrDefault();

                switch (despesa.TipoParcela)
                {
                    case TipoParcelaEnum.Unica:
                    case TipoParcelaEnum.Parcelada:
                        if (despesaParcelaPagamento != null)
                        {
                            await BaixarDespesaCorrente(despesaParcelaPagamento, dtoLiquidarDespesa, false);
                        }
                        break;
                    case TipoParcelaEnum.DespesaRecorrente:
                        await BaixarDespesaCorrente(despesaParcelaPagamento, dtoLiquidarDespesa);
                        break;
                    default:
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<DespesaParcela> BaixarDespesaCorrente(DespesaParcela despesaParcela, DtoLiquidarDespesa liquidarDespesa, bool despesaRecorrente = true)
        {
            try
            {
                despesaParcela.StatusPagamento = StatusPagamentoEnum.Pago;
                despesaParcela.ValorPago = liquidarDespesa.ValorTotalPagar;
                despesaParcela.Observacao = liquidarDespesa.Observacao;
                despesaParcela.UnidadeId = liquidarDespesa.UnidadeId;
                despesaParcela.TipoPagamento = liquidarDespesa.TipoPagamento;
                despesaParcela.Juros = liquidarDespesa.Juros;
                despesaParcela.DescontoTaxa = liquidarDespesa.DescontoTaxa;


                DateTime dataPagamento = new DateTime(liquidarDespesa.DataPagamento.Year, liquidarDespesa.DataPagamento.Month, liquidarDespesa.DataPagamento.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                despesaParcela.DataPagamento = dataPagamento;

                await _despesaParcelaRepository.UpdateAsync(despesaParcela);

                DespesaParcela novaDespesaParcela = new DespesaParcela
                {
                    Id = 0,
                    DataVencimento = despesaParcela.DataVencimento.AddMonths(1),
                    DespesaId = despesaParcela.DespesaId,
                    StatusPagamento = StatusPagamentoEnum.AReceber,
                    ValorParcela = despesaParcela.ValorParcela,
                    TipoPagamento = despesaParcela.TipoPagamento,
                    LancamentoManual = despesaParcela.LancamentoManual
                };

                if (despesaRecorrente)
                {
                    return await _despesaParcelaRepository.AddAsync(novaDespesaParcela);
                }
                else
                {
                    return await _despesaParcelaRepository.GetByIdAsync(despesaParcela.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DtoDetalheDespesa BuscarDetalheDespesa(TipoParcelaEnum tipoParcela, DtoDespesa despesa, List<DtoHistoricoDespesa> historicoDespesa)
        {
            try
            {
                DtoDetalheDespesa detalheDespesa = new DtoDetalheDespesa();

                var proximoVencimento = despesa.DespesaParcela.Where(x => x.StatusPagamento == StatusPagamentoEnum.AReceber).FirstOrDefault();

                switch (tipoParcela)
                {
                    case TipoParcelaEnum.Unica:
                        detalheDespesa = new DtoDetalheDespesa
                        {
                            IdDespesa = despesa.Id,
                            Fornecedor = despesa.Fornecedor != null ? despesa.Fornecedor.NomeFantasia : "",
                            NomeDespesa = despesa.NomeDespesa,
                            TipoParcela = despesa.TipoParcela,
                            ValorDespesa = despesa.DespesaParcela.FirstOrDefault().ValorParcela,
                            HistoricoDespesa = historicoDespesa.OrderByDescending(x => x.Data),
                            DespesaParcelas = despesa.DespesaParcela,
                            Quitado = despesa.DespesaParcela.Where(x => x.StatusPagamento == StatusPagamentoEnum.AReceber).Count() > 0 ? false : true,
                            BaixaManual = despesa.DespesaParcela.Where(x => x.LancamentoManual && x.StatusPagamento == StatusPagamentoEnum.AReceber).Count() > 0 ? true : false
                        };

                        var parcela = despesa.DespesaParcela.FirstOrDefault();

                        switch (parcela.StatusPagamento)
                        {
                            case StatusPagamentoEnum.AReceber:
                                detalheDespesa.ProximoVencimento = proximoVencimento.DataVencimento.ToString("dd/MM/yyy");
                                break;
                            case StatusPagamentoEnum.Pago:
                                detalheDespesa.ProximoVencimento = "Quitado";
                                break;
                            case StatusPagamentoEnum.Cancelado:
                                detalheDespesa.ProximoVencimento = "Cancelado";
                                break;
                            default:
                                break;
                        }

                        break;
                    case TipoParcelaEnum.Parcelada:
                        detalheDespesa = new DtoDetalheDespesa
                        {
                            IdDespesa = despesa.Id,
                            Fornecedor = despesa.Fornecedor != null ? despesa.Fornecedor.NomeFantasia : "",
                            NomeDespesa = despesa.NomeDespesa,
                            TipoParcela = despesa.TipoParcela,
                            ProximoVencimento = proximoVencimento != null ? proximoVencimento.DataVencimento.ToString("dd/MM/yyy") : "Quitado",
                            ValorDespesa = despesa.DespesaParcela.FirstOrDefault().ValorParcela,
                            HistoricoDespesa = historicoDespesa.OrderByDescending(x => x.Data),
                            DespesaParcelas = despesa.DespesaParcela,
                            Quitado = despesa.DespesaParcela.Where(x => x.StatusPagamento == StatusPagamentoEnum.AReceber).Count() > 0 ? false : true,
                            BaixaManual = despesa.DespesaParcela.Where(x => x.LancamentoManual && x.StatusPagamento == StatusPagamentoEnum.AReceber).Count() > 0 ? true : false
                        };
                        break;
                    case TipoParcelaEnum.DespesaRecorrente:
                        detalheDespesa = new DtoDetalheDespesa
                        {
                            IdDespesa = despesa.Id,
                            Fornecedor = despesa.Fornecedor != null ? despesa.Fornecedor.NomeFantasia : "",
                            NomeDespesa = despesa.NomeDespesa,
                            TipoParcela = despesa.TipoParcela,
                            ProximoVencimento = proximoVencimento != null ? proximoVencimento.DataVencimento.ToString("dd/MM/yyy") : "Quitado",
                            ValorDespesa = despesa.DespesaParcela.FirstOrDefault().ValorParcela,
                            HistoricoDespesa = historicoDespesa.OrderByDescending(x => x.Data),
                            DespesaParcelas = despesa.DespesaParcela,
                            Quitado = despesa.DespesaParcela.Where(x => x.StatusPagamento == StatusPagamentoEnum.AReceber).Count() > 0 ? false : true,
                            BaixaManual = despesa.DespesaParcela.Where(x => x.LancamentoManual && x.StatusPagamento == StatusPagamentoEnum.AReceber).Count() > 0 ? true : false
                        };
                        break;
                    default:
                        break;
                }

                return detalheDespesa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DtoHistoricoDespesa> MontarHistoricoDespesa(DtoDespesa despesa)
        {
            try
            {
                List<DtoHistoricoDespesa> historicoDespesa = new List<DtoHistoricoDespesa>();

                DtoHistoricoDespesa primeiroItem = new DtoHistoricoDespesa
                {
                    Data = despesa.DataCriacao,
                    Descricao = $"Conta {EnumerationExtension.Description(despesa.TipoParcela)} criada",
                    Usuario = "Ricardo Castro",
                    Valor = despesa.TipoParcela == TipoParcelaEnum.DespesaRecorrente ? "Despesa Recorrente" : $"1/{despesa.DespesaParcela.Count()} - R$ {despesa.DespesaParcela.FirstOrDefault().ValorParcela.ToString("N2")}",
                    Status = HistoricoDespesaParcelaEnum.Criado
                };

                foreach (var despesaParcela in despesa.DespesaParcela.Where(x => x.StatusPagamento != StatusPagamentoEnum.AReceber))
                {
                    var historico = new DtoHistoricoDespesa
                    {
                        Data = despesaParcela.DataPagamento.Value,
                        Valor = despesaParcela.ValorPago.HasValue ? $"R$ {despesaParcela.ValorPago.Value.ToString("N2")}" : "",
                        Usuario = "Ricardo Castro"
                    };

                    switch (despesaParcela.StatusPagamento)
                    {
                        case StatusPagamentoEnum.Pago:
                            historico.Descricao = "Conta Liquidada";
                            historico.Status = HistoricoDespesaParcelaEnum.Pago;
                            break;
                        case StatusPagamentoEnum.Cancelado:
                            historico.Descricao = "Conta Cancelada";
                            historico.Status = HistoricoDespesaParcelaEnum.Cancelado;
                            break;
                        default:
                            break;
                    }

                    historicoDespesa.Add(historico);
                }

                historicoDespesa.Add(primeiroItem);

                return historicoDespesa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<DtoGridDespesaResponse> MontarGridPrincipal(List<Despesa> despesasLista)
        {
            try
            {
                DtoGridDespesaResponse dtoGridDespesa = new DtoGridDespesaResponse();

                dtoGridDespesa.Despesa = new List<DtoGridDespesa>();

                if (despesasLista.Count() > 0)
                {
                    foreach (var item in despesasLista.Where(x => !x.IsDelete))
                    {
                        var despesa = await BuscarPorId(item.Id);

                        DtoGridDespesa despesaGrid = MontarItemGrid(despesa);

                        foreach (var despesaParcela in despesa.DespesaParcela.DistinctBy(x => x.TipoPagamento))
                        {
                            if (!string.IsNullOrEmpty(despesaGrid.FormasPagamentos))
                            {
                                despesaGrid.FormasPagamentos = despesaGrid.FormasPagamentos + ", " + EnumerationExtension.Description(despesaParcela.TipoPagamento);
                            }
                            else
                            {
                                despesaGrid.FormasPagamentos = EnumerationExtension.Description(despesaParcela.TipoPagamento);
                            }
                        }

                        dtoGridDespesa.Despesa.Add(despesaGrid);
                    }

                    dtoGridDespesa.ValorTotalDespesa = dtoGridDespesa.Despesa.Sum(x => x.ValorParcela);
                }

                return dtoGridDespesa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DtoGridDespesa MontarItemGrid(DtoDespesa despesa)
        {
            try
            {
                DtoGridDespesa despesaGrid = new DtoGridDespesa
                {
                    IdDespesa = despesa.Id,
                    Unidade = despesa.Unidade.Nome,
                    Fornecedor = despesa.Fornecedor != null ? despesa.Fornecedor.NomeFantasia : "",
                    Categoria = despesa.Categoria.Descricao,
                    NomeDespesa = despesa.NomeDespesa,
                    ValorParcela = despesa.DespesaParcela.FirstOrDefault().ValorParcela,
                    //Vencimento = despesa.DespesaParcela.Where(x => x.StatusPagamento != StatusPagamentoEnum.Pago).FirstOrDefault().DataVencimento,
                };

                switch (despesa.TipoParcela)
                {
                    case TipoParcelaEnum.Unica:

                        var validarStatus = despesa.DespesaParcela.FirstOrDefault();

                        switch (validarStatus.StatusPagamento)
                        {
                            case StatusPagamentoEnum.AReceber:
                                despesaGrid.StatusDespesa = Dto.ContasAPagarVO.StatusDespesaEnum.EmAberto;
                                break;
                            case StatusPagamentoEnum.Pago:
                                despesaGrid.StatusDespesa = Dto.ContasAPagarVO.StatusDespesaEnum.Liquidado;
                                break;
                            case StatusPagamentoEnum.Cancelado:
                                despesaGrid.StatusDespesa = Dto.ContasAPagarVO.StatusDespesaEnum.Cancelado;
                                break;
                            default:
                                break;
                        }

                        despesaGrid.Vencimento = despesa.DespesaParcela.FirstOrDefault().DataVencimento;
                        despesaGrid.NumeroParcelas = despesa.DespesaParcela.Any() ? $"{despesa.DespesaParcela.Where(x => x.StatusPagamento == StatusPagamentoEnum.Pago).Count()}/{despesa.DespesaParcela.Count()}" : "1/1";
                        break;
                    case TipoParcelaEnum.Parcelada:
                        var validarVencimentoDespesa = despesa.DespesaParcela.Where(x => x.StatusPagamento != StatusPagamentoEnum.Pago).FirstOrDefault();
                        despesaGrid.Vencimento = validarVencimentoDespesa != null ? validarVencimentoDespesa.DataVencimento : despesa.DespesaParcela.Last().DataVencimento;
                        despesaGrid.NumeroParcelas = despesa.DespesaParcela.Any() ? $"{despesa.DespesaParcela.Where(x => x.StatusPagamento == StatusPagamentoEnum.Pago).Count()}/{despesa.DespesaParcela.Count()}" : "1/1";
                        despesaGrid.Quitado = despesa.DespesaParcela.Where(x => x.StatusPagamento == StatusPagamentoEnum.AReceber).Count() > 0 ? false : true;

                        var quantidadesCanceladas = despesa.DespesaParcela.Where(x => x.StatusPagamento == StatusPagamentoEnum.Cancelado).Count();

                        if (quantidadesCanceladas == despesa.DespesaParcela.Count())
                        {
                            despesaGrid.StatusDespesa = StatusDespesaEnum.Cancelado;
                        }
                        else
                        {
                            despesaGrid.StatusDespesa = despesa.DespesaParcela.Where(x => x.StatusPagamento == StatusPagamentoEnum.AReceber).Count() > 0 ? StatusDespesaEnum.EmAberto : StatusDespesaEnum.Liquidado;
                        }
                        break;
                    case TipoParcelaEnum.DespesaRecorrente:
                        despesaGrid.Vencimento = despesa.DespesaParcela.Where(x => x.StatusPagamento != StatusPagamentoEnum.Pago).FirstOrDefault().DataVencimento;
                        despesaGrid.StatusDespesa = StatusDespesaEnum.DespesaRecorrente;
                        despesaGrid.NumeroParcelas = "Recorrente";
                        despesaGrid.Quitado = false;
                        break;
                    default:
                        break;
                }

                return despesaGrid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
