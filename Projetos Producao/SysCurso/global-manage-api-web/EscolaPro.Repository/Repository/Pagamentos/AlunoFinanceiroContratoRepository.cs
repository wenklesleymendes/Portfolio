using EscolaPro.Core.Helpers;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.Solicitacoes;
using EscolaPro.Repository.Interfaces.Pagamentos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.Pagamentos
{
    public class AlunoFinanceiroContratoRepository : DomainRepository<Pagamento>, IAlunoFinanceiroContratoRepository
    {
        public AlunoFinanceiroContratoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task AtualizarPagamento(List<Pagamento> pagamentosIds, PlanoPagamento planoPagamento = null, SolicitacaoEfetuar solicitacaoEfetuar = null, bool contrato = false)
        {
            try
            {
                List<Pagamento> pagamentos = new List<Pagamento>();

                if (!contrato)
                {
                    foreach (var item in pagamentosIds)
                    {

                        IQueryable<Pagamento> query = await Task.FromResult(GenerateQuery((x => x.Id == item.Id), null).AsNoTracking());

                        //var pagamento = await GetByIdAsync(item.Id);
                        var pagamento = query.FirstOrDefault();

                        decimal valorPago = 0;

                        if (pagamento.DataVencimento.HasValue && DateTime.Now.Date <= pagamento.DataVencimento.Value.Date)
                        {
                            valorPago = pagamento.Valor - ((pagamento.Valor * pagamento.Desconto.Value) / 100);
                        }
                        else
                        {
                            valorPago = pagamento.Valor;
                        }

                        DadosCartao dadosCartao = new DadosCartao
                        {
                            Id = 0,
                            AcquirersEnum = AcquirersEnum.TEF,
                            //ValorParcela = credito ? valorTotal / quantidadeParcelas : valorTotal,
                            QuantidadeParcela = solicitacaoEfetuar.QuantidadeParcela,
                            AnoValidade = "",
                            MesValidade = "",
                            NomePessoa = "",
                            NumeroCartao = solicitacaoEfetuar.NumeroCartao,
                            TipoPagamento = solicitacaoEfetuar.Credito ? Core.Model.Enums.TipoPagamentoEnum.CartaoCredito : Core.Model.Enums.TipoPagamentoEnum.CartaoDebito,
                            ValorTotal = solicitacaoEfetuar.ValorTotal,
                            TID = "",
                            CodigoAutorizacao = solicitacaoEfetuar.NumeroControle,
                            BandeiraCartao = CoreHelpers.BandeiraCartao(solicitacaoEfetuar.NumeroCartao)
                        };

                        if (solicitacaoEfetuar.Credito)
                        {
                            dadosCartao.ValorParcela = solicitacaoEfetuar.ValorTotal / solicitacaoEfetuar.QuantidadeParcela;
                        }
                        else
                        {
                            dadosCartao.ValorParcela = solicitacaoEfetuar.ValorTotal;
                        }

                        var dadosCartaoRetorno = await InserirDetalheCartao(dadosCartao);

                        pagamento.DataPagamento = DateTime.Now;
                        pagamento.TipoSituacao = TipoSituacaoEnum.Pago;
                        pagamento.TipoPagamento = solicitacaoEfetuar.Credito ? Core.Model.Enums.TipoPagamentoEnum.CartaoCredito : Core.Model.Enums.TipoPagamentoEnum.CartaoDebito;
                        pagamento.DadosCartaoId = dadosCartaoRetorno.Id;
                        pagamento.TipoSituacao = TipoSituacaoEnum.Pago;
                        pagamento.DataPagamento = DateTime.Now;
                        pagamento.ValorPago = valorPago; //valorTotal;
                        pagamento.DadosCartaoId = dadosCartaoRetorno.Id;
                        pagamento.ComprovanteCartao = solicitacaoEfetuar.ComprovanteCartao;

                        //AtualizarSolicitação

                        if (pagamento.SolicitacaoAlunoId.HasValue)
                        {
                            var solicitacaoAluno = dbContext.Set<SolicitacaoAluno>().Where(x => x.Id == pagamento.SolicitacaoAlunoId).FirstOrDefault();

                            if(solicitacaoAluno != null)
                            {
                                solicitacaoAluno.StatusPagamento = Core.Model.MetasComissoes.StatusPagamentoEnum.Pago;

                                dbContext.Entry(solicitacaoAluno).State = EntityState.Modified;
                                await dbContext.SaveChangesAsync();
                            }
                        }

                        await UpdateAsync(pagamento);
                    }
                }
                else
                {
                    Pagamento pagamento = new Pagamento();

                    pagamento.Descricao = pagamentosIds.FirstOrDefault().Descricao;
                    pagamento.DataPagamento = DateTime.Now;
                    pagamento.DataEmissao = DateTime.Now;
                    pagamento.TipoSituacao = TipoSituacaoEnum.Pago;
                    pagamento.TipoPagamento = solicitacaoEfetuar.Credito ? Core.Model.Enums.TipoPagamentoEnum.CartaoCredito : Core.Model.Enums.TipoPagamentoEnum.CartaoDebito;
                    pagamento.TipoSituacao = TipoSituacaoEnum.Pago;
                    pagamento.DataPagamento = DateTime.Now;
                    pagamento.MatriculaId = pagamentosIds.FirstOrDefault().MatriculaId;
                    pagamento.NossoNumero = "TEF";
                    pagamento.ComprovanteCartao = solicitacaoEfetuar.ComprovanteCartao;
                    pagamento.Valor = solicitacaoEfetuar.ValorTotal; //pagamentosIds.FirstOrDefault().ValorPago;

                    DadosCartao dadosCartao = new DadosCartao
                    {
                        Id = 0,
                        AcquirersEnum = AcquirersEnum.TEF,
                        //ValorParcela = pagamento.TipoPagamento != Core.Model.Enums.TipoPagamentoEnum.CartaoDebito ? valorTotal / planoPagamento.QuantidadeParcela : valorTotal,
                        QuantidadeParcela = solicitacaoEfetuar.QuantidadeParcela,
                        AnoValidade = "",
                        MesValidade = "",
                        NomePessoa = "",
                        NumeroCartao = solicitacaoEfetuar.NumeroCartao,
                        TipoPagamento = solicitacaoEfetuar.Credito ? Core.Model.Enums.TipoPagamentoEnum.CartaoCredito : Core.Model.Enums.TipoPagamentoEnum.CartaoDebito,
                        ValorTotal = solicitacaoEfetuar.ValorTotal,
                        TID = "",
                        CodigoAutorizacao = solicitacaoEfetuar.NumeroControle,
                        BandeiraCartao = CoreHelpers.BandeiraCartao(solicitacaoEfetuar.NumeroCartao)
                    };

                    if (solicitacaoEfetuar.Credito)
                    {
                        pagamento.ValorPago = solicitacaoEfetuar.ValorTotal;
                        dadosCartao.ValorParcela = solicitacaoEfetuar.ValorTotal / solicitacaoEfetuar.QuantidadeParcela;
                    }
                    else
                    {
                        pagamento.ValorPago = solicitacaoEfetuar.ValorTotal;
                        dadosCartao.ValorParcela = solicitacaoEfetuar.ValorTotal;
                    }

                    pagamento.DadosCartao = dadosCartao;

                    await AddAsync(pagamento);
                    //var dadosCartaoRetorno = await InserirDetalheCartao(dadosCartao);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Pagamento> BuscarPorId(int pagamentoId)
        {
            try
            {
                IQueryable<Pagamento> query = await Task.FromResult(GenerateQuery(x => x.Id == pagamentoId)
                    .Include(x => x.DadosCartao)
                    .AsNoTracking());

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Pagamento>> ParcelasAVencer(int quantidoDias, int reguaContatoRegrasId)
        {
            try
            {
                IQueryable<Pagamento> query = await Task.FromResult(GenerateQuery(x =>
                    (x.ReguaContatoFila == null ||
                     !x.ReguaContatoFila.Any(x=> x.ReguaContatoRegrasId == reguaContatoRegrasId)) 
                    && x.DataVencimento != null 
                    && x.DataVencimento.Value.Date == DateTime.Now.AddDays(quantidoDias).Date
                    && x.TipoSituacao != TipoSituacaoEnum.Pago
                    && x.TipoSituacao != TipoSituacaoEnum.Isento
                    && x.TipoPagamento == TipoPagamentoEnum.BoletoBancario)
                    //.Include(x=> x.ReguaContatoFila)
                    //.ThenInclude(x=> x.ReguaContatoRegras)
                    .AsNoTracking());

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Pagamento>> BuscarPorId(List<int> pagamentoIds)
        {
            try
            {
                IQueryable<Pagamento> query = await Task.FromResult(GenerateQuery(x => pagamentoIds.Contains(x.Id))
                    .Include(x => x.DadosCartao)
                    .AsNoTracking());

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Pagamento> BuscarPorNossoNumero(string nossoNumero)
        {
            try
            {
                IQueryable<Pagamento> query = await Task.FromResult(GenerateQuery(x => x.NossoNumero == nossoNumero).AsNoTracking());

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Pagamento>> ConsultarPainelFinanceiro(int matriculaId)
        {
            IQueryable<Pagamento> query = await Task.FromResult(GenerateQuery((x => x.MatriculaId == matriculaId), null)
                .Include(x => x.EmailEnviado).AsNoTracking());

            return query.Select(x => new Pagamento
            {
                Acrescimo = x.Acrescimo,
                CodigoBarras = x.CodigoBarras,
                DataEmissao = x.DataEmissao,
                DataPagamento = x.DataPagamento,
                MatriculaId = x.MatriculaId,
                DataVencimento = x.DataVencimento,
                Desconto = x.Desconto,
                Descricao = x.Descricao,
                NossoNumero = x.NossoNumero,
                NumeroLinhaDigitavel = x.NumeroLinhaDigitavel,
                NumeroRegistro = x.NumeroRegistro,
                PagamentoIdOld = x.PagamentoIdOld,
                PromocaoBolsaConvenio = x.PromocaoBolsaConvenio,
                TipoPagamento = x.TipoPagamento,
                TipoSituacao = x.TipoSituacao,
                Valor = x.Valor,
                ValorPago = x.ValorPago,
                Id = x.Id,
                DeletedAt = x.DeletedAt,
                IsActive = x.IsActive,
                IsDelete = x.IsDelete,
                UpdatedAt = x.UpdatedAt,
                EmailEnviado = x.EmailEnviado,
                TarifaBanco = x.TarifaBanco,
                DadosCartao = x.DadosCartao,
                DadosCartaoId = x.DadosCartaoId,
                ComprovanteCartao = x.ComprovanteCartao
            }).ToList();
        }

        public async Task<DadosCartao> InserirDetalheCartao(DadosCartao dadosCartao)
        {
            try
            {

                dbContext.Entry<DadosCartao>(dadosCartao).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                await dbContext.SaveChangesAsync();

                return dadosCartao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UltimoNossoNumeroGerado()
        {
            try
            {
                IQueryable<Pagamento> query = await Task.FromResult(GenerateQuery());

                if (query.Count() > 0)
                {
                    return query.FirstOrDefault().NossoNumero;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InserirPagamentoSolicitacao(SolicitacaoAluno solicitacao, SolicitacaoEfetuar solicitacaoEfetuar)
        {
            try
            {
                Pagamento pagamento = new Pagamento();

                pagamento.Descricao = solicitacao.Solicitacao.Descricao;
                pagamento.DataPagamento = DateTime.Now;
                pagamento.DataEmissao = DateTime.Now;
                pagamento.TipoSituacao = TipoSituacaoEnum.Pago;
                pagamento.TipoPagamento = solicitacaoEfetuar.Credito ? Core.Model.Enums.TipoPagamentoEnum.CartaoCredito : Core.Model.Enums.TipoPagamentoEnum.CartaoDebito;
                pagamento.TipoSituacao = TipoSituacaoEnum.Pago;
                pagamento.DataPagamento = DateTime.Now;
                pagamento.MatriculaId = solicitacaoEfetuar.MatriculaId > 0 ? solicitacaoEfetuar.MatriculaId : solicitacao.MatriculaId;
                pagamento.NossoNumero = "TEF";
                pagamento.ComprovanteCartao = solicitacaoEfetuar.ComprovanteCartao;
                pagamento.Valor = solicitacaoEfetuar.ValorTotal;

                DadosCartao dadosCartao = new DadosCartao
                {
                    Id = 0,
                    AcquirersEnum = AcquirersEnum.TEF,
                    QuantidadeParcela = solicitacaoEfetuar.QuantidadeParcela,
                    AnoValidade = "",
                    MesValidade = "",
                    NomePessoa = "",
                    NumeroCartao = solicitacaoEfetuar.NumeroCartao,
                    TipoPagamento = solicitacaoEfetuar.Credito ? Core.Model.Enums.TipoPagamentoEnum.CartaoCredito : Core.Model.Enums.TipoPagamentoEnum.CartaoDebito,
                    ValorTotal = solicitacaoEfetuar.ValorTotal,
                    TID = "",
                    CodigoAutorizacao = solicitacaoEfetuar.NumeroControle,
                    BandeiraCartao = CoreHelpers.BandeiraCartao(solicitacaoEfetuar.NumeroCartao)
                };

                if (solicitacaoEfetuar.Credito)
                {
                    pagamento.ValorPago = solicitacaoEfetuar.ValorTotal;
                    dadosCartao.ValorParcela = solicitacaoEfetuar.ValorTotal / solicitacaoEfetuar.QuantidadeParcela;
                }
                else
                {
                    pagamento.ValorPago = solicitacaoEfetuar.ValorTotal;
                    dadosCartao.ValorParcela = solicitacaoEfetuar.ValorTotal;
                }

                pagamento.DadosCartao = dadosCartao;

                await AddAsync(pagamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Pagamento> Inserir(Pagamento pagamento)
        {
            try
            {
                dbContext.Entry<Pagamento>(pagamento).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                await dbContext.SaveChangesAsync();
                return pagamento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoverPorMatricula(int id)
        {
            try
            {
                //var pagamentos = dbContext.Set<Pagamento>().Where(x => x.MatriculaId == id).AsNoTracking().ToList();

                IQueryable<Pagamento> query = await Task.FromResult(GenerateQuery(x => x.MatriculaId == id).AsNoTracking());

                foreach (var item in query.ToList())
                {
                    dbContext.Entry<Pagamento>(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

                }

                await dbContext.SaveChangesAsync();
                //await RemoveRangeAsync(query.ToList());
            }
            catch (Exception ex)
            { 
                throw;
            }
        }
    }
}
