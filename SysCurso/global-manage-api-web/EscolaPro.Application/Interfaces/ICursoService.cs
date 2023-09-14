using EscolaPro.Core.Model;
using EscolaPro.Core.Model.CursoTurma;
using EscolaPro.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface ICursoService
    {
        Task<Curso> Inserir(Curso curso);
        Task<IEnumerable<Materia>> InserirMateria(Materia materia);
        Task<IEnumerable<Curso>> BuscarTodos();
        Task<DtoCurso> BuscarPorId(int idCurso);
        Task<IEnumerable<Curso>> BuscarCursosComMateria();
        Task<bool> AtivarOuDesativar(int idCurso);
        Task<bool> Deletar(int id);
        Task<bool> DeletarMateria(int id);
        Task<IEnumerable<Curso>> BuscarPorUnidade(int unidadeId, int? usuarioLogadoId);
    }
}
