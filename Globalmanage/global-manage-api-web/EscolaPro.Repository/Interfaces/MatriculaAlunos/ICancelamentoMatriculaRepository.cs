using EscolaPro.Core.Model.PainelMatricula;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.MatriculaAlunos
{
    public interface ICancelamentoMatriculaRepository : IDomainRepository<CancelamentoMatricula>
    {
        Task<CancelamentoMatricula> BuscarPorMatricula(int matriculaId);
    }
}
