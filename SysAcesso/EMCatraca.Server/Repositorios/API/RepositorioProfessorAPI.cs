using EMCatraca.Core.Dominio;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.Server.Repositorios.API
{
    public class RepositorioProfessorAPI : IRepositorioProfessor
    {
        private readonly IAPIConexao _apiConexao;

        public RepositorioProfessorAPI(IAPIConexao apiConexao)
        {
            _apiConexao = apiConexao;
        }

        public Professor ConsulteProfessor(int idProfessor)
        {
            return _apiConexao.Get<Professor>("Professor", $"ConsulteProfessor?idProfessor={idProfessor}");
        }

        public IEnumerable<Professor> ConsulteTodosProfessorAtivos()
        {
            return _apiConexao.Get<IEnumerable<Professor>>("Professor", "ConsulteTodosProfessorAtivos");
        }

        public bool ProfessorEstaAtivo(int idProfessor)
        {
            return _apiConexao.Get<bool>("Professor", $"ProfessorEstaAtivo?idProfessor={idProfessor}");
        }
    }
}
