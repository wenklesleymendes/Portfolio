using EscolaPro.Core.Model.PainelMatricula;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.MatriculaAlunos
{
    public interface ICertificadoProvaRepository : IDomainRepository<CertificadoProva>
    {
        Task<CertificadoProva> BuscarSolicitacaoAtual(int matriculaId);
        Task<IEnumerable<CertificadoProva>> BuscarPorMatriculaId(int matriculaId);
    }
}
