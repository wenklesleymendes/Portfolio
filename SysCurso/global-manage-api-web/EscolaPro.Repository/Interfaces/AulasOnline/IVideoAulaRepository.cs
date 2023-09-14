using EscolaPro.Core.Model.AulasOnline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.AulasOnline
{
    public interface IVideoAulaRepository : IDomainRepository<VideoAula>
    {
        Task<IEnumerable<VideoAula>> BuscarPorMateria(int materiaOnlineId);
        Task<VideoPausado> SalvarUltimaSessao(VideoPausado videoPausado);
        Task<VideoPausado> BuscarUltimaSessao(int matriculaId);
        Task<VideoAula> BuscarPorId(int videoAulaId);
    }
}
