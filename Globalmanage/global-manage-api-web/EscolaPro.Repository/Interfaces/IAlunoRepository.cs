using EscolaPro.Core.Model;
using EscolaPro.Core.Model.CadastroAluno;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IAlunoRepository : IDomainRepository<Aluno>
    {
        Task<Aluno> BuscarPorId(int idAluno);
        Task<List<Aluno>> FiltrarAluno(FiltrarAluno filtrarAluno);
        Task<byte[]> UploadFoto(byte[] file, int alunoId, string extensao);
        Task<Aluno> SelecionarFoto(int alunoId);
        Task<Aluno> BuscarPorCPF(string cpf);
        Aluno BuscarPorMatriculaId(int matriculaId);
        Task<List<Aluno>> BuscarPorEmail(string[] emails);
    }
}
