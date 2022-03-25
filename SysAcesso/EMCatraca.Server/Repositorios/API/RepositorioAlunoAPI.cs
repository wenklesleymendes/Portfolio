using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.Server.Repositorios.API
{
    public class RepositorioAlunoAPI : IRepositorioAluno
    {
        private readonly IAPIConexao _apiConexao;

        public RepositorioAlunoAPI(IAPIConexao apiConexao)
        {
            _apiConexao = apiConexao;
        }

        public Aluno ConsulteAluno(int idAluno)
        {
            return _apiConexao.Get<Aluno>("Aluno", $"ConsulteAluno?idAluno={idAluno}");
        }

        public string ObtenhaMatriculaPorRFID(string codigo)
        {
            LogAuditoria.Escreva($"{nameof(ObtenhaMatriculaPorRFID)}: " +
                   $"Aluno com Codigo RFID={codigo}",
                   $"{nameof(RepositorioAlunoAPI)}");

            return _apiConexao.Get<string>("Aluno", $"{nameof(ObtenhaMatriculaPorRFID)}?codigo={codigo}");
        }

        public IEnumerable<Aluno> ConsulteAlunosAtivos()
        {
            return _apiConexao.Get<List<Aluno>>("Aluno", "ConsulteAlunosAtivos");
        }

        public bool AlunoEstaAtivo(int idAluno)
        {
            return _apiConexao.Get<bool>("Aluno", $"AlunoEstaAtivo?idAluno={idAluno}");
        }

        public bool AlunoEstaBloqueado(int idAluno, int idAtributo)
        {
            return _apiConexao.Get<bool>("Aluno", $"AlunoEstaBloqueado?idAluno={idAluno}&idAtributo={idAtributo}");
        }

        public bool AlunoEstaInadimplenteDeCheques(int idAluno)
        {
            return _apiConexao.Get<bool>("Aluno", $"AlunoEstaInadimplenteDeCheques?idAluno={idAluno}");
        }

        public bool AlunoEstaInadimplenteDuplicata(int idAluno)
        {
            return _apiConexao.Get<bool>("Aluno", $"AlunoEstaInadimplenteDuplicata?idAluno={idAluno}");
        }

        public bool AlunoEstaInadimplenteDuplicata(int idAluno, int diasDeAtraso)
        {
            return _apiConexao.Get<bool>("Aluno", $"AlunoEstaInadimplenteDuplicata?idAluno={idAluno}&diasDeAtraso={diasDeAtraso}");
        }

        public bool AlunoEstaPendenteDeDocumentos(int idAluno)
        {
            return _apiConexao.Get<bool>("Aluno", $"AlunoEstaPendenteDeDocumentos?idAluno={idAluno}");
        }

        public bool AlunoEstaPendenteDeMateriais(int idAluno)
        {
            return _apiConexao.Get<bool>("Aluno", $"AlunoEstaPendenteDeMateriais?idAluno={idAluno}");
        }

        public bool AlunoPodeSairSozinho(int idAluno, int idAtributo)
        {
            return _apiConexao.Get<bool>("Aluno", $"AlunoPodeSairSozinho?idAluno={idAluno}&idAtributo={idAtributo}");
        }

        public IEnumerable<Aluno> ConsulteAlunosPorTurmaMontada(int codigoDaSerie, int codigoDaTurma)
        {
            return _apiConexao.Get<IEnumerable<Aluno>>("Aluno", $"ConsulteAlunoPorSerie?codigoDaSerie={codigoDaSerie}&codigoDaTurma={codigoDaTurma}");
        }

        public SerieTurma ConsulteSerieTurmaPorAluno(int idAluno)
        {
            return _apiConexao.Get<SerieTurma>("Aluno", $"ConsulteAlunoPorSerieEhTurma?idAluno={idAluno}");
        }

        public TurmaMontada ConsulteTurmaMontadaPorAluno(int idAluno)
        {
            return _apiConexao.Get<TurmaMontada>("Aluno", $"ConsulteTurmaMontadaPorAluno?idAluno={idAluno}");
        }

        public bool AlunoPodeSerLiberadoPeloAutorizado(int idAluno, int idAtributo)
        {
            return _apiConexao.Get<bool>("Aluno", $"AlunoPodeSerLiberadoPorAutorizado?idAluno={idAluno}&idAtributo={idAtributo}");
        }

        public bool AlunoPodeSerLiberadoPeloResponsavel(int idAluno, int idAtributo)
        {
            return _apiConexao.Get<bool>("Aluno", $"AlunoLiberadoPeloResponsavel?idAluno={idAluno}&idAtributo={idAtributo}");
        }
    }
}
