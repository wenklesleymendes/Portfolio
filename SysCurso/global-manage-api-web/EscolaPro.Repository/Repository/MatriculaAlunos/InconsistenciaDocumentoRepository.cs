using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.MatriculaAlunos
{
    public class InconsistenciaDocumentoRepository : DomainRepository<InconsistenciaDocumento>, IInconsistenciaDocumentoRepository
    {
        public InconsistenciaDocumentoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<InconsistenciaDocumento>> BuscarPorMatricula(int matriculaId)
        {
            try
            {
                IQueryable<InconsistenciaDocumento> query = await Task.FromResult(GenerateQuery((x => !x.IsDelete), null));

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
