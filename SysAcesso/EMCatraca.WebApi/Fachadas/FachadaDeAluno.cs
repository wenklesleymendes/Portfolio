using EMCatraca.Core.Dominio;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.WebApi.Fachadas
{
    public class FachadaDeAluno
    {
        private readonly IRepositorioAluno _repositorioAluno = FabricaDeRepositorios.Instancia.CrieRepositorioAluno();

        public Aluno ConsulteAluno(int idAluno)
        {
            return _repositorioAluno.ConsulteAluno(idAluno);
        }

        public string ConsulteMatriculaAlunoPorRFID(string codigo)
        {
            return _repositorioAluno.ObtenhaMatriculaPorRFID(codigo);
        }

        public IEnumerable<Aluno> ConsulteAlunosAtivos()
        {
            return _repositorioAluno.ConsulteAlunosAtivos();
        }

        public bool AlunoEstaAtivo(int idAluno)
        {
            return _repositorioAluno.AlunoEstaAtivo(idAluno);
        }

        public bool AlunoEstaBloqueado(int idAluno, int idAtributo)
        {
            return _repositorioAluno.AlunoEstaBloqueado(idAluno, idAtributo);
        }

        public bool AlunoEstaInadimplenteDeCheques(int idAluno)
        {
            return _repositorioAluno.AlunoEstaInadimplenteDeCheques(idAluno);
        }

        public bool AlunoEstaInadimplenteDuplicata(int idAluno)
        {
            return _repositorioAluno.AlunoEstaInadimplenteDuplicata(idAluno);
        }

        public bool AlunoEstaInadimplenteDuplicata(int idAluno, int diasDeAtraso)
        {
            return _repositorioAluno.AlunoEstaInadimplenteDuplicata(idAluno, diasDeAtraso);
        }

        public bool AlunoEstaPendenteDeDocumentos(int idAluno)
        {
            return _repositorioAluno.AlunoEstaPendenteDeDocumentos(idAluno);
        }

        public bool AlunoEstaPendenteDeMateriais(int idAluno)
        {
            return _repositorioAluno.AlunoEstaPendenteDeMateriais(idAluno);
        }

        public bool AlunoPodeSairSozinho(int idAluno, int idAtributo)
        {
            return _repositorioAluno.AlunoPodeSairSozinho(idAluno, idAtributo);
        }

        public bool AlunoPodeSerLiberadoPeloAutorizado(int idAluno, int codigoAutorizacao)
        {
            return _repositorioAluno.AlunoPodeSerLiberadoPeloAutorizado(idAluno, codigoAutorizacao);
        }

        public bool AlunoPodeSerLiberadoPeloResponsavel(int idAluno, int codigoResponsavel)
        {
            return _repositorioAluno.AlunoPodeSerLiberadoPeloResponsavel(idAluno, codigoResponsavel);
        }

        public IEnumerable<Aluno> ConsulteAlunosPorTurmaMomtada(int codigoDaSerie, int codigoDaTurma)
        {
            return _repositorioAluno.ConsulteAlunosPorTurmaMontada(codigoDaSerie, codigoDaTurma);
        }

        public SerieTurma ConsulteTurmaMontadaPorAluno(int idAluno)
        {
            return _repositorioAluno.ConsulteSerieTurmaPorAluno(idAluno);
        }
    }
}