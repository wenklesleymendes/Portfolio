using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class UnidadeRepository : DomainRepository<Unidade>, IUnidadeRepository
    {
        public UnidadeRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<Unidade> BuscarPorId(int idUnidade)
        {
            try
            {
                IQueryable<Unidade> query = await Task.FromResult(GenerateQuery((x => x.Id == idUnidade), null)
                    .Include(x => x.DadosBancario)
                    .Include(x => x.ContratoLocacao)
                    .Include(x => x.Contato)
                    .Include(x => x.Endereco)
                    .Include(x => x.UnidadeDespesas)
                    .Include(x => x.HistoricoOcorrencias)
                    .Include(x => x.HorarioFuncionamento)
                    .Include(x => x.CentroCusto).AsNoTracking());

                return query.SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Unidade>> BuscarTodos()
        {
            try
            {
                IQueryable<Unidade> query = await Task.FromResult(GenerateQuery((x => !x.IsDelete), null)
                                                    .Include(x => x.DadosBancario)
                                                    .Include(x => x.ContratoLocacao)
                                                    .Include(x => x.Contato)
                                                    .Include(x => x.Endereco)
                                                    .Include(x => x.UnidadeDespesas)
                                                    .Include(x => x.HistoricoOcorrencias)
                                                    .Include(x => x.HorarioFuncionamento)
                                                    .Include(x => x.CentroCusto));

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Unidade>> BuscarPorTipo(TipoUnidade tipoUnidade)
        {
            try
            {
                IQueryable<Unidade> query = await Task.FromResult(GenerateQuery((x => !x.IsDelete && x.TipoUnidade == tipoUnidade), null)
                                                    .Include(x => x.DadosBancario)
                                                    .Include(x => x.ContratoLocacao)
                                                    .Include(x => x.Contato)
                                                    .Include(x => x.Endereco)
                                                    .Include(x => x.UnidadeDespesas)
                                                    .Include(x => x.HistoricoOcorrencias)
                                                    .Include(x => x.HorarioFuncionamento)
                                                    .Include(x => x.CentroCusto));

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Unidade> SelecionarFoto(int unidadeId)
        {
            try
            {
                IQueryable<Unidade> query = await Task.FromResult(GenerateQuery((x => x.Id == unidadeId), null));

                return query.AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> UploadFoto(byte[] file, int unidadeId, string extensao)
        {
            try
            {
                var unidade = await BuscarPorId(unidadeId);
                unidade.Foto = file;
                unidade.Extensao = extensao;

                await UpdateAsync(unidade);
                return unidade.Foto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
