using Acesso.Dominio.Pessoas.Tipo;
using System.Collections.Generic;

namespace Acesso.Interfaces.Repositorios.API
{
    public class RepositorioProfessorAPI : IRepositorioProfessor
    {
        public Professor ConsulteProfessor(int idProfessor)
        {
            return APIHelper.Instancia.Get<Professor>("Professor", $"ConsulteProfessor?idProfessor={idProfessor}");
        }

        public IEnumerable<Professor> ConsulteTodosProfessorAtivos()
        {
            return APIHelper.Instancia.Get<IEnumerable<Professor>>("Professor", "ConsulteTodosProfessorAtivos");
        }

        public bool ProfessorEstaAtivo(int idProfessor)
        {
            return APIHelper.Instancia.Get<bool>("Professor", $"ProfessorEstaAtivo?idProfessor={idProfessor}");
        }
    }
}
