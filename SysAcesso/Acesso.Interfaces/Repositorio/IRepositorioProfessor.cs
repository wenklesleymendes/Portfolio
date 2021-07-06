using  Acesso.Dominio.Pessoas.Tipo;
using System.Collections.Generic;

namespace Acesso.Interfaces
{
    public interface IRepositorioProfessor
    {
        Professor ConsulteProfessor(int codigo);

        IEnumerable<Professor> ConsulteTodosProfessorAtivos();

        bool ProfessorEstaAtivo(int idProfessor);
    }
}
