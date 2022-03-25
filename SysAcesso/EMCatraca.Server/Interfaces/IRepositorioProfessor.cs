using EMCatraca.Core.Dominio;
using System.Collections.Generic;

namespace EMCatraca.Server.Interfaces
{
    public interface IRepositorioProfessor
    {
        Professor ConsulteProfessor(int codigo);

        IEnumerable<Professor> ConsulteTodosProfessorAtivos();

        bool ProfessorEstaAtivo(int idProfessor);
    }
}
