using EscolaPro.Service.Dto.AtendimentoVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.Atendimentos
{
    public interface ILeadsService
    {
        Task<DtoLeads> Inserir(string dtoLeads);
        Task<IEnumerable<DtoLeads>> BuscarTodos();
    }
}
