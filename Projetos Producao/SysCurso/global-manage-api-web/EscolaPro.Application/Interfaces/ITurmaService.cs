using EscolaPro.Core.Model;
using EscolaPro.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface ITurmaService
    {
        Task<DtoTurma> Inserir(DtoTurma model);
        Task<List<DtoTurma>> BuscarTodos();
        Task<DtoTurma> BuscarPorId(int id);
        Task<bool> Deletar(int id);
        Task<IEnumerable<DtoTurma>> BuscarPorCursoId(int cursoId, int unidadeId, int? usuarioLogadoId);
        Task<DtoTurma> TransferirDeTurma(DtoTransferirTurma turma);
        Task<IEnumerable<DtoTurma>> Filtrar(DtoTurmaFiltrar turma);
    }
}
