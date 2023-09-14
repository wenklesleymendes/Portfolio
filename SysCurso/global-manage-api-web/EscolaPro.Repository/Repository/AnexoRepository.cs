using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Anexos;
using EscolaPro.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class AnexoRepository : DomainRepository<Anexo>, IAnexoRepository
    {
        public AnexoRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Anexo>> BuscarAnexo(AnexoFiltrar anexoFiltrar)
        {
            IQueryable<Anexo> query = null;

            if (anexoFiltrar.IdUnidade > 0)
            {
                query = await Task.FromResult(GenerateQuery((x => x.UnidadeId == anexoFiltrar.IdUnidade && !x.IsDelete), null));
            }
            else if (anexoFiltrar.IdFuncionario > 0)
            {
                query = await Task.FromResult(GenerateQuery((x => x.FuncionarioId == anexoFiltrar.IdFuncionario && !x.IsDelete), null));

            }
            else if (anexoFiltrar.IdPontoEletronico > 0)
            {
                query = await Task.FromResult(GenerateQuery((x => x.PontoEletronicoId == anexoFiltrar.IdPontoEletronico && !x.IsDelete), null));

            }
            else if (anexoFiltrar.IdFerias > 0)
            {
                query = await Task.FromResult(GenerateQuery((x => x.FeriasFuncionarioId == anexoFiltrar.IdFerias && !x.IsDelete), null));

            }
            else if (anexoFiltrar.MensagemTicketId > 0)
            {
                query = await Task.FromResult(GenerateQuery((x => x.MensagemTicketId == anexoFiltrar.MensagemTicketId && !x.IsDelete), null));

            }
            else if (anexoFiltrar.FornecedorId > 0)
            {
                query = await Task.FromResult(GenerateQuery((x => x.FornecedorId == anexoFiltrar.FornecedorId && !x.IsDelete), null));

            }
            else if (anexoFiltrar.FolhaPagamentoId > 0)
            {
                query = await Task.FromResult(GenerateQuery((x => x.FolhaPagamentoId == anexoFiltrar.FolhaPagamentoId && !x.IsDelete), null));

            }
            else if (anexoFiltrar.DespesaId > 0)
            {
                query = await Task.FromResult(GenerateQuery((x => x.DespesaId == anexoFiltrar.DespesaId && !x.IsDelete), null));

            }
            else if (anexoFiltrar.DestinatarioTicketId > 0)
            {
                query = await Task.FromResult(GenerateQuery((x => x.DestinatarioTicketId == anexoFiltrar.DestinatarioTicketId && !x.IsDelete), null));

            }
            else if (anexoFiltrar.PerguntaId > 0)
            {
                query = await Task.FromResult(GenerateQuery((x => x.PerguntaId == anexoFiltrar.PerguntaId && !x.IsDelete), null));

            }
            else if (anexoFiltrar.RespostaId > 0)
            {
                query = await Task.FromResult(GenerateQuery((x => x.RespostaId == anexoFiltrar.RespostaId && !x.IsDelete), null));

            }
            else if (anexoFiltrar.MatriculaAlunoId > 0)
            {
                query = await Task.FromResult(GenerateQuery((x => x.MatriculaAlunoId == anexoFiltrar.MatriculaAlunoId && !x.IsDelete), null));

            }
            else if (anexoFiltrar.SolicitacaoAlunoId > 0)
            {
                query = await Task.FromResult(GenerateQuery((x => x.SolicitacaoAlunoId == anexoFiltrar.SolicitacaoAlunoId && !x.IsDelete), null));

            }

            return query.Select(x => new Anexo
            {
                Id = x.Id,
                ArquivoString = x.ArquivoString,
                UnidadeId = x.UnidadeId,
                DeletedAt = x.DeletedAt,
                DataAnexo = x.DataAnexo,
                Extensao = x.Extensao,
                Descricao = x.Descricao,
                TipoAnexo = x.TipoAnexo,
                IsDelete = x.IsDelete,
                FeriasFuncionarioId = x.FeriasFuncionarioId,
                FornecedorId = x.FornecedorId,
                PontoEletronicoId = x.PontoEletronicoId,
                AlunoId = x.AlunoId,
                FuncionarioId = x.FuncionarioId,
                MensagemTicketId = x.MensagemTicketId,
                DespesaId = x.DespesaId,
                FolhaPagamentoId = x.FolhaPagamentoId,
                DestinatarioTicketId = x.DestinatarioTicketId,
                PerguntaId = x.PerguntaId,
                RespostaId = x.RespostaId,
                MatriculaAlunoId = x.MatriculaAlunoId,
                IsActive = x.IsActive,
                Mensagem = x.Mensagem,
                IsRecusado = x.IsRecusado,
                SolicitacaoAlunoId = x.SolicitacaoAlunoId,
                TipoRecusa = x.TipoRecusa
            }).Where(x => x.TipoAnexo != TipoAnexoEnum.DeclaracaoPendenciaDocumental && x.TipoAnexo != TipoAnexoEnum.ContratoProcuracaoEja).AsNoTracking().ToList();
        }

        public async Task<Anexo> BuscarPorId(int anexoId)
        {
            try
            {
                IQueryable<Anexo> query = await Task.FromResult(GenerateQuery((x => x.Id == anexoId && !x.IsDelete), null).AsNoTracking());

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Anexo>> BuscarPorIdDespesa(int despesaId, bool documento)
        {
            IQueryable<Anexo> query = null;

            if (documento)
            {
                query = await Task.FromResult(GenerateQuery((x => x.DespesaId == despesaId && !x.IsDelete), null));

                return query.Select(x => new Anexo
                {
                    Id = x.Id,
                    ArquivoString = x.ArquivoString,
                    UnidadeId = x.UnidadeId,
                    DeletedAt = x.DeletedAt,
                    DataAnexo = x.DataAnexo,
                    Extensao = x.Extensao,
                    Descricao = x.Descricao,
                    TipoAnexo = x.TipoAnexo,
                    IsDelete = x.IsDelete,
                    FeriasFuncionarioId = x.FeriasFuncionarioId,
                    FornecedorId = x.FornecedorId,
                    PontoEletronicoId = x.PontoEletronicoId,
                    AlunoId = x.AlunoId,
                    FuncionarioId = x.FuncionarioId,
                    MensagemTicketId = x.MensagemTicketId,
                    DespesaId = x.DespesaId,
                    FolhaPagamentoId = x.FolhaPagamentoId,
                    PerguntaId = x.PerguntaId,
                    RespostaId = x.RespostaId,
                    IsActive = x.IsActive,
                    Mensagem = x.Mensagem,
                    IsRecusado = x.IsRecusado,
                    TipoRecusa = x.TipoRecusa
                }).Where(x =>
                          x.TipoAnexo == TipoAnexoEnum.Outros ||
                          x.TipoAnexo == TipoAnexoEnum.NotaFiscal ||
                          x.TipoAnexo == TipoAnexoEnum.BoletoBancario ||
                          x.TipoAnexo == TipoAnexoEnum.GuiaImposto ||
                          x.TipoAnexo == TipoAnexoEnum.Pedido ||
                          x.TipoAnexo == TipoAnexoEnum.Orcamento).AsNoTracking().ToList();
            }
            else
            {
                query = await Task.FromResult(GenerateQuery((x => x.DespesaId == despesaId && !x.IsDelete), null));

                return query.Select(x => new Anexo
                {
                    Id = x.Id,
                    ArquivoString = x.ArquivoString,
                    UnidadeId = x.UnidadeId,
                    DeletedAt = x.DeletedAt,
                    DataAnexo = x.DataAnexo,
                    Extensao = x.Extensao,
                    Descricao = x.Descricao,
                    TipoAnexo = x.TipoAnexo,
                    IsDelete = x.IsDelete,
                    FeriasFuncionarioId = x.FeriasFuncionarioId,
                    FornecedorId = x.FornecedorId,
                    PontoEletronicoId = x.PontoEletronicoId,
                    AlunoId = x.AlunoId,
                    FuncionarioId = x.FuncionarioId,
                    MensagemTicketId = x.MensagemTicketId,
                    DespesaId = x.DespesaId,
                    FolhaPagamentoId = x.FolhaPagamentoId,
                    PerguntaId = x.PerguntaId,
                    RespostaId = x.RespostaId,
                    IsActive = x.IsActive,
                    Mensagem = x.Mensagem,
                    IsRecusado = x.IsRecusado,
                    TipoRecusa = x.TipoRecusa
                }).Where(x => x.TipoAnexo == TipoAnexoEnum.ReciboPagamento || 
                              x.TipoAnexo == TipoAnexoEnum.ComprovanteTransacaoBancaria ||
                              x.TipoAnexo == TipoAnexoEnum.ComprovanteRetornoItau ||
                              x.TipoAnexo == TipoAnexoEnum.Outros).AsNoTracking().ToList();
            }

        }

        public async Task<bool> DeleterDocumento(int? matriculaAlunoId, TipoAnexoEnum tipoAnexo)
        {
            try
            {
                IQueryable<Anexo> query = await Task.FromResult(GenerateQuery((x => x.MatriculaAlunoId == matriculaAlunoId.Value && x.TipoAnexo == tipoAnexo), null).AsNoTracking());

                return await RemoveRangeAsync(query.ToList()) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Anexo> DownloadArquivo(int idAnexo)
        {
            IQueryable<Anexo> query = await Task.FromResult(GenerateQuery((x => x.Id == idAnexo && !x.IsDelete), null).AsNoTracking());

            return query.FirstOrDefault();
        }

        public async Task<Anexo> DownloadComprovanteBancario(int idFolhaPagamento)
        {
            var query = await Task.FromResult(GenerateQuery((x => x.FolhaPagamentoId == idFolhaPagamento && x.TipoAnexo == TipoAnexoEnum.ComprovanteTransacaoBancaria && !x.IsDelete), null));

            return query.Select(x => new Anexo
            {
                Id = x.Id,
                ArquivoString = x.ArquivoString,
                UnidadeId = x.UnidadeId,
                DeletedAt = x.DeletedAt,
                DataAnexo = x.DataAnexo,
                Extensao = x.Extensao,
                Descricao = x.Descricao,
                TipoAnexo = x.TipoAnexo,
                IsDelete = x.IsDelete,
                FeriasFuncionarioId = x.FeriasFuncionarioId,
                FornecedorId = x.FornecedorId,
                PontoEletronicoId = x.PontoEletronicoId,
                AlunoId = x.AlunoId,
                FuncionarioId = x.FuncionarioId,
                MensagemTicketId = x.MensagemTicketId,
                DespesaId = x.DespesaId,
                FolhaPagamentoId = x.FolhaPagamentoId,
                PerguntaId = x.PerguntaId,
                RespostaId = x.RespostaId,
                IsActive = x.IsActive,
                Mensagem = x.Mensagem,
                IsRecusado = x.IsRecusado,
                SolicitacaoAlunoId = x.SolicitacaoAlunoId,
                TipoRecusa = x.TipoRecusa
            }).AsNoTracking().FirstOrDefault();
        }

        public async Task<Anexo> DownloadDocumentoPorTipoEnum(int matriculaId, TipoAnexoEnum tipoAnexoEnum)
        {
            try
            {
                IQueryable<Anexo> query = await Task.FromResult(GenerateQuery((x => x.MatriculaAlunoId == matriculaId && x.TipoAnexo == tipoAnexoEnum), null));

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Anexo> DownloadPorTipoAnexo(int matriculaId, bool isComprovante = false)
        {
            if (isComprovante)
            {
                IQueryable<Anexo> query = await Task.FromResult(GenerateQuery((x => x.MatriculaAlunoId == matriculaId && 
                                                                               !x.IsDelete && 
                                                                               x.TipoAnexo == TipoAnexoEnum.ComprovanteBolsaConvenio), null));

                return query.FirstOrDefault();
            }
            else
            {
                IQueryable<Anexo> query = await Task.FromResult(GenerateQuery((x => x.MatriculaAlunoId == matriculaId && !x.IsDelete), null));

                return query.FirstOrDefault(); 
            }
        }

        public async Task<int> ExisteAnexo(int solicitacaoAlunoId)
        {
            try
            {
                IQueryable<Anexo> query = await Task.FromResult(GenerateQuery((x => x.SolicitacaoAlunoId == solicitacaoAlunoId && !x.IsDelete), null).AsNoTracking());

                return query.Count() > 0 ? query.FirstOrDefault().Id : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExisteContrato(int matriculaId)
        {
            return dbContext.Set<Anexo>()
                .AsNoTracking()
                .Any(x => !x.IsDelete &&
                    x.MatriculaAlunoId == matriculaId &&
                    x.TipoAnexo == TipoAnexoEnum.ContratoProcuracaoEja);
        }
    }
}
