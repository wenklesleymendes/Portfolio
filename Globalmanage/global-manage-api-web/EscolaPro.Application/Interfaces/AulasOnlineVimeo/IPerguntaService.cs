using EscolaPro.Service.Dto.AulaOnlineVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IPerguntaService
    {
        Task<DtoPergunta> Inserir(DtoPergunta dtoPergunta);
        Task<DtoGridGeneric<DtoPergunta>> BuscarPorVideoAula(int videoAulaId);
        Task<DtoPergunta> BuscarPorId(int perguntaId);
        Task<bool> Excluir(int perguntaId);
    }
}
