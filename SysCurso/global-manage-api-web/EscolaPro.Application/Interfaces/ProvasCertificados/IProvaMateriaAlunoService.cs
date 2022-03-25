using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.ProvasCertificados
{
    public interface IProvaMateriaAlunoService
    {
        Task<DtoProvaMateriaAluno> Inserir(DtoProvaMateriaAluno dtoProvaMateriaAluno);
        Task<int> Inserir(IEnumerable<DtoProvaMateriaAluno> dtoProvaMateriaAlunos);
        Task<IEnumerable<DtoProvaMateriaAluno>> BuscarPorProvaId(int ExcluirProvaMateria);
        Task<int> ExcluirProvaMateria(int provaAlunoId);
    }
}
