using EMCatraca.Core.Dominio;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.WebApi.Fachadas
{
    public class FachadaDeProfessor
    {
        private readonly IRepositorioProfessor _repositorioDeAluno = FabricaDeRepositorios.Instancia.CrieRepositorioProfessor();

        public Professor ConsulteProfessor(int idProfessor)
        {
            return _repositorioDeAluno.ConsulteProfessor(idProfessor);
        }

        public IEnumerable<Professor> ConsulteTodosProfessorAtivos()
        {
            return _repositorioDeAluno.ConsulteTodosProfessorAtivos();
        }

        public bool ProfessorEstaAtivo(int idProfessor)
        {
            return _repositorioDeAluno.ProfessorEstaAtivo(idProfessor);
        }
    }
}