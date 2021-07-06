using System.Collections.Generic;
using Acesso.Dominio.Pessoas.Tipo;
using Acesso.Dominio;

namespace Acesso.Interfaces
{
    public interface IRepositorioAluno
    {
        Aluno ConsulteAluno(int codigo);

        TurmaMontada ConsulteTurmaMontadaPorAluno(int codigo);

        IEnumerable<Aluno> ConsulteAlunosAtivos();

        bool AlunoEstaAtivo(int idAluno);

        bool AlunoEstaBloqueado(int idAluno, int idAtributo);

        bool AlunoEstaInadimplenteDuplicata(int idAluno);

        bool AlunoEstaInadimplenteDuplicata(int idAluno, int diasDeAtraso);

        bool AlunoEstaInadimplenteDeCheques(int idAluno);

        bool AlunoEstaPendenteDeDocumentos(int idAluno);

        bool AlunoEstaPendenteDeMateriais(int idAluno);

        IEnumerable<Aluno> ConsulteAlunosPorTurmaMontada(int codigoDaSerie, int codigoDaTurma);

        bool AlunoPodeSairSozinho(int idAluno, int idAtributo);

        bool AlunoPodeSerLiberadoPeloResponsavel(int idAluno, int codigoResponsavel);

        bool AlunoPodeSerLiberadoPeloAutorizado(int idAluno, int codigoAutorizacao);

        string ObtenhaMatriculaPeloRfid(string codigo);

        SerieTurma ConsulteSerieTurmaPorAluno(int idAluno);
    }
}
