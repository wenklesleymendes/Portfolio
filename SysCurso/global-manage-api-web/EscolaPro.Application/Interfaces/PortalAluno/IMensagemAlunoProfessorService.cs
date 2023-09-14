using EscolaPro.Service.Dto.PortalAlunoProfessorVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.PortalAluno
{
    public interface IMensagemAlunoProfessorService
    {
        Task<DtoMensagemAlunoProfessor> Inserir(DtoMensagemAlunoProfessor dtoMensagemAluno);
        Task<DtoMensagemAlunoProfessor> BuscarPorId(int mensagemId);
        Task<IEnumerable<DtoMensagemAlunoProfessor>> BuscarPorProfessor(int professorId);
        Task<IEnumerable<DtoMensagemAlunoProfessor>> BuscarPorMatricula(int alunoId);
        Task<bool> Excluir(int mensagemId);
    }
}
