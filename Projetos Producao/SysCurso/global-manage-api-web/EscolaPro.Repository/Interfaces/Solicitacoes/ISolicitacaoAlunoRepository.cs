using EscolaPro.Core.Model.PainelMatricula;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.Solicitacoes
{
    public interface ISolicitacaoAlunoRepository : IDomainRepository<SolicitacaoAluno>
    {
        Task<IEnumerable<SolicitacaoAluno>> BuscarHistorico(int matriculaId);
        Task<SolicitacaoAluno> BuscarPorId(int solicitacaoId);
    }
}
