using EscolaPro.Core.Model;
using EscolaPro.Service.Dto.AlunosVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IAlunoService
    {
        Task<DtoAluno> Inserir(DtoAluno model);
        Task<IEnumerable<DtoAluno>> BuscarTodos();
        Task<DtoAluno> BuscarPorId(int idAluno);
        Task<bool> Excluir(int idAluno);
        Task<List<DtoAluno>> FiltrarAluno(DtoFiltrarAluno filtrarAluno);
        Task<DtoAlunoFoto> UploadFoto(byte[] file, int alunoId, string extensao);
        Task<DtoAlunoFoto> SelecionarFoto(int alunoId);
        Task<bool> ExcluirFoto(int alunoId);
        Task<DtoAluno> BuscarPorCPF(string cpf);
    }
}
