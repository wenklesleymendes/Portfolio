using EscolaPro.Core.Model.BolsaConvenio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface ICampanhaRepository : IDomainRepository<Campanha>
    {
        Task<Campanha> Inserir(Campanha campanha, List<CampanhaUnidade> campanhaUnidades);
        Task<Campanha> BuscarPorId(int idCampanha);
        Task<IEnumerable<Campanha>> BuscarCampanhaVigente(int unidadeId, int cursoId, int tipoPagamento);
    }
}
