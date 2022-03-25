using EscolaPro.API.Dto;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.Provas;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.MatriculaAlunos
{
    public class ProvaAlunoRepository : DomainRepository<ProvaAluno>, IProvaAlunoRepository
    {
        public ProvaAlunoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<ProvaAluno> BuscarPorId(int provaAlunoId)
        {
            IQueryable<ProvaAluno> query = await Task.FromResult(GenerateQuery((x => x.Id == provaAlunoId), null).Include(x=> x.ProvaMateriaAluno));

            return query.FirstOrDefault();
        }

        public async Task<IEnumerable<ProvaAluno>> BuscarPorMatriculaId(int matriculaId)
        {
            IQueryable<ProvaAluno> query = await Task.FromResult(GenerateQuery((x => x.MatriculaAlunoId == matriculaId), null));

            return query.ToList();
        }

        public IEnumerable<ProvaAluno> BuscarProvasRealizadas(int matriculaId)
        {
            return dbContext.Set<ProvaAluno>()
                .Include(x => x.ColegioAutorizado)
                .Include(x => x.UnidadeTransporteProva)
                .ThenInclude(x=> x.UnidadeParticipanteProva)
                .Where(x => x.MatriculaAlunoId == matriculaId && 
                            x.StatusProva != StatusProvaEnum.InscritoParaProva && 
                            x.StatusProva != StatusProvaEnum.NaoInscrito)
                .AsNoTracking();
        }

        public ProvaAluno BuscarInscritoPorMatricula(int matriculaId)
        {
            return dbContext.Set<ProvaAluno>()
                .Include(x => x.ColegioAutorizado)
                .Where(x => x.MatriculaAlunoId == matriculaId && 
                            (x.StatusProva == StatusProvaEnum.InscritoParaProva || 
                             x.StatusProva == StatusProvaEnum.Aprovado))
                .AsNoTracking()
                .FirstOrDefault();
        }

        public async Task<IEnumerable<ProvaAluno>> BuscarTodas()
        {
            return dbContext.Set<ProvaAluno>().Include(x => x.ColegioAutorizado)
                                              .Include(x => x.UnidadeTransporteProva)
                                              .ThenInclude(y => y.UnidadeParticipanteProva)
                                              .AsNoTracking();
        }

        public List<ProvaAluno> BuscarFiltro(FiltroHistoricoProvas filtro)
        {
            try
            {
                return dbContext.Set<ProvaAluno>().Include(x => x.ColegioAutorizado)
                                                  .Include(x => x.UnidadeTransporteProva)
                                                  .ThenInclude(y => y.UnidadeParticipanteProva)
                                                  .Where(x => filtro.unidadeSelect != null ? filtro.unidadeSelect.Contains(x.UnidadeTransporteProva.UnidadeParticipanteProva.UnidadeId) : x.UnidadeTransporteProva.UnidadeParticipanteProva.UnidadeId != null ||
                                                              filtro.onibus != null ? x.UnidadeTransporteProva.Id == filtro.onibus : x.UnidadeTransporteProva.Id != null ||
                                                              filtro.colegioSelect != null ? x.LocalProva == filtro.colegioSelect : x.LocalProva != null ||
                                                              filtro.tipoProva != null ? x.TipoProva == (TipoProvaEnum)filtro.tipoProva : x.TipoProva != null ||
                                                              filtro.statusProva != null ? x.StatusProva == (StatusProvaEnum)filtro.statusProva : x.StatusProva != null ||
                                                              filtro.dataInicioMatricula != null ? x.DataProva >= filtro.dataInicioMatricula : x.DataProva != null ||
                                                              filtro.dataFimMatricula != null ? x.DataProva <= filtro.dataFimMatricula : x.DataProva != null)
                                                  .AsNoTracking().ToList();

                //if (filtro.unidadeSelect != null &&
                //    filtro.colegioSelect != null &&
                //    filtro.tipoProva != null &&
                //    filtro.dataInicioMatricula != null &&
                //    filtro.dataFimMatricula != null &&
                //    filtro.statusProva != null)
                //{
                //    return dbContext.Set<ProvaAluno>().Include(x => x.ColegioAutorizado)
                //                                      .Include(x => x.UnidadeTransporteProva)
                //                                      .ThenInclude(y => y.UnidadeParticipanteProva)
                //                                      .Where(x => filtro.unidadeSelect.Contains(x.UnidadeTransporteProva.UnidadeParticipanteProva.UnidadeId) &&
                //                                                  x.UnidadeTransporteProva.Id == filtro.onibus &&
                //                                                  x.LocalProva == filtro.colegioSelect &&
                //                                                  x.TipoProva == (TipoProvaEnum)filtro.tipoProva &&
                //                                                  x.StatusProva == (StatusProvaEnum)filtro.statusProva &&
                //                                                  x.DataProva >= filtro.dataInicioMatricula &&
                //                                                  x.DataProva <= filtro.dataFimMatricula)
                //                                      .AsNoTracking();
                //}
                //else
                //{
                //    return dbContext.Set<ProvaAluno>().Include(x => x.ColegioAutorizado)
                //                                      .Include(x => x.UnidadeTransporteProva)
                //                                      .ThenInclude(y => y.UnidadeParticipanteProva)
                //                                      .AsNoTracking();
                //}
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
