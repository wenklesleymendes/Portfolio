using EscolaPro.Service.Dto.CampanhaVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface ICampanhaService
    {
        Task<DtoCampanha> Inserir(DtoCampanha dtoCampanha);
        Task<bool> Deletar(int idCampanha);
        Task<DtoCampanha> BuscarPorId(int idCampanha);
        Task<IEnumerable<DtoCampanha>> BuscarTodos();
        public Task<DtoCampanha> AtivarOuDesativar(int idCampanha);
        Task<IEnumerable<DtoCampanha>> BuscarCampanhaVigente(int unidadeId, int cursoId, int tipoPagamento);
    }
}
