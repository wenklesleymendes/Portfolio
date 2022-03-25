using EscolaPro.Core.Model;
using EscolaPro.Core.Model.AlunoQuestionarioProva;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.AulasOnline
{
    public interface IAlunoQuestionarioRepository : IDomainRepository<AlunoQuestionario>
    {
        //Task<AlunoQuestionario> ResponderQuestionario(AlunoQuestionario alunoQuestionario);
        Task Excluir(int alunoQuestionarioId);
        Task<AlunoQuestionario> BuscarPorId(int alunoQuestionarioId);
        Task<AlunoQuestionario> BuscarPorPerguntaId(int perguntaId);
    }
}
