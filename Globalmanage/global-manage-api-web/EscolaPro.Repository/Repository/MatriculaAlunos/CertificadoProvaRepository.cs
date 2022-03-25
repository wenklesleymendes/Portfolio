using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.PainelMatricula.Enums;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.MatriculaAlunos
{
    public class CertificadoProvaRepository : DomainRepository<CertificadoProva>, ICertificadoProvaRepository
    {
        public CertificadoProvaRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<CertificadoProva>> BuscarPorMatriculaId(int matriculaId)
        {
            IQueryable<CertificadoProva> query = await Task.FromResult(GenerateQuery((x => x.MatriculaAlunoId == matriculaId), null));

            return query.ToList();
        }

        public async Task<CertificadoProva> BuscarSolicitacaoAtual(int matriculaId)
        {
            IQueryable<CertificadoProva> query = await Task.FromResult(GenerateQuery((x => 
                                                                   x.MatriculaAlunoId == matriculaId) //&& 
                                                                   //x.StatusCertificado != StatusCertificadoEnum.EntregueAluno)
                                                               , null));

            return query.FirstOrDefault();
        }
    }
}
