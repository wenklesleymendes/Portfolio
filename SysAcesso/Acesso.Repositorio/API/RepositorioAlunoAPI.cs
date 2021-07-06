using System.Collections.Generic;
using Acesso.Dominio;
using Acesso.Dominio.Pessoas.Tipo;
using Acesso.Interfaces;

namespace Acesso.Interfaces.Repositorios.API
{
    public class RepositorioAlunoAPI : IRepositorioAluno
    {
        public Aluno ConsulteAluno(int idAluno)
        {
            return APIHelper.Instancia.Get<Aluno>("Aluno", $"ConsulteAluno?idAluno={idAluno}");
        }

        public IEnumerable<Aluno> ConsulteAlunosAtivos()
        {
            return APIHelper.Instancia.Get<List<Aluno>>("Aluno", "ConsulteAlunosAtivos");
        }

        public bool AlunoEstaAtivo(int idAluno)
        {
            return APIHelper.Instancia.Get<bool>("Aluno", $"AlunoEstaAtivo?idAluno={idAluno}");
        }

        public bool AlunoEstaBloqueado(int idAluno, int idAtributo)
        {
            return APIHelper.Instancia.Get<bool>("Aluno", $"AlunoEstaBloqueado?idAluno={idAluno}&idAtributo={idAtributo}");
        }

        public bool AlunoEstaInadimplenteDeCheques(int idAluno)
        {
            return APIHelper.Instancia.Get<bool>("Aluno", $"AlunoEstaInadimplenteDeCheques?idAluno={idAluno}");
        }

        public bool AlunoEstaInadimplenteDuplicata(int idAluno)
        {
            return APIHelper.Instancia.Get<bool>("Aluno", $"AlunoEstaInadimplenteDuplicata?idAluno={idAluno}");
        }

        public bool AlunoEstaInadimplenteDuplicata(int idAluno, int diasDeAtraso)
        {
            return APIHelper.Instancia.Get<bool>("Aluno", $"AlunoEstaInadimplenteDuplicata?idAluno={idAluno}&diasDeAtraso={diasDeAtraso}");
        }

        public bool AlunoEstaPendenteDeDocumentos(int idAluno)
        {
            return APIHelper.Instancia.Get<bool>("Aluno", $"AlunoEstaPendenteDeDocumentos?idAluno={idAluno}");
        }

        public bool AlunoEstaPendenteDeMateriais(int idAluno)
        {
            return APIHelper.Instancia.Get<bool>("Aluno", $"AlunoEstaPendenteDeMateriais?idAluno={idAluno}");
        }

        public bool AlunoPodeSairSozinho(int idAluno, int idAtributo)
        {
            return APIHelper.Instancia.Get<bool>("Aluno", $"AlunoPodeSairSozinho?idAluno={idAluno}&idAtributo={idAtributo}");
        }

        public IEnumerable<Aluno> ConsulteAlunosPorTurmaMontada(int codigoDaSerie, int codigoDaTurma)
        {
            return APIHelper.Instancia.Get<IEnumerable<Aluno>>("Aluno", $"ConsulteAlunoPorSerie?codigoDaSerie={codigoDaSerie}&codigoDaTurma={codigoDaTurma}");
        }

        public SerieTurma ConsulteSerieTurmaPorAluno(int idAluno)
        {
            return APIHelper.Instancia.Get<SerieTurma>("Aluno", $"ConsulteAlunoPorSerieEhTurma?idAluno={idAluno}");
        }

        public TurmaMontada ConsulteTurmaMontadaPorAluno(int idAluno)
        {
            return APIHelper.Instancia.Get<TurmaMontada>("Aluno", $"ConsulteTurmaMontadaPorAluno?idAluno={idAluno}");
        }

        public bool AlunoPodeSerLiberadoPeloAutorizado(int idAluno, int idAtributo)
        {
            return APIHelper.Instancia.Get<bool>("Aluno", $"AlunoPodeSerLiberadoPorAutorizado?idAluno={idAluno}&idAtributo={idAtributo}");
        }

        public bool AlunoPodeSerLiberadoPeloResponsavel(int idAluno, int idAtributo)
        {
            return APIHelper.Instancia.Get<bool>("Aluno", $"AlunoLiberadoPeloResponsavel?idAluno={idAluno}&idAtributo={idAtributo}");
        }

        public string ObtenhaMatriculaPeloRfid(string codigo)
        {
            return APIHelper.Instancia.Get<string>("Aluno", $"ObtenhaMatriculaPeloRfid?codigo={codigo}");
        }

        Aluno IRepositorioAluno.ConsulteAluno(int codigo)
        {
            throw new System.NotImplementedException();
        }

        TurmaMontada IRepositorioAluno.ConsulteTurmaMontadaPorAluno(int codigo)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<Aluno> IRepositorioAluno.ConsulteAlunosAtivos()
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<Aluno> IRepositorioAluno.ConsulteAlunosPorTurmaMontada(int codigoDaSerie, int codigoDaTurma)
        {
            throw new System.NotImplementedException();
        }

        SerieTurma IRepositorioAluno.ConsulteSerieTurmaPorAluno(int idAluno)
        {
            throw new System.NotImplementedException();
        }
    }
}
