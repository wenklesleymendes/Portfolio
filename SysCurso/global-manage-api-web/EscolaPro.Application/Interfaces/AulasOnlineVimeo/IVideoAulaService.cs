using EscolaPro.Service.Dto.AulaOnlineVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IVideoAulaService
    {
        Task<DtoVideoAula> Inserir(DtoVideoAula dtoVideoAula);
        Task<DtoGridGeneric<DtoVideoAula>> BuscarPorMateria(int materiaId);
        Task<DtoVideoAula> BuscarPorId(int videoAulaId);
        Task<bool> Excluir(int videoAulaId);
        Task<DtoVideoPausado> SalvarUltimaSessao(DtoVideoPausado videoPausado);
        Task<DtoVideoPausado> BuscarUltimaSessao(int matriculaId);
    }
}
